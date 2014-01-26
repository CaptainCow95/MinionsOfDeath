namespace MinionsOfDeath.Behaviors
{
    internal abstract class Action : DecisionNode
    {
        public Action(Minion owner) : base (owner){}
    }
}