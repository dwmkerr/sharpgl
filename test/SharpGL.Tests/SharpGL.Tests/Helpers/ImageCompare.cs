using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//  NOTE:
//  Thanks to http://www.dotnetexamples.com/2012/07/fast-bitmap-comparison-c.html for
//  a safe implementation.

namespace SharpGL.Tests.Helpers
{
    /// <summary>
    /// Compares two images.
    /// </summary>
    internal static class ImageCompare
    {
        public static bool Compare(Bitmap lhs, Bitmap rhs)
        {
            if (lhs == null || rhs == null)
                throw new InvalidOperationException("Provided images are not valid.");
            if (object.Equals(lhs, rhs))
                throw new InvalidOperationException("Image data comparison between the same object is invalid.");
            if (lhs.PixelFormat != PixelFormat.Format32bppArgb || rhs.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only comparison between 32bppArgb images is supported");
            if (!lhs.Size.Equals(rhs.Size))
                return false;
            
            //  Lock the bits.
            var lhsData = lhs.LockBits(new Rectangle(new Point(0, 0), lhs.Size), ImageLockMode.ReadOnly, lhs.PixelFormat);
            var rhsData = rhs.LockBits(new Rectangle(new Point(0, 0), rhs.Size), ImageLockMode.ReadOnly, rhs.PixelFormat);

            //  Create storage for the raw bytes. We have 32 bpp.
            var lhsByteCount = Math.Abs(lhsData.Stride) * lhsData.Height;
            var rhsByteCount = Math.Abs(rhsData.Stride) * rhsData.Height;
            
            //  We need to be able to handle 32 bpp.
            if((lhsByteCount % 4) != 0 || (rhsByteCount % 4) != 0)
                throw new InvalidOperationException("Cannot compare images that are not internally stored as 32 bit values.");
            if(lhsByteCount != rhsByteCount)
                throw new InvalidOperationException("Internally, the images appear to be of different sizes.");

            var pixelCount = lhsByteCount/4;

            var lhsPixels = new int[pixelCount];
            var rhsPixels = new int[pixelCount];

            try
            {
                //  Copy the data into the managed arrays and compare. Direct pointer access is faster,
                //  however requires that we use unsafe code.
                Marshal.Copy(lhsData.Scan0, lhsPixels, 0, pixelCount);
                Marshal.Copy(rhsData.Scan0, rhsPixels, 0, pixelCount);

                for (var n = 0; n < pixelCount - 1; n++)
                    if (lhsPixels[n] != rhsPixels[n])
                        return false;
            }
            finally
            {
                //  Always unlock the data.
                lhs.UnlockBits(lhsData);
                rhs.UnlockBits(rhsData);
            }
            
            //  The images are exactly the same.
            return true;
        }
    }
}
