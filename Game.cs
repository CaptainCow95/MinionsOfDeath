using MinionsOfDeath.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MinionsOfDeath
{
    public class Game : GameWindow
    {
        private Sprite _sprite;

        public Game()
        {
            this.UpdateFrame += Game_UpdateFrame;
            this.RenderFrame += Game_RenderFrame;

            _sprite = new Sprite("XRay-High.png");
        }

        private void Game_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, -1, 1);

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);

            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.ClearColor(OpenTK.Graphics.Color4.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Camera.Begin();

            _sprite.Draw();

            Camera.End();

            SwapBuffers();
        }

        private void Game_UpdateFrame(object sender, FrameEventArgs e)
        {
        }
    }
}