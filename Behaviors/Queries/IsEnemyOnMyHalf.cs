using MinionsOfDeath.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors.Queries
{
    internal class IsEnemyOnMyHalf : QueryNode
    {
        int halfline_y;

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            bool succeed;
            if (player1)
            {
                Minion minion = Game.Player2.Minions.Values.Where(e => e.Pos.Y > 500).First();
                succeed = minion != null;
            }
            else
            {
                Minion minion = Game.Player1.Minions.Values.Where(e => e.Pos.Y < 500).First();
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
