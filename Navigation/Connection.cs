namespace MinionsOfDeath.Navigation
{
    public class Connection
    {
        private double cost;
        private WaypointNode fromNode;
        private WaypointNode toNode;

        public Connection(double cost, WaypointNode fromNode, WaypointNode toNode)
        {
            this.cost = cost;
            this.fromNode = fromNode;
            this.toNode = toNode;
        }

        public double getCost()
        {
            return cost;
        }

        public WaypointNode getFromNode()
        {
            return fromNode;
        }

        public WaypointNode getToNode()
        {
            return toNode;
        }
    }
}