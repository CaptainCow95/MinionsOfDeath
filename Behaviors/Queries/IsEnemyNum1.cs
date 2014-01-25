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
            if ()
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
