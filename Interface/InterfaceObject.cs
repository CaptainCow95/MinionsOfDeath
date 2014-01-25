﻿namespace MinionsOfDeath.Interface
{
    public abstract class InterfaceObject
    {
        private int _height;
        private int _width;
        private int _x;
        private int _y;

        public InterfaceObject(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public abstract void Draw();

        public abstract void Update(double timeSinceFrame);
    }
}