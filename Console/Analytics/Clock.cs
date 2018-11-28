using System;
using System.Diagnostics;

namespace Analytics
{
    public class Clock
    {
        private Stopwatch SW;

        public Clock()
        {
            SW = new Stopwatch();
        }

        public void Start()
        {
            SW.Start();
        }

        public void Stop()
        {
            SW.Stop();
        }

        public string GetTime()
        {
            string result = String.Format("{0:F3}", SW.Elapsed.TotalSeconds);
            SW.Reset();
            return result;
        }
    }
}
