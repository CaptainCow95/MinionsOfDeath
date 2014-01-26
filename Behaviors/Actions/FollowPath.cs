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
            if (WaypointGraph.getDistance(_path[0].X, (int)_owner.Pos.X, _path[0].Y, (int)_owner.Pos.Y) < 2 || WaypointGraph.getDistance(_path[1].X, (int)_owner.Pos.X, _path[1].Y, (int)_owner.Pos.Y) < WaypointGraph.getDistance(_path[1].X, _path[0].X, _path[1].Y, _path[0].Y))
            {
                _path.RemoveAt(0);
            }



            return new DoublePoint(_path[0].X, _path[0].Y);
        }
    }
}
