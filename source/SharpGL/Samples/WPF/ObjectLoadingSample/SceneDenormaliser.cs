using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //  Go through every group and denormalize it.
            foreach(var group in scene.Groups)
            {
                //  We are going to duplicate everything, so create storage.
                var count = group.Faces.Sum(f => f.Indices.Count);
                var mesh = new Mesh
                           {
                               vertices =
                                   group.Faces.SelectMany(f => f.Indices).Select(i => vertics[i.vertex]).Select(
                                       v => new vec3(v.x, v.y, v.z)).ToArray(),
                               normals =
                                   group.Faces.SelectMany(f => f.Indices).Select(i => normals[i.normal.Value]).Select(
                                       v => new vec3(v.x, v.y, v.z)).ToArray(),
                               uvs =
                                   group.Faces.SelectMany(f => f.Indices).Select(i => uvs[i.uv.Value]).Select(
                                       v => new vec2(v.u, v.v)).ToArray(),
                                       material = group.Faces.First().Material
                           };
                meshes.Add(mesh);
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
        public FileFormatWavefront.Model.Material material;
    }
}
