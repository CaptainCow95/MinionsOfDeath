using MinionsOfDeath.Behaviors;

namespace MinionsOfDeath
{
    internal class Minion : GameObject
    {
        DecisionTree _decisionTree;

        public DecisionTree DecisionTree
        {
            get { return _decisionTree; }
            set { _decisionTree = value; }
		}

		public abstract void update(double time)
		{
            FloatPoint targetMove = _decisionTree.getMove();
		}
    }
}