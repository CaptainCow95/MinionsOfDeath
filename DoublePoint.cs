using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath
{
    class DoublePoint
    {
        public DoublePoint(double x,double y){
            _x = x;
            _y = y;
        }

        private double _x, _y;

        public double X
        {
			get { return _x; }
			set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public void SetToLessOrEqualMag(double mag)
		{
			double m = Math.Sqrt((_x*_x) + (_y*_y));
			if (m < mag) return;
			_x /= m;
			_y /= m;
		}

		public void Add(DoublePoint point)
		{
			_x += point._x;
			_y += point._y;
		}
    }
}
