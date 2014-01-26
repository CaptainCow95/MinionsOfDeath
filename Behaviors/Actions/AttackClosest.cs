﻿using MinionsOfDeath.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MinionsOfDeath.Behaviors.Actions
{
    internal class AttackClosest : Action
    {
        WaypointNode curNode;

        public AttackClosest(Minion owner) : base(owner) 
        {
            curNode = WaypointGraph.GetClosestWaypoint((int)_owner.Pos.X, (int)_owner.Pos.Y);
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
            List<WaypointNode> path = WaypointGraph.pathfindDijkstra(curNode, WaypointGraph.GetClosestWaypoint((int)closest.Pos.X, (int)closest.Pos.Y) );
            
            return new DoublePoint(path[0].X,path[0].Y);
        }
    }
}
