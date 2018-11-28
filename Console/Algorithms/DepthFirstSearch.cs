using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class DepthFirstSearch : Solution
    {
        #region ATTRIBUTES
        private int depthLimit;
        #endregion //ATTRIBUTES

        #region CONSTRUCTORS
        public DepthFirstSearch(Fifteen _state, string _searchOrder)
        {
            state = _state;
            searchOrder = _searchOrder;
            maxDepth = 0;

            depthLimit = 20;
        }
        #endregion //CONSTRUCTORS

        public override string Resolve()
        {
            Dictionary<Fifteen, Fifteen> visited = new Dictionary<Fifteen, Fifteen>(new FifteenComparer());
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

                if(visited.ContainsKey(current))
                {
                    if (current.Depth >= visited[current].Depth) continue;
                    else visited.Remove(current);
                }

                visited.Add(current, current);
                current.CreateNextFifteens(searchOrder);

                List<Fifteen> nextFifteens = current.Next;
                nextFifteens.Reverse();

                foreach(Fifteen F in nextFifteens)
                {
                    toVisit.Push(F);
                    visitedFifteens++;
                }
            }

            processedFifteens = visited.Count;

            return finished ? result : "There is no solution.";
        }
    }
}
