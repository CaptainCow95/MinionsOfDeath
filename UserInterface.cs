using MinionsOfDeath.Graphics;
using MinionsOfDeath.Interface;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    public enum UserInterfaceState
    {
        Player1MinionSelect,
        Player2MinionSelect,
        Player1EditMinionTree,
        Player2EditMinionTree,
        Running,
    }

    public class UserInterface
    {
        private Button _player1Go;
        private StackPanel _player1TreeRoot;
        private Button _player2Go;
        private StackPanel _player2TreeRoot;
        private UserInterfaceState _state;

        public UserInterface()
        {
            _player1TreeRoot = new StackPanel(0, 0, 1000, 700, new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }));
            _player2TreeRoot = new StackPanel(0, 0, 1000, 700, new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }));

            _player1Go = new Button(0, 600, 100, 100, "Go to\nPlayer 2", new Sprite(new List<string>() { "Images/square.png" }));
            _player2Go = new Button(0, 600, 100, 100, "Run Simulation", new Sprite(new List<string>() { "Images/square.png" }));

            _state = UserInterfaceState.Player1MinionSelect;
        }

        public void Draw()
        {
            switch (_state)
            {
                case UserInterfaceState.Player1EditMinionTree:
                    break;

                case UserInterfaceState.Player1MinionSelect:
                    _player1Go.Draw();
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Draw();
                    break;

                case UserInterfaceState.Running:
                    break;
            }
        }

        public void Update(double lastFrameTime)
        {
            switch (_state)
            {
                case UserInterfaceState.Player1EditMinionTree:
                    break;

                case UserInterfaceState.Player1MinionSelect:
                    _player1Go.Update(lastFrameTime);
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Update(lastFrameTime);
                    break;

                case UserInterfaceState.Running:
                    break;
            }
        }
    }
}