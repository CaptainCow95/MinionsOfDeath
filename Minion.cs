using MinionsOfDeath.Behaviors;
using MinionsOfDeath.Graphics;
using System.Collections.Generic;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
    {
		private static double speed = 1.0;
        DecisionTree _decisionTree;
        private int _MinionID;
        private bool _isSpecial;


        public Minion(List<Sprite> sprites)
            : base(sprites)
        {
        }

        public DecisionTree DecisionTree
        {
            get { return _decisionTree; }
            set { _decisionTree = value; }
		}

		public override void Update(double time)
		{
			DoublePoint fp = _decisionTree.GetGoal();
			DoublePoint v = new DoublePoint (fp.X - _pos.X, fp.Y - _pos.Y);
			v.SetToLessOrEqualMag (speed);
			_pos.Add (v);
		}
    }
}