using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    class ElapsedTimer
    {
        DateTime startTime { get; }

        public ElapsedTimer()
        {
            startTime = DateTime.Now;
        }

        public TimeSpan ElapsedTime()
        {
            return DateTime.Now - startTime;
        }
    }
}
