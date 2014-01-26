﻿using System;

namespace MinionsOfDeath
{
    public class DoublePoint
    {
        private double _x, _y;

        public DoublePoint(double x, double y)
        {
            _x = x;
            _y = y;
        }

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

        public void Add(DoublePoint point)
        {
            _x += point._x;
            _y += point._y;
        }

		public void Set(DoublePoint point)
		{
			_x = point._x;
			_y = point._y;
		}

		public bool SetToLessOrEqualMag(double mag)
        {
			double m = GetMag();
			if (m <= mag) return true;
            _x /= m;
            _y /= m;
			return false;
        }

		public double GetMag()
		{
			return Math.Sqrt((_x * _x) + (_y * _y));
		}
    }
}