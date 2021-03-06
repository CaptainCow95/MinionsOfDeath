﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinionsOfDeath.Navigation;

namespace MinionsOfDeath.Behaviors.Actions
{
    public class PatrolHomeHalf : Action
    {

        private WaypointNode target;
        public PatrolHomeHalf(Minion owner)
            : base(owner)
        {
        }

        public override DoublePoint GetGoal()
        {
            bool player1 = Game.Player1.Minions.ContainsValue(_owner);
            //List<WaypointNode> target_nodes = new List<WaypointNode>();
            /*      These are guesses for base nodes: ...?
                    <Waypoint X="231" Y="1586" />
                    <Waypoint X="326" Y="1760" />
                    <Waypoint X="495" Y="1777" />

                    <Waypoint X="160" Y="47" />
                    <Waypoint X="499" Y="44" />
                    <Waypoint X="810" Y="37" />
                 * */
            if (target == null || WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y) == target)
            {
                if (player1)
                {
                    Random random = new Random();
                    int randX = random.Next(0, 1000);
                    int randY = random.Next(0, 900);

                    target = WaypointGraph.GetClosestWaypoint(randX, randY);
                }
                else
                {
                    Random random = new Random();
                    int randX = random.Next(0, 1000);
                    int randY = random.Next(900, 1800);

                    target = WaypointGraph.GetClosestWaypoint(randX, randY);
                }
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
