﻿﻿using MinionsOfDeath.Behaviors;
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
        private Button _makeSpecialMinionSelected;
        private ScrollBar _mapScroll;
        private Button _player1Go;
        private Dictionary<Button, Tuple<StackPanel, StackPanel>> _player1ParentList = new Dictionary<Button, Tuple<StackPanel, StackPanel>>();
        private Dictionary<int, StackPanel> _player1TreeRoot;
        private Button _player2Go;
        private Dictionary<Button, Tuple<StackPanel, StackPanel>> _player2ParentList = new Dictionary<Button, Tuple<StackPanel, StackPanel>>();
        private Dictionary<int, StackPanel> _player2TreeRoot;
        private List<string> _selectOptions = new List<string>() { "Is Enemy\nClose", "Is 1 \nEnemy Left", "Is 2\nEnemies Left", "Is Enemy\nOn My Half", "Is Nearest\nEnemy Moving\nAway", "Is Nearest\nEnemy Moving\nTowards", "Attack Closest", "Go To Base", "Run Away", "Stop" };
        private UserInterfaceState _state;
        private Button _minion1;
        private Button _minion1Selected;
        private Button _minion2;
        private Button _minion2Selected;
        private Button _minion3;
        private Button _minion3Selected;
        private int _minionEditing = 1;
        private static Sprite player1wins = new Sprite(new List<string>() { "Images/Player1Wins.png" }, 0);
        private static Sprite player2wins = new Sprite(new List<string>() { "Images/Player2Wins.png" }, 0);
        private static Sprite draw = new Sprite(new List<string>() { "Images/draw.png" }, 0);


        public UserInterface()
        {
            _player1Go = new Button(0, 600, 100, 100, true, "Go to\nPlayer 2", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

            _player2Go = new Button(0, 600, 100, 100, true, "Run\nSimulation", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

            _makeSpecialMinion = new Button(0, 500, 100, 100, true, "Make Special\nMinion", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _makeSpecialMinionSelected = new Button(0, 500, 100, 100, true, "Make Special\nMinion", new Sprite(new List<string>() { "Images/blueButtonLIGHT.png" }, 0));

            _editTree = new Button(0, 400, 100, 100, true, "Edit\nBehaviours", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));

            _mapScroll = new ScrollBar(970, 0, 30, 1800, false, 0, 1800, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0));

            _editSelect = new Select(0, 0, false, _selectOptions, new Sprite(new List<string>() { "Images/blueButtonSmall.png" }, 0), new List<string>() { "Images/blueButtonSmall.png" });

            _minion1 = new Button(0, 0, 100, 100, true, "Minion 1", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _minion1Selected = new Button(0, 0, 100, 100, true, "Minion 1", new Sprite(new List<string>() { "Images/blueButtonLIGHT.png" }, 0));
            _minion2 = new Button(0, 100, 100, 100, true, "Minion 2", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _minion2Selected = new Button(0, 100, 100, 100, true, "Minion 2", new Sprite(new List<string>() { "Images/blueButtonLIGHT.png" }, 0));
            _minion3 = new Button(0, 200, 100, 100, true, "Minion 3", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _minion3Selected = new Button(0, 200, 100, 100, true, "Minion 3", new Sprite(new List<string>() { "Images/blueButtonLIGHT.png" }, 0));

            _state = UserInterfaceState.Player1MinionSelect;

            Reset();

            Sound.Strategize.PlayLooping();
        }

        public void CreateDecisionTrees()
        {
            DecisionTree tree = new DecisionTree(Game.Player1.Minions[1], GetNode(Game.Player1.Minions[1], _player1TreeRoot[1].Children[0], _player1TreeRoot[1], null));
            Game.Player1.Minions[1].DecisionTree = tree;

            tree = new DecisionTree(Game.Player1.Minions[2], GetNode(Game.Player1.Minions[2], _player1TreeRoot[2].Children[0], _player1TreeRoot[2], null));
            Game.Player1.Minions[2].DecisionTree = tree;

            tree = new DecisionTree(Game.Player1.Minions[3], GetNode(Game.Player1.Minions[3], _player1TreeRoot[3].Children[0], _player1TreeRoot[3], null));
            Game.Player1.Minions[3].DecisionTree = tree;

            DecisionTree tree2 = new DecisionTree(Game.Player2.Minions[1], GetNode(Game.Player2.Minions[1], _player2TreeRoot[1].Children[0], _player2TreeRoot[1], null));
            Game.Player2.Minions[1].DecisionTree = tree2;

            tree2 = new DecisionTree(Game.Player2.Minions[2], GetNode(Game.Player2.Minions[2], _player2TreeRoot[2].Children[0], _player2TreeRoot[2], null));
            Game.Player2.Minions[2].DecisionTree = tree2;

            tree2 = new DecisionTree(Game.Player2.Minions[3], GetNode(Game.Player2.Minions[3], _player2TreeRoot[3].Children[0], _player2TreeRoot[3], null));
            Game.Player2.Minions[3].DecisionTree = tree2;
        }

        public void Draw()
        {
            Game.Player1.Draw();
            Game.Player2.Draw();
            Game.DeathClouds.Draw();

            switch (_state)
            {
                case UserInterfaceState.Player1EditMinionTree:
                    if (_inSelectMode)
                    {
                        _editSelect.Draw();
                    }
                    else
                    {
                        _player1TreeRoot[_minionEditing].Draw();
                    }
                    break;

                case UserInterfaceState.Player1MinionSelect:
                    _player1Go.Draw();
                    if (Game.Player1.Minions[_minionEditing].IsSpecial)
                    {
                        _makeSpecialMinionSelected.Draw();
                    }
                    else
                    {
                        _makeSpecialMinion.Draw();
                    }
                    _editTree.Draw();
                    _mapScroll.Draw();
                    switch (_minionEditing)
                    {
                        case 1:
                            _minion1Selected.Draw();
                            _minion2.Draw();
                            _minion3.Draw();
                            break;
                        case 2:
                            _minion1.Draw();
                            _minion2Selected.Draw();
                            _minion3.Draw();
                            break;
                        case 3:
                            _minion1.Draw();
                            _minion2.Draw();
                            _minion3Selected.Draw();
                            break;
                    }
                    break;

                case UserInterfaceState.Player2EditMinionTree:
                    if (_inSelectMode)
                    {
                        _editSelect.Draw();
                    }
                    else
                    {
                        _player2TreeRoot[_minionEditing].Draw();
                    }
                    break;

                case UserInterfaceState.Player2MinionSelect:
                    _player2Go.Draw();
                    if (Game.Player2.Minions[_minionEditing].IsSpecial)
                    {
                        _makeSpecialMinionSelected.Draw();
                    }
                    else
                    {
                        _makeSpecialMinion.Draw();
                    }
                    _editTree.Draw();
                    _mapScroll.Draw();
                    switch (_minionEditing)
                    {
                        case 1:
                            _minion1Selected.Draw();
                            _minion2.Draw();
                            _minion3.Draw();
                            break;
                        case 2:
                            _minion1.Draw();
                            _minion2Selected.Draw();
                            _minion3.Draw();
                            break;
                        case 3:
                            _minion1.Draw();
                            _minion2.Draw();
                            _minion3Selected.Draw();
                            break;
                    }
                    break;

                case UserInterfaceState.Running:
                    _mapScroll.Draw();
                    break;

                case UserInterfaceState.Player1Wins:
                    player1wins.Draw();
                    break;

                case UserInterfaceState.Player2Wins:
                    player2wins.Draw();
                    break;

                case UserInterfaceState.Draw:
                    draw.Draw();
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

                    case "Stop":
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
                        _player1TreeRoot[_minionEditing].Update(lastFrameTime);

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

                    if (((Button)_player1TreeRoot[_minionEditing].Children[_player1TreeRoot[_minionEditing].Children.Count - 1]).Pressed)
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
                    _minion1.Update(lastFrameTime);
                    _minion2.Update(lastFrameTime);
                    _minion3.Update(lastFrameTime);

                    if (Game.Player1.Minions[_minionEditing].IsSpecial)
                    {
                        _makeSpecialMinionSelected.Update(lastFrameTime);
                    }
                    else
                    {
                        _makeSpecialMinion.Update(lastFrameTime);
                    }

                    if (_makeSpecialMinion.Pressed)
                    {
                        foreach (var minion in Game.Player1.Minions)
                        {
                            minion.Value.IsSpecial = false;
                        }

                        Game.Player1.Minions[_minionEditing].IsSpecial = true;
                    }

                    if (_player1Go.Pressed)
                    {
                        _state = UserInterfaceState.Player2MinionSelect;
                        _minionEditing = 1;
                    }
                    if (_makeSpecialMinion.Pressed)
                    {
                        // Make current minion special
                    }
                    if (_editTree.Pressed)
                    {
                        _state = UserInterfaceState.Player1EditMinionTree;
                    }
                    if (_minion1.Pressed)
                    {
                        _minionEditing = 1;
                    }
                    else if (_minion2.Pressed)
                    {
                        _minionEditing = 2;
                    }
                    else if (_minion3.Pressed)
                    {
                        _minionEditing = 3;
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
                        _player2TreeRoot[_minionEditing].Update(lastFrameTime);

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

                    if (((Button)_player2TreeRoot[_minionEditing].Children[_player2TreeRoot[_minionEditing].Children.Count - 1]).Pressed)
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
                    _minion1.Update(lastFrameTime);
                    _minion2.Update(lastFrameTime);
                    _minion3.Update(lastFrameTime);

                    if (Game.Player2.Minions[_minionEditing].IsSpecial)
                    {
                        _makeSpecialMinionSelected.Update(lastFrameTime);
                    }
                    else
                    {
                        _makeSpecialMinion.Update(lastFrameTime);
                    }

                    if (_makeSpecialMinion.Pressed)
                    {
                        foreach (var minion in Game.Player2.Minions)
                        {
                            minion.Value.IsSpecial = false;
                        }

                        Game.Player2.Minions[_minionEditing].IsSpecial = true;
                    }

                    if (_player2Go.Pressed)
                    {
                        _state = UserInterfaceState.Running;
                        CreateDecisionTrees();
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
                    if (_minion1.Pressed)
                    {
                        _minionEditing = 1;
                    }
                    else if (_minion2.Pressed)
                    {
                        _minionEditing = 2;
                    }
                    else if (_minion3.Pressed)
                    {
                        _minionEditing = 3;
                    }
                    break;

                case UserInterfaceState.Running:
                    _mapScroll.Update(lastFrameTime);

                    Game.Player1.Update(lastFrameTime);
                    Game.Player2.Update(lastFrameTime);
                    Game.DeathClouds.Update(lastFrameTime);
                    Game.DeathClouds.Flush();

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
                                Game.DeathClouds.Add(new DeathCloud(DoublePoint.GetAverage(
                                    player1Minion.Value.Pos, player2Minion.Value.Pos)));
                            }
                        }
                    }
                    foreach (var player1Minion in Game.Player1.Minions)
                    {
                        if (player1Minion.Value.IsCollidingWith(Game.Player2.Base) && player1Minion.Value.IsSpecial)
                        {
                            //TODO: win state
                            _state = UserInterfaceState.Player1Wins;
						Sound.Victory.Play();
                        }
                    }
                    foreach (var player2Minion in Game.Player2.Minions)
                    {
                        if (player2Minion.Value.IsCollidingWith(Game.Player1.Base) && player2Minion.Value.IsSpecial)
                        {
                            _state = UserInterfaceState.Player2Wins;
						Sound.Victory.Play();
                        }
                    }
                    bool player1SpecialAlive = Game.Player1.Minions.Where(e => e.Value.IsSpecial).Count() > 0;
                    bool player2SpecialAlive = Game.Player2.Minions.Where(e => e.Value.IsSpecial).Count() > 0;

                    if (!player1SpecialAlive && !player2SpecialAlive)
                    {
                        _state = UserInterfaceState.Draw;
                        Sound.Death.Play();
                    }
                    else if (!player1SpecialAlive)
                    {
                        _state = UserInterfaceState.Player2Wins;
                        Sound.Victory.Play();
                    }
                    else if (!player2SpecialAlive)
                    {
                        _state = UserInterfaceState.Player1Wins;
                        Sound.Victory.Play();
                    }

                    player1MinionsToRemove.ForEach(f => Game.Player1.Minions.Remove(f));
                    player2MinionsToRemove.ForEach(f => Game.Player2.Minions.Remove(f));
                    break;

                case UserInterfaceState.Player1Wins:
                    Camera.X = 0;
                    Camera.Y = 0;

                    if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Released && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed)
                    {
                        _state = UserInterfaceState.Player1MinionSelect;
                        Reset();
                    }
                    break;

                case UserInterfaceState.Player2Wins:
                    Camera.X = 0;
                    Camera.Y = 0;

                    if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Released && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed)
                    {
                        _state = UserInterfaceState.Player1MinionSelect;
                        Reset();
                    }
                    break;

                case UserInterfaceState.Draw:
                    Camera.X = 0;
                    Camera.Y = 0;

                    if (Game.MouseState.LeftButton == OpenTK.Input.ButtonState.Released && Game.PreviousMouseState.LeftButton == OpenTK.Input.ButtonState.Pressed)
                    {
                        _state = UserInterfaceState.Player1MinionSelect;
                        Reset();
                    }
                    break;
            }
        }

        public void Reset()
        {
            _player1TreeRoot = new Dictionary<int, StackPanel>();
            _player1TreeRoot[1] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            Button player1GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player1TreeRoot[1].Children.Add(player1GoToBase);
            _player1ParentList.Add(player1GoToBase, new Tuple<StackPanel, StackPanel>(_player1TreeRoot[1], null));
            _player1TreeRoot[1].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));
            _player1TreeRoot[2] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            player1GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player1TreeRoot[2].Children.Add(player1GoToBase);
            _player1ParentList.Add(player1GoToBase, new Tuple<StackPanel, StackPanel>(_player1TreeRoot[2], null));
            _player1TreeRoot[2].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));
            _player1TreeRoot[3] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            player1GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player1TreeRoot[3].Children.Add(player1GoToBase);
            _player1ParentList.Add(player1GoToBase, new Tuple<StackPanel, StackPanel>(_player1TreeRoot[3], null));
            _player1TreeRoot[3].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));

            _player2TreeRoot = new Dictionary<int, StackPanel>();
            _player2TreeRoot[1] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            Button player2GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player2TreeRoot[1].Children.Add(player2GoToBase);
            _player2ParentList.Add(player2GoToBase, new Tuple<StackPanel, StackPanel>(_player2TreeRoot[1], null));
            _player2TreeRoot[1].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));
            _player2TreeRoot[2] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            player2GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player2TreeRoot[2].Children.Add(player2GoToBase);
            _player2ParentList.Add(player2GoToBase, new Tuple<StackPanel, StackPanel>(_player2TreeRoot[2], null));
            _player2TreeRoot[2].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));
            _player2TreeRoot[3] = new StackPanel(0, 0, 1000, 700, false, false, new Sprite(new List<string>() { "Images/SCROLLDAGGER5000.png" }, 0), new Sprite(new List<string>() { "Images/SCROLLDAGGER3000.png" }, 0));
            player2GoToBase = new Button(0, 0, 100, 100, false, "Go To Base", new Sprite(new List<string>() { "Images/blueButton.png" }, 0));
            _player2TreeRoot[3].Children.Add(player2GoToBase);
            _player2ParentList.Add(player2GoToBase, new Tuple<StackPanel, StackPanel>(_player2TreeRoot[3], null));
            _player2TreeRoot[3].Children.Add(new Button(0, 0, 100, 100, false, "Go Back", new Sprite(new List<string>() { "Images/blueButton.png" }, 0)));

            _minionEditing = 1;

            Game.Player1 = new Player(1);
            Minion minion1Player1 = new Minion(true, 1);
            minion1Player1.IsSpecial = true;
            Game.Player1.AddMinion(minion1Player1);
            Minion minion2Player1 = new Minion(true, 2);
            Game.Player1.AddMinion(minion2Player1);
            Minion minion3Player1 = new Minion(true, 3);
            Game.Player1.AddMinion(minion3Player1);
            //_player1.Base = new Base((List<Sprite>)null, 1);
            Game.Player1.Base = new Base(null, 1);
            Game.Player1.Base.Pos.X = 499;
            Game.Player1.Base.Pos.Y = 44;

            Game.Player2 = new Player(2);
            Minion minion1Player2 = new Minion(false, 1);
            minion1Player2.IsSpecial = true;
            Game.Player2.AddMinion(minion1Player2);
            Minion minion2Player2 = new Minion(false, 2);
            Game.Player2.AddMinion(minion2Player2);
            Minion minion3Player2 = new Minion(false, 3);
            Game.Player2.AddMinion(minion3Player2);
            //_player2.Base = new Base((List<Sprite>)null, 2);
            Game.Player2.Base = new Base(null, 2);
            Game.Player2.Base.Pos.X = 495;
            Game.Player2.Base.Pos.Y = 1700;

            minion1Player1.Pos.X = 175;
            minion1Player1.Pos.Y = 100;

            minion2Player1.Pos.X = 505;
            minion2Player1.Pos.Y = 180;

            minion3Player1.Pos.X = 825;
            minion3Player1.Pos.Y = 100;


            minion1Player2.Pos.X = 340;
            minion1Player2.Pos.Y = 1750;

            minion2Player2.Pos.X = 495;
            minion2Player2.Pos.Y = 1615;

            minion3Player2.Pos.X = 575;
            minion3Player2.Pos.Y = 1615;
        }
    }
}