using System.Drawing;

namespace Urban.QingDao3Line.MMI.底层共用
{
    //画笔
    public static class SolidBrushsItems
    {
        /// <summary>
        /// Color.Black
        /// </summary>
        public static readonly SolidBrush BlackBrush=new SolidBrush(Color.Black);

        /// <summary>
        /// Color.White
        /// </summary>
        public static readonly SolidBrush WhiteBrush=new SolidBrush(Color.White);

        /// <summary>
        /// Color.Red
        /// </summary>
        public static readonly SolidBrush RedBrush=new SolidBrush(Color.Red);

        /// <summary>
        /// Color.FromArgb(128,0,0)
        /// </summary>
        public static readonly SolidBrush DarkRedBrush=new SolidBrush(Color.FromArgb(128,0,0));

        /// <summary>
        /// Color.FromArgb(128,128,255)
        /// </summary>
        public static readonly SolidBrush BlueBrush=new SolidBrush(Color.FromArgb(128,128,255));
        
        /// <summary>
        /// Color.FromArgb(128,204,250)
        /// </summary>
        public static readonly SolidBrush BackGrounBrush = new SolidBrush(Color.FromArgb(85,148,255));

        public static readonly SolidBrush SoftYellow = new SolidBrush(Color.FromArgb(255,255,206));
       
        /// <summary>
        /// Color.Green
        /// </summary>
        public static readonly SolidBrush GreenBrush=new SolidBrush(Color.Green);

        /// <summary>
        /// Color.Yellow
        /// </summary>
        public static readonly SolidBrush YellowBrush=new SolidBrush(Color.Yellow);

        /// <summary>
        /// Color.FromArgb(255,153,0)
        /// </summary>
        public static readonly SolidBrush OrangeBrush=new SolidBrush(Color.FromArgb(255,153,0));

        /// <summary>
        /// Color.FromArgb(32,32,64)
        /// </summary>
        public static readonly SolidBrush SoftBlackBrush=new SolidBrush(Color.FromArgb(32,32,64));

        /// <summary>
        /// Color.FromArgb(220,220,240)
        /// </summary>
        public static readonly SolidBrush LightGreybBrush=new SolidBrush(Color.FromArgb(220,220,240));

        /// <summary>
        /// Color.FromArgb(150,150,170)
        /// </summary>
        public static readonly SolidBrush ShadowGreyBrush=new SolidBrush(Color.FromArgb(150,150,170));

        /// <summary>
        /// Color.FromArgb(0,64,128)
        /// </summary>
        public static readonly SolidBrush BrownBrush=new SolidBrush(Color.FromArgb(0,64,128));
    }

    //字体格式
    public static class FontItems
    {
        public static readonly Font DefaultFont=new Font("宋体",10.5f*Coordinate.Scaling,FontStyle.Regular);
        private static Font m_FontEx;

        public static Font Fonts_Regular(FontName strFont, float fontNumb, bool isBold)
        {
            m_FontEx=new Font(strFont.ToString(),
                fontNumb*Coordinate.Scaling,
                GetFontStyle(isBold)?FontStyle.Bold:FontStyle.Regular);
            return m_FontEx;
        }

        private static bool GetFontStyle(bool isBold)
        {
            if(Coordinate.Scaling<1)
                return false;
            else
            {
                if(isBold) return true;
                else return false;                
            }
        }

        public static StringFormat TheAlignment(FontRelated fontrelate)
        {
            var format=new StringFormat();
            switch (fontrelate)
            {
                 case FontRelated.居中:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    break;
                case FontRelated.靠左:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Near;
                    break;
                case FontRelated.靠右:
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Far;
                    break;
                case FontRelated.靠左上:
                    format.LineAlignment = StringAlignment.Near;
                    format.Alignment=StringAlignment.Near;
                    break;
                default:
                    format.LineAlignment=StringAlignment.Center;
                    format.Alignment=StringAlignment.Center;
                    break;
            }
            return format;
        }
    }


    ///<summary>
    ///闪烁
    ///</summary>
    public class FlashTimer
    {
        ///<summary>
        /// 闪烁用递增量
        /// </summary>
        private int m_FlashCount = 0;

        private float Time_contine=0.0f;

        ///<summary>
        /// 闪烁时间间隔
        /// </summary>
        private readonly int m_FlashTime = 0;

        public FlashTimer(int interval)
        {
            m_FlashTime = interval;
        }

        ///<summary>
        /// 闪烁时间间隔
        /// </summary>
        ///<return></return>
        public bool IsNeedFlash()
        {
            if (m_FlashCount < m_FlashTime*4)
            {
                ++m_FlashCount;
                return true;
            }
            else if(m_FlashCount>=m_FlashTime*4 && m_FlashCount<m_FlashTime*8)
            {
                ++m_FlashCount;
                return false;
            }
            else
            {
                m_FlashCount = 0;
                return false;
            }
        }

        public float GetTimeFlash()
        {
            Time_contine+=0.1f;
            return Time_contine;
        }

        public bool isDone(float _time)
        {
            if (Time_contine<_time)
                return false;
            else
                Time_contine=0.0f;
            return true;
        }
    }
}
