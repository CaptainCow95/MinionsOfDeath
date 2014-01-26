using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinionsOfDeath.Navigation;

namespace MinionsOfDeath.Behaviors.Actions
{
    public class DefendSpecial : Action
    {
        public DefendSpecial(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            WaypointNode target;
            if (player1)
                {
                    Minion special = Game.Player1.Minions.Values.Where(e => e.IsSpecial).FirstOrDefault();
                    if (special != null)
                        target = WaypointGraph.GetClosestWaypoint((int)special.Pos.X, (int)special.Pos.Y + 400);
                    else
                        target = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
                }
                else
                {
                    Minion special = Game.Player2.Minions.Values.Where(e => e.IsSpecial).FirstOrDefault();
                    if (special != null)
                        target = WaypointGraph.GetClosestWaypoint((int)special.Pos.X, (int)special.Pos.Y - 400);
                    else
                        target = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
                }
            WaypointNode myNode = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
            List<WaypointNode> path = new List<WaypointNode>();
            /*
             * int length = Int32.MaxValue;
            foreach(WaypointNode target in target_nodes){
                List<WaypointNode> p = WaypointGraph.pathfindDijkstra(myNode, target);
                if (p.Count < length){
                    path = p;
                    length = p.Count;
                }
            }
             * */
            path = WaypointGraph.pathfindDijkstra(myNode, target);
            //path.Add(new WaypointNode((int)closest.Pos.X, (int)closest.Pos.Y, new List<WaypointNode>()));
            FollowPath fp = new FollowPath(_owner, path);

            return fp.GetGoal(); ;
        }
    }


}
