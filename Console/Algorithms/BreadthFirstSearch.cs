﻿using Logic;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public class BreadthFirstSearch : Solution
    {
        public BreadthFirstSearch(Fifteen _state, string _searchOrder)
        {
            state = _state;
            strategyInfo = _searchOrder;

            maxDepth = 0;
        }

        public override string Resolve()
        {
            Queue<Fifteen> notVisited = new Queue<Fifteen>();
            Queue<Fifteen> visited = new Queue<Fifteen>();

            string result = "";

            bool finished = false;

            notVisited.Enqueue(state);

            while(notVisited.Count > 0)
            {
                Fifteen current = notVisited.Dequeue();
                visited.Enqueue(current);

                if (current.Depth > maxDepth) maxDepth = current.Depth;

                if(current.CheckCompletion())
                {
                    result = current.History;
                    finished = true;
                    break;
                }

                current.CreateNextFifteens(strategyInfo);

                foreach(Fifteen element in current.Next)
                {
                    if (!CheckRepetition(element, notVisited.Concat(visited))) notVisited.Enqueue(element);
                }
            }

            visitedFifteens = visited.Count + notVisited.Count;
            processedFifteens = visited.Count;

            return finished ? result : "There is no solution.";
        }

        public bool CheckRepetition(Fifteen _state, IEnumerable<Fifteen> _states)
        {
            foreach(Fifteen element in _states)
            {
                if(element.CheckSimilarity(_state)) return true;
            }
            return false;
        }
    }
}
