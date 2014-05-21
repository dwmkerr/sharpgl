using GlmNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharpGLObjFileImport
{
    public class ObjFileModel
    {
        #region fields
        #endregion fields

        #region properties
        public uint[] Indices { get; set; }
        public vec4[] Vertices { get; set; }
        public vec3[] Normals { get; set; }
        #endregion properties

        #region events
        #endregion events

        #region constructors
        public ObjFileModel(string path)
        {
            Load(path);
        }
        #endregion constructors

        private void Load(string path)
        {
            var v = new List<vec4>(); // Vertices coordinates
            var vt = new List<vec3>(); // Texture coordinates
            var vn = new List<vec3>(); // Normals
            var vp = new List<vec3>(); // Parameter space vertices
            var f = new List<List<PositionNormal>>(); // Face definitions (position id and normal id)

            var culture = System.Globalization.CultureInfo.InvariantCulture;

            #region read data
            using (var sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    {
                        // # : comments
                        continue;
                    }
                    else if (line.StartsWith("vt"))
                    {
                        // vt : texture coordinate
                        #region load texture coordinate
                        var pattern = @"\d+.?\d*";
                        var matches = new Regex(pattern).Matches(line);
                        var matchCount = matches.Count;
                        if (matchCount < 2) continue; // Invalid texture coordinate.
                        var vec = new vec3();
                        vec.x = float.Parse(matches[0].Value, culture);
                        vec.y = float.Parse(matches[1].Value, culture);
                        vec.z = matchCount > 2 ? float.Parse(matches[2].Value, culture) : 0;

                        vt.Add(vec);
                        #endregion load texture coordinate
                    }
                    else if (line.StartsWith("vn"))
                    {
                        // vn : vertex normal
                        #region load vertex normal
                        var pattern = @"-?\d+.?\d*";
                        var matches = new Regex(pattern).Matches(line);
                        var matchCount = matches.Count;
                        if (matchCount < 2) continue; // Invalid.
                        var vec = new vec3();
                        vec.x = float.Parse(matches[0].Value, culture);
                        vec.y = float.Parse(matches[1].Value, culture);
                        vec.z = float.Parse(matches[2].Value, culture);

                        vn.Add(vec);
                        #endregion load vertex normal
                    }
                    else if (line.StartsWith("v "))
                    {
                        // v : vertex coordinates
                        #region load vertex coordinates
                        var pattern = @"-?\d+.?\d*";
                        var matches = new Regex(pattern).Matches(line);
                        var matchCount = matches.Count;
                        if (matchCount < 3) continue; // Invalid vertex.
                        var vec = new vec4();
                        vec.x = (float)double.Parse(matches[0].Value, culture);
                        vec.y = float.Parse(matches[1].Value, culture);
                        vec.z = float.Parse(matches[2].Value, culture);
                        vec.w = matchCount > 3 ? float.Parse(matches[3].Value, culture) : 1;

                        v.Add(vec);
                        #endregion load vertex coordinates
                    }
                    else if (line.StartsWith("vp"))
                    {
                        // vp : parameter space vertices
                        #region load parameter space vertices
                        var pattern = @"\d+.?\d*";
                        var matches = new Regex(pattern).Matches(line);
                        var matchCount = matches.Count;
                        if (matchCount < 1) continue; // Invalid vertex.
                        var vec = new vec3();
                        vec.x = float.Parse(matches[0].Value, culture);
                        vec.y = matchCount > 1 ? float.Parse(matches[1].Value, culture) : float.MaxValue;
                        vec.z = matchCount > 2 ? float.Parse(matches[2].Value, culture) : float.MaxValue;

                        vp.Add(vec);
                        #endregion load parameter space vertices
                    }
                    else if (line.StartsWith("f"))
                    {
                        // vt : face definitions
                        #region load face definitions
                        var pattern = @"(\d+)(//(\d+))?";
                        var matches = Regex.Matches(line, pattern);
                        var matchCount = matches.Count;
                        if (matchCount < 3) continue; // Invalid face.

                        List<PositionNormal> points = new List<PositionNormal>();
                        foreach (Match match in matches)
                        {
                            var groups = match.Groups;
                            var posId = int.Parse(groups[1].ToString());
                            var normId = int.Parse("0" + groups[3].ToString());
                            points.Add(new PositionNormal(posId-1,normId-1));
                        }

                        f.Add(points);
                        #endregion load face definitions
                    }
                }
            }
            #endregion read data

            ConvertInputDataToUsableData(v, vt, vn, vp, f);
        }

        private void ConvertInputDataToUsableData(List<vec4> positions, 
            List<vec3> textureCoords, List<vec3> normals, List<vec3> paramSpacePositions, 
            List<List<PositionNormal>> faces)
        {
            var indices = new List<uint>();
            uint nextIdxValue = 0;

            var posNormMap = new Dictionary<vec4, Dictionary<vec3, uint>>(); // posNormMap[position][normal] = index

            var posNormMap2 = new Dictionary<int, Dictionary<int, uint>>(); // posNormMap[positionId][normalId] = index

            #region create the indices from the normals and positions.
            //for (int i = 0; i < faces.Count; i++)
            //{
            //    var face = faces[i];

            //    var triangulatedFaces = TriangulateSimple(face, ref positions);
            //    foreach (var tFace in triangulatedFaces)
            //    {
            //        var i1 = tFace.Item1;
            //        var i2 = tFace.Item2;
            //        var i3 = tFace.Item3;


            //        foreach (var point in new PositionNormal[]{i1, i2, i3})
            //        {
            //            var position = positions[point.Position];
            //            var normal = normals[point.NormalPosition];

            //            uint idx;

            //            if (posNormMap.ContainsKey(position))
            //            {
            //                if (posNormMap[position].ContainsKey(normal))
            //                {
            //                    // Index that uses this position and normal combination already exists, so use it again.
            //                    idx = posNormMap[position][normal];
            //                }
            //                else
            //                {
            //                    // Index for this combination doesn't exist, so create and add it.
            //                    idx = nextIdxValue++;
            //                    posNormMap[position].Add(normal, idx);
            //                }
            //            }
            //            else
            //            {
            //                var dic = new Dictionary<vec3, uint>();

            //                // Index for this combination doesn't exist, so create and add it.
            //                idx = nextIdxValue++;
            //                dic.Add(normal, idx);

            //                posNormMap.Add(position, dic);
            //            }
            //            indices.Add(idx);
            //        }
            //    }
            //}
            #endregion create the indices from the normals and positions.

            #region v2
            for (int i = 0; i < faces.Count; i++)
            {
                var face = faces[i];

                var triangulatedFaces = TriangulateSimple(face, ref positions);
                foreach (var tFace in triangulatedFaces)
                {
                    var i1 = tFace.Item1;
                    var i2 = tFace.Item2;
                    var i3 = tFace.Item3;


                    foreach (var point in new PositionNormal[] { i1, i2, i3 })
                    {
                        var position = point.Position;
                        var normal = point.NormalPosition;

                        uint idx;

                        if (posNormMap2.ContainsKey(position))
                        {
                            if (posNormMap2[position].ContainsKey(normal))
                            {
                                // Index that uses this position and normal combination already exists, so use it again.
                                idx = posNormMap2[position][normal];
                            }
                            else
                            {
                                // Index for this combination doesn't exist, so create and add it.
                                idx = nextIdxValue++;
                                posNormMap2[position].Add(normal, idx);
                            }
                        }
                        else
                        {
                            var dic = new Dictionary<int, uint>();

                            // Index for this combination doesn't exist, so create and add it.
                            idx = nextIdxValue++;
                            dic.Add(normal, idx);

                            posNormMap2.Add(position, dic);
                        }
                        indices.Add(idx);
                    }
                }
            }
            #endregion v2

            var positionsRes = new vec4[nextIdxValue];
            var normalsRes = new vec3[nextIdxValue];


            //for (int posId = 0; posId < posNormMap.Keys.Count; posId++)
            //{
            //    var elemPos = posNormMap.ElementAt(posId);
            //    var position = elemPos.Key;
            //    var dicNorms = elemPos.Value;
            //    for (int normId = 0; normId < dicNorms.Count; normId++)
            //    {
            //        var elemNorm = dicNorms.ElementAt(normId);
            //        var normal = elemNorm.Key;
                    
            //        var idx = elemNorm.Value;
            //        positionsRes[idx] = position;
            //        normalsRes[idx] = normal;
            //    }
            //} 
            
            for (int posId = 0; posId < posNormMap2.Keys.Count; posId++)
            {
                var elemPos = posNormMap2.ElementAt(posId);
                var position = elemPos.Key;
                var dicNorms = elemPos.Value;
                for (int normId = 0; normId < dicNorms.Count; normId++)
                {
                    var elemNorm = dicNorms.ElementAt(normId);
                    var normal = elemNorm.Key;

                    var idx = elemNorm.Value;
                    positionsRes[idx] = positions[position];
                    normalsRes[idx] = normals[normal];
                }
            }


            Indices = indices.ToArray();
            Vertices = positionsRes;
            Normals = normalsRes;

            
            var str = AsString(Indices, Vertices);
        }

        private string AsString(uint[] indices, vec4[] verts)
        {
            var sb = new StringBuilder();

            foreach (var idx in indices)
            {
                var v = verts[idx];
                var signs = v.x < 0 ? "-" : "+";
                signs += v.y < 0 ? "-" : "+";
                signs += v.z < 0 ? "-" : "+";
                sb.Append(idx + " : " + signs + "\n");
            }

            return sb.ToString();
        }

        private List<Tuple<PositionNormal, PositionNormal, PositionNormal>> TriangulateSimple(List<PositionNormal> points, ref List<vec4> positions)
        {
            var res = new List<Tuple<PositionNormal, PositionNormal, PositionNormal>>();
            if (points.Count == 3)
            {
                res.Add(new Tuple<PositionNormal, PositionNormal, PositionNormal>(points[0], points[1], points[2]));
            }
            else if (points.Count == 4)
            {
                res.Add(new Tuple<PositionNormal, PositionNormal, PositionNormal>(points[0], points[1], points[2]));
                res.Add(new Tuple<PositionNormal, PositionNormal, PositionNormal>(points[0], points[2], points[3]));
            }
            else
            {
                throw new Exception("This algorithm doesn't know how to triangulate a face with " + points.Count + " points.");
            }

            return res;
        }
    }
}
