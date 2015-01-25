
using System;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using SharpGL.Enumerations;
using SharpGL.RenderContextProviders;
using SharpGL.Tests.Helpers;
using SharpGL.Version;

namespace SharpGL.Tests.BasicRendering
{
    [TestFixture(
        Description = 
            "This test ensures that we can create a DIB Section Render Context for OpenGL 1.1." +
            "It makes sure that we can create a simple viewport, perform a projection transformation, " +
            "draw basic geometry and render the scene.")]
    class BasicRenderingTest : RenderingTest
    {
        private const int Width = 1024;
        private const int Height = 768;

        [Test]
        public void CanPerformBasicRendering()
        {
            //  Create an OpenGL instance.
            var gl = new OpenGL();
            Assert.AreEqual(ErrorCode.InvalidOperation, gl.GetErrorCode(), "glGetError should return INVALID_OPERATION as OpenGL is not yet initialised.");
            
            //  Create a DIB section render context provider.
            gl.Create(OpenGLVersion.OpenGL1_1, RenderContextType.DIBSection, Width, Height, 32, null);
            gl.Viewport(0, 0, Width, Height);
            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during render context setup.");

            //  Setup a black background, depth testing and smooth shading.
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            gl.ClearDepth(1.0f);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during setup of scene.");

            //  Create a projection matrix, then go back to the modelview.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.Perspective(45.0f, Width / (float)Height, 0.1f, 100.0f);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during setup of projection matrix.");

            //  Render a coloured pyramid.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Translate(0f, 0.0f, -6.0f);
            
            gl.Begin(OpenGL.GL_TRIANGLES);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);

            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();
            gl.Flush();
            Assert.AreEqual(ErrorCode.NoError, gl.GetErrorCode(), "OpenGL error during rendering of geometry.");

            //  Get the rendered scene as an image.
            using(var renderedScene = CreateComparibleBitmap(((DIBSectionRenderContextProvider)gl.RenderContextProvider).DIBSection.HBitmap))
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
