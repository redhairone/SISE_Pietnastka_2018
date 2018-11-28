using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public abstract class Solution
    {
        protected Fifteen state;
        protected string searchOrder;
        protected int maxDepth;
        protected int visitedFifteens;
        protected int processedFifteens;

        public Fifteen State { get => state; }
        public string SearchOrder { get => searchOrder; }
        public int MaxDepth { get => maxDepth; }
        public int VisitedFifteens { get => visitedFifteens; }
        public int ProcessedFifteens { get => processedFifteens; set => processedFifteens = value; }

        public abstract string Resolve();
    }
}
