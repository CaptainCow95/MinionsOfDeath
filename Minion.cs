using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MinionsOfDeath
{
    public class Minion : GameObject
    {
        private static double speed = 1.0;

        //dictates the movement behavior of the minion
        private DecisionTree _decisionTree;

        //determines if the minion is special, and therfore if it will end the game when reaching the enemy base
        private bool _isSpecial;

        //identifies each minion on a team
        private int _MinionID;

        //Represent the player the Minion belongs to
        private int _pid;

        private bool _moveDirectionUp;

        public Minion(List<Sprite> sprites, int MinionID)
            : base(sprites)
        {
            _MinionID = MinionID;
            _isSpecial = false;
			_left += 4;
			_right -= 4;
			_top += 4;
        }

        public DecisionTree DecisionTree
        {
            get { return _decisionTree; }
            set { _decisionTree = value; }
        }

        public int ID
        {
            set { _MinionID = value; }
            get { return _MinionID; }
        }

        public bool IsSpecial
        {
            get { return _isSpecial; }
            set { _isSpecial = value; }
        }

        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        }

        public bool MoveDirectionUp
        {
            get { return _moveDirectionUp; }
        }

        public override void Update(double time)
        {
			DoublePoint fp;
			double mag = speed;
			bool b = true;
			while (b && ((fp = _decisionTree.GetGoal ()) != null && !(Math.Abs(fp.X - _pos.X) < double.Epsilon && Math.Abs(fp.Y - _pos.Y) < double.Epsilon))) {
				DoublePoint v = new DoublePoint (fp.X - _pos.X, fp.Y - _pos.Y);
				b = v.SetToLessOrEqualMag (mag);
				_moveDirectionUp = fp.Y - _pos.Y > 0; 
				if (b) {
					_pos.Set(v);
				} else {
					_pos.Add(v);
				}
			}

            base.Update(time);
        }

		public override void Draw()
		{
			GetSprite().X = _pos.X + _left - _right;
			GetSprite().Y = _pos.Y + _top - _bottom;
			GetSprite().Draw();
		}
    }
}