using System.Collections.Generic;

namespace MinionsOfDeath.Navigation
{
    internal class Path
    {
        WaypointNode node;
        List<WaypointNode> connection;
        double cost;

        public Path(WaypointNode node, double cost)
        {
            this.node = node;
            connection = new List<WaypointNode>();
            this.cost = cost;
        }
    }
}