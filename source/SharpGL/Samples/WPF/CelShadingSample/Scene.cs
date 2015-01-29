using System;
using System.Collections.Generic;
using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.Shaders;

namespace CelShadingSample
{
    /// <summary>
    /// A class that represents the scene for this sample.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// Initialises the Scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void Initialise(OpenGL gl)
        {
            //  We're going to specify the attribute locations for the position and normal, 
            //  so that we can force both shaders to explicitly have the same locations.
            const uint positionAttribute = 0;
            const uint normalAttribute = 1;
            var attributeLocations = new Dictionary<uint, string>
            {
                {positionAttribute, "Position"},
                {normalAttribute, "Normal"},
            };

            //  Create the per pixel shader.
            shaderPerPixel = new ShaderProgram();
            shaderPerPixel.Create(gl, 
                ManifestResourceLoader.LoadTextFile(@"Shaders\PerPixel.vert"),
                ManifestResourceLoader.LoadTextFile(@"Shaders\PerPixel.frag"), attributeLocations);
            
            //  Create the toon shader.
            shaderToon = new ShaderProgram();
            shaderToon.Create(gl,
                ManifestResourceLoader.LoadTextFile(@"Shaders\Toon.vert"),
                ManifestResourceLoader.LoadTextFile(@"Shaders\Toon.frag"), attributeLocations);
            
            //  Generate the geometry and it's buffers.
            trefoilKnot.GenerateGeometry(gl, positionAttribute, normalAttribute);
        }

        /// <summary>
        /// Creates the projection matrix for the given screen size.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="screenWidth">Width of the screen.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        public void CreateProjectionMatrix(OpenGL gl, float screenWidth, float screenHeight)
        {
            //  Create the projection matrix for our screen size.
            const float S = 0.46f;
            float H = S * screenHeight / screenWidth;
            projectionMatrix = glm.pfrustum(-S, S, -H, H, 1, 100);

            //  When we do immediate mode drawing, OpenGL needs to know what our projection matrix
            //  is, so set it now.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.MultMatrix(projectionMatrix.to_array());
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        /// <summary>
        /// Creates the modelview and normal matrix. Also rotates the sceen by a specified amount.
        /// </summary>
        /// <param name="rotationAngle">The rotation angle, in radians.</param>
        public void CreateModelviewAndNormalMatrix(float rotationAngle)
        {
            //  Create the modelview and normal matrix. We'll also rotate the scene
            //  by the provided rotation angle, which means things that draw it 
            //  can make the scene rotate easily.
            mat4 rotation = glm.rotate(mat4.identity(), rotationAngle, new vec3(0, 1, 0));
            mat4 translation = glm.translate(mat4.identity(), new vec3(0, 0, -4));
            modelviewMatrix = rotation * translation;
            normalMatrix = modelviewMatrix.to_mat3();
        }

        /// <summary>
        /// Renders the scene in immediate mode.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public void RenderImmediateMode(OpenGL gl)
        {
            //  Setup the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.MultMatrix(modelviewMatrix.to_array());
            
            //  Push the polygon attributes and set line mode.
            gl.PushAttrib(OpenGL.GL_POLYGON_BIT);
            gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);

            //  Render the trefoil.
            var vertices = trefoilKnot.Vertices;
            gl.Begin(BeginMode.Triangles);
            foreach (var index in trefoilKnot.Indices)
                gl.Vertex(vertices[index].x, vertices[index].y, vertices[index].z);
            gl.End();

            //  Pop the attributes, restoring all polygon state.
            gl.PopAttrib();
        }

        /// <summary>
        /// Renders the scene in retained mode.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="useToonShader">if set to <c>true</c> use the toon shader, otherwise use a per-pixel shader.</param>
        public void RenderRetainedMode(OpenGL gl, bool useToonShader)
        {
            //  Get a reference to the appropriate shader.
            var shader = useToonShader ? shaderToon : shaderPerPixel;

            //  Use the shader program.
            shader.Bind(gl);

            //  Set the variables for the shader program.
            shader.SetUniform3(gl, "DiffuseMaterial", 0f, 0.75f, 0.75f);
            shader.SetUniform3(gl, "AmbientMaterial", 0.04f, 0.04f, 0.04f);
            shader.SetUniform3(gl, "SpecularMaterial", 0.5f, 0.5f, 0.5f);
            shader.SetUniform1(gl, "Shininess", 50f);

            //  Set the light position.
            shader.SetUniform3(gl, "LightPosition", 0.25f, 0.25f, 1f);

            //  Set the matrices.
            shader.SetUniformMatrix4(gl, "Projection", projectionMatrix.to_array());
            shader.SetUniformMatrix4(gl, "Modelview", modelviewMatrix.to_array());
            shader.SetUniformMatrix3(gl, "NormalMatrix", normalMatrix.to_array());

            //  Bind the vertex buffer array.
            trefoilKnot.VertexBufferArray.Bind(gl);
                        
            //  Draw the elements.
            gl.DrawElements(OpenGL.GL_TRIANGLES, trefoilKnot.Indices.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);

            //  Unbind the shader.
            shader.Unbind(gl);
        }
        
        //  The shaders we use.
        private ShaderProgram shaderPerPixel;
        private ShaderProgram shaderToon;
        
        //  The modelview, projection and normal matrices.
        private mat4 modelviewMatrix = mat4.identity();
        private mat4 projectionMatrix = mat4.identity();
        private mat3 normalMatrix = mat3.identity();

        //  Scene geometry - a trefoil knot.
        private readonly TrefoilKnot trefoilKnot = new TrefoilKnot();
    }
}
