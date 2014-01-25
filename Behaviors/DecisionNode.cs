using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    abstract class DecisionNode
    {
        protected Minion _owner;
        public abstract DoublePoint GetGoal();
    }
}
