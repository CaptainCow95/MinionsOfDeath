﻿using MinionsOfDeath.Graphics;
using System;
using System.Collections.Generic;

namespace MinionsOfDeath.Interface
{
    public class StackPanel : InterfaceObject
    {
        private List<InterfaceObject> _children = new List<InterfaceObject>();
        private bool _horizontal = false;
        private ScrollBar _horizontalScrollbar;
        private bool _scrollable = false;
        private ScrollBar _verticalScrollbar;

        public StackPanel(int x, int y, bool staticImage, bool horizontal)
            : base(x, y, 0, 0, staticImage)
        {
            _scrollable = false;
            _horizontal = horizontal;
        }

        public StackPanel(int x, int y, int width, int height, bool staticImage, bool horizontal, Sprite verticalScrollBar, Sprite horizontalScrollBar)
            : base(x, y, width, height, staticImage)
        {
            _scrollable = true;
            _horizontal = horizontal;
            _verticalScrollbar = new ScrollBar((int)(x + width - verticalScrollBar.Width), y, (int)verticalScrollBar.Width, height, staticImage, 0, height, false, verticalScrollBar);
            _horizontalScrollbar = new ScrollBar(x, (int)(y + height - horizontalScrollBar.Height), width, (int)horizontalScrollBar.Height, staticImage, 0, width, true, horizontalScrollBar);
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
            int currentX = X;
            int currentY = Y;

            if (Static)
            {
                currentX += Camera.X;
                currentY += Camera.Y;
            }

            foreach (var child in _children)
            {
                child.X = currentX;
                child.Y = currentY;
                child.Update(timeSinceFrame);

                if (_horizontal)
                {
                    currentX += child.Width;
                }
                else
                {
                    currentY += child.Height;
                }

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