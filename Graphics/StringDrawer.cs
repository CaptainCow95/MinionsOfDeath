using OpenTK;
using QuickFont;

namespace MinionsOfDeath.Graphics
{
    public static class StringDrawer
    {
        public static void Draw(QFont font, string text, QFontAlignment alignment, Vector2 position)
        {
            position.Y -= font.Measure(text, alignment).Height / 2;
            font.Options.UseDefaultBlendFunction = false;
            font.Print(text, alignment, position);
        }
    }
}