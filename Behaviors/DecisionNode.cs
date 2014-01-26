namespace MinionsOfDeath.Behaviors
{
    public abstract class DecisionNode
    {
        protected Minion _owner;

        public DecisionNode(Minion owner)
        {
            _owner = owner;
        }

        public abstract DoublePoint GetGoal();
    }
}