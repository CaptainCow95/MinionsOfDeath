using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace MinionsOfDeath.Graphics
{
    public class Sprite
    {
        private const double UPDATETIME = 0.2;
        private int _textureHeight;
        private List<int> _textureIds = new List<int>();
        private int _textureNumber;
        private int _textureWidth;
        private double _timeSinceUpdate;
        private float _x;
        private float _y;

        public Sprite(List<string> filenames)
        {
            foreach (string filename in filenames)
            {
                Bitmap bitmap = new Bitmap(filename);
                _textureWidth = bitmap.Width;
                _textureHeight = bitmap.Height;

                int _textureId = GL.GenTexture();
                _textureIds.Add(_textureId);

                GL.BindTexture(TextureTarget.Texture2D, _textureId);

                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                bitmap.UnlockBits(data);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
        }

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public void Draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureIds[_textureNumber]);

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

        public void Update(double timeSinceFrame)
        {
            _timeSinceUpdate += timeSinceFrame;
            while (_timeSinceUpdate > UPDATETIME)
            {
                ++_textureNumber;
                if (_textureNumber >= _textureIds.Count)
                {
                    _textureNumber = 0;
                }
            }
        }
    }
}