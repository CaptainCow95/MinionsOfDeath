using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class Select : InterfaceObject
    {
        private Sprite _background;
        private StackPanel _buttons;
        private int _selectedIndex = -1;

        public Select(int x, int y, bool staticImage, List<string> selections, Sprite background, List<string> backgroundFilenames)
            : base(x, y, 0, 0, staticImage)
        {
            _background = background;
            _buttons = new StackPanel(x, y, staticImage, false);
            selections.ForEach(e => _buttons.Children.Add(new Button(x, (int)(y + background.Height), (int)background.Width, (int)background.Height, false, e, new Sprite(backgroundFilenames))));
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
        }

        public override void Draw()
        {
            _buttons.Draw();
        }

        public override void Update(double timeSinceFrame)
        {
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