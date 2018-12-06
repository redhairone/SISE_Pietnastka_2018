using Logic;
using System.Collections.Generic;

namespace Algorithms
{
    public class DepthFirstSearch : Solution
    {
        private readonly int depthLimit;

        public DepthFirstSearch(Fifteen _state, string _searchOrder)
        {
            state = _state;
            strategyInfo = _searchOrder;
            maxDepth = 0;

            depthLimit = 20;
        }

        public override string Resolve()
        {
            Dictionary<Fifteen, Fifteen> visited = new Dictionary<Fifteen, Fifteen>();
            Stack<Fifteen> toVisit = new Stack<Fifteen>();

            string result = "";
            visitedFifteens = 1;

            bool finished = false;

            toVisit.Push(state);

            while(toVisit.Count != 0)
            {
                Fifteen current = toVisit.Pop();

                if (current.Depth > depthLimit) continue;
                else if (current.Depth > maxDepth) maxDepth = current.Depth;

                if (current.CheckCompletion())
                {
                    result = current.History;
                    finished = true;
                    break;
                }

                visited.Add(current, current);
                current.CreateNextFifteens(strategyInfo);

                List<Fifteen> nextFifteens = current.Next;
                nextFifteens.Reverse();

                foreach(Fifteen F in nextFifteens)
                {
                    if (visited.ContainsKey(F)) continue;
                    toVisit.Push(F);
                    visitedFifteens++;
                }
            }

            processedFifteens = visited.Count;

            return finished ? result : "There is no solution.";
        }
    }
}
