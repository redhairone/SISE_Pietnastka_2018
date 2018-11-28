using Analytics;
using Logic;
using Outputs;
using System;
using System.Threading;

namespace ConsoleProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Starter S = new Starter(args);
            S.Start();
        }
    }
}
