using MinionsOfDeath.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace MinionsOfDeath.Behaviors.Actions
{
    public class AttackClosest : Action
    {
        public AttackClosest(Minion owner)
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
            //TODO: update curNode at some point
            WaypointNode enemyNode = WaypointGraph.GetClosestWaypoint((int)closest.Pos.X, (int)closest.Pos.Y);
            WaypointNode myNode = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
            List<WaypointNode> path = WaypointGraph.pathfindDijkstra(myNode, enemyNode);
            path.Add(new WaypointNode((int)closest.Pos.X, (int)closest.Pos.Y, new List<WaypointNode>()));
            FollowPath fp = new FollowPath(_owner, path);

            return fp.GetGoal();
        }
    }
}