using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace MinionsOfDeath.Graphics
{
    public class Sprite
    {
        public Sprite(string filename)
        {
            Bitmap bitmap = new Bitmap(filename);
            _textureWidth = bitmap.Width;
            _textureHeight = bitmap.Height;

            _textureId = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        private int _textureId;
        private int _textureWidth;
        private int _textureHeight;
        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, _textureHeight);
            GL.TexCoord2(1, 1);
            GL.Vertex2(_textureWidth, _textureHeight);
            GL.TexCoord2(1, 0);
            GL.Vertex2(_textureWidth, 0);
            GL.End();
        }
    }
}
