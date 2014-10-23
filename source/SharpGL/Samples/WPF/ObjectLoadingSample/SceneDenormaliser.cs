using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileFormatWavefront.Model;
using GlmNet;

namespace ObjectLoadingSample
{
    /// <summary>
    /// The scene denormalizer
    /// </summary>
    public static class SceneDenormaliser
    {
        public static List<Mesh> Denormalize(FileFormatWavefront.Model.Scene scene)
        {
            var meshes = new List<Mesh>();
            var vertics = scene.Vertices;
            var normals = scene.Normals;
            var uvs = scene.Uvs;

            List<Face> facesWithSameIndexCount = new List<Face>();
            int currentIndexCount = -1;

            //  Go through every group and denormalize it.
            foreach(var group in scene.Groups)
            {
                //  Go through each face.
                for(int i=0; i< group.Faces.Count; i++)
                {
                    var face = group.Faces[i];

                    //  If this is the first face, set the current index count.
                    if(currentIndexCount == -1)
                        currentIndexCount = face.Indices.Count;
                    else if(currentIndexCount == face.Indices.Count)
                        facesWithSameIndexCount.Add(face);
                    //  If this is a new index count, or the end, complete the mesh.
                    if(currentIndexCount != face.Indices.Count || i == group.Faces.Count - 1)
                    {
                        var indices = facesWithSameIndexCount.SelectMany(f => f.Indices).ToList();

                        //  Create the mesh vertices.
                        var vs = indices.Select(ind => vertics[ind.vertex]).Select(v => new vec3(v.x, v.y, v.z)).ToArray();
                        var ns = indices.Any(ind => ind.normal.HasValue == false)
                                          ? null
                                          : indices.Select(ind => normals[ind.normal.Value]).Select(
                                              v => new vec3(v.x, v.y, v.z)).ToArray();
                        var ts = indices.Any(ind => ind.uv.HasValue == false)
                                     ? null
                                     : indices.Select(ind => uvs[ind.uv.Value]).Select(v => new vec2(v.u, v.v)).ToArray();

                        meshes.Add(new Mesh
                                   {
                                       vertices = vs,
                                       normals = ns,
                                       uvs = ts,
                                       material = facesWithSameIndexCount.First().Material,
                                       indicesPerFace = currentIndexCount
                                   });

                        facesWithSameIndexCount = new List<Face>();
                        facesWithSameIndexCount.Add(face);
                        currentIndexCount = face.Indices.Count;
                    }
                }
            }

            return meshes;
        }
    }

    public class Mesh
    {
        public vec3[] vertices;
        public vec3[] normals;
        public vec2[] uvs;
        public uint[] indices;
        public int indicesPerFace;
        public FileFormatWavefront.Model.Material material;
    }
}
