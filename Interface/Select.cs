using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class Select : InterfaceObject
    {
        private Sprite _background;
        private List<Button> _buttons = new List<Button>();
        private int _selectedIndex = -1;

        public Select(int x, int y, List<string> selections, Sprite background, List<string> backgroundFilenames)
            : base(x, y, 0, 0)
        {
            _background = background;
            selections.ForEach(e => _buttons.Add(new Button(x, (int)(y + background.Height), (int)background.Width, (int)background.Height, e, new Sprite(backgroundFilenames))));
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
        }

        public override void Draw()
        {
            _buttons.ForEach(e => e.Draw());
        }

        public override void Update(double timeSinceFrame)
        {
            _buttons.ForEach(e => e.Update(timeSinceFrame));

            _buttons.ForEach(e =>
            {
                if (e.Pressed)
                {
                    _selectedIndex = _buttons.IndexOf(e);
                }
            });
        }
    }
}