using MinionsOfDeath.Graphics;
using System;
using System.Diagnostics;

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
                _scrollBar.X = _currentValue / (_maxValue + _scrollBar.Width - _minValue) * (_maxValue - _minValue);
                _scrollBar.Y = Y;
                _scrollBar.Height = Height;
            }
            else
            {
                _scrollBar.X = X;
                _scrollBar.Y = _currentValue - _minValue;
                _scrollBar.Width = Width;
            }

            _scrollBar.Draw();
        }

        public override void Update(double timeSinceFrame)
        {
            if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Pressed && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed &&
               Game.PreviousMousePosition.X > _scrollBar.X - Camera.X && Game.PreviousMousePosition.X < _scrollBar.X + _scrollBar.Width - Camera.Y &&
               Game.PreviousMousePosition.Y > _scrollBar.Y - Camera.X && Game.PreviousMousePosition.Y < _scrollBar.Y + _scrollBar.Height - Camera.Y)
            {
                if (_horizontal)
                {
                    _currentValue = Game.MousePosition.X + Camera.X - _minValue;
                }
                else
                {
                    _currentValue = Game.MousePosition.Y + Camera.Y - _minValue;
                }

                _currentValue = Math.Max(_minValue, Math.Min(_maxValue, _currentValue));

                if (_horizontal)
                {
                    //Camera.X = (int)Math.Max(_minValue, _currentValue * 2 - Game.WindowWidth);
                    Camera.X = (int)((_currentValue / (_maxValue - _scrollBar.Width - _minValue)) * (_maxValue - Game.WindowWidth - _minValue));
                }
                else
                {
                    Camera.Y = (int)Math.Max(_minValue, _currentValue * 2 - Game.WindowHeight);
                }
            }
        }
    }
}