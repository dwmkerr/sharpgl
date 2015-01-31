using System;
using System.Text;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_framebuffer_object

        private delegate bool glIsRenderbuffer(uint renderbuffer);

        private delegate void glBindRenderbuffer(uint target, uint renderbuffer);

        private delegate void glDeleteRenderbuffers(int n, uint[] renderbuffers);

        private delegate void glGenRenderbuffers(int n, uint[] renderbuffers);

        private delegate void glRenderbufferStorage(uint target, uint internalformat, int width, int height);

        private delegate void glRenderbufferStorageMultisample(
            uint target, int samples, uint internalformat, int width, int height);

        private delegate void glGetRenderbufferParameteriv(uint target, uint pname, int[] @params);

        private delegate bool glIsFramebuffer(uint framebuffer);

        private delegate void glBindFramebuffer(uint target, uint framebuffer);

        private delegate void glDeleteFramebuffers(int n, uint[] framebuffers);

        private delegate void glGenFramebuffers(int n, uint[] framebuffers);

        private delegate uint glCheckFramebufferStatus(uint target);

        private delegate void glFramebufferTexture1D(
            uint target, uint attachment, uint textarget, uint texture, int level);

        private delegate void glFramebufferTexture2D(
            uint target, uint attachment, uint textarget, uint texture, int level);

        private delegate void glFramebufferTexture3D(
            uint target, uint attachment, uint textarget, uint texture, int level, int layer);

        private delegate void glFramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer
            );

        private delegate void glFramebufferRenderbuffer(
            uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

        private delegate void glGetFramebufferAttachmentParameteriv(
            uint target, uint attachment, uint pname, int[] @params);

        private delegate void glBlitFramebuffer(
            int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask,
            uint filter);

        private delegate void glGenerateMipmap(uint target);

        public const uint GL_FRAMEBUFFER = 0x8D40;
        public const uint GL_READ_FRAMEBUFFER = 0x8CA8;
        public const uint GL_DRAW_FRAMEBUFFER = 0x8CA9;
        public const uint GL_RENDERBUFFER = 0x8D41;
        public const uint GL_STENCIL_INDEX1 = 0x8D46;
        public const uint GL_STENCIL_INDEX4 = 0x8D47;
        public const uint GL_STENCIL_INDEX8 = 0x8D48;
        public const uint GL_STENCIL_INDEX16 = 0x8D49;
        public const uint GL_RENDERBUFFER_WIDTH = 0x8D42;
        public const uint GL_RENDERBUFFER_HEIGHT = 0x8D43;
        public const uint GL_RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
        public const uint GL_RENDERBUFFER_RED_SIZE = 0x8D50;
        public const uint GL_RENDERBUFFER_GREEN_SIZE = 0x8D51;
        public const uint GL_RENDERBUFFER_BLUE_SIZE = 0x8D52;
        public const uint GL_RENDERBUFFER_ALPHA_SIZE = 0x8D53;
        public const uint GL_RENDERBUFFER_DEPTH_SIZE = 0x8D54;
        public const uint GL_RENDERBUFFER_STENCIL_SIZE = 0x8D55;
        public const uint GL_RENDERBUFFER_SAMPLES = 0x8CAB;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER = 0x8CD4;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING = 0x8210;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE = 0x8211;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_RED_SIZE = 0x8212;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_GREEN_SIZE = 0x8213;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_BLUE_SIZE = 0x8214;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE = 0x8215;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE = 0x8216;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE = 0x8217;
        public const uint GL_UNSIGNED_NORMALIZED = 0x8C17;
        public const uint GL_FRAMEBUFFER_DEFAULT = 0x8218;
        public const uint GL_INDEX = 0x8222;
        public const uint GL_COLOR_ATTACHMENT0 = 0x8CE0;
        public const uint GL_COLOR_ATTACHMENT1 = 0x8CE1;
        public const uint GL_COLOR_ATTACHMENT2 = 0x8CE2;
        public const uint GL_COLOR_ATTACHMENT3 = 0x8CE3;
        public const uint GL_COLOR_ATTACHMENT4 = 0x8CE4;
        public const uint GL_COLOR_ATTACHMENT5 = 0x8CE5;
        public const uint GL_COLOR_ATTACHMENT6 = 0x8CE6;
        public const uint GL_COLOR_ATTACHMENT7 = 0x8CE7;
        public const uint GL_COLOR_ATTACHMENT8 = 0x8CE8;
        public const uint GL_COLOR_ATTACHMENT9 = 0x8CE9;
        public const uint GL_COLOR_ATTACHMENT10 = 0x8CEA;
        public const uint GL_COLOR_ATTACHMENT11 = 0x8CEB;
        public const uint GL_COLOR_ATTACHMENT12 = 0x8CEC;
        public const uint GL_COLOR_ATTACHMENT13 = 0x8CED;
        public const uint GL_COLOR_ATTACHMENT14 = 0x8CEE;
        public const uint GL_COLOR_ATTACHMENT15 = 0x8CEF;
        public const uint GL_DEPTH_ATTACHMENT = 0x8D00;
        public const uint GL_STENCIL_ATTACHMENT = 0x8D20;
        public const uint GL_DEPTH_STENCIL_ATTACHMENT = 0x821A;
        public const uint GL_MAX_SAMPLES = 0x8D57;
        public const uint GL_FRAMEBUFFER_BINDING = 0x8CA6;
        public const uint GL_DRAW_FRAMEBUFFER_BINDING = 0x8CA6;
        public const uint GL_READ_FRAMEBUFFER_BINDING = 0x8CAA;
        public const uint GL_RENDERBUFFER_BINDING = 0x8CA7;
        public const uint GL_MAX_COLOR_ATTACHMENTS = 0x8CDF;
        public const uint GL_MAX_RENDERBUFFER_SIZE = 0x84E8;
        public const uint GL_FRAMEBUFFER_COMPLETE = 0x8CD5;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER = 0x8CDB;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER = 0x8CDC;
        public const uint GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;
        public const uint GL_FRAMEBUFFER_UNDEFINED = 0x8219;
        public const uint GL_INVALID_FRAMEBUFFER_OPERATION = 0x0506;
        public const uint GL_DEPTH_STENCIL = 0x84F9;
        public const uint GL_UNSIGNED_INT_24_8 = 0x84FA;
        public const uint GL_DEPTH24_STENCIL8 = 0x88F0;
        public const uint GL_TEXTURE_STENCIL_SIZE = 0x88F1;


        /// <summary>
        /// Determine if a name corresponds to a renderbuffer object.
        /// </summary>
        /// <param name="renderbuffer">Specifies a value that may be the name of a renderbuffer object.</param>
        /// <returns>glIsRenderbuffer returns GL_TRUE if renderbuffer is currently the name of a renderbuffer object. If renderbuffer is zero, or is a non-zero value that is not currently the name of a renderbuffer object, or if an error occurs, glIsRenderbuffer returns GL_FALSE.</returns>
        public bool IsRenderbuffer(uint renderbuffer)
        {
            return GetDelegateFor<glIsRenderbuffer>()(renderbuffer);
        }

        /// <summary>
        /// Bind a named renderbuffer object.
        /// </summary>
        /// <param name="target">Specifies the target to which the renderbuffer object is bound. The symbolic constant must be GL_RENDERBUFFER.</param>
        /// <param name="renderbuffer">Specifies the name of a renderbuffer object.</param>
        public void BindRenderbuffer(uint target, uint renderbuffer)
        {
            GetDelegateFor<glBindRenderbuffer>()(target, renderbuffer);
        }

        /// <summary>
        /// Delete named renderbuffer objects.
        /// </summary>
        /// <param name="n">Specifies the number of renderbuffer objects to be deleted.</param>
        /// <param name="renderbuffers">Specifies an array of renderbuffer objects to be deleted.</param>
        public void DeleteRenderbuffers(int n, uint[] renderbuffers)
        {
            GetDelegateFor<glDeleteRenderbuffers>()(n, renderbuffers);
        }

        /// <summary>
        /// Generate renderbuffer object names.
        /// </summary>
        /// <param name="n">Specifies the number of renderbuffer object names to be generated.</param>
        /// <param name="renderbuffers">Specifies an array in which the generated renderbuffer object names are stored.</param>
        public void GenRenderbuffers(int n, uint[] renderbuffers)
        {
            GetDelegateFor<glGenRenderbuffers>()(n, renderbuffers);
        }

        /// <summary>
        /// Create and initialize a renderbuffer object's data store.
        /// </summary>
        /// <param name="target">Specifies the renderbuffer target. The symbolic constant must be GL_RENDERBUFFER.</param>
        /// <param name="internalformat">Specifies the color-renderable, depth-renderable, or stencil-renderable format of the renderbuffer. Must be one of the following symbolic constants: GL_RGBA4, GL_RGB565, GL_RGB5_A1, GL_DEPTH_COMPONENT16, or GL_STENCIL_INDEX8.</param>
        /// <param name="width">Specifies the width of the renderbuffer in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer in pixels.</param>
        public void RenderbufferStorage(uint target, uint internalformat, int width, int height)
        {
            GetDelegateFor<glRenderbufferStorage>()(target, internalformat, width, height);
        }

        /// <summary>
        /// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
        /// </summary>
        /// <param name="target">Specifies a binding to which the target of the allocation and must be GL_RENDERBUFFER.</param>
        /// <param name="samples">Specifies the number of samples to be used for the renderbuffer object's storage.</param>
        /// <param name="internalformat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        public void RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height)
        {
            GetDelegateFor<glRenderbufferStorageMultisample>()(target, samples, internalformat, width, height);
        }

        /// <summary>
        /// Return parameters of a renderbuffer object.
        /// </summary>
        /// <param name="target">Specifies the target renderbuffer object. The symbolic constant must be GL_RENDERBUFFER.</param>
        /// <param name="pname">Specifies the symbolic name of a renderbuffer object parameter. Accepted values are GL_RENDERBUFFER_WIDTH, GL_RENDERBUFFER_HEIGHT, GL_RENDERBUFFER_INTERNAL_FORMAT, GL_RENDERBUFFER_RED_SIZE, GL_RENDERBUFFER_GREEN_SIZE, GL_RENDERBUFFER_BLUE_SIZE, GL_RENDERBUFFER_ALPHA_SIZE, GL_RENDERBUFFER_DEPTH_SIZE, or GL_RENDERBUFFER_STENCIL_SIZE.</param>
        /// <param name="params">Returns the requested parameter.</param>
        public void GetRenderbufferParameter(uint target, uint pname, int[] @params)
        {
            GetDelegateFor<glGetRenderbufferParameteriv>()(target, pname, @params);
        }

        /// <summary>
        /// Determine if a name corresponds to a framebuffer object.
        /// </summary>
        /// <param name="framebuffer">Specifies a value that may be the name of a framebuffer object.</param>
        /// <returns>glIsFramebuffer returns GL_TRUE if framebuffer is currently the name of a framebuffer object. If framebuffer is zero, or is a non-zero value that is not currently the name of a framebuffer object, or if an error occurs, glIsFramebuffer returns GL_FALSE.</returns>
        public bool IsFramebuffer(uint framebuffer)
        {
            return GetDelegateFor<glIsFramebuffer>()(framebuffer);
        }

        /// <summary>
        /// Bind a named framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the target to which the framebuffer object is bound. The symbolic constant must be GL_FRAMEBUFFER.</param>
        /// <param name="framebuffer">Specifies the name of a framebuffer object.</param>
        public void BindFramebuffer(uint target, uint framebuffer)
        {
            GetDelegateFor<glBindFramebuffer>()(target, framebuffer);
        }

        /// <summary>
        /// Delete named framebuffer objects.
        /// </summary>
        /// <param name="n">Specifies the number of framebuffer objects to be deleted.</param>
        /// <param name="framebuffers">Specifies an array of framebuffer objects to be deleted.</param>
        public void DeleteFramebuffers(int n, uint[] framebuffers)
        {
            GetDelegateFor<glDeleteFramebuffers>()(n, framebuffers);
        }

        /// <summary>
        /// Generate framebuffer object names.
        /// </summary>
        /// <param name="n">Specifies the number of framebuffer object names to be generated.</param>
        /// <param name="framebuffers">Specifies an array in which the generated framebuffer object names are stored.</param>
        public void GenFramebuffers(int n, uint[] framebuffers)
        {
            GetDelegateFor<glGenFramebuffers>()(n, framebuffers);
        }

        /// <summary>
        /// Check the completeness status of a framebuffer.
        /// </summary>
        /// <param name="target">Specify the target of the framebuffer completeness check.</param>
        /// <returns>The return value is GL_FRAMEBUFFER_COMPLETE if the framebuffer bound to target is complete. Otherwise, the return value is determined as follows:
        /// GL_FRAMEBUFFER_UNDEFINED is returned if target is the default framebuffer, but the default framebuffer does not exist.
        /// GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT is returned if any of the framebuffer attachment points are framebuffer incomplete.
        /// GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT is returned if the framebuffer does not have at least one image attached to it.
        /// GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER is returned if the value of GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE is GL_NONE for any color attachment point(s) named by GL_DRAW_BUFFERi.
        /// GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER is returned if GL_READ_BUFFER is not GL_NONE and the value of GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE is GL_NONE for the color attachment point named by GL_READ_BUFFER.
        /// GL_FRAMEBUFFER_UNSUPPORTED is returned if the combination of internal formats of the attached images violates an implementation-dependent set of restrictions.
        /// GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE is returned if the value of GL_RENDERBUFFER_SAMPLES is not the same for all attached renderbuffers; if the value of GL_TEXTURE_SAMPLES is the not same for all attached textures; or, if the attached images are a mix of renderbuffers and textures, the value of GL_RENDERBUFFER_SAMPLES does not match the value of GL_TEXTURE_SAMPLES.
        /// GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE is also returned if the value of GL_TEXTURE_FIXED_SAMPLE_LOCATIONS is not the same for all attached textures; or, if the attached images are a mix of renderbuffers and textures, the value of GL_TEXTURE_FIXED_SAMPLE_LOCATIONS is not GL_TRUE for all attached textures.
        /// GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS is returned if any framebuffer attachment is layered, and any populated attachment is not layered, or if all populated color attachments are not from textures of the same target.
        /// Additionally, if an error occurs, zero is returned.</returns>
        public uint CheckFramebufferStatus(uint target)
        {
            return GetDelegateFor<glCheckFramebufferStatus>()(target);
        }

        /// <summary>
        /// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target. target must be GL_DRAW_FRAMEBUFFER, GL_READ_FRAMEBUFFER, or GL_FRAMEBUFFER. GL_FRAMEBUFFER is equivalent to GL_DRAW_FRAMEBUFFER.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer. attachment must be GL_COLOR_ATTACHMENTi, GL_DEPTH_ATTACHMENT, GL_STENCIL_ATTACHMENT or GL_DEPTH_STENCIL_ATTACHMENT.</param>
        /// <param name="textarget">For glFramebufferTexture1D, glFramebufferTexture2D and glFramebufferTexture3D, specifies what type of texture is expected in the texture parameter, or for cube map textures, which face is to be attached.</param>
        /// <param name="texture">Specifies the texture object to attach to the framebuffer attachment point named by attachment.</param>
        /// <param name="level">Specifies the mipmap level of texture to attach.</param>
        public void FramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            GetDelegateFor<glFramebufferTexture1D>()(target, attachment, textarget, texture, level);
        }

        /// <summary>
        /// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target. target must be GL_DRAW_FRAMEBUFFER, GL_READ_FRAMEBUFFER, or GL_FRAMEBUFFER. GL_FRAMEBUFFER is equivalent to GL_DRAW_FRAMEBUFFER.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer. attachment must be GL_COLOR_ATTACHMENTi, GL_DEPTH_ATTACHMENT, GL_STENCIL_ATTACHMENT or GL_DEPTH_STENCIL_ATTACHMENT.</param>
        /// <param name="textarget">For glFramebufferTexture1D, glFramebufferTexture2D and glFramebufferTexture3D, specifies what type of texture is expected in the texture parameter, or for cube map textures, which face is to be attached.</param>
        /// <param name="texture">Specifies the texture object to attach to the framebuffer attachment point named by attachment.</param>
        /// <param name="level">Specifies the mipmap level of texture to attach.</param>
        public void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            GetDelegateFor<glFramebufferTexture2D>()(target, attachment, textarget, texture, level);
        }

        /// <summary>
        /// Attach a level of a texture object as a logical buffer to the currently bound framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target. target must be GL_DRAW_FRAMEBUFFER, GL_READ_FRAMEBUFFER, or GL_FRAMEBUFFER. GL_FRAMEBUFFER is equivalent to GL_DRAW_FRAMEBUFFER.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer. attachment must be GL_COLOR_ATTACHMENTi, GL_DEPTH_ATTACHMENT, GL_STENCIL_ATTACHMENT or GL_DEPTH_STENCIL_ATTACHMENT.</param>
        /// <param name="textarget">For glFramebufferTexture1D, glFramebufferTexture2D and glFramebufferTexture3D, specifies what type of texture is expected in the texture parameter, or for cube map textures, which face is to be attached.</param>
        /// <param name="texture">Specifies the texture object to attach to the framebuffer attachment point named by attachment.</param>
        /// <param name="level">Specifies the mipmap level of texture to attach.</param>
        /// <param name="layer">Specifies the layer of a 2-dimensional image within a 3-dimensional texture.</param>
        public void FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level,
                                         int layer)
        {
            GetDelegateFor<glFramebufferTexture3D>()(target, attachment, textarget, texture, level, layer);
        }

        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object
        /// </summary>
        /// <param name="target">Specifies the target to which the framebuffer is bound for glFramebufferTextureLayer.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="texture">Specifies the name of an existing texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        public void FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer)
        {
            GetDelegateFor<glFramebufferTextureLayer>()(target, attachment, texture, level, layer);
        }

        /// <summary>
        /// Attach a renderbuffer object to a framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the framebuffer target. The symbolic constant must be GL_FRAMEBUFFER.</param>
        /// <param name="attachment">Specifies the attachment point to which renderbuffer should be attached. Must be one of the following symbolic constants: GL_COLOR_ATTACHMENT0, GL_DEPTH_ATTACHMENT, or GL_STENCIL_ATTACHMENT.</param>
        /// <param name="renderbuffertarget">Specifies the renderbuffer target. The symbolic constant must be GL_RENDERBUFFER.</param>
        /// <param name="renderbuffer">Specifies the renderbuffer object that is to be attached.</param>
        public void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
        {
            GetDelegateFor<glFramebufferRenderbuffer>()(target, attachment, renderbuffertarget, renderbuffer);
        }

        /// <summary>
        /// Return attachment parameters of a framebuffer object.
        /// </summary>
        /// <param name="target">Specifies the target framebuffer object. The symbolic constant must be GL_FRAMEBUFFER.</param>
        /// <param name="attachment">Specifies the symbolic name of a framebuffer object attachment point. Accepted values are GL_COLOR_ATTACHMENT0, GL_DEPTH_ATTACHMENT, and GL_STENCIL_ATTACHMENT.</param>
        /// <param name="pname">Specifies the symbolic name of a framebuffer object attachment parameter. Accepted values are GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE, GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME, GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL, and GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE.</param>
        /// <param name="params">Returns the requested parameter.</param>
        public void GetFramebufferAttachmentParameter(uint target, uint attachment, uint pname, int[] @params)
        {
            GetDelegateFor<glGetFramebufferAttachmentParameteriv>()(target, attachment, pname, @params);
        }

        /// <summary>
        /// Copy a block of pixels from the read framebuffer to the draw framebuffer.
        /// </summary>
        /// <param name="srcX0">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcY0">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcX1">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="srcY1">Specify the bounds of the source rectangle within the read buffer of the read framebuffer.</param>
        /// <param name="dstX0">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstY0">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstX1">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="dstY1">Specify the bounds of the destination rectangle within the write buffer of the write framebuffer.</param>
        /// <param name="mask">The bitwise OR of the flags indicating which buffers are to be copied. The allowed flags are GL_COLOR_BUFFER_BIT, GL_DEPTH_BUFFER_BIT and GL_STENCIL_BUFFER_BIT.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched. Must be GL_NEAREST or GL_LINEAR.</param>
        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1,
                                    int dstY1, uint mask, uint filter)
        {
            GetDelegateFor<glBlitFramebuffer>()(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1,
                                                dstY1, mask, filter);
        }

        /// <summary>
        /// Generate mipmaps for a specified texture object.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for glGenerateMipmap. Must be one of GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_3D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_CUBE_MAP, or GL_TEXTURE_CUBE_MAP_ARRAY.</param>
        public void GenerateMipmap(uint target)
        {
            GetDelegateFor<glGenerateMipmap>()(target);
        }

        #endregion

        #region ARB_vertex_array_object

        private delegate void glBindVertexArray(uint array);

        private delegate void glDeleteVertexArrays(int n, uint[] arrays);

        private delegate void glGenVertexArrays(int n, uint[] arrays);

        private delegate bool glIsVertexArray(uint array);

        public const uint GL_VERTEX_ARRAY_BINDING = 0x85B5;

        /// <summary>
        /// Bind a vertex array object.
        /// </summary>
        /// <param name="array">Specifies the name of the vertex array to bind.</param>
        public void BindVertexArray(uint array)
        {
            GetDelegateFor<glBindVertexArray>()(array);
        }

        /// <summary>
        /// Delete vertex array objects.
        /// </summary>
        /// <param name="n">Specifies the number of vertex array objects to be deleted.</param>
        /// <param name="arrays">Specifies the address of an array containing the n names of the objects to be deleted.</param>
        public void DeleteVertexArrays(int n, uint[] arrays)
        {
            GetDelegateFor<glDeleteVertexArrays>()(n, arrays);
        }

        /// <summary>
        /// Generate vertex array object names.
        /// </summary>
        /// <param name="n">Specifies the number of vertex array object names to generate.</param>
        /// <param name="arrays">Specifies an array in which the generated vertex array object names are stored.</param>
        public void GenVertexArrays(int n, uint[] arrays)
        {
            GetDelegateFor<glGenVertexArrays>()(n, arrays);
        }

        /// <summary>
        /// Determine if a name corresponds to a vertex array object.
        /// </summary>
        /// <param name="array">Specifies a value that may be the name of a vertex array object.</param>
        /// <returns>glIsVertexArray returns GL_TRUE if array is currently the name of a renderbuffer object. If renderbuffer is zero, or if array is not the name of a renderbuffer object, or if an error occurs, glIsVertexArray returns GL_FALSE. If array is a name returned by glGenVertexArrays, by that has not yet been bound through a call to glBindVertexArray, then the name is not a vertex array object and glIsVertexArray returns GL_FALSE.</returns>
        public bool IsVertexArray(uint array)
        {
            return GetDelegateFor<glIsVertexArray>()(array);
        }

        #endregion

        #region NV_conditional_render

        private delegate void glBeginConditionalRender(uint id, uint mode);

        private delegate void glEndConditionalRender();

        public const uint GL_QUERY_WAIT = 0x8E13;
        public const uint GL_QUERY_NO_WAIT = 0x8E14;
        public const uint GL_QUERY_BY_REGION_WAIT = 0x8E15;
        public const uint GL_QUERY_BY_REGION_NO_WAIT = 0x8E16;

        /// <summary>
        /// Start conditional rendering.
        /// </summary>
        /// <param name="id">Specifies the name of an occlusion query object whose results are used to determine if the Rendering Commands are discarded.</param>
        /// <param name="mode">Specifies how glBeginConditionalRender interprets the results of the occlusion query.</param>
        public void BeginConditionalRender(uint id, uint mode)
        {
            GetDelegateFor<glBeginConditionalRender>()(id, mode);
        }

        /// <summary>
        /// End conditional rendering.
        /// </summary>
        public void EndConditionalRender()
        {
            GetDelegateFor<glEndConditionalRender>()();
        }

        #endregion

        #region ARB_color_buffer_float

        private delegate void glClampColor(uint target, uint clamp);

        public const uint GL_RGBA_FLOAT_MODE = 0x8820;
        public const uint GL_CLAMP_VERTEX_COLOR = 0x891A;
        public const uint GL_CLAMP_FRAGMENT_COLOR = 0x891B;
        public const uint GL_CLAMP_READ_COLOR = 0x891C;
        public const uint GL_FIXED_ONLY = 0x891D;
        public const uint WGL_TYPE_RGBA_FLOAT = 0x21A0;
        public const uint GLX_RGBA_FLOAT_TYPE = 0x20B9;
        public const uint GLX_RGBA_FLOAT_BIT = 0x00000004;

        /// <summary>
        /// Specify whether data read via glReadPixels should be clamped.
        /// </summary>
        /// <param name="target">Target for color clamping. target must be GL_CLAMP_READ_COLOR.</param>
        /// <param name="clamp">Specifies whether to apply color clamping. clamp must be GL_TRUE or GL_FALSE.</param>
        public void ClampColor(uint target, uint clamp)
        {
            GetDelegateFor<glClampColor>()(target, clamp);
        }

        #endregion

        #region ARB_depth_buffer_float

        //  NV_depth_buffer_float seems to be implemented as ARB_depth_buffer_float.

        public const uint GL_DEPTH_COMPONENT32F = 0x8CAC;
        public const uint GL_DEPTH32F_STENCIL8 = 0x8CAD;
        public const uint GL_FLOAT_32_UNSIGNED_INT_24_8_REV = 0x8DAD;

        #endregion

        #region GL_ARB_texture_float

        //  Constants
        public const uint GL_TEXTURE_RED_TYPE_ARB = 0x8C10;
        public const uint GL_TEXTURE_GREEN_TYPE_ARB = 0x8C11;
        public const uint GL_TEXTURE_BLUE_TYPE_ARB = 0x8C12;
        public const uint GL_TEXTURE_ALPHA_TYPE_ARB = 0x8C13;
        public const uint GL_TEXTURE_LUMINANCE_TYPE_ARB = 0x8C14;
        public const uint GL_TEXTURE_INTENSITY_TYPE_ARB = 0x8C15;
        public const uint GL_TEXTURE_DEPTH_TYPE_ARB = 0x8C16;
        public const uint GL_UNSIGNED_NORMALIZED_ARB = 0x8C17;
        public const uint GL_RGBA32F_ARB = 0x8814;
        public const uint GL_RGB32F_ARB = 0x8815;
        public const uint GL_ALPHA32F_ARB = 0x8816;
        public const uint GL_INTENSITY32F_ARB = 0x8817;
        public const uint GL_LUMINANCE32F_ARB = 0x8818;
        public const uint GL_LUMINANCE_ALPHA32F_ARB = 0x8819;
        public const uint GL_RGBA16F_ARB = 0x881A;
        public const uint GL_RGB16F_ARB = 0x881B;
        public const uint GL_ALPHA16F_ARB = 0x881C;
        public const uint GL_INTENSITY16F_ARB = 0x881D;
        public const uint GL_LUMINANCE16F_ARB = 0x881E;
        public const uint GL_LUMINANCE_ALPHA16F_ARB = 0x881F;

        #endregion

        #region EXT_packed_float

        public const uint GL_R11F_G11F_B10F = 0x8C3A;
        public const uint GL_UNSIGNED_INT_10F_11F_11F_REV = 0x8C3B;
        public const uint GL_RGBA_SIGNED_COMPONENTS = 0x8C3C;
        public const uint WGL_TYPE_RGBA_UNSIGNED_FLOAT = 0x20A8;
        public const uint GLX_RGBA_UNSIGNED_FLOAT_TYPE = 0x20B1;
        public const uint GLX_RGBA_UNSIGNED_FLOAT_BIT = 0x00000008;

        #endregion

        #region EXT_texture_shared_exponent

        public const uint GL_RGB9_E5 = 0x8C3D;
        public const uint GL_UNSIGNED_INT_5_9_9_9_REV = 0x8C3E;
        public const uint GL_TEXTURE_SHARED_SIZE = 0x8C3F;

        #endregion

        #region EXT_texture_integer

        private delegate void glClearColorIi(int r, int g, int b, int a);

        private delegate void glClearColorIui(uint r, uint g, uint b, uint a);

        private delegate void glTexParameterIiv(uint target, uint pname, int[] @params);

        private delegate void glTexParameterIuiv(uint target, uint pname, uint[] @params);

        private delegate void glGetTexParameterIiv(uint target, uint pname, int[] @params);

        private delegate void glGetTexParameterIuiv(uint target, uint pname, uint[] @params);

        /// <summary>
        /// Specify clear values for the color buffers.
        /// </summary>
        /// <param name="r">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="g">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="b">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="a">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        public void ClearColorI(int r, int g, int b, int a)
        {
            GetDelegateFor<glClearColorIi>()(r, g, b, a);

        }

        /// <summary>
        /// Specify clear values for the color buffers.
        /// </summary>
        /// <param name="r">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="g">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="b">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        /// <param name="a">Specify the red, green, blue, and alpha values used when the color buffers are cleared. The initial values are all 0.</param>
        public void ClearColorI(uint r, uint g, uint b, uint a)
        {
            GetDelegateFor<glClearColorIui>()(r, g, b, a);
        }

        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="target">Specifies the target texture, which must be either GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_3D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_RECTANGLE, or GL_TEXTURE_CUBE_MAP.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. pname can be one of the following: GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R.</param>
        /// <param name="params">Specifies a pointer to an array where the value or values of pname are stored.</param>
        public void TexParameterI(uint target, uint pname, int[] @params)
        {
            GetDelegateFor<glTexParameterIiv>()(target, pname, @params);
        }

        /// <summary>
        /// Set texture parameters.
        /// </summary>
        /// <param name="target">Specifies the target texture, which must be either GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_3D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_RECTANGLE, or GL_TEXTURE_CUBE_MAP.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. pname can be one of the following: GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, or GL_TEXTURE_WRAP_R.</param>
        /// <param name="params">Specifies a pointer to an array where the value or values of pname are stored.</param>
        public void TexParameterI(uint target, uint pname, uint[] @params)
        {
            GetDelegateFor<glTexParameterIuiv>()(target, pname, @params);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture. GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_3D, GL_TEXTURE_RECTANGLE, and GL_TEXTURE_CUBE_MAP are accepted.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void GetTexParameterI(uint target, uint pname, int[] @params)
        {
            GetDelegateFor<glGetTexParameterIiv>()(target, pname, @params);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture. GL_TEXTURE_1D, GL_TEXTURE_2D, GL_TEXTURE_1D_ARRAY, GL_TEXTURE_2D_ARRAY, GL_TEXTURE_3D, GL_TEXTURE_RECTANGLE, and GL_TEXTURE_CUBE_MAP are accepted.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter. GL_TEXTURE_BASE_LEVEL, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, GL_TEXTURE_COMPARE_FUNC, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MAX_LEVEL, GL_TEXTURE_MAX_LOD, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_SWIZZLE_R, GL_TEXTURE_SWIZZLE_G, GL_TEXTURE_SWIZZLE_B, GL_TEXTURE_SWIZZLE_A, GL_TEXTURE_SWIZZLE_RGBA, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, and GL_TEXTURE_WRAP_R are accepted.</param>
        /// <param name="params">Returns the texture parameters.</param>
        public void GetTexParameterI(uint target, uint pname, uint[] @params)
        {
            GetDelegateFor<glGetTexParameterIuiv>()(target, pname, @params);
        }

        public const uint GL_RGBA_INTEGER_MODE = 0x8D9E;
        public const uint GL_RGBA32UI = 0x8D70;
        public const uint GL_RGB32UI = 0x8D71;
        public const uint GL_ALPHA32UI = 0x8D72;
        public const uint GL_INTENSITY32UI = 0x8D73;
        public const uint GL_LUMINANCE32UI = 0x8D74;
        public const uint GL_LUMINANCE_ALPHA32UI = 0x8D75;
        public const uint GL_RGBA16UI = 0x8D76;
        public const uint GL_RGB16UI = 0x8D77;
        public const uint GL_ALPHA16UI = 0x8D78;
        public const uint GL_INTENSITY16UI = 0x8D79;
        public const uint GL_LUMINANCE16UI = 0x8D7A;
        public const uint GL_LUMINANCE_ALPHA16UI = 0x8D7B;
        public const uint GL_RGBA8UI = 0x8D7C;
        public const uint GL_RGB8UI = 0x8D7D;
        public const uint GL_ALPHA8UI = 0x8D7E;
        public const uint GL_INTENSITY8UI = 0x8D7F;
        public const uint GL_LUMINANCE8UI = 0x8D80;
        public const uint GL_LUMINANCE_ALPHA8UI = 0x8D81;
        public const uint GL_RGBA32I = 0x8D82;
        public const uint GL_RGB32I = 0x8D83;
        public const uint GL_ALPHA32I = 0x8D84;
        public const uint GL_INTENSITY32I = 0x8D85;
        public const uint GL_LUMINANCE32I = 0x8D86;
        public const uint GL_LUMINANCE_ALPHA32I = 0x8D87;
        public const uint GL_RGBA16I = 0x8D88;
        public const uint GL_RGB16I = 0x8D89;
        public const uint GL_ALPHA16I = 0x8D8A;
        public const uint GL_INTENSITY16I = 0x8D8B;
        public const uint GL_LUMINANCE16I = 0x8D8C;
        public const uint GL_LUMINANCE_ALPHA16I = 0x8D8D;
        public const uint GL_RGBA8I = 0x8D8E;
        public const uint GL_RGB8I = 0x8D8F;
        public const uint GL_ALPHA8I = 0x8D90;
        public const uint GL_INTENSITY8I = 0x8D91;
        public const uint GL_LUMINANCE8I = 0x8D92;
        public const uint GL_LUMINANCE_ALPHA8I = 0x8D93;
        public const uint GL_RED_INTEGER = 0x8D94;
        public const uint GL_GREEN_INTEGER = 0x8D95;
        public const uint GL_BLUE_INTEGER = 0x8D96;
        public const uint GL_ALPHA_INTEGER = 0x8D97;
        public const uint GL_RGB_INTEGER = 0x8D98;
        public const uint GL_RGBA_INTEGER = 0x8D99;
        public const uint GL_BGR_INTEGER = 0x8D9A;
        public const uint GL_BGRA_INTEGER = 0x8D9B;
        public const uint GL_LUMINANCE_INTEGER = 0x8D9C;
        public const uint GL_LUMINANCE_ALPHA_INTEGER = 0x8D9D;

        #endregion

        #region EXT_texture_array

        public const uint GL_TEXTURE_1D_ARRAY = 0x8C18;
        public const uint GL_TEXTURE_2D_ARRAY = 0x8C1A;
        public const uint GL_PROXY_TEXTURE_2D_ARRAY = 0x8C1B;
        public const uint GL_PROXY_TEXTURE_1D_ARRAY = 0x8C19;
        public const uint GL_TEXTURE_BINDING_1D_ARRAY = 0x8C1C;
        public const uint GL_TEXTURE_BINDING_2D_ARRAY = 0x8C1D;
        public const uint GL_MAX_ARRAY_TEXTURE_LAYERS = 0x88FF;
        public const uint GL_COMPARE_REF_DEPTH_TO_TEXTURE = 0x884E;
        public const uint GL_COMPRESSED_RGB_S3TC_DXT1 = 0x83F0;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT1 = 0x83F1;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT3 = 0x83F2;
        public const uint GL_COMPRESSED_RGBA_S3TC_DXT5 = 0x83F3;
        public const uint GL_SAMPLER_1D_ARRAY = 0x8DC0;
        public const uint GL_SAMPLER_2D_ARRAY = 0x8DC1;
        public const uint GL_SAMPLER_1D_ARRAY_SHADOW = 0x8DC3;
        public const uint GL_SAMPLER_2D_ARRAY_SHADOW = 0x8DC4;

        #endregion

        #region EXT_draw_buffers2

        //  TODO: #86

        #endregion

        #region

        public const uint GL_COMPRESSED_RED_RGTC1 = 0x8DBB;
        public const uint GL_COMPRESSED_SIGNED_RED_RGTC1 = 0x8DBC;
        public const uint GL_COMPRESSED_RED_GREEN_RGTC2 = 0x8DBD;
        public const uint GL_COMPRESSED_SIGNED_RED_GREEN_RGTC2 = 0x8DBE;

        #endregion

        #region EXT_transform_feedback

        private delegate void glBindBufferRange(uint target, uint index, uint buffer, int offset, int size);
        private delegate void glBeginTransformFeedback(uint primitiveMode);
        private delegate void glEndTransformFeedback();
        private delegate void glTransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode);
        private delegate void glGetTransformFeedbackVarying(
            uint program, uint index, int bufSize, out int length, out int size, out uint type, StringBuilder name);


        //  TODO: defined in the extension but not in the core documentation...
        //  Not actually used in the core spec.
        //  private delegate void glBindBufferOffsetEXT(uint target, uint index, uint buffer, int offset);
        //  private delegate void glBindBufferBaseEXT(uint target, uint index, uint buffer);
        //  private delegate void GetIntegerIndexedvEXT(uint param, uint index, int* values);
        //  private delegate void GetBooleanIndexedvEXT(uint param, uint index, boolean* values);

        public const uint GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START                = 0x8C84;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE                 = 0x8C85;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING              = 0x8C8F;
        public const uint GL_INTERLEAVED_ATTRIBS                            = 0x8C8C;
        public const uint GL_SEPARATE_ATTRIBS                               = 0x8C8D;
        public const uint GL_PRIMITIVES_GENERATED                           = 0x8C87;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN          = 0x8C88;
        public const uint GL_RASTERIZER_DISCARD                             = 0x8C89;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS  = 0x8C8A;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS        = 0x8C8B;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS     = 0x8C80;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS                    = 0x8C83;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE                 = 0x8C7F;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH = 0x8C76;

        /// <summary>
        /// Bind a range within a buffer object to an indexed buffer target.
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. target must be either GL_TRANSFORM_FEEDBACK_BUFFER or GL_UNIFORM_BUFFER.</param>
        /// <param name="index">Specify the index of the binding point within the array specified by target.</param>
        /// <param name="buffer">The name of a buffer object to bind to the specified binding point.</param>
        /// <param name="offset">The starting offset in basic machine units into the buffer object buffer.</param>
        /// <param name="size">The amount of data in machine units that can be read from the buffet object while used as an indexed target.</param>
        public void BindBufferRange(uint target, uint index, uint buffer, int offset, int size)
        {
            GetDelegateFor<glBindBufferRange>()(target, index, buffer, offset, size);
        }

        /// <summary>
        /// Start transform feedback operation.
        /// </summary>
        /// <param name="primitiveMode">Specify the output type of the primitives that will be recorded into the buffer objects that are bound for transform feedback.</param>
        public void BeginTransformFeedback(uint primitiveMode)
        {
            GetDelegateFor<glBeginTransformFeedback>()(primitiveMode);
        }

        /// <summary>
        /// Ends transform feedback operation.
        /// </summary>
        public void EndTransformFeedback()
        {
            GetDelegateFor<glEndTransformFeedback>()();
        }

        /// <summary>
        /// Specify values to record in transform feedback buffers.
        /// </summary>
        /// <param name="program">The name of the target program object.</param>
        /// <param name="count">The number of varying variables used for transform feedback.</param>
        /// <param name="varyings">An array of count zero-terminated strings specifying the names of the varying variables to use for transform feedback.</param>
        /// <param name="bufferMode">Identifies the mode used to capture the varying variables when transform feedback is active. bufferMode must be GL_INTERLEAVED_ATTRIBS or GL_SEPARATE_ATTRIBS.</param>
        public void TransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode)
        {
            GetDelegateFor<glTransformFeedbackVaryings>()(program, count, varyings, bufferMode);
        }

        /// <summary>
        /// Retrieve information about varying variables selected for transform feedback.
        /// </summary>
        /// <param name="program">The name of the target program object.</param>
        /// <param name="index">The index of the varying variable whose information to retrieve.</param>
        /// <param name="bufSize">The maximum number of characters, including the null terminator, that may be written into name.</param>
        /// <param name="length">The address of a variable which will receive the number of characters written into name, excluding the null-terminator. If length is NULL no length is returned.</param>
        /// <param name="size">The address of a variable that will receive the size of the varying.</param>
        /// <param name="type">The address of a variable that will recieve the type of the varying.</param>
        /// <param name="name">The address of a buffer into which will be written the name of the varying.</param>
        public void GetTransformFeedbackVarying(uint program, uint index, int bufSize, out int length, out int size,
                                                out uint type, out string name)
        {
            var builder = new StringBuilder(bufSize);
            GetDelegateFor<glGetTransformFeedbackVarying>()(program, index, bufSize, out length, out size,
                                                            out type, builder);
            name = builder.ToString();
        }
        
        #endregion

        #region EXT_framebuffer_sRGB

        public const uint GLX_FRAMEBUFFER_SRGB_CAPABLE_EXT = 0x20B2;
        public const uint WGL_FRAMEBUFFER_SRGB_CAPABLE_EXT = 0x20A9;
        public const uint GL_FRAMEBUFFER_SRGB = 0x8DB9;
        public const uint GL_FRAMEBUFFER_SRGB_CAPABLE = 0x8DBA;

        #endregion

        #region ARB_map_buffer_range

        private delegate IntPtr glMapBufferRange(uint target, int offset, int length, uint access);
        private delegate void glFlushMappedBufferRange(uint target, int offset, int length);
        
        public const uint GL_MAP_READ_BIT                = 0x0001 ;
        public const uint GL_MAP_WRITE_BIT               = 0x0002 ;
        public const uint GL_MAP_INVALIDATE_RANGE_BIT    = 0x0004 ;
        public const uint GL_MAP_INVALIDATE_BUFFER_BIT   = 0x0008 ;
        public const uint GL_MAP_FLUSH_EXPLICIT_BIT      = 0x0010 ;
        public const uint GL_MAP_UNSYNCHRONIZED_BIT = 0x0020;


        /// <summary>
        /// Map all or part of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="target">Specifies the target to which the buffer object is bound for glMapBufferRange.</param>
        /// <param name="offset">Specifies the starting offset within the buffer of the range to be mapped.</param>
        /// <param name="length">Specifies the length of the range to be mapped.</param>
        /// <param name="access">Specifies a combination of access flags indicating the desired access to the mapped range.</param>
        /// <returns>If an error occurs, a NULL pointer is returned.
        ///  If no error occurs, the returned pointer will reflect an allocation aligned to the value of GL_MIN_MAP_BUFFER_ALIGNMENT basic machine units. Subtracting offset from this returned pointer will always produce a multiple of the value of GL_MIN_MAP_BUFFER_ALIGNMENT.</returns>
        public IntPtr MapBufferRange(uint target, int offset, int length, uint access)
        {
            return GetDelegateFor<glMapBufferRange>()(target, offset, length, access);
        }

        /// <summary>
        /// Indicate modifications to a range of a mapped buffer
        /// </summary>
        /// <param name="target">Specifies the target to which the buffer object is bound for glMapBufferRange.</param>
        /// <param name="offset">Specifies the start of the buffer subrange, in basic machine units.</param>
        /// <param name="length">Specifies the length of the buffer subrange, in basic machine units.</param>
        public void FlushMappedBufferRange(uint target, int offset, int length)
        {
            GetDelegateFor<glFlushMappedBufferRange>()(target, offset, length);
        }

        #endregion
    }

// ReSharper restore InconsistentNaming
}
