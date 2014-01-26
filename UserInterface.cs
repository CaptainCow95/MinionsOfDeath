using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Behaviors.Actions;
using MinionsOfDeath.Behaviors.Queries;
using MinionsOfDeath.Graphics;
using MinionsOfDeath.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinionsOfDeath
{
    public class UserInterface
    {
        private Select _editSelect;
        private Button _editTree;
        private bool _inSelectMode = false;
        private Button _inSelectModeButton;
        private Button _makeSpecialMinion;
        private ScrollBar _mapScroll;
        private Button _player1Go;
        private Dictionary<Button, Tuple<StackPanel, StackPanel>> _player1ParentList = new Dictionary<Button, Tuple<StackPanel, StackPanel>>();
        private StackPanel _player1TreeRoot;
        private Button _player2Go;
        private Dictionary<Button, Tuple<StackPanel, StackPanel>> _player2ParentList = new Dictionary<Button, Tuple<StackPanel, StackPanel>>();
        private StackPanel _player2TreeRoot;
        private List<string> _selectOptions = new List<string>() { "Is Enemy\nClose", "Is 1 \nEnemy Left", "Is 2\nEnemies Left", "Is Enemy\nOn My Half", "Is Nearest\nEnemy Moving\nAway", "Is Nearest\nEnemy Moving\nTowards", "Attack Closest", "Follow Path", "Go To Base", "Run Away", "Wait For Time" };
        private UserInterfaceState _state;

        public UserInterface()
        {
			_player1TreeRoot = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
			Button player1GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player1TreeRoot.Children.Add(player1GoToBase);
            _player1ParentList.Add(player1GoToBase, new Tuple<StackPanel, StackPanel>(_player1TreeRoot, null));
			_player1TreeRoot.Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));
			_player2TreeRoot = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
			Button player2GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player2TreeRoot.Children.Add(player2GoToBase);
            _player2ParentList.Add(player2GoToBase, new Tuple<StackPanel, StackPanel>(_player2TreeRoot, null));
			_player2TreeRoot.Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));

			_player1Go = new Button(0, 600, 100, 100, true, "Go to\nPlayer 2", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

			_player2Go = new Button(0, 600, 100, 100, true, "Run\nSimulation", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

			_makeSpecialMinion = new Button(0, 500, 100, 100, true, "Make Special\nMinion", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
			_editTree = new Button(0, 400, 100, 100, true, "Edit\nBehaviours", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

			_mapScroll = new ScrollBar(970, 0, 30, 1800, false, 0, 1800, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0));

			_editSelect = new Select(0, 0, false, _selectOptions, new Sprite(new List<string>() { "Images/blueButtonSmall.png" }, 0), new List<string>() { "Images/blueButtonSmall.png" });

            _state = UserInterfaceState.Player1MinionSelect;

            Sound.Strategize.PlayLooping();
        }

        public void CreateDecisionTrees()
        {
            DecisionTree tree = new DecisionTree(Game.Player1.Minions.ElementAt(0).Value, GetNode(Game.Player1.Minions.ElementAt(0).Value, _player1TreeRoot.Children[0], _player1TreeRoot, null));
            Game.Player1.Minions.ElementAt(0).Value.DecisionTree = tree;

            DecisionTree tree2 = new DecisionTree(Game.Player2.Minions.ElementAt(0).Value, GetNode(Game.Player2.Minions.ElementAt(0).Value, _player2TreeRoot.Children[0], _player2TreeRoot, null));
			Game.Player2.Minions.ElementAt(0).Value.DecisionTree = tree;
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
                    if (_inSelectMode)
                    {
                        _editSelect.Draw();
                    }
                    else
                    {
                        _player2TreeRoot.Draw();
                    }
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

        public DecisionNode GetNode(Minion owner, InterfaceObject obj, StackPanel parent, StackPanel parentsParent)
        {
            if (obj is Button)
            {
                switch (((Button)obj).Text)
                {
                    case "Is Enemy\nClose":
                        return new IsEnemyClose(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Is 1 \nEnemy Left":
                        return new IsEnemyNum1(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Is 2\nEnemies Left":
                        return new IsEnemyNum2(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Is Enemy\nOn My Half":
                        return new IsEnemyOnMyHalf(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Is Nearest\nEnemy Moving\nAway":
                        return new NearestEnemyMovingAway(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Is Nearest\nEnemy Moving\nTowards":
                        return new NearestEnemyMovingTowards(owner)
                        {
                            FalseChild = GetNode(owner, parentsParent.Children[parentsParent.Children.IndexOf(parent) + 1], parent, parentsParent),
                            TrueChild = GetNode(owner, parent.Children[parent.Children.IndexOf(obj) + 1], parent, parentsParent)
                        };

                    case "Attack Closest":
                        return new AttackClosest(owner);

                    case "Follow Path":
                        return new FollowPath(owner, null);

                    case "Go To Base":
                        return new GotoBase(owner);

                    case "Run Away":
                        return new RunAway(owner);

                    case "Wait For Time":
                        return new WaitForTime(owner);
                }

                return null;
            }
            else // obj is StackPanel
            {
                return GetNode(owner, (obj as StackPanel).Children[0], obj as StackPanel, parent);
            }
        }

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
                            int index = _player1ParentList[_inSelectModeButton].Item1.Children.IndexOf(_inSelectModeButton);

                            bool query = _selectOptions[_editSelect.SelectedIndex].StartsWith("Is");
                            if (query)
                            {
                                if (index == -1)
                                {
                                    index = _player1ParentList[_inSelectModeButton].Item1.Children.IndexOf(_player1ParentList[_inSelectModeButton].Item2);
                                    _player1ParentList[_inSelectModeButton].Item1.Children.Remove(_player1ParentList[_inSelectModeButton].Item2);
                                }
                                else
                                {
                                    _player1ParentList[_inSelectModeButton].Item1.Children.Remove(_inSelectModeButton);
                                }

                                StackPanel panel = new StackPanel(0, 0, false, false);
                                StackPanel panel2 = new StackPanel(0, 0, false, true);
                                panel.Children.Add(panel2);
							Button button = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel.Children.Add(button);
                                _player1ParentList.Add(button, new Tuple<StackPanel, StackPanel>(panel, null));

							Button queryButton = new Button(0, 0, 100, 100, false, _selectOptions[_editSelect.SelectedIndex], new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel2.Children.Add(queryButton);
                                _player1ParentList.Add(queryButton, new Tuple<StackPanel, StackPanel>(_player1ParentList[_inSelectModeButton].Item1, panel));

							Button queryChildButton = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel2.Children.Add(queryChildButton);
                                _player1ParentList.Add(queryChildButton, new Tuple<StackPanel, StackPanel>(panel2, null));

                                _player1ParentList[_inSelectModeButton].Item1.Children.Insert(index, panel);
                                _player1ParentList.Remove(_inSelectModeButton);
                            }
                            else
                            {
                                if (index == -1)
                                {
                                    index = _player1ParentList[_inSelectModeButton].Item1.Children.IndexOf(_player1ParentList[_inSelectModeButton].Item2);
                                    _player1ParentList[_inSelectModeButton].Item1.Children.Remove(_player1ParentList[_inSelectModeButton].Item2);
                                }
                                else
                                {
                                    _player1ParentList[_inSelectModeButton].Item1.Children.Remove(_inSelectModeButton);
                                }

							Button button = new Button(0, 0, 100, 100, false, _selectOptions[_editSelect.SelectedIndex], new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                _player1ParentList[_inSelectModeButton].Item1.Children.Insert(index, button);

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
                            int index = _player2ParentList[_inSelectModeButton].Item1.Children.IndexOf(_inSelectModeButton);

                            bool query = _selectOptions[_editSelect.SelectedIndex].StartsWith("Is");
                            if (query)
                            {
                                if (index == -1)
                                {
                                    index = _player2ParentList[_inSelectModeButton].Item1.Children.IndexOf(_player2ParentList[_inSelectModeButton].Item2);
                                    _player2ParentList[_inSelectModeButton].Item1.Children.Remove(_player2ParentList[_inSelectModeButton].Item2);
                                }
                                else
                                {
                                    _player2ParentList[_inSelectModeButton].Item1.Children.Remove(_inSelectModeButton);
                                }

                                StackPanel panel = new StackPanel(0, 0, false, false);
                                StackPanel panel2 = new StackPanel(0, 0, false, true);
                                panel.Children.Add(panel2);
							Button button = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel.Children.Add(button);
                                _player2ParentList.Add(button, new Tuple<StackPanel, StackPanel>(panel, null));

							Button queryButton = new Button(0, 0, 100, 100, false, _selectOptions[_editSelect.SelectedIndex], new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel2.Children.Add(queryButton);
                                _player2ParentList.Add(queryButton, new Tuple<StackPanel, StackPanel>(_player2ParentList[_inSelectModeButton].Item1, panel));

							Button queryChildButton = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                panel2.Children.Add(queryChildButton);
                                _player2ParentList.Add(queryChildButton, new Tuple<StackPanel, StackPanel>(panel2, null));

                                _player2ParentList[_inSelectModeButton].Item1.Children.Insert(index, panel);
                                _player2ParentList.Remove(_inSelectModeButton);
                            }
                            else
                            {
                                if (index == -1)
                                {
                                    index = _player2ParentList[_inSelectModeButton].Item1.Children.IndexOf(_player2ParentList[_inSelectModeButton].Item2);
                                    _player2ParentList[_inSelectModeButton].Item1.Children.Remove(_player2ParentList[_inSelectModeButton].Item2);
                                }
                                else
                                {
                                    _player2ParentList[_inSelectModeButton].Item1.Children.Remove(_inSelectModeButton);
                                }

							Button button = new Button(0, 0, 100, 100, false, _selectOptions[_editSelect.SelectedIndex], new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
                                _player2ParentList[_inSelectModeButton].Item1.Children.Insert(index, button);

                                _player2ParentList.Add(button, _player2ParentList[_inSelectModeButton]);
                                _player2ParentList.Remove(_inSelectModeButton);
                            }

                            _inSelectMode = false;
                            _editSelect.Reset();
                        }
                    }
                    else
                    {
                        _player2TreeRoot.Update(lastFrameTime);

                        for (int i = 0; i < _player2ParentList.Keys.Count; ++i)
                        {
                            Button button = _player2ParentList.Keys.ElementAt(i);
                            if (button.Pressed)
                            {
                                _inSelectMode = true;
                                _inSelectModeButton = button;
                            }
                        }
                    }

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
                        CreateDecisionTrees();
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
                    foreach (var player1Minion in Game.Player1.Minions)
                    {
                        if (player1Minion.Value.IsCollidingWith(Game.Player2.Base))
                        {
                            //TODO: win state
                        }
                    }
                    foreach (var player2Minion in Game.Player2.Minions)
                    {
                        if (player2Minion.Value.IsCollidingWith(Game.Player1.Base))
                        {
                            //TODO: win state
                        }
                    }


                    player1MinionsToRemove.ForEach(f => Game.Player1.Minions.Remove(f));
                    player2MinionsToRemove.ForEach(f => Game.Player2.Minions.Remove(f));
                    break;
            }
        }
    }
}