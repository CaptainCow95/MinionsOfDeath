using MinionsOfDeath.Navigation;
using System.Collections.Generic;

namespace MinionsOfDeath.Behaviors
{
    public class Pathfinder : Action
    {
        private List<WaypointNode> _path;

        public Pathfinder(Minion owner, List<WaypointNode> path)
            : base(owner)
        {
            _owner = owner;
            _path = path;
        }

        public override DoublePoint GetGoal()
        {
            WaypointNode targetNode = _path[0];
            DoublePoint targetNodePos = new DoublePoint(targetNode.X, targetNode.Y);
            // minion at target, advancec target
            if (_owner.Pos == targetNodePos)
            {
                _path.RemoveAt(0);
                targetNode = _path[0];
                targetNodePos = new DoublePoint(targetNode.X, targetNode.Y);
            }
            return targetNodePos;
        }

        public void getPath(WaypointNode curNode, WaypointNode target)
        {
            _path = WaypointGraph.pathfindDijkstra(curNode, target);
        }
    }
}