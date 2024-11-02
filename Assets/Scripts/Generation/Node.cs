using Scripts.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Generation
{
    public class Node
    {
        public ENodeType nodeType;
        public int position;
        public List<Node> connections;
        public List<Node> connectedNodes;

        public Node(ENodeType nodeType, int position) 
        {
            connections = new List<Node>();
            connectedNodes = new List<Node>();
            this.nodeType = nodeType;
            this.position = position;   
        }

        public void AddConnection(Node node)
        {
            connections.Add(node);
        }

        public void AdddConnectedNode(Node node)
        {
            connectedNodes.Add(node);
        }
    }
}
