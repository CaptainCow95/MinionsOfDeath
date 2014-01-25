using MinionsOfDeath.Behaviors;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
    {
		private static float speed = 1.0;
        DecisionTree _decisionTree;

        public DecisionTree DecisionTree
        {
            get { return _decisionTree; }
            set { _decisionTree = value; }
		}

		public abstract void update(double time)
		{
			FloatPoint fp = _decisionTree.getMove();
			FloatPoint v = new FloatPoint (fp.X - _x, fp.Y - _y);
			v.SetToLessOrEqualMag (speed);
			_pos.Add (v);
		}
    }
}