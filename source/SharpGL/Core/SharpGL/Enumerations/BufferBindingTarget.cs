using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGL.Enumerations
{
    public enum BufferBindingTarget : uint
    {
        GL_ARRAY_BUFFER = OpenGL.GL_ARRAY_BUFFER,                           // Vertex attributes
        //GL_ATOMIC_COUNTER_BUFFER = OpenGL.GL_ATOMIC_COUNTER_BUFFER,         // Atomic counter storage
        //GL_COPY_READ_BUFFER = OpenGL.GL_COPY_READ_BUFFER,                   // Buffer copy source
        //GL_COPY_WRITE_BUFFER = OpenGL.GL_COPY_WRITE_BUFFER,                 // Buffer copy destination
        GL_DISPATCH_INDIRECT_BUFFER = OpenGL.GL_DISPATCH_INDIRECT_BUFFER,   // Indirect compute dispatch commands
        //GL_DRAW_INDIRECT_BUFFER = OpenGL.GL_DRAW_INDIRECT_BUFFER,           // Indirect command arguments
        GL_ELEMENT_ARRAY_BUFFER = OpenGL.GL_ELEMENT_ARRAY_BUFFER,           // Vertex array indices
        GL_PIXEL_PACK_BUFFER = OpenGL.GL_PIXEL_PACK_BUFFER,                 // Pixel read target
        GL_PIXEL_UNPACK_BUFFER = OpenGL.GL_PIXEL_UNPACK_BUFFER,             // Texture data source
        //GL_QUERY_BUFFER = OpenGL.GL_QUERY_BUFFER,                           // Query result buffer
        GL_SHADER_STORAGE_BUFFER = OpenGL.GL_SHADER_STORAGE_BUFFER,         // Read-write storage for shaders
        GL_TEXTURE_BUFFER = OpenGL.GL_TEXTURE_BUFFER,                       // Texture data buffer
        GL_TRANSFORM_FEEDBACK_BUFFER = OpenGL.GL_TRANSFORM_FEEDBACK_BUFFER, // Transform feedback buffer
        //GL_UNIFORM_BUFFER = OpenGL.GL_UNIFORM_BUFFER,                       // Uniform block storage
    }
}
