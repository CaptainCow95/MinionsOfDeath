namespace MinionsOfDeath.Behaviors
{
    internal class DecisionTree
    {
        private Minion _owner;
        private DecisionNode _root;

        public DecisionTree(Minion owner, DecisionNode root)
        {
            _root = root;
            _owner = owner;
        }

        public Minion Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public DecisionNode Root
        {
            get { return _root; }
            set { _root = value; }
        }

        /*
         * GetGoal should return a point that an owning minion will seek towards
         */

        public DoublePoint GetGoal()
        {
            return _root.GetGoal();
        }
    }
}