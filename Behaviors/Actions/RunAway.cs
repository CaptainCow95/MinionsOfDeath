namespace MinionsOfDeath.Behaviors.Actions
{
    public class RunAway : Action
    {
        public RunAway(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            return null;
        }
    }
}