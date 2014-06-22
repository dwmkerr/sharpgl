using GlmNet;
using SharpGL;
using SharpGL.SceneGraph.JOG;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpGLHitTestSample
{
    public class HitTestProgram : ShaderProgramJOG
    {
        #region fields
        List<NMHTBufferGroup> _bufferGroups = new List<NMHTBufferGroup>();
        List<Action<OpenGL>> _changedUniforms = new List<Action<OpenGL>>();

        mat4 _projection, _modelview;


        #endregion fields

        #region properties
        public List<NMHTBufferGroup> BufferGroups
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
                    "HTColorId"
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
                };
            }
        }

        private static Dictionary<ShaderTypes, string> ShaderTypeAndCode
        {
            get
            {
                var stc = new Dictionary<ShaderTypes, string>();
                
                var assembly = Assembly.GetExecutingAssembly();
                stc.Add(ShaderTypes.GL_VERTEX_SHADER, SharpGL.SceneGraph.JOG.ManifestResourceLoader.LoadTextFile("ShaderResources.HitTest.vert", assembly));
                stc.Add(ShaderTypes.GL_FRAGMENT_SHADER, SharpGL.SceneGraph.JOG.ManifestResourceLoader.LoadTextFile("ShaderResources.HitTest.frag", assembly));

                return stc;
            }   
        }

        public List<Action<OpenGL>> ChangedUniforms
        {
            get { return _changedUniforms; }
            set { _changedUniforms = value; }
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
        
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public HitTestProgram(OpenGL gl)
            :base(gl, ShaderTypeAndCode, AttributeNames, UniformNames)
        {
        }
        #endregion constructors

        public void AddBufferGroup(NMHTBufferGroup group)
        {
            BufferGroups.Add(group);
        }

        public void BindAll(OpenGL gl)
        {
            //return;
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
                    group.BindHTVAO(gl);

                    // Use draw elements if an index buffer is defined. Else use draw arrays.
                    if (group.IndicesCount > 0)
                        gl.DrawElements(OpenGL.GL_TRIANGLES, group.IndicesCount, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero);
                }
            });
        }
        private void ApplyModelViewProjection(OpenGL gl)
        {
            gl.UniformMatrix4(Uniforms["ModelviewProjection"], 1, false, (Modelview.Transpose() * Projection.Transpose()).to_array());
        }
    }
}
