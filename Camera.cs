using OpenTK.Graphics.OpenGL;

namespace MinionsOfDeath
{
    public static class Camera
    {
        private static int _x = 0;
        private static int _y = 0;

        public static int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public static int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public static void Begin()
        {
            GL.Translate(-_x, -_y, 0);
        }

        public static void End()
        {
            GL.Translate(_x, _y, 0);
        }
    }
}