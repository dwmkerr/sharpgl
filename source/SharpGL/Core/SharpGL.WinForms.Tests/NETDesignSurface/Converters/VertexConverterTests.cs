using NUnit.Framework;
using SharpGL.SceneGraph;
using SharpGL.WinForms.NETDesignSurface.Converters;

namespace SharpGL.WinForms.Tests.NETDesignSurface.Converters
{
    public class VertexConverterTests
    {
        [TestCase("en-US")]
        [TestCase("fr-FR")]
        [TestCase("zh-CN")]
        [TestCase("ar-SA")]
        public void Vertex_is_correctly_parsed_whatever_the_current_culture(string culture)
        {
            using (TestHelper.SetCurrentCulture(culture))
            {
                var text = "  ( 1.2  ,  -3.4    ,  5.6789   )  ";
                var converter = new VertexConverter();
                var vertex = (Vertex)converter.ConvertFrom(text);

                Assert.That(vertex, Is.EqualTo(new System.Numerics.Vector3(1.2f, -3.4f, 5.6789f)));
            }
        }
    }
}
