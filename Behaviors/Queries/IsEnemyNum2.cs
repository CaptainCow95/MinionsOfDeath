namespace MinionsOfDeath.Behaviors.Queries
{
    public class IsEnemyNum2 : QueryNode
    {
        public IsEnemyNum2(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            bool succeed;
            if (player1)
            {
                succeed = Game.Player2.Minions.Count == 2;
            }
            else
            {
                succeed = Game.Player1.Minions.Count == 2;
            }
            if (succeed)
            {
                return TrueChild.GetGoal();
            }
            else
            {
                return FalseChild.GetGoal();
            }
        }
    }
}