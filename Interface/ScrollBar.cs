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
        private Sprite _scrollBar;

        public ScrollBar(int x, int y, int width, int height, int minValue, int maxValue, bool horizontal, Sprite scrollBar)
            : base(x, y, width, height)
        {
            _minValue = minValue;
            _maxValue = maxValue;
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
                _scrollBar.X = ((double)_currentValue / (double)(_maxValue - _minValue)) * (Width - _scrollBar.Width);
                _scrollBar.Y = Y;
                _scrollBar.Height = Height;
            }
            else
            {
                _scrollBar.X = X;
                _scrollBar.Y = ((double)_currentValue / (double)(_maxValue - _minValue)) * (Height - _scrollBar.Height);
                _scrollBar.Width = Width;
            }

            _scrollBar.Draw();
        }

        public override void Update(double timeSinceFrame)
        {
            if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Pressed && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed &&
               Game.PreviousMouseState.X > _scrollBar.X && Game.PreviousMouseState.X < _scrollBar.X + _scrollBar.Width &&
               Game.PreviousMouseState.Y > _scrollBar.Y && Game.PreviousMouseState.Y < _scrollBar.Y + _scrollBar.Height)
            {
                if (_horizontal)
                {
                    _currentValue = (Game.MouseState.X - _minValue) / (_maxValue - _minValue);
                }
                else
                {
                    _currentValue = (Game.MouseState.Y - _minValue) / (_maxValue - _minValue);
                }

                _currentValue = Math.Max(_minValue, Math.Min(_maxValue, _currentValue));

                if (_horizontal)
                {
                    Camera.X = Math.Min(_minValue, _currentValue - Game.WindowWidth);
                }
                else
                {
                    Camera.Y = Math.Min(_minValue, _currentValue - Game.WindowHeight);
                }
            }
        }
    }
}