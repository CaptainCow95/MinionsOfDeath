using System;

namespace MinionsOfDeath
{
    internal class DoublePoint
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

        public void SetToLessOrEqualMag(double mag)
        {
            double m = Math.Sqrt((_x * _x) + (_y * _y));
            if (m < mag) return;
            _x /= m;
            _y /= m;
        }
    }
}