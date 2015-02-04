using System;

namespace SharpGL
{
// ReSharper disable InconsistentNaming

    public partial class OpenGL
    {
        #region ARB_shader_bit_encoding

        //  No new tokens or functions.

        #endregion

        #region ARB_blend_func_extended
        
        private delegate void glBindFragDataLocationIndexed(uint program, uint colorNumber, uint index, string name);
        private delegate int glGetFragDataIndex(uint program, string name);

        /// <summary>
        /// Bind a user-defined varying out variable to a fragment shader color number and index.
        /// </summary>
        /// <param name="program">The name of the program containing varying out variable whose binding to modify.</param>
        /// <param name="colorNumber">The color number to bind the user-defined varying out variable to.</param>
        /// <param name="index">The index of the color input to bind the user-defined varying out variable to.</param>
        /// <param name="name">The name of the user-defined varying out variable whose binding to modify.</param>
        public void BindFragDataLocationIndexed(uint program, uint colorNumber, uint index, string name)
        {
            GetDelegateFor<glBindFragDataLocationIndexed>()(program, colorNumber, index, name);
        }

        /// <summary>
        /// Query the bindings of color indices to user-defined varying out variables.
        /// </summary>
        /// <param name="program">The name of the program containing varying out variable whose binding to query.</param>
        /// <param name="name">The name of the user-defined varying out variable whose index to query.</param>
        /// <returns>Returns the index of the fragment color to which the variable name was bound when the program object program was last linked. If name is not a varying out variable of program, or if an error occurs, -1 will be returned.</returns>
        public int GetFragDataIndex(uint program, string name)
        {
            return GetDelegateFor<glGetFragDataIndex>()(program, name);
        }


        #endregion

        #region ARB_explicit_attrib_location

        //  No new tokens or functions.

        #endregion

        #region ARB_occlusion_query2

        public const uint GL_ANY_SAMPLES_PASSED = 0x8C2F;

        #endregion

        #region ARB_sampler_objects
        
        private delegate void glGenSamplers(int count, uint[] samplers);
        private delegate void glDeleteSamplers(int count, uint[] samplers);
        private delegate bool glIsSampler(uint sampler);
        private delegate void glBindSampler(uint unit, uint sampler);
        private delegate void glSamplerParameteri(uint sampler, uint pname, int param);
        private delegate void glSamplerParameterf(uint sampler, uint pname, float param);
        private delegate void glSamplerParameteriv(uint sampler, uint pname, int[] @params);
        private delegate void glSamplerParameterfv(uint sampler, uint pname, float[] @params);
        private delegate void glSamplerParameterIiv(uint sampler, uint pname, int[] @params);
        private delegate void glSamplerParameterIuiv(uint sampler, uint pname, uint[] @params);
        private delegate void glGetSamplerParameteriv(uint sampler, uint pname, int[] @params);
        private delegate void glGetSamplerParameterfv(uint sampler, uint pname, float[] @params);
        private delegate void glGetSamplerParameterIiv(uint sampler, uint pname, int[] @params);
        private delegate void glGetSamplerParameterIuiv(uint sampler, uint pname, uint[] @params);

        public const uint GL_SAMPLER_BINDING = 0x8919;

        /// <summary>
        /// Generate sampler object names.
        /// </summary>
        /// <param name="count">Specifies the number of sampler object names to generate.</param>
        /// <param name="samplers">Specifies an array in which the generated sampler object names are stored.</param>
        public void GenSamplers(int count, uint[] samplers)
        {
            GetDelegateFor<glGenSamplers>()(count, samplers);
        }

        /// <summary>
        /// Delete named sampler objects.
        /// </summary>
        /// <param name="count">Specifies the number of sampler objects to be deleted.</param>
        /// <param name="samplers">Specifies an array of sampler objects to be deleted.</param>
        public void DeleteSamplers(int count, uint[] samplers)
        {
            GetDelegateFor<glDeleteSamplers>()(count, samplers);
        }

