using Scripts.Generation;
using Scripts.Visualisation;
using System; 
using UnityEngine;

namespace Scripts
{
    internal class GameManager : MonoBehaviour
    {
        private MapGenerator mapGenerator;
        private VisualiseMap visualiseMap;

        private void Start()
        {
            mapGenerator = FindFirstObjectByType<MapGenerator>();
            visualiseMap = FindFirstObjectByType<VisualiseMap>();

            mapGenerator.GenerateMap();
            visualiseMap.Visualise(mapGenerator._map);
        }
    }
}
