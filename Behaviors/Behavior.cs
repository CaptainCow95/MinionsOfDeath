using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    abstract class Behavior
    {
        private Minion owner;
        private Minion target;
       public abstract FloatPoint getTarget;

    }
}
