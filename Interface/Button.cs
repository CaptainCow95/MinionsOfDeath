using OpenTK.Input;

namespace MinionsOfDeath.Interface
{
    public class Button : TextBlock
    {
        private bool _pressed = false;

        public Button(int x, int y, int width, int height, string text)
            : base(x, y, width, height, text)
        {
        }

        public bool Pressed
        {
            get { return _pressed; }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update(double timeSinceFrame)
        {
            base.Update(timeSinceFrame);

            if (Game.MouseState.LeftButton == ButtonState.Released && Game.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                Game.MouseState.X > X && Game.MouseState.X < X + Width && Game.MouseState.Y > Y && Game.MouseState.Y < Y + Height)
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