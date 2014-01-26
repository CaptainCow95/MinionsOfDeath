using MinionsOfDeath.Graphics;
using MinionsOfDeath.Interface;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    public class UserInterface
    {
        private Button _editTree;
        private Button _makeSpecialMinion;
        private Button _player1Go;
        private StackPanel _player1TreeRoot;
        private Button _player2Go;
        private StackPanel _player2TreeRoot;
        private ScrollBar _mapScroll;
        private UserInterfaceState _state;

        public UserInterface()
        {
            _player1TreeRoot = new StackPanel(0, 0, 1000, 700, false, new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }));
            _player2TreeRoot = new StackPanel(0, 0, 1000, 700, false, new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }), new Sprite(new List<string>() { "Images/BlueMinion0.png", "Images/BlueMinion1.png" }));

            _player1Go = new Button(0, 600, 100, 100, true, "Go to\nPlayer 2", new Sprite(new List<string>() { "Images/square.png" }));

            _player2Go = new Button(0, 600, 100, 100, true, "Run\nSimulation", new Sprite(new List<string>() { "Images/square.png" }));

            _makeSpecialMinion = new Button(0, 500, 100, 100, true, "Make Special\nMinion", new Sprite(new List<string>() { "Images/square.png" }));
            _editTree = new Button(0, 400, 100, 100, true, "Edit\nBehaviours", new Sprite(new List<string>() { "Images/square.png" }));

            _mapScroll = new ScrollBar(970, 0, 30, 1800, false, 0, 1800, false, new Sprite(new List<string>() { "Images/rightWalkred1.png" }));

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
                    _makeSpecialMinion.Draw();
                    _editTree.Draw();
                    _mapScroll.Draw();
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Draw();
                    _makeSpecialMinion.Draw();
                    _editTree.Draw();
                    _mapScroll.Draw();
                    break;

                case UserInterfaceState.Running:
                    _mapScroll.Draw();
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
                    _makeSpecialMinion.Update(lastFrameTime);
                    _editTree.Update(lastFrameTime);
                    _mapScroll.Update(lastFrameTime);

                    if (_player1Go.Pressed)
                    {
                        _state = UserInterfaceState.Player2MinionSelect;
                    }
                    if (_makeSpecialMinion.Pressed)
                    {
                        // Make current minion special
                    }
                    if (_editTree.Pressed)
                    {
                        _state = UserInterfaceState.Player1EditMinionTree;
                    }
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Update(lastFrameTime);
                    _makeSpecialMinion.Update(lastFrameTime);
                    _editTree.Update(lastFrameTime);
                    _mapScroll.Update(lastFrameTime);

                    if (_player2Go.Pressed)
                    {
                        _state = UserInterfaceState.Running;
                    }
                    if (_makeSpecialMinion.Pressed)
                    {
                        // Make current minion special
                    }
                    if (_editTree.Pressed)
                    {
                        _state = UserInterfaceState.Player2EditMinionTree;
                    }
                    break;

                case UserInterfaceState.Running:
                    _mapScroll.Update(lastFrameTime);
                    break;
            }
        }
    }
}