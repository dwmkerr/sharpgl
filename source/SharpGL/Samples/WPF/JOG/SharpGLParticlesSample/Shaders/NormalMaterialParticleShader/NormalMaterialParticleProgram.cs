using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.JOG;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpGLParticlesSample
{
    public class NormalMaterialParticleProgram : ShaderProgramJOG
    {
        #region fields
        List<NMPBufferGroup> _bufferGroups = new List<NMPBufferGroup>();
        List<Action<OpenGL>> _changedUniforms = new List<Action<OpenGL>>();

        mat4 _projection, _modelview;
        mat3 _normalMatrix;
        vec3 _lightPosition;


        #endregion fields

        #region properties
        public List<NMPBufferGroup> BufferGroups
        {
            get { return _bufferGroups; }
            set { _bufferGroups = value; }
        }

        private static string[] AttributeNames
        {
            get
            {
                return new string[]
                {
                    "Position",
                    "Normal",
                    "TCol1",
                    "TCol2",
                    "TCol3",
                    "TCol4",
                    "AmbientMaterial",
                    "DiffuseMaterial",
                    "SpecularMaterial",
                    "ShininessValue"
                };
            }
        }

        private static string[] UniformNames
        {
            get
            {
                return new string[] 
                {
                    "ModelviewProjection",
                    "NormalMatrix",
                    "LightPosition"
                };
            }
        }

        private static Dictionary<ShaderTypes, string> ShaderTypeAndCode
        {
            get
            {
                var stc = new Dictionary<ShaderTypes, string>();
                
                var assembly = Assembly.GetExecutingAssembly();
                stc.Add(ShaderTypes.GL_VERTEX_SHADER, SharpGL.SceneGraph.JOG.ManifestResourceLoader.LoadTextFile("ShaderResources.NormalMaterialParticle.vert", assembly));
                stc.Add(ShaderTypes.GL_FRAGMENT_SHADER, SharpGL.SceneGraph.JOG.ManifestResourceLoader.LoadTextFile("ShaderResources.NormalMaterialParticle.frag", assembly));

                return stc;
            }   
        }



        public List<Action<OpenGL>> ChangedUniforms
        {
            get { return _changedUniforms; }
            set { _changedUniforms = value; }
        }
        public mat3 NormalMatrix
        {
            get { return _normalMatrix; }
            set 
            {
                if (NormalMatrix.Equals(value))
                    return;

                ChangedUniforms.Add(ApplyNormalMatrix);
                _normalMatrix = value; 
            }
        }

        public mat4 Projection
        {
            get { return _projection; }
            set
            {
                //if (Projection.Equals(value))
                //    return;

                ChangedUniforms.Add(ApplyModelViewProjection);
                _projection = value;
            }
        }

        public mat4 Modelview
        {
            get { return _modelview; }
            set
            {
                if (Modelview.Equals(value))
                    return;

                ChangedUniforms.Add(ApplyModelViewProjection);
                _modelview = value;
            }
        }
        public vec3 LightPosition
        {
            get { return _lightPosition; }
            set
            {
                if (LightPosition.Equals(value))
                    return;

                ChangedUniforms.Add(ApplyLightPosition);
                _lightPosition = value;
            }
        }
        
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public NormalMaterialParticleProgram(OpenGL gl)
            :base(gl, ShaderTypeAndCode, AttributeNames, UniformNames)
        {
        }
        #endregion constructors

        public void AddBufferGroup(NMPBufferGroup group)
        {
            BufferGroups.Add(group);
        }

        public void BindAll(OpenGL gl)
        {
            UseProgram(gl, () =>
            {
                // Update uniforms.
                foreach (var action in ChangedUniforms)
                {
                    action.Invoke(gl);
                }
                ChangedUniforms.Clear();

                foreach (var group in BufferGroups)
                {
                    group.BindVAO(gl); 
                    gl.DrawElementsInstanced(OpenGL.GL_TRIANGLES, group.IndicesCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero, group.ParticleCount);
                
                }
            });
        }
        private void ApplyModelViewProjection(OpenGL gl)
        {
            gl.UniformMatrix4(Uniforms["ModelviewProjection"], 1, false, (Modelview.Transpose() * Projection.Transpose()).to_array());
        }
        private void ApplyNormalMatrix(OpenGL gl)
        {
            gl.UniformMatrix3(Uniforms["NormalMatrix"], 1, false, NormalMatrix.to_array());
        }
        private void ApplyLightPosition(OpenGL gl)
        {
            gl.Uniform3(Uniforms["LightPosition"], LightPosition.x, LightPosition.y, LightPosition.z);
        }
    }
}
