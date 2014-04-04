using GlmNet;
using SharpGLBase.Events;
using SharpGLBase.Primitives;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SharpGLBase.Shaders;
using System.Threading.Tasks;
using System.Threading;
using SharpGL.Version;
using SharpGLBase.Shaders.Parameters;

namespace SharpGLBase.Scene
{
    /// <summary>
    /// A Scene object is the central place where everything -that influences the visual result- comes together.
    /// </summary>
    public abstract class OpenGLScene
    {
        #region fields
        ModelView _modelView = new ModelView();
        Normal _normal = new Normal();
        Projection _projection = new Projection();
        ExtShaderProgram _currentShader;
        float _performanceScaleValue = 1f;
        Size _sceneSize;
        Size _viewPortSize;
        Point? _modelSelectionPoint = null;
        #endregion fields

        #region properties
        /// <summary>
        /// The projection matrix is contained in here.
        /// </summary>
        public Projection Projection
        {
            get { return _projection; }
            set { _projection = value; }
        }
        /// <summary>
        /// The modelview matrix is contained in here.
        /// </summary>
        public ModelView ModelView
        {
            get { return _modelView; }
            set { _modelView = value; }
        }
        /// <summary>
        /// The normal matrix is contained in here.
        /// </summary>
        public Normal Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }
        /// <summary>
        /// The shader that's currently being used for rendering the models.
        /// </summary>
        public ExtShaderProgram CurrentShader
        {
            get { return _currentShader; }
            set { _currentShader = value; }
        }
        /// <summary>
        /// A value that will be multiplied to the passed view width and -height during resizing.
        /// </summary>
        public float PerformanceScaleValue
        {
            get { return _performanceScaleValue; }
            set { _performanceScaleValue = value; }
        }
        /// <summary>
        /// The size of the scene. ( = ViewPortSize * PerformanceScaleValue)
        /// </summary>
        public Size SceneSize
        {
            get { return _sceneSize; }
            private set { _sceneSize = value; }
        }
        /// <summary>
        /// The size of the viewport.
        /// </summary>
        public Size ViewPortSize
        {
            get { return _viewPortSize; }
            private set { _viewPortSize = value; }
        }

        public OpenGL GL { get; private set; }
        #endregion properties

        #region events
        public event ModelSelectedEvent ModelSelected;
        public void OnModelSelected(Point p, Model3DBase m)
        {
            if (ModelSelected != null)
                ModelSelected(this, new ModelSelectedEventArgs(p, m));
        }
        #endregion events

        /// <summary>
        /// Should be called before the first Draw-call is being made. Sets the GL.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void OpenGLInitialized(OpenGL gl)
        {
            GL = gl;
        }
        /// <summary>
        /// Should be called before the first Draw-call is being made. Sets the GL and shader.
        /// </summary>
        /// <param name="gl"></param>
        public virtual void OpenGLInitialized(OpenGL gl, ExtShaderProgram shader)
        {
            OpenGLInitialized(gl);
            CurrentShader = shader;
        }

        /// <summary>
        /// Redraw the scene.
        /// </summary>
        /// <param name="gl">OpenGL viewport</param>
        public abstract void Draw(OpenGL gl);

        public virtual void ViewResized(OpenGL gl, double actualWidth, double actualHeight)
        {
            var viewPortWidth = (float)actualWidth * _performanceScaleValue;
            var viewPortHeight = (float)actualHeight * _performanceScaleValue;

            ViewPortSize = new Size((int)actualWidth, (int)actualHeight);
            SceneSize = new Size((int)viewPortWidth, (int)viewPortHeight);

            // Create a projection matrix for the scene with the screen size.
            Projection.SetFrustum((float)viewPortWidth, (float)viewPortHeight);

            gl.SetDimensions((int)++viewPortWidth, (int)++viewPortHeight);
            gl.Viewport(0, 0, (int)++viewPortWidth, (int)++viewPortHeight);
        }


