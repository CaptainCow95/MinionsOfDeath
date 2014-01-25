using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors.Queries
{
    internal class IsEnemyNum1 : QueryNode
    {

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            bool succeed;
            if(player1)
            {
                succeed = Game.Player2.Minions.Count == 1;
            }
            else
            {
                succeed = Game.Player1.Minions.Count == 1;
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
