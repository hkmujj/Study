using System;
using System.Collections.Generic;
using System.Drawing;

namespace Motor.HMI.CRH1A.Running.DialStrategy
{
    /// <summary>
    /// 钟表盘的显示策略
    /// </summary>
    interface IDialStrategy
    {
        void Display(Graphics g,int second);

        Action<Graphics, int> DrawLightAction { set; get; }

        Action<Graphics, int> DrawDarkAction { set; get; }
    }
}
