﻿using MinionsOfDeath.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath
{
    public class Base : GameObject
    {
        private int _pid;

        public Base(List<Sprite> sprites, int PlayerID)
            : base(sprites)
        {
            _pid = PlayerID;
			_left = 0;
			_right = 202;
			_top = 0;
			_bottom = 134;
        }

        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public override void Draw()
        {
            //GetSprite().X = _pos.X + _left - _right;
            //GetSprite().Y = _pos.Y + _top - _bottom;
            //GetSprite().Draw();
        }
    }
}
