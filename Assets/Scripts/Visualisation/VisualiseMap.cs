using Scripts.Generation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Visualisation
{
    internal class VisualiseMap : MonoBehaviour
    {
        [SerializeField] private GameObject levelPrefab;
        [SerializeField] private GameObject canvas;

        [SerializeField] private GameObject BuffNode;
        [SerializeField] private GameObject EnemyNode;
        [SerializeField] private GameObject BuffAndEnemyNode;

        [SerializeField] private Transform startPosition;

        [SerializeField] private float nodeHeight = 100;
        [SerializeField] private float xBetweenNodes = 2;

        public void Visualise(Map map)
        {
            List<GameObject> levels = new List<GameObject>();

            for (int i = 0; i <= map.levels.Count - 1; i++)
            {
                var newLevel = Instantiate(levelPrefab);

                newLevel.transform.position = startPosition.position;
                newLevel.transform.position += new Vector3(0, nodeHeight * i, 0);

                levels.Add(newLevel);

                foreach (Node node in map.levels[i])
                {
                    Instantiate(GetNode(node), newLevel.transform);
                }

            }

            foreach (var level in levels)
            {
                if (level.transform.childCount % 2 == 0)
                {
                    float fixedX = xBetweenNodes / 2f;
                    int halfChild = level.transform.childCount / 2;
                    for (int j = 0; j <= halfChild; j++)
                    {
                        level.transform.GetChild(j).transform.position += new Vector3(-xBetweenNodes * (halfChild - j) + fixedX, 0, 0);
                    }
                    for (int k = level.transform.childCount - 1; k > halfChild; k--)
                    {
                        level.transform.GetChild(k).transform.position += new Vector3(xBetweenNodes * (k - halfChild) + fixedX, 0, 0);
                    }
                }
                else
                {
                    int halfChild = level.transform.childCount / 2;
                    for (int j = 0; j <= halfChild; ++j)
                    {
                        level.transform.GetChild(j).transform.position += new Vector3(-xBetweenNodes * (halfChild - j), 0, 0);
                    }
                    for (int k = level.transform.childCount - 1; k > halfChild; k--)
                    {
                        level.transform.GetChild(k).transform.position += new Vector3(xBetweenNodes * (k - halfChild), 0, 0);
                    }
                }
            }

            for (int i = 0; i <= map.levels.Count - 1; i++)
            {
                foreach (Node node in map.levels[i])
                {
                    foreach (Node connection in node.connections)
                    {
                        var lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
                        lineRenderer.startColor = Color.black;
                        lineRenderer.endColor = Color.black;
                        lineRenderer.startWidth = 0.05f;
                        lineRenderer.endWidth = 0.05f;
                        lineRenderer.positionCount = 2;
                        lineRenderer.useWorldSpace = true;
                        lineRenderer.SetPosition(0, levels[i].transform.GetChild(node.position).transform.position);
                        lineRenderer.SetPosition(1, levels[i + 1].transform.GetChild(connection.position).transform.position);
                    }
                }
            }
        }

        public GameObject GetNode(Node node)
        {
            switch (node.nodeType)
            {
                case ENodeType.BUFF:
                    return BuffNode;
                case ENodeType.ENEMY:
                    return EnemyNode;
                case ENodeType.BUFF_AND_ENEMY:
                    return BuffAndEnemyNode;
                default:
                    Debug.LogWarning("Something went wrong with visualising nodes");
                    return EnemyNode;
            }
        }


    }
}
