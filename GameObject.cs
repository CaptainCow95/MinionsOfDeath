﻿﻿using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    internal abstract class GameObject
    {
        protected DoublePoint _pos = new DoublePoint(0, 0);
        private List<Sprite> _sprites;
        private int _state;
		protected double _left, _right, _top, _bottom;

        public GameObject(List<Sprite> sprites)
        {
            _sprites = sprites;

            if (_sprites.Count > 0)
            {
				_left = 0;
				_right = _sprites[0].Width;
				_top = 0;
				_bottom = _sprites[0].Height;
            }
        }

		public bool IsCollidingWith(GameObject obj)
		{
			if (Pos.X + _left < obj.Pos.X + obj._right && Pos.X + _right > obj.Pos.X + obj._left &&
				Pos.Y + _top < obj.Pos.Y + obj._bottom && Pos.Y + _bottom > obj.Pos.Y + obj._top)
				return true;
			return false;
		}

        public DoublePoint Pos
        {
            set { _pos = value; }
            get { return _pos; }
        }

        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Draw()
        {
            _sprites[_state].X = _pos.X;
            _sprites[_state].Y = _pos.Y;
            _sprites[_state].Draw();
        }

        public Sprite GetSprite()
        {
            return _sprites[_state];
        }

        public virtual void Update(double time)
        {
            _sprites[_state].Update(time);
        }
    }
}