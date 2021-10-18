using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization.Wavefront;

namespace SharpGL.Serialization.Tests
{
    public class ObjFileFormatTests
    {
        [TestCase("en-US")]
        [TestCase("fr-FR")]
        [TestCase("zh-CN")]
        [TestCase("ar-SA")]
        public void OBJ_file_is_correctly_parsed_whatever_the_current_culture(string culture)
        {
            using (TestHelper.SetCurrentCulture(culture))
            {
                var path = TestHelper.ResolvePath(@"data\ducky.obj");
                var objFileFormat = new ObjFileFormat();
                var scene = objFileFormat.LoadData(path);
                var polygon = (Polygon)scene.SceneContainer.Children.First();
                
                // The first texture coordinate line with a floating-point value is: vt 0.219297 1 0
                var uv = polygon.UVs.First(x => (int)x.U != x.U);
                Assert.That(uv, Is.EqualTo(new UV(0.219297f, 0f)));

                // We cannot test normals for now as there are none in our test file

                // The first vertex line is: v 29.564405 140.987503 67.743927
                var vertex = polygon.Vertices.First();
                Assert.That(vertex, Is.EqualTo(new System.Numerics.Vector3(29.564405f, 140.987503f, 67.743927f)));

                // Materials should have been read too
                // First material is DBody; its ambient light is defined as: Ka 1.0000 0.6667 0.0000
                // This shoudl give us (in ARGB): FFFFAA00 

                var material = (Material)scene.Assets.First();
                Assert.AreEqual("DBody", material.Name);
                Assert.That(material.Ambient, Is.EqualTo(Color.FromArgb(0xFF, 0xFF, 0xAA, 0x00)));
            }
        }
        
        //[TestCase("en-US")]
        //[TestCase("fr-FR")]
        //[TestCase("zh-CN")]
        //[TestCase("ar-SA")]
        //public void Loaded_OBJ_file_is_correctly_saved_back_whatever_the_current_culture(string culture)
        //{
        //    using (TestHelper.SetCurrentCulture(culture))
        //    {
        //        var path = TestHelper.ResolvePath(@"data\ducky.obj");
        //        var objFileFormat = new ObjFileFormat();
        //        var scene = objFileFormat.LoadData(path);

        //        // Now, save the scene into temporary obj+mtl files
        //        var temporaryFileName = Path.GetTempFileName() + ".obj";
        //        try
        //        {
        //            // And load the temporary files again
        //            objFileFormat.SaveData(scene, temporaryFileName);
        //            var newScene = objFileFormat.LoadData(temporaryFileName);

        //            // Verify some values on both scenes
        //            Assert.AreEqual(scene.Assets.Count, newScene.Assets.Count);

        //            var polygon = (Polygon)scene.SceneContainer.Children.First();
        //            var newPolygon = (Polygon)newScene.SceneContainer.Children.First();

        //            Assert.AreEqual(polygon.Vertices.Count, newPolygon.Vertices.Count);
        //        }
        //        finally
        //        {
        //            try
        //            {
        //                File.Delete(temporaryFileName);
        //                File.Delete(Path.ChangeExtension(temporaryFileName, ".mtl"));
        //            }
        //            catch { }
        //        }
        //    }
        //}
    }
}
