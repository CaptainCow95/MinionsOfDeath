using OpenTK;
using QuickFont;

namespace MinionsOfDeath.Graphics
{
    public static class StringDrawer
    {
        public static void Draw(QFont font, string text, QFontAlignment alignment, Vector2 position)
        {
            font.Options.UseDefaultBlendFunction = false;
            font.Print(text, alignment, position);
        }
    }
}