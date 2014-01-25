namespace MinionsOfDeath.Behaviors
{
    internal class Pathfinder : Action
    {
        public Pathfinder(Minion owner, Minion target)
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
