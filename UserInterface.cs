using MinionsOfDeath.Graphics;
using MinionsOfDeath.Interface;
using System.Collections.Generic;
using System.Linq;

namespace MinionsOfDeath
{
    public class UserInterface
    {
        private Button _editTree;
        private Button _makeSpecialMinion;
        private ScrollBar _mapScroll;
        private Button _player1Go;
        private StackPanel _player1TreeRoot;
        private Button _player2Go;
        private StackPanel _player2TreeRoot;
        private UserInterfaceState _state;
        private Dictionary<Button, StackPanel> _player1ParentList = new Dictionary<Button,StackPanel>();
        private Dictionary<Button, StackPanel> _player2ParentList = new Dictionary<Button, StackPanel>();
        private Select _editSelect;

        public UserInterface()
        {
            _player1TreeRoot = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }));
            Button player1GoToBase = new Button(0, 0, 100, 100, false, "Go To\nBase", new Sprite(new List<string>() { "Images/blueButton.png" }));
            _player1TreeRoot.Children.Add(player1GoToBase);
            _player1ParentList.Add(player1GoToBase, _player1TreeRoot);
            _player1TreeRoot.Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" })));
            _player2TreeRoot = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }));
            Button player2GoToBase = new Button(0, 0, 100, 100, false, "Go To\nBase", new Sprite(new List<string>() { "Images/blueButton.png" }));
            _player2TreeRoot.Children.Add(player2GoToBase);
            _player2ParentList.Add(player2GoToBase, _player2TreeRoot);
            _player2TreeRoot.Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" })));

            _player1Go = new Button(0, 600, 100, 100, true, "Go to\nPlayer 2", new Sprite(new List<string>() { "Images/blueButton.png" }));

            _player2Go = new Button(0, 600, 100, 100, true, "Run\nSimulation", new Sprite(new List<string>() { "Images/blueButton.png" }));

            _makeSpecialMinion = new Button(0, 500, 100, 100, true, "Make Special\nMinion", new Sprite(new List<string>() { "Images/blueButton.png" }));
            _editTree = new Button(0, 400, 100, 100, true, "Edit\nBehaviours", new Sprite(new List<string>() { "Images/blueButton.png" }));

            _mapScroll = new ScrollBar(970, 0, 30, 1800, false, 0, 1800, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }));

            _editSelect = new Select(0, 0, false, new List<string>() { "Is Enemy\nClose", "Is 1 \nEnemy Left", "Is 2\nEnemies Left", "Is Enemy\nOn My Half", "Is Nearest\nEnemy Moving\nAway", "Is Nearest\nEnemy Moving\nTowards" }, new Sprite(new List<string>() { "Images/blueButton.png" }), new List<string>() { "Images/blueButton.png" });

            _state = UserInterfaceState.Player1MinionSelect;

            Sound.Strategize.PlayLooping();
        }

        public void Draw()
        {
            Game.Player1.Draw();
            Game.Player2.Draw();

            switch (_state)
            {
                case UserInterfaceState.Player1EditMinionTree:
                    if (_inSelectMode)
                    {
                        _editSelect.Draw();
                    }
                    else
                    {
                        _player1TreeRoot.Draw();
                    }
                    break;

                case UserInterfaceState.Player1MinionSelect:
                    _player1Go.Draw();
                    _makeSpecialMinion.Draw();
                    _editTree.Draw();
                    _mapScroll.Draw();
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    _player2TreeRoot.Draw();
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
        private bool _inSelectMode = false;
        private Button _inSelectModeButton;
        public void Update(double lastFrameTime)
        {
            switch (_state)
            {
                case UserInterfaceState.Player1EditMinionTree:
                    if (_inSelectMode)
                    {
                        _editSelect.Update(lastFrameTime);

                        if (_editSelect.Canceled)
                        {
                            _inSelectMode = false;
                            _editSelect.Reset();
                        }
                        else if (_editSelect.SelectedIndex != -1)
                        {
                            int index = _player1ParentList[_inSelectModeButton].Children.IndexOf(_inSelectModeButton);

                            bool query = true;
                            if (query)
                            {
                                _player1ParentList[_inSelectModeButton].Children.Remove(_inSelectModeButton);
                                StackPanel panel = new StackPanel(0, 0, false, false);
                                StackPanel panel2 = new StackPanel(0, 0, false, true);
                                panel.Children.Add(panel2);
                                Button button = new Button(0, 0, 100, 100, false, "Go To\nBase", new Sprite(new List<string>() { "Images/blueButton.png" }));
                                panel.Children.Add(button);
                                _player1ParentList.Add(button, panel);

                                Button queryButton = new Button(0, 0, 100, 100, false, "query", new Sprite(new List<string>() { "Images/blueButton.png" }));
                                panel2.Children.Add(queryButton);
                                _player1ParentList.Add(queryButton, panel2);

                                Button queryChildButton = new Button(0, 0, 100, 100, false, "Go To\nBase", new Sprite(new List<string>() { "Images/blueButton.png" }));
                                panel2.Children.Add(queryChildButton);
                                _player1ParentList.Add(queryChildButton, panel2);

                                _player1ParentList[_inSelectModeButton].Children.Insert(index, panel);
                                _player1ParentList.Remove(_inSelectModeButton);
                            }
                            else
                            {
                                _player1ParentList[_inSelectModeButton].Children.Remove(_inSelectModeButton);
                                Button button = new Button(0, 0, 100, 100, false, "blah", new Sprite(new List<string>() { "Images/blueButton.png" }));
                                _player1ParentList[_inSelectModeButton].Children.Insert(index, button);

                                _player1ParentList.Add(button, _player1ParentList[_inSelectModeButton]);
                                _player1ParentList.Remove(_inSelectModeButton);
                            }

                            _inSelectMode = false;
                            _editSelect.Reset();
                        }
                    }
                    else
                    {
                        _player1TreeRoot.Update(lastFrameTime);

                        for (int i = 0; i < _player1ParentList.Keys.Count; ++i)
                        {
                            Button button = _player1ParentList.Keys.ElementAt(i);
                            if (button.Pressed)
                            {
                                _inSelectMode = true;
                                _inSelectModeButton = button;
                            }
                        }
                    }

                    if (((Button)_player1TreeRoot.Children[_player1TreeRoot.Children.Count - 1]).Pressed)
                    {
                        _state = UserInterfaceState.Player1MinionSelect;
                        Camera.X = 0;
                        Camera.Y = 0;
                    }
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
                    _player2TreeRoot.Update(lastFrameTime);

                    if (((Button)_player2TreeRoot.Children[_player2TreeRoot.Children.Count - 1]).Pressed)
                    {
                        _state = UserInterfaceState.Player2MinionSelect;
                        Camera.X = 0;
                        Camera.Y = 0;
                    }
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Update(lastFrameTime);
                    _makeSpecialMinion.Update(lastFrameTime);
                    _editTree.Update(lastFrameTime);
                    _mapScroll.Update(lastFrameTime);

                    if (_player2Go.Pressed)
                    {
                        _state = UserInterfaceState.Running;
                        Sound.StopAll();
                        Sound.Combat.PlayLooping();
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

                    Game.Player1.Update(lastFrameTime);
                    Game.Player2.Update(lastFrameTime);

                    // Check for collisions
                    List<int> player1MinionsToRemove = new List<int>();
                    List<int> player2MinionsToRemove = new List<int>();
                    foreach (var player1Minion in Game.Player1.Minions)
                    {
                        foreach (var player2Minion in Game.Player2.Minions)
                        {
                            if (player1Minion.Value.IsCollidingWith(player2Minion.Value))
                            {
                                player1MinionsToRemove.Add(player1Minion.Key);
                                player2MinionsToRemove.Add(player2Minion.Key);
                            }
                        }
                    }

                    player1MinionsToRemove.ForEach(f => Game.Player1.Minions.Remove(f));
                    player2MinionsToRemove.ForEach(f => Game.Player2.Minions.Remove(f));
                    break;
            }
        }
    }
}