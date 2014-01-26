using System.Collections.Generic;

namespace MinionsOfDeath.Navigation
{
    public class WaypointNode
    {
        private int _x;
        private int _y;
        private bool isVisible = false;
        private List<WaypointNode> my_neighbors;

        public WaypointNode(int x, int y, List<WaypointNode> neighbors)
        {
            _x = x;
            _y = y;
            my_neighbors = neighbors;
        }

        public List<WaypointNode> Neighbors
        {
            get { return my_neighbors; }
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