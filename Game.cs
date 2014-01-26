// #define GRAPHMAKER

using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Behaviors.Actions;
using MinionsOfDeath.Graphics;
using MinionsOfDeath.Navigation;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Drawing;

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
        private static Player _player1;
        private static Player _player2;
        private Sprite _map;
        private UserInterface _ui;
#if GRAPHMAKER
        XDocument doc = new XDocument();
        WaypointGraph graph = new WaypointGraph("TestWaypoint.xml");
        int rightClicks = 0;
        int[] clicks = new int[4];
        int minID = 5;
#endif

        public Game()
        {
            WaypointGraph.init("TestWaypointNeighbors.xml");
            this.Title = "Minions of Death";
            this.UpdateFrame += Game_UpdateFrame;
            this.RenderFrame += Game_RenderFrame;

            Mouse.Move += Mouse_Move;

            this.WindowBorder = WindowBorder.Fixed;
            this.Width = 1000;
            WindowWidth = this.Width;
            this.Height = 700;
            WindowHeight = this.Height;

            Sound.Load();

            InitRunningState();

			_map = new Sprite(new List<string>() { "Images/Map1.png" }, 0);

            _ui = new UserInterface();

#if GRAPHMAKER
            doc.Add(new XElement("Waypoints"));
#endif
        }

        public static Player Player1
        {
            get { return _player1; }
        }

        public static Player Player2
        {
            get { return _player2; }
        }

        public static int WindowHeight { get; private set; }

        public static int WindowWidth { get; private set; }

        public void InitRunningState()
        {
            _player1 = new Player(1);
            Minion minion1 = new Minion(new List<Sprite>() {
				new Sprite(new List<string>() { "Images/BlueMinion.png" }, 0),
				new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }, 0.3),
				new Sprite(new List<string>() { "Images/BlueMinionBack0.png", "Images/BlueMinionBack1.png"}, 0.3),
				new Sprite(new List<string>() { "Images/LeftWalkBlue0.png", "Images/LeftWalkBlue1.png", "Images/LeftWalkBlue2.png"}, 0.2),
				new Sprite(new List<string>() { "Images/RightWalkBlue0.png", "Images/RightWalkBlue1.png", "Images/RightWalkBlue2.png"}, 0.2),
				}, 0);
            minion1.State = 1;
            _player1.AddMinion(minion1);
            _player1.Base = new Base((List<Sprite>)null, 1);

            _player2 = new Player(2);
            Minion minion2 = new Minion(new List<Sprite>() {
				new Sprite(new List<string>() { "Images/RedMinion.png" }, 0),
				new Sprite(new List<string>() { "Images/RedMinion0.png", "Images/RedMinion1.png" }, 0.3),
				new Sprite(new List<string>() { "Images/RedMinionBack0.png", "Images/RedMinionBack1.png"}, 0.3),
				new Sprite(new List<string>() { "Images/LeftWalkRed0.png", "Images/LeftWalkRed1.png", "Images/LeftWalkRed2.png"}, 0.2),
				new Sprite(new List<string>() { "Images/RightWalkRed0.png", "Images/RightWalkRed1.png", "Images/RightWalkRed2.png"}, 0.2),
			}, 0);
            minion2.State = 1;
            _player2.AddMinion(minion2);
            _player2.Base = new Base((List<Sprite>)null, 2);

            minion1.Pos.X = 160;
            minion1.Pos.Y = 47;
            //minion1.DecisionTree = new DecisionTree(minion1, new AttackClosest(minion1));
            //minion1.Pos.X = 495;
            //minion1.Pos.Y = 1777;
            minion1.DecisionTree = new DecisionTree(minion1, new GotoBase(minion1));

            minion2.Pos.X = 690;
            minion2.Pos.Y = 912;
            //minion2.DecisionTree = new DecisionTree(minion2, new SeekAction(minion2, minion2));
            //minion2.Pos.X = 499;
            //minion2.Pos.Y = 45;
            minion2.DecisionTree = new DecisionTree(minion2, new GotoBase(minion2));
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

            _map.Draw();

            _ui.Draw();

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
            int x = Camera.X + MousePosition.X;
            int y = Camera.Y + MousePosition.Y;

            if (MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released && MousePosition.X > 40) {
               doc.Element("Waypoints").Add(new XElement("Waypoint", new XAttribute("X", x), new XAttribute("Y", y)));
               minID++;
               Minion markerMinion = new Minion(new List<Sprite>() { new Sprite(new List<string>() { "Images/BlueMinion.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }) }, minID);
                markerMinion.Pos.X = x;
                markerMinion.Pos.Y = y;
                _player1.AddMinion(markerMinion);
              // doc.Save("TestWaypoint.xml");
            XDocument doc = new XDocument();
            doc.Add(new XElement("Waypoints"));
            if (MouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released) {
            }

            if (MouseState.RightButton == ButtonState.Pressed && PreviousMouseState.RightButton == ButtonState.Released && MousePosition.X > 40)
            {
                switch (rightClicks)
                {
                    case 0: clicks[0] = x;
                        clicks[1] = y;
                        break;

                    case 1:
                        clicks[2] = x;
                        clicks[3] = y;
                         minID++;
                         Minion markerMinion = new Minion(new List<Sprite>() { new Sprite(new List<string>() { "Images/square.png" }), new Sprite(new List<string>() { "Images/square.png", "Images/square.png" }) }, minID);
                         WaypointNode waypoint1 = graph.GetClosestWaypoint(clicks[0], clicks[1]);
                         WaypointNode waypoint2 = graph.GetClosestWaypoint(clicks[2], clicks[3]);
                         markerMinion.Pos.X = waypoint1.X + (waypoint2.X - waypoint1.X) / 2;
                         markerMinion.Pos.Y = waypoint1.Y + (waypoint2.Y - waypoint1.Y) / 2;
                _player1.AddMinion(markerMinion);
                graph.ConnectNodes(waypoint1, waypoint2);
                        Console.Write("Added neighbor");
                        break;
                }
                rightClicks++;
                rightClicks = rightClicks % 2;
                graph.WriteGraph("TestWaypointNeighbors.xml");
            }

            _scrollBar.Update(e.Time);
#else

            _map.Update(e.Time);

            _ui.Update(e.Time);

#endif
        }

        private void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
            MousePosition = e.Position;
        }
    }
}