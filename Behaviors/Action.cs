namespace MinionsOfDeath.Behaviors
{
    public abstract class Action : DecisionNode
    {
        public Action(Minion owner)
            : base(owner)
        {
        }
    }
}