using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Navigation
{
    class WaypointGraph
    {
        // Storing nodes in dictionary, indexed by Tuple<_x,_y>
        private Dictionary<Tuple<int,int>,WaypointNode> nodes;

        private void addNode(int x, int y)
        {
            //TODO: give node its neighbors
            List<WaypointNode> ns = new List<WaypointNode>();
            WaypointNode n = new WaypointNode(this,x,y, ns);
            nodes.Add(Tuple.Create(x,y),n);
        }

        private WaypointNode getNode(int x, int y)
        {
            return nodes[Tuple.Create(x, y)];
        }

        private double getDistance(WaypointNode node1, WaypointNode node2)
        {
            return getDistance(node1.X, node2.X, node1.Y, node2.Y);
        }
        private double getDistance(int x1, int x2, int y1, int y2){
            return Math.Sqrt(Math.Pow(x2 - x1,2) + Math.Pow(y2 - y1, 2));
        }

        private void pathfindDijkstra(WaypointNode start, WaypointNode end)
        {
            Path startPath = new Path(start, 0);

            //TODO: Change this, it is not correct
            List<Path> open = new List<Path>();
            
        }
    }
}