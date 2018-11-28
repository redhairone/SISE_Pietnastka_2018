using Logic;

namespace Algorithms
{
    public abstract class Solution
    {
        protected Fifteen state;
        protected string strategyInfo;
        protected int maxDepth;
        protected int visitedFifteens;
        protected int processedFifteens;

        public Fifteen State { get => state; }
        public string StrategyInfo { get => strategyInfo; }
        public int MaxDepth { get => maxDepth; }
        public int VisitedFifteens { get => visitedFifteens; }
        public int ProcessedFifteens { get => processedFifteens; set => processedFifteens = value; }

        public abstract string Resolve();
    }
}
