using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpGL.Tests.Helpers
{
    /// <summary>
    /// Work in progress. Do not use.
    /// </summary>
    internal class ReadmeGenerator
    {
        public string GenerateReadmeMarkdown()
        {
            //  Get each test fixture.
            var fixtures = Assembly.GetAssembly(GetType())
                                   .GetTypes()
                                   .Select(t =>
                                           new
                                           {
                                               Type = t,
                                               TestFixture = t.GetCustomAttribute<TestFixtureAttribute>(false)
                                           })
                                   .Where(tt => tt.TestFixture != null)
                                   .ToList();

            var builder = new StringBuilder();

            foreach (var fixture in fixtures)
            {
                builder.AppendFormat("### {0} {1}", fixture.Type.Name, Environment.NewLine);
                builder.AppendFormat("{0} {1}", fixture.TestFixture.Description, Environment.NewLine);
                var imagePath = string.Format("/{0}/{1}.png", fixture.Type.Namespace, fixture.Type.Name);
                builder.AppendFormat("![Expected Result]({0}) {1}", imagePath, Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
