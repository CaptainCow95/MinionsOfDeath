﻿using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    internal abstract class GameObject
    {
        protected DoublePoint _pos = new DoublePoint(0, 0);
        private double _height;
        private List<Sprite> _sprites;
        private int _state;
        private double _width;

        public GameObject(List<Sprite> sprites)
        {
            _sprites = sprites;

            if (_sprites.Count > 0)
            {
                _width = _sprites[0].Width;
                _height = _sprites[0].Height;
            }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
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

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public void Draw()
        {
            _sprites[_state].X = _pos.X;
            _sprites[_state].Y = _pos.Y;
            _sprites[_state].Width = _width;
            _sprites[_state].Height = _height;
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