using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Running.DialStrategy
{
    class OldDialStrategy : DialStrategyBase
    {
        public override void Display(Graphics g, int second)
        {
            for (int i = 0; i < 60; i++)
            {
                if (second < 15)
                {
                    if (second - i + 60 < 15 || i <= second)
                    {
                        DrawLightAction(g, i);
                    }
                    else
                    {
                        DrawDarkAction(g, i);
                    }
                }
                else
                {
                    if (second - i < 15 && second >= i)
                    {
                        DrawLightAction(g, i);
                    }
                    else
                    {
                        DrawDarkAction(g, i);
                    }
                }

            }
        }

    }
}
