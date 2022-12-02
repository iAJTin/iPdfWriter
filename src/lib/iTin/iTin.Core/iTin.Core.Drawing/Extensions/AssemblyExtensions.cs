
using System.Drawing;
using System.IO;
using System.Reflection;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="Assembly"/>.
    /// </summary> 
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets an image from assembly resources.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="name">Resource name.</param>
        /// <returns>
        /// A <see cref="Image"/> that contains the image resource.
        /// </returns>
        public static Image GetImageResource(this Assembly assembly, string name)
        {
            string[] resNames = assembly.GetManifestResourceNames();
            foreach (var s in resNames)
            {
                if (s != assembly.GetName().Name + ".img." + name)
                {
                    continue;
                }

                Stream imgStream = assembly.GetManifestResourceStream(s);
                if (imgStream == null)
                {
                    return null;
                }

                Image img = Image.FromStream(imgStream);
                imgStream.Close();
                imgStream = null;

                return img;
            }

            return null;
        }
    }
}
