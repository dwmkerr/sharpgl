using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGL.Enumerations
{
    public enum BufferUsage : uint
    {
        GL_STREAM_DRAW = OpenGL.GL_STREAM_DRAW,
        GL_STREAM_READ = OpenGL.GL_STREAM_READ,
        GL_STREAM_COPY = OpenGL.GL_STREAM_COPY,
        GL_STATIC_DRAW = OpenGL.GL_STATIC_DRAW,
        GL_STATIC_READ = OpenGL.GL_STATIC_READ,
        GL_STATIC_COPY = OpenGL.GL_STATIC_COPY,
        GL_DYNAMIC_DRAW = OpenGL.GL_DYNAMIC_DRAW,
        GL_DYNAMIC_READ = OpenGL.GL_DYNAMIC_READ,
        GL_DYNAMIC_COPY = OpenGL.GL_DYNAMIC_COPY
    }
}
