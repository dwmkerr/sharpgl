using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL.SceneGraph;
using System.IO;
using System.ComponentModel.Composition;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Assets;

namespace SharpGL.Serialization.Wavefront
{
    [Export(typeof(IFileFormat))]
    public class ObjFileFormat : IFileFormat
    {

        private System.Drawing.Color ReadMaterialColor(string line, float alpha)
        {
            string[] lineParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (lineParts.Length >= 4)
            {
                lineParts[1] = lineParts[1].Replace(".", ",");
                lineParts[2] = lineParts[1].Replace(".", ",");
                lineParts[3] = lineParts[1].Replace(".", ",");
                // Convert float a,r,g,b values to byte values.  Make sure they fall in 0-255 range.
                int a = Convert.ToInt32(255 * alpha);
                if (a < 0) a = 0; if (a > 255) a = 255;
                int r = Convert.ToInt32(255 * Convert.ToSingle(lineParts[1]));
                if (r < 0) r = 0; if (r > 255) r = 255;
                int g = Convert.ToInt32(255 * Convert.ToSingle(lineParts[2]));
                if (g < 0) g = 0; if (g > 255) g = 255;
                int b = Convert.ToInt32(255 * Convert.ToSingle(lineParts[3]));
                if (b < 0) b = 0; if (b > 255) b = 255;
                return System.Drawing.Color.FromArgb(a, r, g, b);
            }
            else
                return System.Drawing.Color.White;
        }

        private void SetAlphaForMaterial(Material material, float alpha)
        {
            int a = Convert.ToInt32(255 * alpha);
            material.Ambient = System.Drawing.Color.FromArgb(a, material.Ambient);
            material.Diffuse = System.Drawing.Color.FromArgb(a, material.Diffuse);
            material.Specular = System.Drawing.Color.FromArgb(a, material.Specular);
            material.Emission = System.Drawing.Color.FromArgb(a, material.Emission);
        }

        private string ReadMaterialValue(string line)
        {
            //  The material is everything after the first space.
            int spacePos = line.IndexOf(' ');
            if (spacePos == -1 || (spacePos + 1) >= line.Length)
                return null;

            //  Return the material path.
            return line.Substring(spacePos + 1);
        }

        private void LoadMaterials(string path, Scene scene)
        {

            //  Create a stream reader.
            using (StreamReader reader = new StreamReader(path))
            {
                Material mtl = null;
                float alpha = 1;

                //  Read line by line.
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    //  Skip any comments (lines that start with '#').
                    if (line.StartsWith("#"))
                        continue;

                    // newmatl indicates start of material definition.
                    if (line.StartsWith("newmtl"))
                    {
                        // Add new material to scene's assets.
                        mtl = new Material();
                        scene.Assets.Add(mtl);

                        // Name of material is on same line, immediately follows newmatl.
                        mtl.Name = ReadMaterialValue(line);

                        // Reset assumed alpha.
                        alpha = 1;
                    }

                    // Read properties of material.
                    if (mtl != null)
                    {
                        if (line.StartsWith("Ka"))
                            mtl.Ambient = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Kd"))
                            mtl.Diffuse = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Ks"))
                            mtl.Specular = ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Ke"))
                            mtl.Emission = this.ReadMaterialColor(line, alpha);
                        else if (line.StartsWith("Ns"))
                            mtl.Shininess = Convert.ToSingle(ReadMaterialValue(line));
                        else if (line.StartsWith("map_Ka") ||
                            line.StartsWith("map_Kd") ||
                            line.StartsWith("map_Ks"))
                        {
                            // Get texture map.                    		
                            string textureFile = ReadMaterialValue(line);

                            // Check for existing textures.  Create if does not exist.
                            Texture theTexture = null;
                            var existingTextures = scene.Assets.Where(t => t is Texture && t.Name == textureFile);
                            if (existingTextures.Count() >= 1)
                                theTexture = existingTextures.FirstOrDefault() as Texture;
                            else
                            {
                                //  Does the texture file exist?
                                if (File.Exists(textureFile) == false)
                                {
                                    //  It doesn't, assume its in the same location
                                    //  as the obj file.
                                    textureFile = Path.Combine(Path.GetDirectoryName(path),
                                        Path.GetFileName(textureFile));
                                }
                                
                                // Create/load texture.
                                theTexture = new Texture();
                                theTexture.Create(scene.OpenGL, textureFile);
                            }

                            // Set texture for material.
                            mtl.Texture = theTexture;
                        }
                        else if (line.StartsWith("d") || line.StartsWith("Tr"))
                        {
                            alpha = Convert.ToSingle(ReadMaterialValue(line));
                            SetAlphaForMaterial(mtl, alpha);
                        }
                        // TODO: Handle illumination mode (illum)                    	                    
                    }
                }

            }
        }

