namespace MinionsOfDeath.Behaviors
{
    public class SeekAction : Action
    {
        protected Minion _target;

        public SeekAction(Minion owner, Minion target)
            : base(owner)
        {
            _target = target;
        }

        public override DoublePoint GetGoal()
        {
            return _target.Pos;
        }
    }
}