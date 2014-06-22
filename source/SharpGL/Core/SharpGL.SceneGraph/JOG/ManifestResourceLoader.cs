using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace SharpGL.SceneGraph.JOG
{
    /// <summary>
    /// A small helper class to load manifest resource files.
    /// </summary>
    public static class ManifestResourceLoader
    {
        /// <summary>
        /// Loads the named manifest resource as a text string.
        /// </summary>
        /// <param name="textFileName">Name of the text file.</param>
        /// <returns>The contents of the manifest resource.</returns>
        public static string LoadTextFile(string path, Assembly executingAssembly, bool autoAttachAssemblyName = true)
        {
            if (executingAssembly == null)
                executingAssembly = Assembly.GetExecutingAssembly();

            var pathToDots = path.Replace("\\", ".").Replace("/", ".");


            string location;
            if (autoAttachAssemblyName)
            {
                string assemblyName = executingAssembly.GetName().Name;
                location = string.Format("{0}.{1}", assemblyName, pathToDots);
            }
            else
            {
                location = pathToDots;
            }

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