        /// <summary>
        /// This method let's the Draw(...) know to retrieve the object at the requested location. When an object is found, the ModelSelectedEvent will be triggered.
        /// </summary>
        /// <param name="p">The 2D point relative to the OpenGL viewport.</param>
        public void GetModelAtPoint(Point p, IEnumerable<Model3DBase> models, HitTestMethod method = HitTestMethod.OpenGLHack)
        {
            if (p != null && models != null && models.Count() > 0)
            {
                Point correctedPoint = new Point((int)(p.X * _performanceScaleValue), (int)(p.Y * _performanceScaleValue));


                // Create an array that will be the viewport.
                int[] viewport = new int[4];
                // Get the viewport, then convert the mouse point to an opengl point.
                GL.GetInteger(OpenGL.GL_VIEWPORT, viewport);


                // Take deep copy of everything we need.
                var mvCopy = new ModelView()
                {
                    ResultMatrix = ModelView.ModelviewMatrix
                };
                var prCopy = new Projection()
                {
                    ProjectionMatrix = Projection.ProjectionMatrix
                };
                var nrmlCopy = new Normal()
                {
                    NormalMatrix = Normal.NormalMatrix
                };

                Task t = new Task(() =>
                {
                    Model3DBase clickedModel = null;

                    if (method == HitTestMethod.OpenGLHack)
                    {
                        clickedModel = GetModelAtPointHack(correctedPoint, models, viewport, mvCopy, prCopy, nrmlCopy);
                    }

                    OnModelSelected(correctedPoint, clickedModel);
                });

                t.Start();
            }
        }


        #region HitTest

        public enum HitTestMethod { OpenGLHack }//, RayOBB}
        

        /// <summary>
        /// Source: http://www.opengl-tutorial.org/miscellaneous/clicking-on-objects/picking-with-an-opengl-hack/
        /// It's recommended to read the source before using this algorithm.
        /// </summary>
        /// <param name="scene">The OpenGLScene.</param>
        /// <param name="point">The 2D point.</param>
        /// <param name="models">The drawn models.</param>
        /// <param name="performanceScaleValue">A factor that affects performance by scaling the size of the temperory viewport.</param>
        /// <returns>The model on this location or null.</returns>
        public static Model3DBase GetModelAtPointHack(Point point, IEnumerable<Model3DBase> models, 
            int[] viewport, ModelView modelview, Projection projection, Normal normal, float performanceScaleValue = 1)
        {
            int id = -1;

            int width = (int)(viewport[2] * performanceScaleValue);
            int height = (int)(viewport[3] * performanceScaleValue);
            int x = (int)(point.X * performanceScaleValue);
            int y = height - (int)(point.Y * performanceScaleValue);


            #region create a temperory gl to prevent flickering
            OpenGL gl = new OpenGL();

            // Create OpenGL.
            var openGLVersion = OpenGLVersion.OpenGL2_1;
            var renderContextType = RenderContextType.FBO;
            gl.Create(openGLVersion, renderContextType, 1, 1, 32, null);
            // Set the dimensions and viewport.
            gl.SetDimensions(width, height);
            gl.Viewport(0, 0, width, height);


            // Make GL current.
            gl.MakeCurrent();

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            //gl.Clear(OpenGL.GL_DEPTH_CLEAR_VALUE);
            
            #endregion create a temperory gl to prevent flickering

            // Initialize the shader for our new GL.
            var esp = Shaders.LoadSimpleShader(gl);

            esp.UseProgram(gl, () =>
            {
                // Set the matrices.
                esp.ApplyMVPNMatrices(gl, modelview, projection, normal);

                // render models, using a temperory color
                foreach (var model in models)
                {
                    var col = model.GenerateColorFromId();
                    esp.ApplyMaterial(gl, new Material() { Ambient = col });
                    Model3DBase.GenerateAndDrawOnce(gl, model); // model.Render(gl, RenderMode.HitTest);
                }
            });
            esp.Dispose();

            // Wait for GPU to finish.
            gl.Flush();
            gl.Finish();


            gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 1);

            uint format = OpenGL.GL_RGBA;
            uint type = OpenGL.GL_UNSIGNED_BYTE;

            byte[] data = new byte[40];
            gl.ReadPixels(x, y, 1, 1, format, type, data);


            // Remove the temperory gl from memory.
            gl.RenderContextProvider.Dispose();

            // Get color id from pixel data.
            id = data[0] + data[1] * 255 + data[2] * 65025; // id = r + g * 255 + b * 255².

            // Find model with id.
            var resultModel = models.FirstOrDefault(m => m.VertexBuffer.VertexBufferObject == id);
            
            return resultModel;
        }
        #endregion HitTest
    }
}
