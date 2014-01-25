namespace MinionsOfDeath.Behaviors
{
    internal abstract class DecisionNode
    {
        protected Minion _owner;

        public abstract DoublePoint GetGoal();
    }
}