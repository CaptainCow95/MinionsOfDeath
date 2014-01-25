using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
    {
		private static double speed = 1.0;
        
        //dictates the movement behavior of the minion
        DecisionTree _decisionTree;
        
        //identifies each minion on a team
        private int _MinionID;

        //determines if the minion is special, and therfore if it will end the game when reaching the enemy base
        private bool _isSpecial;
        
        //Represent the player the Minion belongs to
        private int _pid;

        public int PID
        {
            get { return _pid; }
            set { _pid = value; }
        } 

        public bool IsSpecial
        {
            get { return _isSpecial; }
            set { _isSpecial = value; }
        }

        public int ID
        {
            set { _MinionID = value; }
            get {return _MinionID;}
        }

        public Minion(List<Sprite> sprites, int MinionID)
            : base(sprites)
        {
            _MinionID = MinionID;
            _isSpecial = false;
        }


        public DecisionTree DecisionTree
        {
            get { return _decisionTree; }
            set { _decisionTree = value; }
		}

		public override void Update(double time)
		{
            base.Update(time);

			DoublePoint fp = _decisionTree.GetGoal();
			DoublePoint v = new DoublePoint (fp.X - _pos.X, fp.Y - _pos.Y);
			v.SetToLessOrEqualMag (speed);
			_pos.Add (v);
		}
    }
}