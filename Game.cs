using MinionsOfDeath.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    public class Game : GameWindow
    {
        public static KeyboardState KeyboardState;
        public static MouseState MouseState;
        public static KeyboardState PreviousKeyboardState;
        public static MouseState PreviousMouseState;
        private GameState _gameState;
        private Sprite _map;

        public Game()
        {
            this.UpdateFrame += Game_UpdateFrame;
            this.RenderFrame += Game_RenderFrame;

            this.WindowBorder = WindowBorder.Fixed;
            this.Width = 1000;
            this.Height = 700;

            _gameState = GameState.Running;
            _map = new Sprite(new List<string>() { "Images\\Map1.png" });
            _map.Width *= 2;
            _map.Height *= 2;
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

            switch (_gameState)
            {
                case GameState.PlanningTeam1:
                    break;

                case GameState.PlanningTeam2:
                    break;

                case GameState.Running:
                    _map.Draw();
                    break;
            }

            Camera.End();

            SwapBuffers();
        }

        private void Game_UpdateFrame(object sender, FrameEventArgs e)
        {
            PreviousMouseState = MouseState;
            PreviousKeyboardState = KeyboardState;

            MouseState = OpenTK.Input.Mouse.GetState();
            KeyboardState = OpenTK.Input.Keyboard.GetState();

            switch (_gameState)
            {
                case GameState.PlanningTeam1:
                    break;

                case GameState.PlanningTeam2:
                    break;

                case GameState.Running:
                    break;
            }
        }
    }
}