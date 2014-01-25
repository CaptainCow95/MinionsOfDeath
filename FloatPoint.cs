using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath
{
    class FloatPoint
    {
        public FloatPoint(float x,float y){
            _x = x;
            _y = y;
        }

        private float _x, _y;

        public float X
        {
			get { return _x; }
			set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

		public void SetToLessOrEqualMag(float mag)
		{
			float m = Math.Sqrt((_x*_x) + (_y*_y));
			if (m < mag) return;
			_x /= m;
			_y /= m;
		}

		public void Add(FloatPoint point)
		{
			_x += point._x;
			_y += point._y;
		}
    }
}
