using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph.Assets;
using SharpGLBase.Scene;
using SharpGLBase;
using SharpGLTest.Shapes;
using SharpGLBase.Primitives;
using System.Drawing;
using SharpGLBase.Events;
using SharpGLBase.Extensions;

namespace SharpGLTest
{
    public enum PointedMovedAction { RotateAroundCenter, MoveRelatively, RotateAroundEye, ChangePerspectiveDepth }
    public enum PointedClickedAction { SelectModel }
    public enum MouseScrollAction { Zoom, ChangePerspectiveDepth }
    public class MainWindowViewModel
    {
        #region fields
        OpenGL _gl = null; // Permits using gl for methods that aren't called by the Draw-event
        MyScene _scene = new MyScene();
        Point? _lastPointerLocation;
        Model3DBase _selectedModel = null;
        float _rotationSensitivity = 0.01f / 200000f;
        float _moveSensitivity = 0.01f;
        float _zoomSensitivity = 1.1f;
        float _changePerspectiveDepthSensitivity = 0.2f;
        long? _timeLastPointerLocation;
        #endregion fields

        #region properties
        public Point? LastPointerLocation
        {
            get { return _lastPointerLocation; }
            set { _lastPointerLocation = value; }
        }
        public long? TimeLastPointerLocation
        {
            get { return _timeLastPointerLocation; }
            set { _timeLastPointerLocation = value; }
        }

        public float MoveSensitivity
        {
            get { return _moveSensitivity; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Sensitivity has to be more than 0.");
                _moveSensitivity = value;
            }
        }
        public float RotationSensitivity
        {
            get { return _rotationSensitivity; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Sensitivity has to be more than 0.");
                _rotationSensitivity = value;
            }
        }

        public float ZoomSensitivity
        {
            get { return _zoomSensitivity; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Sensitivity has to be more than 0.");
                _zoomSensitivity = value;
            }
        }

        public float ChangePerspectiveDepthSensitivity
        {
            get { return _changePerspectiveDepthSensitivity; }
            set { _changePerspectiveDepthSensitivity = value; }
        }

        public MyScene Scene
        {
            get { return _scene; }
            set { _scene = value; }
        }
        #endregion properties

