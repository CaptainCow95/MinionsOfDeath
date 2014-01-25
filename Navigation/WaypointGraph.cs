using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Navigation
{
    class WaypointGraph
    {
        private List<WaypointNode> nodes;

        private double getDistance(WaypointNode node1, WaypointNode node2)
        {
            return Math.Sqrt(Math.Pow(node1.X - node2.X, 2) + Math.Pow(node1.Y - node1.Y, 2));
        }
    }
}