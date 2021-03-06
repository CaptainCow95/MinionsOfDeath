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

        public double GetMag()
        {
            return Math.Sqrt((_x * _x) + (_y * _y));
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
			_x *= mag/m;
			_y *= mag/m;
            return false;
        }

		public static DoublePoint GetAverage(DoublePoint a, DoublePoint b)
		{
			return new DoublePoint ((a._x / 2) + (b._x / 2), (a._y / 2) + (b._y / 2));
		}
    }
}