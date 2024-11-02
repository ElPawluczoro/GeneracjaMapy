using Scripts.Generation;
using System.Collections.Generic;
using System.Diagnostics;

namespace Scripts.Generation
{
    public class Map
    {
        public List<List<Node>> levels; 

        public Map() 
        {
            levels = new List<List<Node>>();
        }

        public void AddLevels(int count)
        {
            for (int i = 0; i <= count - 1; i++)
            {
                levels.Add(new List<Node>());
            }
        }

        public void AddNodes(int iLevel, Node node)
        {
            if(iLevel >= levels.Count)
            {
                return;
            }
            levels[iLevel].Add(node);
        }
    }
}
