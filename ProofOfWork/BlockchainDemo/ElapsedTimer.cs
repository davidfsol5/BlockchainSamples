﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    class ElapsedTimer
    {
        DateTime startTime { get; set;  }

        public ElapsedTimer()
        {
            this.Reset();
        }

        public void Reset()
        {
            startTime = DateTime.Now;
        }
        public TimeSpan ElapsedTime()
        {
            return DateTime.Now - startTime;
        }
    }
}
