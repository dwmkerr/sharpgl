using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CelShadingSample
{
    public static class ManifestResourceLoader
    {
        public static string LoadTextFile(string textFileName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var pathToDots = textFileName.Replace("\\", ".");
            var location = string.Format("{0}.{1}", executingAssembly.GetName().Name, pathToDots);

            using (var stream = executingAssembly.GetManifestResourceStream(location))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
