using MinionsOfDeath.Graphics;
using QuickFont;
using System.Drawing;

namespace MinionsOfDeath.Interface
{
    public class TextBlock : InterfaceObject
    {
        private Sprite _background;
        private QFont _font;
        private string _text;

        public TextBlock(int x, int y, int width, int height, bool staticImage, string text, Sprite background)
            : base(x, y, width, height, staticImage)
        {
			//_font = new QFont(SystemFonts.DefaultFont);
			_font = new QFont (SystemFonts.DialogFont);
            _background = background;
            _text = text;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public override void Draw()
        {
            _background.X = X;
            _background.Y = Y;

            if (Static)
            {
                _background.X += Camera.X;
                _background.Y += Camera.Y;
            }

            _background.Width = Width;
            _background.Height = Height;
            _background.Draw();
            if (Static)
            {
                StringDrawer.Draw(_font, _text, QFontAlignment.Centre, new OpenTK.Vector2(X + (Width / 2) + Camera.X, Y + (Height / 2) + Camera.Y));
            }
            else
            {
                StringDrawer.Draw(_font, _text, QFontAlignment.Centre, new OpenTK.Vector2(X + (Width / 2), Y + (Height / 2)));
            }
        }

        public override void Update(double timeSinceFrame)
        {
            _background.Update(timeSinceFrame);
        }
    }
}