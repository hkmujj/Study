using System.Drawing;
using Subway.ATC.Casco.Common;


namespace Subway.ATC.Casco.MMI_Main
{
    public class Screensaver
    {
        /// <summary>
        /// 是否屏保
        /// </summary>
        public static bool IsScreensaver;
        public void Draw(Graphics g)
        {
            if (IsScreensaver)
            {
                g.FillRectangle(FormatStyle.BlackSolidBrush, 0, 0, 800, 600);
            }
        }
    }
}
