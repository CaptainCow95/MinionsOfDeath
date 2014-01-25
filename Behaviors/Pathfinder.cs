using MinionsOfDeath.Navigation;
using System.Collections.Generic;

namespace MinionsOfDeath.Behaviors
{
    internal class Pathfinder : Action
    {
        public Pathfinder(Minion owner, Minion target)
        {
            _owner = owner;
            _target = target;
        }

        public override DoublePoint GetGoal()
        {
            return _target.Pos;
        }

        public void getPath(WaypointGraph graph, WaypointNode curNode, WaypointNode target)
        {
            List<WaypointNode> path = graph.pathfindDijkstra(curNode, target);
        }
    }
}
