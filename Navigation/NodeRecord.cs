namespace MinionsOfDeath.Navigation
{
    public class NodeRecord
    {
        private Connection connection;
        private double costSoFar;
        private WaypointNode node;

        public NodeRecord()
        {
        }

        public NodeRecord(WaypointNode node)
        {
            this.node = node;
            costSoFar = 0;
        }

        public Connection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public double CostSoFar
        {
            get { return costSoFar; }
            set { costSoFar = value; }
        }

        public WaypointNode Node
        {
            get { return node; }
            set { node = value; }
        }
    }
}