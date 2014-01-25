namespace MinionsOfDeath.Behaviors
{
    internal class SeekAction : Action
    {
        public SeekAction(Minion owner, Minion target)
        {
            _owner = owner;
            _target = target;
        }

        public override DoublePoint GetGoal()
        {
            return _target.Pos;
        }
    }
}