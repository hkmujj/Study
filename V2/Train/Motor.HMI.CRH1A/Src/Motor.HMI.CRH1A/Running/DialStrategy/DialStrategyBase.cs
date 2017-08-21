using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Running.DialStrategy
{
    abstract class DialStrategyBase: IDialStrategy
    {
        public abstract void Display(Graphics g, int second);

        public Action<Graphics, int> DrawLightAction { get; set; }

        public Action<Graphics, int> DrawDarkAction { get; set; }

        protected void OnDrawDark(Graphics g, int idx)
        {
            if (DrawDarkAction != null)
            {
                DrawDarkAction(g, idx);
            }
        }

        protected void OnDrawLight(Graphics g, int idx)
        {
            if (DrawLightAction != null)
            {
                DrawLightAction(g, idx);
            }
        }
    }
}
