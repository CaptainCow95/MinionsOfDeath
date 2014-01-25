using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionsOfDeath.Navigation
{
    class WaypointNode
    {
        private List<WaypointNode> neighbors;
        private int _x;
        private int _y;

        public WaypointNode(int x, int y)
        {

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