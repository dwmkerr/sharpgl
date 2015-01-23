using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using CelShadingSample;
using GlmNet;
using NUnit.Framework;
using SharpGL.Enumerations;
using SharpGL.RenderContextProviders;
using SharpGL.Shaders;
using SharpGL.Tests.Helpers;
using SharpGL.Version;
using SharpGL.VertexBuffers;

namespace SharpGL.Tests.BasicShaders
{
    [TestFixture(
        Description = 
            "This test ensures we can create an FBO Render Context Provider for OpenGL 3.0. " +
            "It tests that we can create a shader program, vertex shader, fragment shader, " +
            "compile the program, build geometry in a VBA, set attributes and render using " +
            "the shader.")]
    class BasicShadersTest : RenderingTest
    {
        private const int Width = 1024;
        private const int Height = 768;

        [Test]
        public void CanPerformBasicRendering()
        {
            //  TODO: Demand 3.0 minimum (to give us access to GLSL 1.3).

            //  Create an OpenGL instance.
            var gl = new OpenGL();

            //  Create an FBO render context provider.
            gl.Create(OpenGLVersion.OpenGL2_1, RenderContextType.FBO, Width, Height, 32, null);
            gl.Viewport(0, 0, Width, Height);
            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during render context setup.");

            //  Give our attributes codes (how we refer to them in C#) and
            //  names (how we load them as 'in' data in the shader).
            const uint positionAttribute = 0;
            const uint normalAttribute = 1;
            var attributeLocations = new Dictionary<uint, string>
            {
                {positionAttribute, "Position"},
                {normalAttribute, "Normal"},
            };

            //  Create a Shader Program.
            var program = new ShaderProgram();
            program.Create(gl,
                ManifestResourceLoader.LoadTextFile(@"BasicShaders.PerPixel.vert"),
                ManifestResourceLoader.LoadTextFile(@"BasicShaders.PerPixel.frag"), attributeLocations);

            //  Create raw torus geometry.
            var torus = new TorusGeometry(1f, .3f, 24, 18);

            //  Create a VBA.
            var vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            //  Bind the vertices and normals.
            var vertexBuffer = new VertexBuffer();
            vertexBuffer.Create(gl);
            vertexBuffer.Bind(gl);
            vertexBuffer.SetData(gl, positionAttribute, torus.vertices.SelectMany(v => v.to_array()).ToArray(), false, 3);

            var normalBuffer = new VertexBuffer();
            normalBuffer.Create(gl);
            normalBuffer.Bind(gl);
            normalBuffer.SetData(gl, normalAttribute, torus.normals.SelectMany(v => v.to_array()).ToArray(), false, 3);

            var indexBuffer = new IndexBuffer();
            indexBuffer.Create(gl);
            indexBuffer.Bind(gl);
            indexBuffer.SetData(gl, torus.triangles.Select(u => (ushort)u).ToArray());

            vertexBufferArray.Unbind(gl);

            //  Create the projection matrix for our screen size.
            const float S = 0.46f;
            float H = S * Height / Width;
            var projectionMatrix = glm.frustum(-S, S, -H, H, 1, 100);

            //  Use the shader program.
            program.Bind(gl);

            //  Set the variables for the shader program.
            program.SetUniform3(gl, "DiffuseMaterial", 0f, 0.75f, 0.75f);
            program.SetUniform3(gl, "AmbientMaterial", 0.04f, 0.04f, 0.04f);
            program.SetUniform3(gl, "SpecularMaterial", 0.5f, 0.5f, 0.5f);
            program.SetUniform1(gl, "Shininess", 50f);

            //  Set the light position.
            program.SetUniform3(gl, "LightPosition", 0.25f, 0.25f, 1f);

            //  Set the matrices.
            program.SetUniformMatrix4(gl, "Projection", projectionMatrix.to_array());
            program.SetUniformMatrix4(gl, "Modelview", mat4.identity().to_array());
            program.SetUniformMatrix3(gl, "NormalMatrix", mat3.identity().to_array());

            //  Bind the vertex buffer array.
            vertexBufferArray.Bind(gl);

            //  Draw the elements.
            gl.DrawElements(OpenGL.GL_TRIANGLES, torus.triangles.Length, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);

            //  Unbind the shader.
            program.Unbind(gl);

            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during rendering of geometry.");

            var fbo = ((FBORenderContextProvider) gl.RenderContextProvider);
            fbo.ReadBuffer();

            //  Get the rendered scene as an image.
            using (var renderedScene = CreateComparibleBitmap(fbo.InternalDIBSection.HBitmap))
            {
                if (ImageCompare.Compare(renderedScene, LoadReferenceBitmap()) == false)
                {
                    //  If they do not match, save the rendered scene and fail.
                    var path = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
                    renderedScene.Save(path, ImageFormat.Png);

                    //  Fail the test.
                    Assert.Fail("The rendered scene does not match the reference image. The rendered scene has been saved to: '{0}'.", path);
                }
            }
        }
    }
}
