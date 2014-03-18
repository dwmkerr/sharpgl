using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace SharpGL.Textures
{
    /// <summary>
    /// TODO: Move into the core in version 3.0 when we reference Drawing.
    /// </summary>
    public class Texture2D
    {
        public void Create(OpenGL gl)
        {
            //  Generate the texture object array.
            uint[] ids = new uint[1];
            gl.GenTextures(1, ids);
            textureObject = ids[0];
        }

        public void Delete(OpenGL gl)
        {
            gl.DeleteTextures(1, new [] {textureObject});
            textureObject = 0;
        }

        public void Bind(OpenGL gl)
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureObject);
        }

        public void Unbind(OpenGL gl)
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }

        public void SetParameter(OpenGL gl, uint parameterName, uint parameterValue)
        {
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, parameterName, parameterValue);
        }

        /// <summary>
        /// This function creates the texture from an image.
        /// </summary>
        /// <param name="gl">The OpenGL object.</param>
        /// <param name="image">The image.</param>
        /// <returns>True if the texture was successfully loaded.</returns>
        public void SetImage(OpenGL gl, Bitmap image)
        {
            //	Get the maximum texture size supported by OpenGL.
            int[] textureMaxSize = { 0 };
            gl.GetInteger(OpenGL.GL_MAX_TEXTURE_SIZE, textureMaxSize);

            //	Find the target width and height sizes, which is just the highest
            //	posible power of two that'll fit into the image.
            int targetWidth = textureMaxSize[0];
            int targetHeight = textureMaxSize[0];

            for (int size = 1; size <= textureMaxSize[0]; size *= 2)
            {
                if (image.Width < size)
                {
                    targetWidth = size / 2;
                    break;
                }
                if (image.Width == size)
                    targetWidth = size;

            }

            for (int size = 1; size <= textureMaxSize[0]; size *= 2)
            {
                if (image.Height < size)
                {
                    targetHeight = size / 2;
                    break;
                }
                if (image.Height == size)
                    targetHeight = size;
            }

            //  If need to scale, do so now.
            bool destroyImage = false;
            if (image.Width != targetWidth || image.Height != targetHeight)
            {
                //  Resize the image.
                Image newImage = image.GetThumbnailImage(targetWidth, targetHeight, null, IntPtr.Zero);
                image = (Bitmap)newImage;
                destroyImage = true;
            }

            //  Lock the image bits (so that we can pass them to OGL).
            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            //	Set the width and height.
            Width = (uint)image.Width;
            Height = (uint)image.Height;

            //	Bind our texture object (make it the current texture).
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureObject);

            //  Set the image data.
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA,
                (int)Width, (int)Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                bitmapData.Scan0);

            //  Unlock the image.
            image.UnlockBits(bitmapData);

            //  Dispose of the image file if it's an intermediate we created.
            if(destroyImage)
                image.Dispose();
        }

        public uint Width { get; private set; }
        public uint Height { get; private set; }

        private uint textureObject;
    }
}