        #region user input
        /// <summary>
        /// If LastPointerLocation == null then LastPointerLocation = pointerLocation
        /// Else act on movement between the 2 points.
        /// </summary>
        /// <param name="pointerLocation">The new point to where the pointer moved</param>
        public void PointerMoved(Point pointerLocation, PointedMovedAction action)
        {
            if (_lastPointerLocation != null)
            {
                var diffX = _lastPointerLocation.Value.X - pointerLocation.X;
                var diffY = _lastPointerLocation.Value.Y - pointerLocation.Y;
                var values = new Point(diffX, diffY);

                if (action == PointedMovedAction.RotateAroundCenter)
                {
                    Rotate(values);
                }
                else if (action == PointedMovedAction.MoveRelatively)
                {
                    MoveRelatively(values);
                }
            }
            _lastPointerLocation = pointerLocation;
            _timeLastPointerLocation = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Clears LastPointerLocation.
        /// </summary>
        public void PointerMoveEnd()
        {
            _lastPointerLocation = null;
            _timeLastPointerLocation = null;
        }

        public void PointerClicked(PointedClickedAction action, Point point)
        {
            if (action == PointedClickedAction.SelectModel)
            {
                Scene.GetModelAtPoint(point, Scene.MaterialForObjects.ToValueList());
            }
        }
        public void MouseScroll(MouseScrollAction action, bool forward)
        {
            if (action == MouseScrollAction.Zoom)
            {
                //_zoomSteps += forward ? -1 : 1;

                //float zoomFactor = (float)Math.Pow(ZoomSensitivity, _zoomSteps);
                float zoomDistance = forward ? ZoomSensitivity : -ZoomSensitivity;
                Zoom(zoomDistance);
            }
            else if (action == MouseScrollAction.ChangePerspectiveDepth)
            {
                ChangePerspectiveDepth(forward ? ChangePerspectiveDepthSensitivity : -ChangePerspectiveDepthSensitivity);
            }
        }
        #endregion user input

        #region camera control.
        /// <summary>
        /// Rotates CameraLookAt around its Center.
        /// </summary>
        /// <param name="moveValues">Y => rotate in Y direction (Azimuth). X => rotate sideways (Zenith)</param>
        public void Rotate(Point moveValues)
        {
            // Convert 2D pointer movement to 3D rotation

            var size = _scene.ViewPortSize;
            var constForXY = _rotationSensitivity * (DateTime.Now.Ticks - _timeLastPointerLocation.Value);
            var vertRotLimiter = size.Width > size.Height ? size.Width / (double)size.Height : size.Height / (double)size.Width;


            float rotationRadiansX = (float)(moveValues.Y * constForXY / vertRotLimiter);// / 1000.0f + 1);
            float rotationRadiansY = (float)(moveValues.X * constForXY);// / 1000.0f + 1);
            Scene.ModelView.RotateAbsoluteX(rotationRadiansX);
            Scene.ModelView.RotateAbsoluteY(rotationRadiansY);
            //Scene.ModelView.RotateAbsoluteZ(rotationRadiansX);
        }
        /// <summary>
        /// Moves camera relatively
        /// </summary>
        /// <param name="moveValues">X => move horizontally. Y => move vertically.</param>
        public void MoveRelatively(Point moveValues)
        {
            // Adapt the moving speed to the zoom amount.
            //float moveSensitivity = (float)Math.Pow(ZoomSensitivity, _zoomSteps) * MoveSensitivity;
            float moveSensitivity = MoveSensitivity;

            // Convert 2D pointer movement to 3D rotation
            float moveX = (float)moveValues.X * moveSensitivity;
            float moveY = (float)moveValues.Y * moveSensitivity;
            Scene.ModelView.TranslateAbsoluteX(-moveX);
            Scene.ModelView.TranslateAbsoluteY(moveY);
            //Scene.ModelView.TranslateOnRotationMatrix(new vec3(-moveX, moveY, 0));
        }


        public void Zoom(float distance, OpenGL gl = null)
        {
            //Scene.Projection.Zoom(zoomFactor, gl == null ? _gl : gl);
            Scene.Projection.Zoom(distance);
        }

        public void ChangePerspectiveDepth(float distance)
        {

            //TODO
        }
        #endregion camera control.

        private Model3DBase _shape;
        public void AddElements(OpenGL gl)
        {
            var redMat = new Material();
            var blueMat = new Material();
            var redObjects = new List<Model3DBase>();
            var blueObjects = new List<Model3DBase>();


            // Set red material
            redMat.Ambient = System.Drawing.Color.FromArgb(255, 50, 0, 50);
            redMat.Diffuse = System.Drawing.Color.FromArgb(255, 100, 0, 100);
            redMat.Specular = System.Drawing.Color.FromArgb(255, 225, 225, 225);
            redMat.Shininess = 100f;
            // Set blue material
            //blueMat.Ambient = System.Drawing.Color.FromArgb(255, 0, 0, 100);
            blueMat.Diffuse = System.Drawing.Color.FromArgb(255, 0, 100, 255);
            //blueMat.Specular = System.Drawing.Color.FromArgb(255, 100, 0, 255);
            //blueMat.Shininess = 0.0001f;

            _shape = new MyTrefoilKnot(gl);
            _shape.TranslateAbsoluteZ(3);
            // Add some test models to the viewport
            var cube = new FlatShadedCube(gl);
            //cube.Scale(new vec3(0.5f, -0.5f, -0.5f));
            cube.TranslateAbsoluteX(3);
            cube.RotateAbsoluteY(2);
            cube.CalculateNormals();

            var trefoilKnot = new MyTrefoilKnot(gl);
            trefoilKnot.RotateAbsoluteZ(1f);
            //trefoilKnot.RotateAbsoluteY(4.5f);
            //trefoilKnot.ScaleY(1.3f);
            trefoilKnot.TranslateAbsoluteY(2);

            blueObjects.Add(_shape);
            redObjects.Add(cube);
            redObjects.Add(trefoilKnot);
            blueObjects.Add(new FlatShadedCube(gl));

            Scene.MaterialForObjects.Add(redMat, redObjects);
            Scene.MaterialForObjects.Add(blueMat, blueObjects);
        }

        public void OpenGLInitialized(OpenGL gl)
        {
            _gl = gl;
            Scene.ModelSelected += Scene_ModelSelected;

            //Scene.ModelView.CameraHandling = CameraRotationHandling.TSR;
            //Scene.ModelView.RotationMethod = RotationMethod.TurnTableYZ;
            Scene.ModelView.CameraHandling = CameraRotationHandling.TRS;
            Scene.ModelView.RotationMethod = RotationMethod.TurnTableXZ;

            // Scale down the opengl viewport size in order to increase performance.
            //Scene.PerformanceScaleValue = 0.5f;

            //Scene.DrawEdgesOnly = true;
            Scene.OpenGLInitialized(gl);
            AddElements(gl);
            var lp = new vec3(5, 50, 15);
            Scene.LightPosition = lp;

            // Move view to best position
            Scene.Projection.Zoom(-10);
            //Scene.ModelView.TranslateAbsoluteZ(-5);  
            //Scene.ModelView.RotateAbsoluteY((float)Math.PI);
        }

        void Scene_ModelSelected(object sender, ModelSelectedEventArgs e)
        {
            if (_selectedModel != null)
                _selectedModel.Material = null;

            if (e.SelectedModel == null)
                return;

            var mat = new Material();
            mat.Diffuse = Color.Silver;
            mat.Ambient = Color.Gray;


            e.SelectedModel.Material = mat;


            _selectedModel = e.SelectedModel;
        }

        public void OpenGLDraw(OpenGL gl)
        {
            float rotPerTime = (float)(DateTime.Now.Millisecond / 500.0 * Math.PI % (2 * Math.PI));

            //Scene.ModelView.RotateAbsoluteY(0.1f);

            _shape.RotationMatrix = mat4.identity();
            _shape.RotateAbsoluteY(rotPerTime);
            Scene.Draw(gl);
        }

        public void ViewResized(OpenGL gl, double width, double height)
        {
            Scene.ViewResized(gl, width, height);
        }

    }
}
