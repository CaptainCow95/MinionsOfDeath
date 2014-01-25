using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors.Queries
{
    internal class IsEnemyNum2 : QueryNode
    {

        public override DoublePoint GetGoal()
        {
            if (true)
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