        /// <summary>
        /// Determine if a name corresponds to a sampler object
        /// </summary>
        /// <param name="sampler">Specifies a value that may be the name of a sampler object.</param>
        public void IsSampler(uint sampler)
        {
            GetDelegateFor<glIsSampler>()(sampler);
        }

        /// <summary>
        /// Bind a named sampler to a texturing target.
        /// </summary>
        /// <param name="unit">Specifies the index of the texture unit to which the sampler is bound.</param>
        /// <param name="sampler">Specifies the name of a sampler.</param>
        public void BindSampler(uint unit, uint sampler)
        {
            GetDelegateFor<glBindSampler>()(unit, sampler);
        }

        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="param">For the scalar commands, specifies the value of pname.</param>
        public void SamplerParameter(uint sampler, uint pname, int param)
        {
            GetDelegateFor<glSamplerParameteri>()(sampler, pname, param);
        }

        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="param">For the scalar commands, specifies the value of pname.</param>
        public void SamplerParameter(uint sampler, uint pname, float param)
        {
            GetDelegateFor<glSamplerParameterf>()(sampler, pname, param);
        }

        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="params">For the vector commands (glSamplerParameter*v), specifies a pointer to an array where the value or values of pname are stored.</param>
        public void SamplerParameter(uint sampler, uint pname, int[] @params)
        {
            GetDelegateFor<glSamplerParameteriv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="params">For the vector commands (glSamplerParameter*v), specifies a pointer to an array where the value or values of pname are stored.</param>
        public void SamplerParameter(uint sampler, uint pname, float[] @params)
        {
            GetDelegateFor<glSamplerParameterfv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="params">For the vector commands (glSamplerParameter*v), specifies a pointer to an array where the value or values of pname are stored.</param>
        public void SamplerParameterI(uint sampler, uint pname, int[] @params)
        {
            GetDelegateFor<glSamplerParameterIiv>()(sampler, pname, @params);
        }
        
        /// <summary>
        /// Set sampler parameters.
        /// </summary>
        /// <param name="sampler">Specifies the sampler object whose parameter to modify.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. pname can be one of the following: GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MAG_FILTER, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS GL_TEXTURE_COMPARE_MODE, or GL_TEXTURE_COMPARE_FUNC.</param>
        /// <param name="params">For the vector commands (glSamplerParameter*v), specifies a pointer to an array where the value or values of pname are stored.</param>
        public void SamplerParameterI(uint sampler, uint pname, uint[] @params)
        {
            GetDelegateFor<glSamplerParameterIuiv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Return sampler parameter values.
        /// </summary>
        /// <param name="sampler">Specifies name of the sampler object from which to retrieve parameters.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, and GL_TEXTURE_COMPARE_FUNC are accepted.</param>
        /// <param name="params">Returns the sampler parameters.</param>
        public void GetSamplerParameter(uint sampler, uint pname, int[] @params)
        {
            GetDelegateFor<glGetSamplerParameteriv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Return sampler parameter values.
        /// </summary>
        /// <param name="sampler">Specifies name of the sampler object from which to retrieve parameters.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, and GL_TEXTURE_COMPARE_FUNC are accepted.</param>
        /// <param name="params">Returns the sampler parameters.</param>
        public void GetSamplerParameter(uint sampler, uint pname, float[] @params)
        {
            GetDelegateFor<glGetSamplerParameterfv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Return sampler parameter values.
        /// </summary>
        /// <param name="sampler">Specifies name of the sampler object from which to retrieve parameters.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, and GL_TEXTURE_COMPARE_FUNC are accepted.</param>
        /// <param name="params">Returns the sampler parameters.</param>
        public void GetSamplerParameterI(uint sampler, uint pname, int[] @params)
        {
            GetDelegateFor<glGetSamplerParameterIiv>()(sampler, pname, @params);
        }

        /// <summary>
        /// Return sampler parameter values.
        /// </summary>
        /// <param name="sampler">Specifies name of the sampler object from which to retrieve parameters.</param>
        /// <param name="pname">Specifies the symbolic name of a sampler parameter. GL_TEXTURE_MAG_FILTER, GL_TEXTURE_MIN_FILTER, GL_TEXTURE_MIN_LOD, GL_TEXTURE_MAX_LOD, GL_TEXTURE_LOD_BIAS, GL_TEXTURE_WRAP_S, GL_TEXTURE_WRAP_T, GL_TEXTURE_WRAP_R, GL_TEXTURE_BORDER_COLOR, GL_TEXTURE_COMPARE_MODE, and GL_TEXTURE_COMPARE_FUNC are accepted.</param>
        /// <param name="params">Returns the sampler parameters.</param>
        public void GetSamplerParameterI(uint sampler, uint pname, uint[] @params)
        {
            GetDelegateFor<glGetSamplerParameterIuiv>()(sampler, pname, @params);
        }

        #endregion

        #region ARB_texture_rgb10_a2ui

        public const uint GL_RGB10_A2UI = 0x906F;
        
        #endregion

        #region ARB_texture_swizzle

        public const uint GL_TEXTURE_SWIZZLE_R = 0x8E42;
        public const uint GL_TEXTURE_SWIZZLE_G = 0x8E43;
        public const uint GL_TEXTURE_SWIZZLE_B = 0x8E44;
        public const uint GL_TEXTURE_SWIZZLE_A = 0x8E45;
        public const uint GL_TEXTURE_SWIZZLE_RGBA = 0x8E46;

        #endregion

        #region ARB_timer_query

        private delegate void glQueryCounter(uint id, uint target);
        private delegate void glGetQueryObjecti64v(uint id, uint pname, Int64[] @params);
        private delegate void glGetQueryObjectui64v(uint id, uint pname, UInt64[] @params);
        
        public const uint GL_TIME_ELAPSED = 0x88BF;
        public const uint GL_TIMESTAMP = 0x8E28;

        /// <summary>
        /// Record the GL time into a query object after all previous commands have reached the GL server but have not yet necessarily executed.
        /// </summary>
        /// <param name="id">Specify the name of a query object into which to record the GL time.</param>
        /// <param name="target">Specify the counter to query. target must be GL_TIMESTAMP.</param>
        public void QueryCounter(uint id, uint target)
        {
            GetDelegateFor<glQueryCounter>()(id, target);
        }

        /// <summary>
        /// Return parameters of a query object.
        /// </summary>
        /// <param name="id">Specifies the name of a query object.</param>
        /// <param name="pname">Specifies the symbolic name of a query object parameter. Accepted values are GL_QUERY_RESULT or GL_QUERY_RESULT_AVAILABLE.</param>
        /// <param name="params">If a buffer is bound to the GL_QUERY_RESULT_BUFFER target, then params is treated as an offset to a location within that buffer's data store to receive the result of the query. If no buffer is bound to GL_QUERY_RESULT_BUFFER, then params is treated as an address in client memory of a variable to receive the resulting data.</param>
        public void GetQueryObject(uint id, uint pname, Int64[] @params)
        {
            GetDelegateFor<glGetQueryObjecti64v>()(id, pname, @params);
        }

        /// <summary>
        /// Return parameters of a query object.
        /// </summary>
        /// <param name="id">Specifies the name of a query object.</param>
        /// <param name="pname">Specifies the symbolic name of a query object parameter. Accepted values are GL_QUERY_RESULT or GL_QUERY_RESULT_AVAILABLE.</param>
        /// <param name="params">If a buffer is bound to the GL_QUERY_RESULT_BUFFER target, then params is treated as an offset to a location within that buffer's data store to receive the result of the query. If no buffer is bound to GL_QUERY_RESULT_BUFFER, then params is treated as an address in client memory of a variable to receive the resulting data.</param>
        public void GetQueryObject(uint id, uint pname, UInt64[] @params)
        {
            GetDelegateFor<glGetQueryObjectui64v>()(id, pname, @params);
        }

        #endregion

        #region ARB_instanced_arrays
        
        private delegate void glVertexAttribDivisor(uint index, uint divisor);

        //  When EXT_direct_state_access is present:
        private delegate void glVertexArrayVertexAttribDivisorEXT(uint vaobj, uint index, uint divisor);

        public const uint GL_VERTEX_ATTRIB_ARRAY_DIVISOR = 0x88FE;

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance during instanced rendering.
        /// </summary>
        /// <param name="index">Specify the index of the generic vertex attribute.</param>
        /// <param name="divisor">Specify the number of instances that will pass between updates of the generic attribute at slot index.</param>
        public void VertexAttribDivisorARB(uint index, uint divisor)
        {
            GetDelegateFor<glVertexAttribDivisor>()(index, divisor);
        }

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance during instanced rendering.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="index">Specify the index of the generic vertex attribute.</param>
        /// <param name="divisor">Specify the number of instances that will pass between updates of the generic attribute at slot index.</param>
        public void VertexArrayVertexAttribDivisorEXT(uint vaobj, uint index, uint divisor)
        {
            GetDelegateFor<glVertexArrayVertexAttribDivisorEXT>()(vaobj, index, divisor);
        }

        #endregion

        #region ARB_vertex_type_2_10_10_10_rev

        public const uint GL_UNSIGNED_INT_2_10_10_10 = 0x8368;
        public const uint GL_INT_2_10_10_10 = 0x8D9F;

        /* As far as I can tell, these functions never made it into the core.
         * 
         * void VertexP2ui(enum type, uint value);
    void VertexP3ui(enum type, uint value);
    void VertexP4ui(enum type, uint value);
    void VertexP2uiv(enum type, const uint *value);
    void VertexP3uiv(enum type, const uint *value);
    void VertexP4uiv(enum type, const uint *value);
    void TexCoordP1ui(enum type, uint coords);
    void TexCoordP2ui(enum type, uint coords);
    void TexCoordP3ui(enum type, uint coords);
    void TexCoordP4ui(enum type, uint coords);
    void TexCoordP1uiv(enum type, const uint *coords);
    void TexCoordP2uiv(enum type, const uint *coords);
    void TexCoordP3uiv(enum type, const uint *coords);
    void TexCoordP4uiv(enum type, const uint *coords);
    void MultiTexCoordP1ui(enum texture, enum type, uint coords);
    void MultiTexCoordP2ui(enum texture, enum type, uint coords);
    void MultiTexCoordP3ui(enum texture, enum type, uint coords);
    void MultiTexCoordP4ui(enum texture, enum type, uint coords);
    void MultiTexCoordP1uiv(enum texture, enum type, const uint *coords);
    void MultiTexCoordP2uiv(enum texture, enum type, const uint *coords);
    void MultiTexCoordP3uiv(enum texture, enum type, const uint *coords);
    void MultiTexCoordP4uiv(enum texture, enum type, const uint *coords);
    void NormalP3ui(enum type, uint coords);
    void NormalP3uiv(enum type, const uint *coords);
    void ColorP3ui(enum type, uint color);
    void ColorP4ui(enum type, uint color);
    void ColorP3uiv(enum type, const uint *color);
    void ColorP4uiv(enum type, const uint *color);
    void SecondaryColorP3ui(enum type, uint color);
    void SecondaryColorP3uiv(enum type, const uint *color);
    void VertexAttribP1ui(uint index, enum type, boolean normalized,
                          uint value);
    void VertexAttribP2ui(uint index, enum type, boolean normalized,
                          uint value);
    void VertexAttribP3ui(uint index, enum type, boolean normalized,
                          uint value);
    void VertexAttribP4ui(uint index, enum type, boolean normalized,
                          uint value);
    void VertexAttribP1uiv(uint index, enum type, boolean normalized,
                          const uint *value);
    void VertexAttribP2uiv(uint index, enum type, boolean normalized,
                          const uint *value);
    void VertexAttribP3uiv(uint index, enum type, boolean normalized,
                          const uint *value);
    void VertexAttribP4uiv(uint index, enum type, boolean normalized,
                          const uint *value);
         * */

        #endregion
    }

// ReSharper restore InconsistentNaming
}
