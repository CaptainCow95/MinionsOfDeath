using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        void Game_UpdateFrame(object sender, FrameEventArgs e)
        {

        }

        void Game_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Viewport(this.ClientRectangle);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

			GL.ClearColor(OpenTK.Graphics.Color4.CornflowerBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
        }
    }
}
