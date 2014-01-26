using System.Linq;

namespace MinionsOfDeath.Behaviors.Queries
{
    public class IsEnemyOnMyHalf : QueryNode
    {
        private int halfline_y;

        public IsEnemyOnMyHalf(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            bool succeed;
            if (player1)
            {
                Minion minion = Game.Player2.Minions.Values.Where(e => e.Pos.Y < 900).FirstOrDefault();
                succeed = minion != null;
            }
            else
            {
                Minion minion = Game.Player1.Minions.Values.Where(e => e.Pos.Y > 900).FirstOrDefault();
                succeed = minion != null;
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