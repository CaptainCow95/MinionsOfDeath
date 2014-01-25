using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MinionsOfDeath.Navigation
{
    public class WaypointGraph
    {
        // Storing nodes in dictionary, indexed by Tuple<_x,_y>
        private Dictionary<Tuple<int, int>, WaypointNode> nodes;

        public WaypointGraph(string filename)
        {
            nodes = new Dictionary<Tuple<int, int>, WaypointNode>();
            XDocument xdoc = XDocument.Load("Resources/WaypointTestGraph.xml");

            //add all the waypoints to the graph
            foreach (var item in xdoc.Elements("Waypoint"))
            {
                int x = int.Parse(item.Attribute("X").Value);
                int y = int.Parse(item.Attribute("Y").Value);
                nodes.Add(new Tuple<int, int>(x, y), new WaypointNode(this, x, y, new List<WaypointNode>()));
            }

            //find the neigbhors for every waypoint
            foreach (var item in xdoc.Elements("Waypoint"))
            {
                int x = int.Parse(item.Attribute("X").Value);
                int y = int.Parse(item.Attribute("Y").Value);
                Tuple<int, int> thisPoint = new Tuple<int, int>(x, y);
                foreach (var neighbor in item.Elements("Neighbor"))
                {
                    int xn = int.Parse(neighbor.Attribute("X").Value);
                    int yn = int.Parse(neighbor.Attribute("Y").Value);
                    Tuple<int, int> neighborPoint = new Tuple<int, int>(xn, yn);
                    nodes[thisPoint].Neighbors.Add(nodes[neighborPoint]);
                }
            }
        }

        private void AddNode(int x, int y)
        {
            //TODO: give node its neighbors
            List<WaypointNode> ns = new List<WaypointNode>();
            WaypointNode n = new WaypointNode(this, x, y, ns);
            nodes.Add(Tuple.Create(x, y), n);
        }

        private void ConnectNodes(WaypointNode aNode, WaypointNode bNode)
        {
            aNode.Neighbors.Add(bNode);
            bNode.Neighbors.Add(aNode);
        }

        public WaypointNode GetClosestWaypoint(int x, int y)
        {
            WaypointNode closest = nodes.ElementAt(0).Value;
            foreach (KeyValuePair<Tuple<int, int>, WaypointNode> item in nodes)
            {
                WaypointNode tempNode = item.Value;
                if (getDistance(x, y, tempNode.X, tempNode.Y) < getDistance(x, y, closest.X, closest.Y))
                {
                    closest = tempNode;
                }
            }
            return closest; 

        }

        private double getDistance(WaypointNode node1, WaypointNode node2)
        {
            return getDistance(node1.X, node2.X, node1.Y, node2.Y);
        }

        public static double getDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private WaypointNode getNode(int x, int y)
        {
            return nodes[Tuple.Create(x, y)];
        }

        public List<WaypointNode> pathfindDijkstra(WaypointNode start, WaypointNode end)
        {
            NodeRecord startRecord = new NodeRecord(start);

            List<NodeRecord> open = new List<NodeRecord>();
            open.Add(startRecord);
            List<NodeRecord> closed = new List<NodeRecord>();
            NodeRecord current = new NodeRecord();

            while (open.Count > 0)
            {
                current = open.Where(e => e.CostSoFar == open.Min(f => f.CostSoFar)).First();

                if (current.Node == end)
                    break;

                // connections = graph.getConnections(current);
                List<Connection> connections = new List<Connection>();
                foreach (WaypointNode n in current.Node.Neighbors)
                {
                    WaypointNode fromNode = current.Node;
                    Connection c = new Connection(getDistance(current.Node, n), current.Node, n);
                    connections.Add(c);
                }

                foreach (Connection connection in connections)
                {
                    WaypointNode endNode = connection.getToNode();
                    double endNodeCost = current.CostSoFar + connection.getCost();
                    NodeRecord endNodeRecord = closed.Where(e => e.Node == endNode).First();
                    if (endNodeRecord == null)
                        endNodeRecord = open.Where(e => e.Node == endNode).First();

                    // if (closed.Contains(endNode))
                    if (closed.Contains(endNodeRecord))
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
            if (current.Node != end)
            {
                return null;
            }
            else
            {
                List<WaypointNode> path = new List<WaypointNode>();
                path.Add(current.Connection.getFromNode());
                while (current.Node != start)
                {
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
}