using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors.Actions
{
    public class PatrolHomeHalf : Action
    {
        public PatrolHomeHalf(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            return null;
        }
    }
}
