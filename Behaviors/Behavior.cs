using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    abstract class Behavior : DecisionNode
    {
        private Minion owner;
        private Minion target;
        public abstract DoublePoint getTarget();

    }
}
