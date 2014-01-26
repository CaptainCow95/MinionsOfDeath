namespace MinionsOfDeath.Behaviors.Actions
{
    public class WaitForTime : Action
    {
        public WaitForTime(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            return null;
        }
    }
}