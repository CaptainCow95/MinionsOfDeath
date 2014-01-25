using System;
using MinionsOfDeath.Graphics;

namespace MinionsOfDeath.Interface
{
	public class ScrollBar : InterfaceObject
	{
		private int _minValue;
		private int _maxValue;
		private bool _horizontal;
		private Sprite _scrollBar;
		private int _currentValue;
		public ScrollBar (int x, int y, int width, int height, int minValue, int maxValue, bool horizontal, Sprite scrollBar):base(x,y,width,height)
		{
			_minValue = minValue;
			_maxValue = maxValue;
			_horizontal = horizontal;
			_currentValue = minValue;
			_scrollBar = scrollBar;
		}

		public override void Draw ()
		{
			if (_horizontal) {
				_scrollBar.X = ((double)_currentValue / (double)(_maxValue - _minValue)) * (Width - _scrollBar.Width);
				_scrollBar.Y = Y + Height / 2;
			} else {
			}
			_scrollBar.Draw ();
		}

		public override void Update (double timeSinceFrame)
		{
		}
	}
}

