using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL.Tests.Contexts
{
    internal class OpenGLVersionException : Exception
    {
        public OpenGLVersionException()
        {
        }

        public OpenGLVersionException(string message) : base(message)
        {
        }

        public OpenGLVersionException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
