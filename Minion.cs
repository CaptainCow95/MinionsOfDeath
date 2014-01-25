using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
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

        public Minion(List<Sprite> sprites, int MinionID)
            : base(sprites)
        {
            _MinionID = MinionID;
            _isSpecial = false;
			_left += 2;
			_right -= 2;
			_top += 2;
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

        public override void Update(double time)
        {
            DoublePoint fp = _decisionTree.GetGoal();
            DoublePoint v = new DoublePoint(fp.X - _pos.X, fp.Y - _pos.Y);
            v.SetToLessOrEqualMag(speed);
            _pos.Add(v);

			base.Update(time);
        }
    }
}