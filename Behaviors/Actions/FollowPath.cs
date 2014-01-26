using MinionsOfDeath.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Behaviors.Actions
{
    internal class FollowPath : Action
    {
        private List<WaypointNode> _path;
        public FollowPath(Minion owner, List<WaypointNode> path) : base(owner)
        {
            _path = path;
        }
        public override DoublePoint GetGoal()
        {


            return null;
        }
    }
}
