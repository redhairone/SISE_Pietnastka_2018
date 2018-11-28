using Algorithms;
using Analytics;
using Outputs;
using System;

namespace Logic
{
    class Starter
    {
        #region ATTRIBUTES
        private string strategy;
        private string strategyInfo;
        private string fifteenFile;
        private string resultFile;
        private string metaDataFile;

        private ReaderWriter RW;
        private Fifteen FF;
        private Solution SS;
        private Clock CC;

        private string result;
        #endregion // ATTRIBUTES

        #region CONSTRUCTOR
        public Starter(string[] _args)
        {
            if (_args.Length != 5)
            {
                throw new Exception("Not enough arguments.");
            }

            strategy = _args[0];
            strategyInfo = _args[1];
            fifteenFile = _args[2];
            resultFile = _args[3];
            metaDataFile = _args[4];

            RW = new ReaderWriter();
            CC = new Clock();
        }
        #endregion // CONSTRUCTOR

        public void Start()
        {
            FF = RW.ReadFifteen(fifteenFile);

            switch (strategy)
            {
                case "bfs":
                    SS = new BreadthFirstSearch(FF, strategyInfo);
                    break;
                case "dfs":
                    SS = new DepthFirstSearch(FF, strategyInfo);
                    break;
                case "astr":

                    break;
            }

            CC.Start();
            result = SS.Resolve();
            CC.Stop();

            RW.WriteResult(resultFile, result);
            RW.WriteMetaData(metaDataFile, result.Length, SS.VisitedFifteens, SS.ProcessedFifteens, SS.MaxDepth, CC.GetTime());
        }
    }
}
