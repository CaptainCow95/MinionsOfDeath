using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
	internal abstract class GameObject
    {
		private List<Sprite> _sprites;
		private int _state;
		protected DoublePoint _pos = new DoublePoint(0, 0);

        public DoublePoint Pos
        {
            set { _pos = value; }
            get { return _pos; }
        }

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

        public virtual void Update(double time)
        {
            _sprites[_state].Update(time);
        }

        public void Draw()
        {
            _sprites[_state].X = _pos.X;
            _sprites[_state].Y = _pos.Y;
            _sprites[_state].Draw();
        }
    }
}