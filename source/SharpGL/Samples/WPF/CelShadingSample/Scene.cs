using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using GlmNet;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.Shaders;

namespace CelShadingSample
{
    public class Scene
    {
        //  TODO: still not happy with this
        private const uint attrPosition = 0;
        private const uint attrNormal = 1;


        public void Initialise(OpenGL gl)
        {
            //  Create the per pixel shader.
            shaderPerPixel = new ShaderProgram();
            shaderPerPixel.Create(gl, 
                ManifestResourceLoader.LoadTextFile(@"Shaders\PerPixel.vert"),
                ManifestResourceLoader.LoadTextFile(@"Shaders\PerPixel.frag"));
            
            //  Generate the geometry and it's buffers.
            trefoilKnot.GenerateGeometry(gl);
        }

        public void CreateProjectionMatrix(float screenWidth, float screenHeight)
        {
            //  Create the projection matrix for our screen size.
            const float S = 0.46f;
            float H = S * screenHeight / screenWidth;
            projectionMatrix = glm.pfrustum(-S, S, -H, H, 4, 10);
        }

        public void CreateModelviewAndNormalMatrix(float rotationAngle)
        {
            //  Create the modelview and normal matrix. We'll also rotate the scene
            //  by the provided rotation angle, which means things that draw it 
            //  can make the scene rotate easily.
            mat4 rotation = glm.rotate(mat4.identity(), rotationAngle, new vec3(0, 1, 0));
            mat4 translation = glm.translate(mat4.identity(), new vec3(0, 0, -7));
            modelviewMatrix = rotation * translation;
            normalMatrix = modelviewMatrix.to_mat3();
        }

        public void RenderImmediateMode(OpenGL gl)
        {
            //  Setup the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.MultMatrix(modelviewMatrix.to_array());

            //  Render the trefoil.
            var vertices = trefoilKnot.Vertices.ToList();
            gl.Begin(BeginMode.Triangles);
            foreach (var index in trefoilKnot.indices)
                gl.Vertex(vertices[index].x, vertices[index].y, vertices[index].z);
            gl.End();
        }

        public void RenderRetainedMode(OpenGL gl)
        {
            //  Use the shader program.
            shaderPerPixel.Bind(gl);

            //  Set the variables for the shader program.
            shaderPerPixel.SetUniform3(gl, "DiffuseMaterial", 0f, 0.75f, 0.75f);
            shaderPerPixel.SetUniform3(gl, "AmbientMaterial", 0.04f, 0.04f, 0.04f);
            shaderPerPixel.SetUniform3(gl, "SpecularMaterial", 0.5f, 0.5f, 0.5f);
            shaderPerPixel.SetUniform1(gl, "Shininess", 50f);

            //  Set the light position.
            shaderPerPixel.SetUniform3(gl, "LightPosition", 0.25f, 0.25f, 1f);

            //  Set the matrices.
            shaderPerPixel.SetUniformMatrix4(gl, "Projection", projectionMatrix.to_array());
            shaderPerPixel.SetUniformMatrix4(gl, "Modelview", modelviewMatrix.to_array());
            shaderPerPixel.SetUniformMatrix3(gl, "NormalMatrix", normalMatrix.to_array());

            //  Bind the vertex and index buffer.
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, trefoilKnot.VertexAndNormalBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, trefoilKnot.IndexBuffer);

            gl.EnableVertexAttribArray(attrPosition);
            gl.EnableVertexAttribArray(attrNormal);

            //  Draw the geometry, straight from the vertex buffer.
            //  TODO Move into buffer generation, only specify once.
            gl.VertexAttribPointer(attrPosition, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(vec3)), IntPtr.Zero);
            int normalOffset = Marshal.SizeOf(typeof(vec3));
            gl.VertexAttribPointer(attrNormal, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(vec3)), IntPtr.Add(new IntPtr(0), normalOffset));

            gl.DrawElements(OpenGL.GL_TRIANGLES, (int)trefoilKnot.IndexCount, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
            //gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 50);

            shaderPerPixel.Unbind(gl);
        }

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        public mat4 ProjectionMatrix
        {
            get { return projectionMatrix; }
        }
        
        //  The shaders we use.
        private ShaderProgram shaderPerPixel;
        
        //  The modelview, projection and normal matrices.
        private mat4 modelviewMatrix = mat4.identity();
        private mat4 projectionMatrix = mat4.identity();
        private mat3 normalMatrix = mat3.identity();

        //  Scene geometry - a trefoil knot.
        private readonly TrefoilKnot trefoilKnot = new TrefoilKnot();
    }
}
