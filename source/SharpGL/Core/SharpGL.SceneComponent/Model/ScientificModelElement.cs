using GlmNet;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.SceneComponent
{
    /// <summary>
    /// scene element that holds a <see cref="ScientificModel"/>.
    /// </summary>
    public class ScientificModelElement : SceneElement, IColorCodedPicking
    {
        /// <summary>
        /// The model shown in <see cref="ScientificVisual3DControl"/>.
        /// </summary>
        public ScientificModel Model { get; set; }

        public bool RenderModel { get; set; }

        public ScientificModelElement(ScientificModel model, IScientificCamera camera, bool renderModel = true)
        {
            this.Model = model;
            this.Camera = camera;
            this.RenderModel = renderModel;
        }


        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;

        //  Constants that specify the attribute indexes.
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        //  The vertex buffer array which contains the vertex and colour buffers.
        VertexBufferArray vertexBufferArray;

        //  The shader program for our vertex and fragment shader.
        private ShaderProgram shaderProgram;
        private ShaderProgram pickingShaderProgram;
        private bool initialised;

        /// <summary>
        /// <para>Use <see cref="IHasObjectSpace"/> and <see cref="IScientificCamera"/> to update projection and view matrices.</para>
        /// </summary>
        public IScientificCamera Camera { get; set; }

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="width">The width of the screen.</param>
        /// <param name="height">The height of the screen.</param>
        public void Initialise(OpenGL gl)
        {
            //  Set a blue clear colour.
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.5f);

            {
                //  Create the shader program.
                var vertexShaderSource = ManifestResourceLoader.LoadTextFile(@"Model\Shader.vert");
                var fragmentShaderSource = ManifestResourceLoader.LoadTextFile(@"Model\Shader.frag");
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.shaderProgram = shaderProgram;
            }
            {
                //  Create the picking shader program.
                var vertexShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.VertexShader);
                var fragmentShaderSource = ColorCodedPickingShaderHelper.GetShaderSource(ColorCodedPickingShaderHelper.ShaderTypes.FragmentShader);
                var shaderProgram = new ShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);
                this.pickingShaderProgram = shaderProgram;
            }

            unsafe
            {
                //  Create the vertex array object.
                vertexBufferArray = new VertexBufferArray();
                vertexBufferArray.Create(gl);
                vertexBufferArray.Bind(gl);

                //  Create a vertex buffer for the vertex data.
                var vertexDataBuffer = new VertexBuffer();
                vertexDataBuffer.Create(gl);
                vertexDataBuffer.Bind(gl);
                //vertexDataBuffer.SetData(gl, 0, 
                    //this.Model.VertexCount * sizeof(Vertex), (IntPtr)this.Model.Positions, false, 3, OpenGL.GL_FLOAT);
                vertexDataBuffer.SetData(gl, 0, this.Model.Positions, false, 3);

                //  Now do the same for the colour data.
                var colourDataBuffer = new VertexBuffer();
                colourDataBuffer.Create(gl);
                colourDataBuffer.Bind(gl);
                //colourDataBuffer.SetData(gl, 1,
                    //this.Model.VertexCount * sizeof(ByteColor), (IntPtr)this.Model.Colors, false, 3, OpenGL.GL_BYTE);
                colourDataBuffer.SetData(gl, 1, this.Model.Colors, false, 3);

                //  Unbind the vertex array, we've finished specifying data for it.
                vertexBufferArray.Unbind(gl);
            }
            ////  Create a perspective projection matrix.
            //const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            //projectionMatrix = glm.perspective(rads, width / height, 0.1f, 100.0f);

            ////  Create a view matrix to move us back a bit.
            //viewMatrix = glm.translate(new mat4(1.0f), new vec3(0.0f, 0.0f, -5.0f));

            ////  Create a model matrix to make the model a little bigger.
            //modelMatrix = glm.scale(new mat4(1.0f), new vec3(2.5f));
            ////IPerspectiveCamera camera = this.cameraRotation.Camera;
            ////projectionMatrix = camera.GetProjectionMat4();
            ////viewMatrix = this.cameraRotation.Camera.GetViewMat4();
            ////modelMatrix = mat4.identity();

            ////  Now create the geometry for the square.
            //CreateVertices(gl);

            this.initialised = true;
        }

        #region IRenderable 成员

        void IRenderable.Render(OpenGL gl, RenderMode renderMode)
        {
            if (!this.RenderModel) { return; }

            if(!initialised)
            {
                this.Initialise(gl);
            }
            // Update matrices.
            IScientificCamera camera = this.Camera;
            if (camera != null)
            {
                if (camera.CameraType == CameraTypes.Perspecitive)
                {
                    IPerspectiveViewCamera perspective = camera;
                    this.projectionMatrix = perspective.GetProjectionMat4();
                    this.viewMatrix = perspective.GetViewMat4();
                }
                else if (camera.CameraType == CameraTypes.Ortho)
                {
                    IOrthoViewCamera ortho = camera;
                    this.projectionMatrix = ortho.GetProjectionMat4();
                    this.viewMatrix = ortho.GetViewMat4();
                }
                else
                { throw new NotImplementedException(); }
            }

            modelMatrix = mat4.identity();

            //gl.PointSize(3);

            var shader = (renderMode == RenderMode.HitTest) ? pickingShaderProgram : shaderProgram;
            //  Bind the shader, set the matrices.
            shader.Bind(gl);
            shader.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shader.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            if (renderMode == RenderMode.HitTest)
            {
                shader.SetUniform1(gl, "pickingBaseID", ((IColorCodedPicking)this).PickingBaseID);
            }

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            gl.DrawArrays((uint)this.Model.Mode, 0, this.Model.VertexCount);

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shader.Unbind(gl);
        }

        #endregion

        //#region IRenderable 成员

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="gl"></param>
        ///// <param name="renderMode"></param>
        //public virtual void Render(OpenGL gl, RenderMode renderMode)
        //{
        //    //TODO: this 'virtual' maybe not necessary, consider to remove it.
        //    ScientificModel model = this.Model;
        //    if (model == null) { return; }

        //    switch (RenderOrder)
        //    {
        //        case Order.ModelBoundingBox:
        //            if (this.RenderModel)
        //            {
        //                model.Render(gl, renderMode);
        //            } 
 
        //            if (this.RenderBoundingBox)
        //            {
        //                model.BoundingBox.Render(gl, renderMode);
        //            }
        //           break;
        //        case Order.BoundingBoxModel:
        //            if (this.RenderBoundingBox)
        //            {
        //                model.BoundingBox.Render(gl, renderMode);
        //            }

        //            if (this.RenderModel)
        //            {
        //                model.Render(gl, renderMode);
        //            }
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }

        //}

        //#endregion

        public enum Order
        {
            ModelBoundingBox,
            BoundingBoxModel,
        }

        #region IColorCodedPicking 成员

        uint IColorCodedPicking.PickingBaseID { get; set; }

        uint IColorCodedPicking.GetVertexCount()
        {
            return (uint)this.Model.VertexCount;
        }

        IPickedGeometry IColorCodedPicking.Pick(uint stageVertexID)
        {
            IColorCodedPicking element = this as IColorCodedPicking;
            PickedGeometryColored pickedGeometry = element.TryPick<PickedGeometryColored>(
                this.Model.Mode, stageVertexID, this.Model.Positions);

            if (pickedGeometry == null) { return null; }

            // Fill primitive's positions and colors. This maybe changes much more than lines above in second dev.
            uint lastVertexID;
            if(element.GetLastVertexIDOfPickedGeometry(stageVertexID, out lastVertexID))
            {
                ScientificModel model = this.Model;

                int vertexCount = pickedGeometry.GeometryType.GetVertexCount();
                if (vertexCount == -1) { vertexCount = model.VertexCount; }

                float[] geometryColors = new float[vertexCount * 3];

                float[] modelColors = model.Colors;

                uint i = lastVertexID * 3 + 2;
                for (int j = (geometryColors.Length - 1); j >= 0; i--, j--)
                {
                    if (i == uint.MaxValue)// This is when mode is GL_LINE_LOOP.
                    { i = (uint)modelColors.Length - 1; }
                    geometryColors[j] = modelColors[i];
                }

                pickedGeometry.colors = geometryColors;
            }

            return pickedGeometry;
        }

        #endregion
    }

}
