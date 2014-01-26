using MinionsOfDeath.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace MinionsOfDeath.Behaviors.Actions
{
    public class RunAway : Action
    {
        public RunAway(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            Minion closest;
            if (player1)
            {
                closest = Game.Player2.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
            }
            else
            {
                closest = Game.Player1.Minions.Values.OrderBy(e => WaypointGraph.getDistance((int)_owner.Pos.X, (int)e.Pos.X, (int)_owner.Pos.Y, (int)e.Pos.Y)).First();
            }

            double xDiff = _owner.Pos.X - closest.Pos.X;
            double yDiff = _owner.Pos.Y - closest.Pos.Y;

            double targetX = _owner.Pos.X + xDiff * 2;
            double targetY = _owner.Pos.Y + yDiff * 2;

            WaypointNode targetNode = WaypointGraph.GetClosestWaypoint((int)targetX, (int)targetY);
            WaypointNode myNode = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
            List<WaypointNode> path = WaypointGraph.pathfindDijkstra(myNode, targetNode);
            FollowPath fp = new FollowPath(_owner, path);
            return fp.GetGoal();
        }
    }
}