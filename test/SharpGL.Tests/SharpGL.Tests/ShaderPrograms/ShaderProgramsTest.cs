using System.Collections.Generic;
using CelShadingSample;
using GlmNet;
using NUnit.Framework;
using SharpGL.Enumerations;
using SharpGL.Shaders;
using SharpGL.Version;

namespace SharpGL.Tests.ShaderPrograms
{
    [TestFixture(
        Description = 
            "This test ensures we can create and compile shaders, set uniforms and attributes and query " +
            "information on shaders.")]
    class ShaderProgramsTest : RenderingTest
    {
        [Test]
        public void CanPerformBasicRendering()
        {
            //  TODO: Demand 3.0 minimum (to give us access to GLSL 1.3).

            //  Create an OpenGL instance.
            var gl = new OpenGL();

            //  Create an FBO render context provider.
            gl.Create(OpenGLVersion.OpenGL3_0, RenderContextType.FBO, 800, 600, 32, null);
            gl.Viewport(0, 0, 800, 800);
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
                ManifestResourceLoader.LoadTextFile(@"ShaderPrograms.PerPixel.vert"),
                ManifestResourceLoader.LoadTextFile(@"ShaderPrograms.PerPixel.frag"), attributeLocations);

            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during shader compilation.");

            //  Use the shader program.
            program.Bind(gl);

            //  We should have two active attributes.
            var data = new int[1];
            gl.GetProgram(program.ShaderProgramObject, OpenGL.GL_ACTIVE_ATTRIBUTES, data);
            var activeAttributes = data[0];
            Assert.AreEqual(2, activeAttributes, "An incorrect number of active attributes has been returned.");

            //  Search through the attributes, looking for the Normal attribute.
            for (var i = 0; i < activeAttributes; i++)
            {
                int length, size;
                uint type;
                string name;
                gl.GetActiveAttrib(program.ShaderProgramObject, (uint)i, 64, out length, out size, out type, out name);

                if (name == "Position")
                {
                    Assert.AreEqual(1, size, "An incorrect size has been returned for an active attribute.");
                    Assert.AreEqual(OpenGL.GL_FLOAT_VEC4, type, "An incorrect type has been returned for an active attribute.");
                }
                else if (name == "Normal")
                {
                    Assert.AreEqual(1, size, "An incorrect size has been returned for an active attribute.");
                    Assert.AreEqual(OpenGL.GL_FLOAT_VEC3, type, "An incorrect type has been returned for an active attribute.");
                }
                else
                {
                    Assert.Fail("An unexpected active attribute has been returned.");
                }
            }

            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during attribute access.");

            //  Set the variables for the shader program.
            program.SetUniform3(gl, "DiffuseMaterial", 0f, 0.75f, 0.75f);
            program.SetUniform3(gl, "AmbientMaterial", 0.04f, 0.04f, 0.04f);
            program.SetUniform3(gl, "SpecularMaterial", 0.5f, 0.5f, 0.5f);
            program.SetUniform1(gl, "Shininess", 50f);

            //  Set the light position.
            program.SetUniform3(gl, "LightPosition", 0.25f, 0.25f, 1f);

            //  Set the matrices.
            program.SetUniformMatrix4(gl, "Projection", mat4.identity().to_array());
            program.SetUniformMatrix4(gl, "Modelview", mat4.identity().to_array());
            program.SetUniformMatrix3(gl, "NormalMatrix", mat3.identity().to_array());

            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error setting uniforms.");

            //  We should now have 3 active uniforms.
            data = new int[1];
            gl.GetProgram(program.ShaderProgramObject, OpenGL.GL_ACTIVE_UNIFORMS, data);
            var activeUniforms = data[0];
            Assert.AreEqual(8, activeUniforms, "An incorrect number of active uniforms has been returned.");

            //  Check some uniforms.
            for (var i = 0; i < activeUniforms; i++)
            {
                int length, size;
                uint type;
                string name;
                gl.GetActiveUniform(program.ShaderProgramObject, (uint)i, 64, out length, out size, out type, out name);

                if (name == "Shininess")
                {
                    Assert.AreEqual(1, size, "An incorrect size has been returned for an active uniform.");
                    Assert.AreEqual(OpenGL.GL_FLOAT, type, "An incorrect type has been returned for an active uniform.");
                }
                else if (name == "DiffuseMaterial")
                {
                    Assert.AreEqual(1, size, "An incorrect size has been returned for an active uniform.");
                    Assert.AreEqual(OpenGL.GL_FLOAT_VEC3, type, "An incorrect type has been returned for an active uniform.");
                }
                else if (name == "Projection")
                {
                    Assert.AreEqual(1, size, "An incorrect size has been returned for an active uniform.");
                    Assert.AreEqual(OpenGL.GL_FLOAT_MAT4, type, "An incorrect type has been returned for an active uniform.");
                }
                else if (!(name == "AmbientMaterial" || name == "SpecularMaterial" || name == "LightPosition"
                    || name == "Modelview" || name == "NormalMatrix"))
                {
                    Assert.Fail("An unexpected active uniform has been returned.");
                }
            }

            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error fetching uniforms data.");

            //  Unbind the shader.
            program.Unbind(gl);
        }
    }
}
