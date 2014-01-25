﻿using MinionsOfDeath.Graphics;
using System;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class StackPanel : InterfaceObject
    {
        private List<InterfaceObject> _children = new List<InterfaceObject>();
        private ScrollBar _horizontalScrollbar;
        private bool _scrollable = false;
        private ScrollBar _verticalScrollbar;

        public StackPanel(int x, int y)
            : base(x, y, 0, 0)
        {
            _scrollable = false;
        }

        public StackPanel(int x, int y, int width, int height, Sprite verticalScrollBar, Sprite horizontalScrollBar)
            : base(x, y, width, height)
        {
            _scrollable = true;
            _verticalScrollbar = new ScrollBar((int)(x + width - verticalScrollBar.Width), y, (int)verticalScrollBar.Width, height, 0, height, false, verticalScrollBar);
            _horizontalScrollbar = new ScrollBar(x, (int)(y + height - horizontalScrollBar.Height), width, (int)horizontalScrollBar.Height, 0, width, true, horizontalScrollBar);
        }

        public List<InterfaceObject> Children { get { return _children; } }

        public override void Draw()
        {
            foreach (var child in _children)
            {
                child.Draw();
            }

            if (_scrollable)
            {
                _horizontalScrollbar.Y = Height + Camera.Y - _horizontalScrollbar.Height;
                _horizontalScrollbar.Draw();
                _verticalScrollbar.X = Width + Camera.X - _verticalScrollbar.Width;
                _verticalScrollbar.Draw();
            }
        }

        public override void Update(double timeSinceFrame)
        {
            int maxWidth = 0;
            int maxHeight = 0;
            foreach (var child in _children)
            {
                child.Update(timeSinceFrame);
                maxWidth = Math.Max(maxWidth, child.X + child.Width - X);
                maxHeight = Math.Max(maxHeight, child.Y + child.Height - Y);
            }

            if (!_scrollable)
            {
                Width = maxWidth;
                Height = maxHeight;
            }
            else
            {
                _horizontalScrollbar.MaxValue = maxWidth - Game.WindowWidth;
                _verticalScrollbar.MaxValue = maxHeight - Game.WindowHeight;
            }

            if (_scrollable)
            {
                _horizontalScrollbar.Update(timeSinceFrame);
                _verticalScrollbar.Update(timeSinceFrame);
            }
        }
    }
}