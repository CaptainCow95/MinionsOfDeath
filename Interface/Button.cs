using MinionsOfDeath.Graphics;
using OpenTK.Input;

namespace MinionsOfDeath.Interface
{
    public class Button : TextBlock
    {
        private bool _pressed = false;

        public Button(int x, int y, int width, int height, string text, Sprite background)
            : base(x, y, width, height, text, background)
        {
        }

        public bool Pressed
        {
            get { return _pressed; }
        }

        public override void Update(double timeSinceFrame)
        {
            base.Update(timeSinceFrame);

            if (Game.MouseState.LeftButton == ButtonState.Released && Game.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                Game.MousePosition.X > X && Game.MousePosition.X < X + Width && Game.MousePosition.Y > Y && Game.MousePosition.Y < Y + Height)
            {
                _pressed = true;
            }
            else
            {
                _pressed = false;
            }
        }
    }
}