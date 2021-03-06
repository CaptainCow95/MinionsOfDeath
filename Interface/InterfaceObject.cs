﻿namespace MinionsOfDeath.Interface
{
    public abstract class InterfaceObject
    {
        private int _height;
        private bool _static;
        private int _width;
        private int _x;
        private int _y;

        public InterfaceObject(int x, int y, int width, int height, bool staticImage)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _static = staticImage;
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

        protected bool Static
        {
            get { return _static; }
        }

        public abstract void Draw();

        public abstract void Update(double timeSinceFrame);
    }
}