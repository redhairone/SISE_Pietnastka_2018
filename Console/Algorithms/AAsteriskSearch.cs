using Logic;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public class AAsteriskSearch : Solution
    {
        public AAsteriskSearch(Fifteen _state, string _heuristic)
        {
            state = _state;
            strategyInfo = _heuristic;

            maxDepth = 0;
        }

        public override string Resolve()
        {
            List<Fifteen> visited = new List<Fifteen>();
            List<Fifteen> notVisited = new List<Fifteen>();

            string result = "";

            bool finished = false;

            notVisited.Add(state);

            while(notVisited.Count > 0)
            {
                Fifteen current = notVisited.First();
                notVisited.Remove(current);

                if (current.Depth > maxDepth) maxDepth = current.Depth;

                if(current.CheckCompletion())
                {
                    result = current.History;
                    finished = true;
                    break;
                }

                visited.Add(current);
                current.CreateNextFifteens("LRUD");

                foreach(Fifteen item in current.Next)
                {
                    if (!CheckRepetition(item, visited))
                    {
                        item.CountHeuristic(strategyInfo);
                        notVisited.Add(item);
                    }

                }

                notVisited.Sort();
            }

            visitedFifteens = visited.Count + notVisited.Count;
            processedFifteens = visited.Count;

            return finished ? result : "There is no solution.";
        }

        public bool CheckRepetition(Fifteen _state, IEnumerable<Fifteen> _states)
        {
            foreach (Fifteen element in _states)
            {
                if (element.CheckSimilarity(_state)) return true;
            }
            return false;
        }
    }
}
