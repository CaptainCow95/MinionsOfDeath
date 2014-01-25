using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors
{
    class SeekAction : Action
    {
        public SeekAction(Minion owner, Minion target)
        {
            _owner = owner;
            _target = target;
        }

        public override DoublePoint GetGoal()
        {
            return _target.Pos;
        }
    }
}
