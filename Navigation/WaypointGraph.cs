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

        private Path pathfindDijkstra(WaypointNode start, WaypointNode end)
        {
            NodeRecord startRecord = new NodeRecord(start);

            List<NodeRecord> open = new List<NodeRecord>();
            open.Add(startRecord);
            List<NodeRecord> closed = new List<NodeRecord>();
            NodeRecord current = new NodeRecord();

            while (open.Count > 0){
                current = open.Where(e => e.CostSoFar == open.Min(f => f.CostSoFar)).First();

                if (current.Node == end)
                    break;

                // connections = graph.getConnections(current);
                List<Connection> connections = new List<Connection>();
                foreach (WaypointNode n in current.Node.Neighbors)
                {
                    WaypointNode fromNode = current.Node;
                    Connection c = new Connection(getDistance(current.Node, n), current.Node , n);
                    connections.Add(c);
                }

                foreach (Connection connection in connections){
                    WaypointNode endNode = connection.getToNode();
                    double endNodeCost = current.CostSoFar + connection.getCost();
                    NodeRecord endNodeRecord = closed.Where(e => e.Node == endNode).First();
                    if (endNodeRecord==null)
                        endNodeRecord = open.Where(e => e.Node == endNode).First();

                    // if (closed.Contains(endNode))
                    if(closed.Contains(endNodeRecord))
                        continue;
                    // else if (open.Contains(endNode))
                    else if (open.Contains(endNodeRecord))
                    {
                        //endNodeRecord = open.Find(endNode);
                        if (endNodeRecord.CostSoFar <= endNodeCost)
                            continue;
                    }
                    else
                    {
                        endNodeRecord = new NodeRecord(endNode);
                    }
                    endNodeRecord.CostSoFar = endNodeCost;
                    endNodeRecord.Connection = connection;

                    if (!open.Contains(endNodeRecord))
                    {
                        open.Add(endNodeRecord);
                    }
                }
                open.Remove(current);
                closed.Add(current);    
            }
            if (current.Node != end){
                return null;
            }
            else
            {
                Path path = new Path();
                path.Add(current.Connection.getFromNode());
                while (current.Node != start){
                    // path += current.connection;
                    path.Add(current.Connection.getToNode()); 
                    // current = current.connection.getFromNode();
                    current = closed.Where(e => e.Node == current.Connection.getFromNode()).First();
                }
                path.Reverse();
                return path;
            }
        }
    }


    public class Connection
    {
        double cost;
        WaypointNode fromNode;
        WaypointNode toNode;

        public Connection(double cost, WaypointNode fromNode, WaypointNode toNode)
        {
            this.cost = cost;
            this.fromNode = fromNode;
            this.toNode = toNode;
        }

        public double getCost() { return cost; }
        public WaypointNode getFromNode() { return fromNode; }     
        public WaypointNode getToNode() { return toNode; }
    }
     
    public class NodeRecord
    {
        WaypointNode node;
        Connection connection;
        double costSoFar;

        public NodeRecord() { }
        public NodeRecord(WaypointNode node)
        {
            this.node = node;
            costSoFar = 0;
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
        public Connection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
    }
}