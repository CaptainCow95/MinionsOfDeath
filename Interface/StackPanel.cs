﻿using System;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class StackPanel : InterfaceObject
    {
        private List<InterfaceObject> _children = new List<InterfaceObject>();
        private bool _scrollable = false;

        public StackPanel(int x, int y)
            : base(x, y, 0, 0)
        {
        }

        public StackPanel(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
            _scrollable = true;
        }

        public List<InterfaceObject> Children { get { return _children; } }

        public override void Draw()
        {
            foreach (var child in _children)
            {
                child.Draw();
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
        }
    }
}