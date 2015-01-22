using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SharpGL.Tests
{
    /// <summary>
    /// A base class for tests which render an image and compare it to a reference image.
    /// </summary>
    internal abstract class RenderingTest
    {
        /// <summary>
        /// Loads the reference bitmap. This bitmap must be in the same folder as the test, have the same
        /// name as the test class plus '.png' and be set to 'Embedded Resource'.
        /// </summary>
        /// <returns>The reference bitmap.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the manifest resource cannot be loaded.</exception>
        /// <remarks>
        /// Callers must dispose of the returned bitmap.
        /// </remarks>
        protected Bitmap LoadReferenceBitmap()
        {
            //  Work out the manifest resource name.
            var concreteType = GetType();
            var manifestResourceName = string.Format("{0}.{1}.png", concreteType.Namespace, concreteType.Name);

            //  Load the manifest resource stream.
            using (var manifestResourceStream = concreteType.Assembly.GetManifestResourceStream(manifestResourceName))
            {
                if(manifestResourceStream == null)
                    throw new InvalidOperationException(string.Format("Cannot find the reference image for test {0} at {1}. Has the image been set to 'Embedded Resource'?", concreteType.Name, manifestResourceName));
            
                //  Create the image from the stream.
                return (Bitmap)Image.FromStream(manifestResourceStream);
            }
        }

        /// <summary>
        /// Creates a comparible bitmap. This is a bitmap based on <paramref name="hBitmap"/>, but with the
        /// same pixel format as the image loaded by <see cref="LoadReferenceBitmap"/>. This means the
        /// two images can be compared.
        /// </summary>
        /// <param name="hBitmap">The handle to the bitmap.</param>
        /// <returns>A Bitmap which can be compared to the image loaded from <see cref="LoadReferenceBitmap"/>.</returns>
        protected Bitmap CreateComparibleBitmap(IntPtr hBitmap)
        {
            //  NOTE: Reference bitmaps are loaded as 32Argb images. The safest way to make sure the images
            //  are comparible is to take the source bitmap, save it to a temp location as a png file, then
            //  reload it and trash the original file.
            //  An improvement in the future would be to do this with unmanaged code, comparing RGB only
            //  and ignoring the alpha channel.

            //  Save the source bitmap to a temporary location.
            var tempPath = Path.GetTempPath() + Guid.NewGuid() + ".png";
            using (var bitmap = Image.FromHbitmap(hBitmap))
                bitmap.Save(tempPath, ImageFormat.Png);

            //  Reload, we've now got comparable bitmaps.
            Bitmap comparibleBitmap;
            try
            {
                using (var stream = new FileStream(tempPath, FileMode.Open, FileAccess.Read))
                    comparibleBitmap = (Bitmap)Image.FromStream(stream);
            }
            finally 
            {
                //  Trash the temporary file.
                File.Delete(tempPath);
            }
            
            //  Return the comparible bitmap.
            return comparibleBitmap;
        }
    }
}