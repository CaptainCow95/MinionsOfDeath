namespace MinionsOfDeath.Behaviors
{
    public abstract class DecisionNode
    {
        protected Minion _owner;

        public abstract DoublePoint GetGoal();
    }
}