#define GRAPHMAKER

using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Graphics;
using MinionsOfDeath.Interface;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace MinionsOfDeath
{
    public class Game : GameWindow
    {
        public static KeyboardState KeyboardState;
        public static Point MousePosition;
        public static MouseState MouseState;
        public static KeyboardState PreviousKeyboardState;
        public static Point PreviousMousePosition;
        public static MouseState PreviousMouseState;
        private GameState _gameState;
        private Sprite _map;

        private Player _player1;
        private Player _player2;
        private ScrollBar _scrollBar;

        public Game()
        {
            this.UpdateFrame += Game_UpdateFrame;
            this.RenderFrame += Game_RenderFrame;

            Mouse.Move += Mouse_Move;

            this.WindowBorder = WindowBorder.Fixed;
            this.Width = 1000;
            WindowWidth = this.Width;
            this.Height = 700;
            WindowHeight = this.Height;

            _gameState = GameState.Running;
            InitRunningState();
            _map = new Sprite(new List<string>() { "Images/testMap.png" });
            //_map.Width *= 2;
            //_map.Height *= 2;

            //_scrollBar = new ScrollBar(0, 400, 2000, 40, 0, 2000, true, new Sprite(new List<string>() { "Images/redMinion0.png" }));//horizontal
            _scrollBar = new ScrollBar(0, 0, 40, 1800, 0, 1800, false, new Sprite(new List<string>() { "Images/redMinion0.png" }));//vertical
        }

        public static int WindowHeight { get; private set; }

        public static int WindowWidth { get; private set; }

        public void InitRunningState()
        {
            _player1 = new Player(1);
            Minion minion1 = new Minion(new List<Sprite>() { new Sprite(new List<string>() { "Images/BlueMinion.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }) }, 0);
            minion1.State = 1;
            _player1.AddMinion(minion1);

            _player2 = new Player(2);
            Minion minion2 = new Minion(new List<Sprite>() { new Sprite(new List<string>() { "Images/RedMinion.png" }), new Sprite(new List<string>() { "Images/RedMinion0.png", "Images/RedMinion1.png" }) }, 0);
            minion2.State = 1;
            _player2.AddMinion(minion2);

            minion1.Pos.X = 50;
            minion1.Pos.Y = 900;
            minion1.DecisionTree = new DecisionTree(minion1, new SeekAction(minion1, minion2));

            minion2.Pos.X = 900;
            minion2.Pos.Y = 100;
            minion2.DecisionTree = new DecisionTree(minion2, new SeekAction(minion2, minion1));
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

                    _player1.Draw();
                    _player2.Draw();

                    _scrollBar.Draw();
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

            PreviousMousePosition = MousePosition;

#if GRAPHMAKER
            XDocument doc = new XDocument();
            doc.Add(new XElement("Waypoints"));
            if (MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released) { 
                 doc.Element("Waypoints").Add(new XElement("Waypoint", new XAttribute("X", MousePosition.X), new XAttribute("Y", MousePosition.y), new XElement("Subwaypoint")));
            }
            doc.Save("TestWaypoint.xml");
#else

            switch (_gameState)
            {
                case GameState.PlanningTeam1:
                    break;

                case GameState.PlanningTeam2:
                    break;

                case GameState.Running:
                    _scrollBar.Update(e.Time);
                    _map.Update(e.Time);
                    _player1.Update(e.Time);
                    _player2.Update(e.Time);

                    // Check for collisions
                    List<int> player1MinionsToRemove = new List<int>();
                    List<int> player2MinionsToRemove = new List<int>();
                    foreach (var player1Minion in _player1.Minions)
                    {
                        foreach (var player2Minion in _player2.Minions)
                        {
                            if (player1Minion.Value.IsCollidingWith(player2Minion.Value))
                            {
                                player1MinionsToRemove.Add(player1Minion.Key);
                                player2MinionsToRemove.Add(player2Minion.Key);
                            }
                        }
                    }

                    player1MinionsToRemove.ForEach(f => _player1.Minions.Remove(f));
                    player2MinionsToRemove.ForEach(f => _player2.Minions.Remove(f));

                    break;
            }

#endif
        }

        private void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
			MousePosition = e.Position;
        }
    }
}