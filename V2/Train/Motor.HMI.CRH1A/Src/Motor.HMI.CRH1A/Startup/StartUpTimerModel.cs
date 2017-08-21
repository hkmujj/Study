using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Motor.HMI.CRH1A.Startup
{
    class StartUpTimerModel
    {
        public StartUpTimerModel(StartUpView content)
        {
            Content = content;
            TimeCount = 0;
        }

        public StartUpView Content { private set; get; }

        public int TimeCount { set; get; }

        public Timer CurretnTimer { set; get; }

        
    }
}
