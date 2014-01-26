using MinionsOfDeath.Graphics;
using System;

namespace MinionsOfDeath.Interface
{
    public class ScrollBar : InterfaceObject
    {
        private int _currentValue;
        private bool _horizontal;
        private int _maxValue;
        private int _minValue;
		private bool _scrolling = false;
        private Sprite _scrollBar;

        public ScrollBar(int x, int y, int width, int height, bool staticImage, int minValue, int maxValue, bool horizontal, Sprite scrollBar)
            : base(x, y, width, height, staticImage)
        {
            _minValue = minValue;
			_maxValue = maxValue - (horizontal ? Game.WindowWidth : Game.WindowHeight);
            _horizontal = horizontal;
            _currentValue = minValue;
            _scrollBar = scrollBar;
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public override void Draw()
        {
            if (_horizontal)
            {
				_scrollBar.X = _currentValue + Camera.X;
                _scrollBar.Y = Y;
				_scrollBar.Height = Height;
            }
            else
            {
				_scrollBar.X = X;
				_scrollBar.Y = _currentValue + Camera.Y;
				_scrollBar.Width = Width;
            }

            _scrollBar.Draw();
        }

        public override void Update(double timeSinceFrame)
        {
			if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Pressed && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed &&
				Game.PreviousMousePosition.X > _scrollBar.X - Camera.X && Game.PreviousMousePosition.X < _scrollBar.X + _scrollBar.Width - Camera.X &&
				Game.PreviousMousePosition.Y > _scrollBar.Y - Camera.Y && Game.PreviousMousePosition.Y < _scrollBar.Y + _scrollBar.Height - Camera.Y)
			{
				_scrolling = true;
			} else if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Released)
			{
				_scrolling = false;
			}
			if (_scrolling)
            {
                if (_horizontal)
                {
					_currentValue = Game.MousePosition.X - (int)(_scrollBar.Width / 2);
					_currentValue = Math.Max(0, Math.Min((int) (Game.WindowWidth - _scrollBar.Width), _currentValue));
                }
                else
                {
					_currentValue = Game.MousePosition.Y - (int)(_scrollBar.Height / 2);
					_currentValue = Math.Max(0, Math.Min((int) (Game.WindowHeight - _scrollBar.Height), _currentValue));
                }
            }

            if (_horizontal)
            {
				Camera.X = (int)((_currentValue * (_maxValue - _minValue) / (Game.WindowWidth - _scrollBar.Width)) + _minValue);
            }
            else
            {
				Camera.Y = (int)((_currentValue * (_maxValue - _minValue) / (Game.WindowHeight - _scrollBar.Height)) + _minValue);
            }
        }
    }
}