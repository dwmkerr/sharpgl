using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using SharpGL.RenderContextProviders;
using SharpGL.Version;

namespace SharpGL
{
	/// <summary>
	/// The OpenGL class wraps Suns OpenGL 3D library.
    /// </summary>
	public partial class OpenGL
	{        
		#region Error Checking

        /// <summary>
        /// Gets the error description for a given error code.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>The error description for the given error code.</returns>
        public string GetErrorDescription(uint errorCode)
        {
            switch (errorCode)
            {
                case GL_NO_ERROR:
                    return "No Error";
                case GL_INVALID_ENUM:
                    return "A GLenum argument was out of range.";
                case GL_INVALID_VALUE:
                    return "A numeric argument was out of range.";
                case GL_INVALID_OPERATION:
                    return "Invalid operation.";
                case GL_STACK_OVERFLOW:
                    return "Command would cause a stack overflow.";
                case GL_STACK_UNDERFLOW:
                    return "Command would cause a stack underflow.";
                case GL_OUT_OF_MEMORY:
                    return "Not enough memory left to execute command.";
                default:
                    return "Unknown Error";
            }
        }

        /// <summary>
        /// Called before an OpenGL call to enable error checking and ensure the
        /// correct OpenGL context is correct.
        /// </summary>
		protected virtual void PreGLCall()
		{
            //  If we are in debug mode, clear the error flag.
#if DEBUG
      // GetError() should not be called at all inside glBegin-glEnd
if (insideGLBegin == false)
{
            GetError();
      }
#endif

      //  If we are not the current OpenGL object, make ourselves current.
            if (currentOpenGLInstance != this)
            {
                MakeCurrent();
            }
		}

        /// <summary>
        /// Called after an OpenGL call to enable error checking.
        /// </summary>
        protected virtual void PostGLCall()
        {
#if DEBUG
            //  We can only perform the following error check if we
            //  are not in a glBegin function.
            if (insideGLBegin == false)
            {
                //	This error check is very useful, as you can break anytime 
                //	an OpenGL error occurs, going through a program with this on
                //	can rid it of bugs. It's VERY slow though, as every call is monitored.
                uint errorCode = GetError();

                //	What error is it?
                if (errorCode != GL_NO_ERROR)
                {
                    //  Get the error message.
                    var errorMessage = GetErrorDescription(errorCode);

                    //  Create a stack trace.
                    var stackTrace = new StackTrace();

                    //  Get the stack frames.
                    var stackFrames = stackTrace.GetFrames();

                    //  Write the error to the trace log.
                    var functionName = (stackFrames != null && stackFrames.Length > 1) ? stackFrames[1].GetMethod().Name : "Unknown Function";
                    Trace.WriteLine("OpenGL Error: \"" + errorMessage + "\", when calling function SharpGL." + functionName);
                }
            }
#endif
        }

		#endregion
       
    }
}
