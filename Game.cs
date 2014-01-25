using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MinionsOfDeath
{
    public class Game : GameWindow
    {
        public Game()
        {
            this.UpdateFrame += Game_UpdateFrame;
            this.RenderFrame += Game_RenderFrame;
        }

        private void Game_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Viewport(this.ClientRectangle);
            GL.Ortho(0, Width, Height, 0, 0, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.ClearColor(OpenTK.Graphics.Color4.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }

        private void Game_UpdateFrame(object sender, FrameEventArgs e)
        {
        }
    }
}