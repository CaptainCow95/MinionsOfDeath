﻿using MinionsOfDeath.Graphics;

namespace MinionsOfDeath
{
	internal abstract class GameObject
    {
		private Sprite[] _sprites;
		private int _state;
		protected FloatPoint _pos;

		public GameObject(Sprite[] sprites)
		{
			_sprites = sprites;
		}

		public int State
		{
			get { return _state; }
			set { _state = value; }
		}

		public Sprite getSprite()
		{
			return _sprites[_state];
		}

		public abstract void update(double time);
    }
}