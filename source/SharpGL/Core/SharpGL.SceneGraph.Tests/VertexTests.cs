using NUnit.Framework;

namespace SharpGL.SceneGraph.Tests
{
    public class VertexTests
    {
        [Test]
        public void Can_Normalize_Non_Zero_Vertex()
        {
            var vertex = new System.Numerics.Vector3(0.1f, 0f, 0f);
            vertex.Normalize();
            Assert.That(vertex, Is.EqualTo(new System.Numerics.Vector3(1f, 0f, 0f)));
        }

        [Test]
        public void Can_Normalize_Zero_Vertex()
        {
            var vertex = new System.Numerics.Vector3(0f, 0f, 0f);
            vertex.Normalize();
            Assert.That(vertex, Is.EqualTo(new System.Numerics.Vector3(0f, 0f, 0f)));
        }
    }
}
