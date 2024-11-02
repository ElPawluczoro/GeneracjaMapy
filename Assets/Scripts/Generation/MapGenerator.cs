using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Generation
{
    public class MapGenerator : MonoBehaviour
    {
        [Range(1, 25)] public int Levels = 5;
        [Range(1, 9)] public int minNodesPerLevel = 1;
        [Range(1, 9)] public int maxNodesPerLevel = 3;

        private System.Random random = new System.Random();

        [HideInInspector] public Map _map;

        public void GenerateMap()
        {
            Map map = new Map();

            map.AddLevels(Levels);

            for (int i = 0; i <= Levels - 1; i++)
            {
                int nodesCount = random.Next(minNodesPerLevel, maxNodesPerLevel+1);
                for (int j = 0; j < nodesCount; j++) 
                {
                    map.AddNodes(i, new Node(GenerateNodeType(), j));
                }       
            }

            for (int i = 0; i <= (Levels - 1) - 1; i++)
            {
                for (int j = 0; j <= (map.levels[i].Count - 1); j++)
                {
                    GenerateConections(map.levels[i][j], map.levels[i + 1], map.levels[i]);
                }
            }

            for (int i = 1; i <= (Levels - 1) - 1; i++)
            {
                for (int j = 0; j <= (map.levels[i].Count - 1); j++)
                {
                    if (!CheckForConnections(map.levels[i][j]))
                    {
                        ConnectToNode(map.levels[i][j], map.levels[i - 1]);
                    }
                }
            }




            _map = map;
        }

        public ENodeType GenerateNodeType()
        {
            int randomMod = random.Next(0, 3);

            switch (randomMod)
            {
                case 0:
                    return ENodeType.BUFF;
                case 1:
                    return ENodeType.ENEMY;
                case 2:
                    return ENodeType.BUFF_AND_ENEMY;
                default:
                    Debug.LogWarning("Something went wrong with generating node type");
                    return ENodeType.ENEMY;
            }
        }

        public void GenerateConections(Node node, List<Node> nextLevel, List<Node> currentLevel)
        {
            if (nextLevel.Count == 1)
            {
                node.AddConnection(nextLevel[0]);
                nextLevel[0].AdddConnectedNode(node);
                return;
            }

            if (currentLevel.Count == 1)
            {
                for (int i = 0; i <= nextLevel.Count - 1; i++) 
                {
                    node.AddConnection(nextLevel[i]);
                    nextLevel[i].AdddConnectedNode(node);
                }
                return;
            }

            int closestNode = nextLevel.Count - 1 - (currentLevel.Count - 1 - node.position);
            if(closestNode <= 0)
            {
                node.AddConnection(nextLevel[0]);
                nextLevel[0].AdddConnectedNode(node);
            }
            else
            {
                node.AddConnection(nextLevel[closestNode]);
                nextLevel[closestNode].AdddConnectedNode(node);
            }

            if (node.position == currentLevel.Count - 1 && !node.connections.Contains(nextLevel[^1]))
            {
                node.AddConnection(nextLevel[^1]);
                nextLevel[^1].AdddConnectedNode(node);
            }

            if (node.position == 0 && !node.connections.Contains(nextLevel[0]))
            {
                node.AddConnection(nextLevel[0]);
                nextLevel[0].AdddConnectedNode(node);
            }

            for (int i = 1; i < nextLevel.Count - 1; i++)
            {
                if (node.connections.Contains(nextLevel[i])) continue;
                if(random.Next(0, 3) == 0)
                {
                    node.AddConnection(nextLevel[i]);
                    nextLevel[i].AdddConnectedNode(node);
                }
            }
        }

        public bool CheckForConnections(Node node)
        {
            if(node.connectedNodes.Count == 0) return false;
            return true;
        }

        public void ConnectToNode(Node node, List<Node> previousLevel)
        {
            int randomNode = random.Next(0, previousLevel.Count - 1);
            previousLevel[randomNode].AddConnection(node);
            node.AdddConnectedNode(previousLevel[randomNode]);
        }

    }
}
