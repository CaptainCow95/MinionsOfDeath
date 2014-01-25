using MinionsOfDeath.Graphics;
using QuickFont;
using System.Drawing;

namespace MinionsOfDeath.Interface
{
    public class TextBlock : InterfaceObject
    {
        private QFont _font;
        private string _text;
        private Sprite _background;

        public TextBlock(int x, int y, int width, int height, string text, Sprite background)
            : base(x, y, width, height)
        {
            _font = new QFont(SystemFonts.DefaultFont);
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public override void Draw()
        {
            _background.Draw();
            StringDrawer.Draw(_font, _text, QFontAlignment.Centre, new OpenTK.Vector2(X + (Width / 2), Y + (Height / 2)));
        }

        public override void Update(double timeSinceFrame)
        {
            _background.Update(timeSinceFrame);
        }
    }
}