using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Navigation
{
    class WaypointNode
    {
        private WaypointGraph my_graph;
        private List<WaypointNode> my_neighbors;
        private int _x;
        private int _y;
        private bool isVisible = false;

        public WaypointNode(WaypointGraph graph, int x, int y, List<WaypointNode> neighbors)
        {
            my_graph = graph;
            _x = x;
            _y = y;
            my_neighbors = neighbors;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}