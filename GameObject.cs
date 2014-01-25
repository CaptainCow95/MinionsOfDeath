using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
	internal abstract class GameObject
    {
		private List<Sprite> _sprites;
		private int _state;
		protected DoublePoint _pos;

		public GameObject(List<Sprite> sprites)
		{
			_sprites = sprites;
		}

		public int State
		{
			get { return _state; }
			set { _state = value; }
		}

		public Sprite GetSprite()
		{
			return _sprites[_state];
		}

		public abstract void Update(double time);
    }
}