        public Scene LoadData(string path)
        {
            char[] split = new char[] { ' ' };

            //  Create a scene and polygon.
            Scene scene = new Scene();
            Polygon polygon = new Polygon();

            string mtlName = null;

            //  Create a stream reader.
            using (StreamReader reader = new StreamReader(path))
            {
                //  Read line by line.
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    //  Skip any comments (lines that start with '#').
                    if (line.StartsWith("#"))
                        continue;

                    //  Do we have a texture coordinate?
                    if (line.StartsWith("vt"))
                    {
                        //  Get the texture coord strings.
                        string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);
                        float x = float.Parse(values[0]);
                        float y = float.Parse(values[1]);
                        
                        //  Parse texture coordinates.
                        float u = x// float.Parse(values[0]);
                        float v = 1.0f - y; //float.Parse(values[1]);

                        //  Add the texture coordinate.
                        polygon.UVs.Add(new UV(u, v));

                        continue;
                    }

                    //  Do we have a normal coordinate?
                    if (line.StartsWith("vn"))
                    {
                        //  Get the normal coord strings.
                        string[] values = line.Substring(3).Split(split, StringSplitOptions.RemoveEmptyEntries);
                        values[0] = values[0].Replace(".", ",");
                        values[1] = values[1].Replace(".", ",");
                        values[2] = values[2].Replace(".", ",");

                        //  Parse normal coordinates.
                        float x = float.Parse(values[0]);
                        float y = float.Parse(values[1]);
                        float z = float.Parse(values[2]);

                        //  Add the normal.
                        polygon.Normals.Add(new Vertex(x, y, z));

                        continue;
                    }

                    //  Do we have a vertex?
                    if (line.StartsWith("v"))
                    {
                        //  Get the vertex coord strings.
                        string[] values = line.Substring(2).Split(split, StringSplitOptions.RemoveEmptyEntries);
                        values[0] = values[0].Replace(".", ",");
                        values[1] = values[1].Replace(".", ",");
                        values[2] = values[2].Replace(".", ",");

                        //  Parse vertex coordinates.
                        float x = float.Parse(values[0]);
                        float y = float.Parse(values[1]);
                        float z = float.Parse(values[2]);

                        //   Add the vertices.
                        polygon.Vertices.Add(new Vertex(x, y, z));

                        continue;
                    }

                    //  Do we have a face?
                    if (line.StartsWith("f"))
                    {
                        Face face = new Face();

                        if (!String.IsNullOrWhiteSpace(mtlName))
                            face.Material = scene.Assets.Where(t => t.Name == mtlName).FirstOrDefault() as Material;

                        //  Get the face indices
                        string[] indices = line.Substring(2).Split(split,
                            StringSplitOptions.RemoveEmptyEntries);

                        //  Add each index.
                        foreach (var index in indices)
                        {
                            //  Split the parts.
                            string[] parts = index.Split(new char[] { '/' }, StringSplitOptions.None);

                            //  Add each part.
                            face.Indices.Add(new Index(
                                (parts.Length > 0 && parts[0].Length > 0) ? int.Parse(parts[0]) - 1 : -1,
                                (parts.Length > 1 && parts[1].Length > 0) ? int.Parse(parts[1]) - 1 : -1,
                                (parts.Length > 2 && parts[2].Length > 0) ? int.Parse(parts[2]) - 1 : -1));
                        }



                        //  Add the face.
                        polygon.Faces.Add(face);

                        continue;
                    }

                    if (line.StartsWith("mtllib"))
                    {
                        // Set current directory in case a relative path to material file is used.
                        Environment.CurrentDirectory = Path.GetDirectoryName(path);

                        // Load materials file.
                        string mtlPath = ReadMaterialValue(line);
                        LoadMaterials(mtlPath, scene);
                    }

                    if (line.StartsWith("usemtl"))
                        mtlName = ReadMaterialValue(line);
                }
            }

            scene.SceneContainer.AddChild(polygon);

            return scene;
        }


        public bool SaveData(Scene scene, string path)
        {
            throw new NotImplementedException("The SaveData method has not been implemented for .obj files.");
            //return SaveData(scene, scene.SceneContainer, path);
        }

        //        private void WriteSceneElement(StreamWriter writer, SceneElement element, ref string currentObjectName, ref string currentMaterialName)
        //        {
        //        	// If object name different than for last element processed, write a g(roup) statement.
        //        	if (!String.IsNullOrWhiteSpace(element.Name))
        //        		if (element.Name != currentObjectName)
        //	        	{
        //        			currentObjectName = element.Name;
        //        			writer.WriteLine("g {0}", currentObjectName);        			
        //	        	}
        //        	
        //        	// If material name different than for last element processed, write a usemtl statement.
        //        	if (element is IHasMaterial)
        //        	{
        //        		IHasMaterial hasMaterial = element as IHasMaterial;
        //        		if (hasMaterial.Material != null)
        //        		if (!String.IsNullOrWhiteSpace(hasMaterial.Material.Name))
        //        			if (hasMaterial.Material.Name != currentMaterialName)
        //    				{
        //        				currentMaterialName = hasMaterial.Material.Name;
        //        				writer.WriteLine("usemtl {0}", currentMaterialName);
        //        			}
        //        	}
        //        	
        //        	// Write out this element.
        //        	if (element is Polygon)
        //        	{
        //        		Polygon poly = element as Polygon;
        //        		foreach (Face face in poly.Faces)
        //        		{
        //		        	// If material name different than for last face processed, write a usemtl statement.
        //		        	if (face.Material != null)
        //		        		if (!String.IsNullOrWhiteSpace(face.Material.Name))
        //		        			if (face.Material.Name != currentMaterialName)
        //		    				{
        //		        				currentMaterialName = face.Material.Name;
        //		        				writer.WriteLine("usemtl {0}", currentMaterialName);
        //		        			}
        //        			
        //		        	// Write out the vertices.
        //		        	foreach (Index i in face.Indices)
        //		        	{
        //		        		Vertex v = poly.Vertices[i.Vertex];
        //		        		writer.WriteLine("v {0} {1} {2}", v.X, v.Y, v.Z);
        //		        	}
        //        		}
        //        	}
        //			// TODO: Handle shapes other than polygons.
        //        	
        //        	
        //        	// Write out any child elements.
        //        	foreach (SceneElement child in element.Children)
        //        		WriteSceneElement(writer, child, ref currentObjectName, ref currentMaterialName);
        //
        //        	writer.WriteLine();
        //        }
        //
        //        public bool SaveData(Scene scene, SceneElement element, string path)
        //        {
        //        	string mtlPath = Path.ChangeExtension(path, ".mtl");
        //        	string shortMtlPath = Path.GetFileName(mtlPath);
        //        	SaveMaterials(mtlPath, scene);
        //        	using (StreamWriter writer = new StreamWriter(path))
        //        	{
        //        		writer.WriteLine("mtlib {0}", shortMtlPath);
        //        		string objName = "";
        //        		string mtlName = "";
        //        		WriteSceneElement(writer, element, ref objName, ref mtlName);
        //	        	writer.Flush();
        //	        	writer.Close();
        //        	}        	
        //        	return true;        	
        //        }
        //
        //        
        //        private void WriteMaterialColor(StreamWriter writer, string name, System.Drawing.Color color)
        //        {
        //        	float r = Convert.ToSingle(color.R) / 255F;
        //        	float g = Convert.ToSingle(color.G) / 255F;
        //        	float b = Convert.ToSingle(color.B) / 255F;
        //        	writer.WriteLine("{0} {1} {2} {3}", name, r, g, b);        	
        //        }
        //
        //                
        //        private void WriteMaterialValue(StreamWriter writer, string name, string value)
        //        {
        //        	writer.WriteLine("{0} {1}", name, value);        	
        //        }
        //        
        //        private void SaveMaterials(string path, Scene scene)
        //        {
        //        	using (StreamWriter writer = new StreamWriter(path))
        //        	{
        //	        	foreach (Material mtl in scene.Assets)
        //	        	{
        //	        		if (mtl == null) continue;
        //	        		
        //	        		writer.WriteLine("newmtl {0}", mtl.Name);
        //	        		WriteMaterialColor(writer, "Ka", mtl.Ambient);
        //	        		WriteMaterialColor(writer, "Kd", mtl.Diffuse);
        //	        		WriteMaterialColor(writer, "Ks", mtl.Specular);
        //					// TODO: Shininess	        		
        //	        		WriteMaterialValue(writer, "Tr", (Convert.ToSingle(mtl.Diffuse.A) / 255).ToString());
        //	        		// TODO: Illumination model
        //	        		// TODO: Textures
        //	        		
        //	        		writer.WriteLine();
        //	        	}
        //	        	writer.Flush();
        //	        	writer.Close();
        //        	}
        //        }

        public string[] FileTypes
        {
            get { return new string[] { "obj" }; }
        }

        public string Filter
        {
            get { return "Wavefont Obj Files (*.obj)|*.obj"; }
        }
    }
}
