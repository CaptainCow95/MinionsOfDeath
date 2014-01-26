using MinionsOfDeath.Graphics;
using OpenTK.Input;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class Select : InterfaceObject
    {
        private Sprite _background;
        private StackPanel _buttons;
        private bool _canceled = false;
        private int _selectedIndex = -1;

        public Select(int x, int y, bool staticImage, List<string> selections, Sprite background, List<string> backgroundFilenames)
            : base(x, y, 0, 0, staticImage)
        {
            _background = background;
            _buttons = new StackPanel(x, y, staticImage, false);
            selections.ForEach(e => _buttons.Children.Add(new Button(x, (int)(y + background.Height), (int)background.Width, (int)background.Height, false, e, new Sprite(backgroundFilenames))));
        }

        public bool Canceled
        {
            get { return _canceled; }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
        }

        public override void Draw()
        {
            _buttons.Draw();
        }

        public void Reset()
        {
            _selectedIndex = -1;
            _canceled = false;
        }

        public override void Update(double timeSinceFrame)
        {
            Width = _buttons.Width;
            Height = _buttons.Height;
            if (Game.MouseState.LeftButton == ButtonState.Released && Game.PreviousMouseState.LeftButton == ButtonState.Pressed &&
                !(Game.MousePosition.X > X - Camera.X && Game.MousePosition.X < X + Width - Camera.X && Game.MousePosition.Y > Y - Camera.Y && Game.MousePosition.Y < Y + Height - Camera.Y))
            {
                _canceled = true;
            }

            _buttons.Update(timeSinceFrame);

            _buttons.Children.ForEach(e =>
            {
                if (((Button)e).Pressed)
                {
                    _selectedIndex = _buttons.Children.IndexOf(e);
                }
            });
        }
    }
}