﻿using System;
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
                    Minion special = (Minion)Game.Player1.Minions.Where(e=> e.Value.IsSpecial);
                    target = WaypointGraph.GetClosestWaypoint((int)special.Pos.X, (int)special.Pos.Y+500);
                }
                else
                {
                    Minion special = (Minion)Game.Player2.Minions.Where(e => e.Value.IsSpecial);
                    target = WaypointGraph.GetClosestWaypoint((int)special.Pos.X, (int)special.Pos.Y - 500);
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
