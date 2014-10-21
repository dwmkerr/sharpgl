using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace SharpGL.WPF
{
  /// <summary>
  /// This class handles conversion to and from various bitmap types.
  /// </summary>
    public static class BitmapConversion
    {
      /// <summary>
      /// Converts an HBitmap the bitmap to a bitmap source.
      /// </summary>
      /// <param name="hBitmap">The hbitmap.</param>
      /// <returns>A BitmapSource.</returns>
        public static BitmapSource HBitmapToBitmapSource(IntPtr hBitmap)
        {
            BitmapSource bitSrc = null;
            
            try
            {
                if (hBitmap != IntPtr.Zero)
                {

                    bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                Win32.DeleteObject(hBitmap);
                GC.Collect();
            }

            return bitSrc;
        }

        /// <summary>
        /// Convert a DIB section to a BitmapSource.
        /// </summary>
        /// <param name="dibSection">The dib section.</param>
        /// <returns>The BitmapSource.</returns>
        public static BitmapSource DIBSectionToBitmapSource(DIBSection dibSection)
        {
            BitmapSource bitSrc = null;

            try
            {
                bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(dibSection.Bits,
                    dibSection.Width, dibSection.Height, PixelFormats.Bgra32, dibSection.Width * 4, 0);
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }

            return bitSrc;
        }
    }
}
