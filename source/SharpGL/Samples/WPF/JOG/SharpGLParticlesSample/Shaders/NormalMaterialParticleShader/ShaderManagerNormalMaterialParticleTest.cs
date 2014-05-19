using GlmNet;
using SharpGL;
using SharpGLHelper;
using SharpGLHelper.Buffers;
using SharpGLHelper.Common;
using SharpGLHelper.OGLOverloads;
using SharpGLHelper.SceneElements;
using SharpGLTest.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGLTest.Shaders
{
    public class ShaderManagerNormalMaterialParticleTest
    {

        #region fields
        vec3 _lightPosition = new vec3();
        #endregion fields

        #region properties

        protected Dictionary<uint, string> AttributeLocations
        {
            get
            {
                var dic = new Dictionary<uint, string>();

                dic.Add(0, "Position");
                dic.Add(1, "Normal");
                dic.Add(2, "TCol1");
                dic.Add(3, "TCol2");
                dic.Add(4, "TCol3");
                dic.Add(5, "TCol4");
                dic.Add(6, "AmbientMaterial");
                dic.Add(7, "DiffuseMaterial");
                dic.Add(8, "SpecularMaterial");
                dic.Add(9, "ShininessValue");

                return dic;
            }
        }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public ShaderManagerNormalMaterialParticleTest(OpenGL gl)
        {
            init(gl);
        }
        #endregion constructors

        MyTrefoilKnot knot;
        // List of indices in the generated buffers 
        // and ultimately the buffer IDs.
        uint ibo = 10;
        uint vert = 0;
        uint norm = 1;
        uint tCol1 = 2;
        uint tCol2 = 3;
        uint tCol3 = 4;
        uint tCol4 = 5;
        uint amb = 6;
        uint diff = 7;
        uint spec = 8;
        uint shini = 9;


        uint iboPos = 10;
        uint vertPos = 0;
        uint normPos = 1;
        uint tCol1Pos = 2;
        uint tCol2Pos = 3;
        uint tCol3Pos = 4;
        uint tCol4Pos = 5;
        uint ambPos = 6;
        uint diffPos = 7;
        uint specPos = 8;
        uint shiniPos = 9;

        // Uniform positions
        int lightPos = 0;
        int modelView = 1;
        int normMatrix = 2;
        int projection = 3;

        // Shader program;
        uint programId;

        public void init(OpenGL gl)
        {
	        // Create vert and frag NormalMaterialParticle shader.
	        //scene::vertShader = shader(OpenGL.GL_VERTEX_SHADER, &NORMAL_MATERIAL_PARTICLE_VERT);
            var vertexShaderSource = "ShaderResources.NormalMaterialParticle.vert";
            var fragmentShaderSource = "ShaderResources.NormalMaterialParticle.frag";
            var executingAssembly = Assembly.GetExecutingAssembly();
            var autoAttachAssemblyName = true;
            var NORMAL_MATERIAL_PARTICLE_VERT =  ManifestResourceLoader.LoadTextFile(vertexShaderSource, executingAssembly, autoAttachAssemblyName);
            var NORMAL_MATERIAL_PARTICLE_FRAG =  ManifestResourceLoader.LoadTextFile(fragmentShaderSource, executingAssembly, autoAttachAssemblyName);
	        
            var vertShaderId = gl.CreateShader(OpenGL.GL_VERTEX_SHADER);
	        gl.ShaderSource(vertShaderId, NORMAL_MATERIAL_PARTICLE_VERT);
	        gl.CompileShader(vertShaderId);
	        //scene::fragShader = shader(OpenGL.GL_FRAGMENT_SHADER, &NORMAL_MATERIAL_PARTICLE_FRAG);
	        var fragShaderId = gl.CreateShader(OpenGL.GL_FRAGMENT_SHADER);
	        gl.ShaderSource(fragShaderId, NORMAL_MATERIAL_PARTICLE_FRAG);
	        gl.CompileShader(fragShaderId);

	        // Create Program from shaders.
	        programId = gl.CreateProgram();
	        gl.AttachShader(programId, vertShaderId);
	        gl.AttachShader(programId, fragShaderId);

            gl.BindAttribLocation(programId, vertPos, "Position");
            gl.BindAttribLocation(programId, normPos, "Normal");
            gl.BindAttribLocation(programId, tCol1Pos, "TCol1");
            gl.BindAttribLocation(programId, tCol2Pos, "TCol2");
            gl.BindAttribLocation(programId, tCol3Pos, "TCol3");
            gl.BindAttribLocation(programId, tCol4Pos, "TCol4");
            gl.BindAttribLocation(programId, ambPos, "AmbientMaterial");
            gl.BindAttribLocation(programId, diffPos, "DiffuseMaterial");
            gl.BindAttribLocation(programId, specPos, "SpecularMaterial");
            gl.BindAttribLocation(programId, shiniPos, "ShininessValue");

            //gl.BindAttribLocation(programId, lightPos, "LightPosition");
            //gl.BindAttribLocation(programId, modelView, "Modelview");
            //gl.BindAttribLocation(programId, normMatrix, "NormalMatrix");
            //gl.BindAttribLocation(programId, projection, "Projection");
	        gl.LinkProgram(programId);


	        knot = MyTrefoilKnot.Instance;

	        // Create the IBO and VBO's.
	        uint[] buffers = new uint[11];
	        gl.GenBuffers(11, buffers);
            ibo = buffers[iboPos];
            vert = buffers[vertPos];
            norm = buffers[normPos];
            tCol1 = buffers[tCol1Pos];
            tCol2 = buffers[tCol2Pos];
            tCol3 = buffers[tCol3Pos];
            tCol4 = buffers[tCol4Pos];
            amb = buffers[ambPos];
            diff = buffers[diffPos];
            spec = buffers[specPos];
            shini = buffers[shiniPos];

            var vertices = knot.Vertices.SelectMany(x=>x.to_array()).ToArray();
            var normals = knot.Normals.SelectMany(x=>x.to_array()).ToArray();

            
            var dataSize = knot.Indices.Length * sizeof(uint);
            IntPtr newDataPtr = Marshal.AllocHGlobal(dataSize);
            var intData = new int[knot.Indices.Length];
            Buffer.BlockCopy(knot.Indices, 0, intData, 0, dataSize);
            Marshal.Copy(intData, 0, newDataPtr, knot.Indices.Length);

	        // Set trefoil knot data.
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, ibo);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, dataSize, newDataPtr, OpenGL.GL_STATIC_DRAW);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vert);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, vertices, OpenGL.GL_STATIC_DRAW);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, norm);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, normals, OpenGL.GL_STATIC_DRAW);

	        // Set transformation data.
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol1);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[4] { 1, 0, 0, 0 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol2);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[4] { 0, 1, 0, 0 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol3);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[4] { 0, 0, 1, 0 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol4);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[4] { 0, 0, 0, 1 }, OpenGL.GL_STATIC_COPY);

	        // Set material data.
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, amb);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[3] { 0, 200, 0 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, diff);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[3] { 0, 200, 200 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, spec);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[3] { 200, 200, 0 }, OpenGL.GL_STATIC_COPY);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, shini);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, new float[1] { 5 }, OpenGL.GL_STATIC_COPY);

	        // Apply uniforms.
            gl.UseProgram(programId);
            mat4 modelViewMat = mat4.identity();
            mat4 projectionMat = mat4.identity();
            mat3 NormalMat = mat3.identity();
	        var zoomValue = new vec3(0, 0, -10);
            //glm.translate(projectionMat, zoomValue);

            //projection = gl.GetUniformLocation(programId, "Projection");
            //modelView = gl.GetUniformLocation(programId, "Modelview");
            //normMatrix = gl.GetUniformLocation(programId, "NormalMatrix");
            //lightPos = gl.GetUniformLocation(programId, "LightPosition");
	
	
	        gl.Uniform3((int)lightPos, 5, 10, 15);
            gl.UniformMatrix4(modelView, 1, false, modelViewMat.to_array());
            gl.UniformMatrix4(projection, 1, false, projectionMat.to_array());
            gl.UniformMatrix3(normMatrix, 1, false, NormalMat.to_array());
	        gl.UseProgram(0);


	        gl.FrontFace(OpenGL.GL_CW);
        }

        public void render(OpenGL gl)
        {
	        //Bind buffers.
	        gl.UseProgram(programId);

            float[] parameters = new float[16];
            gl.GetUniform(programId, projection, parameters);


            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vert);
            gl.VertexAttribPointer(vertPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(vertPos, 0);
            gl.EnableVertexAttribArray(vertPos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, norm);
            gl.VertexAttribPointer(normPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(normPos, 0);
            gl.EnableVertexAttribArray(normPos);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol1);
            gl.VertexAttribPointer(tCol1Pos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(tCol1Pos, 1);
            gl.EnableVertexAttribArray(tCol1Pos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol2);
            gl.VertexAttribPointer(tCol2Pos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(tCol2Pos, 1);
            gl.EnableVertexAttribArray(tCol2Pos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol3);
            gl.VertexAttribPointer(tCol3Pos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(tCol3Pos, 1);
            gl.EnableVertexAttribArray(tCol3Pos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, tCol4);
            gl.VertexAttribPointer(tCol4Pos, 4, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(tCol4Pos, 1);
            gl.EnableVertexAttribArray(tCol4Pos);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, amb);
            gl.VertexAttribPointer(ambPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(ambPos, 1);
            gl.EnableVertexAttribArray(ambPos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, diff);
            gl.VertexAttribPointer(diffPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(diffPos, 1);
            gl.EnableVertexAttribArray(diffPos);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, spec);
            gl.VertexAttribPointer(specPos, 3, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(specPos, 1);
            gl.EnableVertexAttribArray(specPos);
	        gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER,shini);
            gl.VertexAttribPointer(shiniPos, 1, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.VertexAttribDivisor(shiniPos, 1);
            gl.EnableVertexAttribArray(shiniPos);

            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ibo);
            //gl.VertexAttribPointer(ibo, knot.Indices.Length, OpenGL.GL_UNSIGNED_INT, false, 0, IntPtr.Zero);

	        gl.DrawElementsInstanced(OpenGL.GL_TRIANGLES, knot.Indices.Length, OpenGL.GL_UNSIGNED_INT, IntPtr.Zero, 1);
	        gl.UseProgram(0);
		
        }

    }
}
