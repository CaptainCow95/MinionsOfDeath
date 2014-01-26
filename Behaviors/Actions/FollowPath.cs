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
            switch (_path.Count) {
                case 0: return null;
                case 1: return new DoublePoint(_path[0].X, _path[0].Y);
                default :
                if (WaypointGraph.getDistance(_path[0].X, (int)_owner.Pos.X, _path[0].Y, (int)_owner.Pos.Y) < 1)
            {
                _path.RemoveAt(0);
            }break;
        }

            Console.WriteLine(_path[0].X + " "+ _path[0].Y);
            return new DoublePoint(_path[0].X, _path[0].Y);
        }
    }
}
