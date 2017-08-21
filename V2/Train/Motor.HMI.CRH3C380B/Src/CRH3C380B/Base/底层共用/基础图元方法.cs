using System;
using System.Collections.Generic;
using System.Drawing;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    /// <summary>
    /// bool量对应图元的变化
    /// </summary>
    public class BoolCorrespodenceGraphic
    {
        private readonly int m_StartIndex;
        private readonly int m_Length;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="boolInitStates">图元变化需要的bool量</param>
        /// <param name="pelobjects">每种变化对应的图元状态</param>
        public BoolCorrespodenceGraphic(bool[] boolInitStates, PelObject[] pelobjects)
        {
            m_StartIndex = 0;
            m_Length = boolInitStates.Length;
            m_BoolInitStates = boolInitStates;
            int index = m_BoolInitStates.Length;
            if (pelobjects.Length == (int) Math.Pow(2, index))
            {
                switch (index)
                {
                    case 1:
                        for (int i = 0; i < 2; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) i, pelobjects[i]);
                        }

                        break;
                    case 2:
                        for (int i = 0; i < 4; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) (i + 2), pelobjects[i]);
                        }

                        break;
                    case 3:
                        for (int i = 0; i < 8; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) (i + 6), pelobjects[i]);
                        }

                        break;
                }

                m_BeInit = true;
                StateChange();
            }
            else
            {
                m_BeInit = false;
            }
        }

        public BoolCorrespodenceGraphic(bool[] boolInitStates, int startIndex, int length, PelObject[] pelobjects)
        {
            m_Length = length;
            m_StartIndex = startIndex;
            m_BoolInitStates = boolInitStates;
            int index = length;
            if (pelobjects.Length == (int) Math.Pow(2, index))
            {
                switch (index)
                {
                    case 1:
                        for (int i = 0; i < 2; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) i, pelobjects[i]);
                        }

                        break;
                    case 2:
                        for (int i = 0; i < 4; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) (i + 2), pelobjects[i]);
                        }

                        break;
                    case 3:
                        for (int i = 0; i < 8; i++)
                        {
                            m_CorrespodenceDict.Add((UnionOperation) (i + 6), pelobjects[i]);
                        }

                        break;
                }

                m_BeInit = true;
                StateChange();
            }
            else
            {
                m_BeInit = false;
            }
        }

        /// <summary>
        /// bool量变化后相应图元也需要变化
        /// </summary>
        public void StateChange()
        {
            if (!m_BeInit)
            {
                return;
            }

            switch (m_Length)
            {
                case 1:
                    m_PelObject = m_BoolInitStates[m_StartIndex + 0]
                        ? m_CorrespodenceDict[UnionOperation.T]
                        : m_CorrespodenceDict[UnionOperation.F];
                    break;
                case 2:
                    if (m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TT];
                    }
                    else if (m_BoolInitStates[m_StartIndex + 0] && !m_BoolInitStates[m_StartIndex + 1])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TF];
                    }
                    else if (!m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FT];
                    }
                    else
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FF];
                    }
                    break;
                case 3:
                    if (m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1] &&
                        m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TTT];
                    }
                    else if (m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1] &&
                             !m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TTF];
                    }
                    else if (m_BoolInitStates[m_StartIndex + 0] && !m_BoolInitStates[m_StartIndex + 1] &&
                             m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TFT];
                    }
                    else if (m_BoolInitStates[m_StartIndex + 0] && !m_BoolInitStates[m_StartIndex + 1] &&
                             !m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.TFF];
                    }
                    else if (!m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1] &&
                             m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FTT];
                    }
                    else if (!m_BoolInitStates[m_StartIndex + 0] && m_BoolInitStates[m_StartIndex + 1] &&
                             !m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FTF];
                    }
                    else if (!m_BoolInitStates[m_StartIndex + 0] &&
                             !m_BoolInitStates[m_StartIndex + 1] &&
                             m_BoolInitStates[m_StartIndex + 2])
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FFT];
                    }
                    else
                    {
                        m_PelObject = m_CorrespodenceDict[UnionOperation.FFF];
                    }
                    break;
            }
        }

        public void Draw(Graphics e)
        {
            if (m_BeInit && m_PelObject != null)
            {
                m_PelObject.DrawPelObject(e);
            }
        }

        private readonly bool[] m_BoolInitStates;

        private readonly bool m_BeInit;

        //指数

        private PelObject m_PelObject;

        private readonly Dictionary<UnionOperation, PelObject> m_CorrespodenceDict =
            new Dictionary<UnionOperation, PelObject>();

        public enum UnionOperation
        {
            F,
            T,

            FF,
            FT,
            TF,
            TT,

            FFF,
            FFT,
            FTF,
            FTT,
            TFF,
            TFT,
            TTF,
            TTT,
        }
    }

    /// <summary>
    /// 文字信息
    /// </summary>
    public class TextInfo
    {
        /// <summary>
        /// 文字信息构造函数-基本款
        /// </summary>
        /// <param name="text">需要绘制的文字</param>
        /// <param name="fontStyle">文字的格式(字体、大小、粗细)</param>
        /// <param name="textBrush">文字颜色</param>
        /// <param name="textLocal">文字位置</param>
        /// <param name="strFormat">文字对齐方式</param>
        public TextInfo(string text, Font fontStyle, SolidBrush textBrush, RectangleF textLocal, StringFormat strFormat)
        {
            m_Text = text;
            m_FontStyle = fontStyle;
            m_TextBrush = textBrush;
            m_TextLocal = textLocal;
            m_StrFormat = strFormat;
        }

        /// <summary>
        /// 文字信息构造函数-对齐方式自填款
        /// </summary>
        /// <param name="text">需要绘制的文字</param>
        /// <param name="textType">默认格式</param>
        /// <param name="textLocal">文字位置</param>
        /// /// <param name="strFormat">对齐方式</param>
        public TextInfo(string text, TextType textType, RectangleF textLocal, StringFormat strFormat)
        {
            m_Text = text;
            if (textType == TextType.默认英文白 || textType == TextType.默认英文黑)
            {
                m_FontStyle = FontsItems.FontE12;
                m_TextBrush = textType == TextType.默认英文白 ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
            }
            if (textType == TextType.默认中文白 || textType == TextType.默认中文黑)
            {
                m_FontStyle = FontsItems.FontC11;
                m_TextBrush = textType == TextType.默认中文白 ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.BlackBrush;
            }
            m_TextLocal = textLocal;
            m_StrFormat = strFormat;
        }

        /// <summary>
        /// 文字信息构造函数-默认居中款
        /// </summary>
        /// <param name="text"></param>
        /// <param name="textType"></param>
        /// <param name="textLocal"></param>
        public TextInfo(string text, TextType textType, RectangleF textLocal)
            : this(text, textType, textLocal, FontsItems.TheAlignment(FontRelated.居中))
        {

        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e">绘图参数</param>
        /// <param name="g"></param>
        public void DrawText(Graphics g)
        {
            g.DrawString(m_Text, m_FontStyle, m_TextBrush, m_TextLocal, m_StrFormat);
        }

        private readonly string m_Text = string.Empty;
        private readonly Font m_FontStyle;
        private readonly StringFormat m_StrFormat;
        private readonly RectangleF m_TextLocal;
        private readonly SolidBrush m_TextBrush;
    }

    /// <summary>
    /// 图元基类
    /// </summary>
    public abstract class PelObject
    {
        public abstract void DrawPelObject(Graphics e);
    }

    /// <summary>
    /// 空图元
    /// </summary>
    public class PelObjectEmpty : PelObject
    {
        /// <summary>
        /// 啥都不画
        /// </summary>
        /// <param name="e"></param>
        public override void DrawPelObject(Graphics e)
        {
        }
    }

    /// <summary>
    /// 图元中只包含文字
    /// </summary>
    public class PelObjectText : PelObject
    {
        /// <summary>
        /// 构造方法-需要画外框
        /// </summary>
        /// <param name="textPen">文字信息</param>
        /// <param name="local">要画的矩形框</param>
        /// <param name="rectPen">矩形框颜色</param>
        /// <param name="flash"></param>
        public PelObjectText(TextInfo textPen, RectangleF local, Pen rectPen, TimeCounter flash)
        {
            m_TextInfo = textPen;
            RectangleF local1 = local;
            m_RectPen = rectPen;
            if (flash != null)
            {
                m_Flash = flash;
                m_NeedFlash = true;
            }
            m_LocalRect = new Rectangle((int) local1.X, (int) local1.Y, (int) local1.Width, (int) local1.Height);
        }

        /// <summary>
        /// 构造方法-不需要画外框
        /// </summary>
        /// <param name="text">文字信息</param>
        /// <param name="local">文字所在矩形框</param>
        /// <param name="flash"></param>
        public PelObjectText(TextInfo text, RectangleF local, TimeCounter flash)
            : this(text, local, null, flash)
        {

        }

        /// <summary>
        /// 绘制图元
        /// </summary>
        /// <param name="e"></param>
        public override void DrawPelObject(Graphics e)
        {
            if (m_NeedFlash)
            {
                if (m_Flash.FlashBegin())
                {
                    Draw(e);
                }
            }
            else
            {
                Draw(e);
            }

        }

        private void Draw(Graphics g)
        {
            m_TextInfo.DrawText(g);
            if (m_RectPen != null)
            {
                g.DrawRectangle(m_RectPen, m_LocalRect);
            }
        }

        private readonly TextInfo m_TextInfo;
        private readonly Rectangle m_LocalRect;
        private readonly Pen m_RectPen;
        private readonly TimeCounter m_Flash;
        private readonly bool m_NeedFlash;

    }

    /// <summary>
    /// 图元中只包含画刷颜色或者图片
    /// </summary>
    public class PelObjectBrushOrImage : PelObject
    {
        /// <summary>
        /// 构造方法-完整
        /// </summary>
        /// <param name="drawBrush">判断画颜色还是图片,true为颜色</param>
        /// <param name="theBrush">颜色</param>
        /// <param name="img">图片</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectBrushOrImage(bool drawBrush, SolidBrush theBrush, Image img, RectangleF local, TimeCounter flash)
        {
            DrawBrush = drawBrush;

            if (DrawBrush)
            {
                TheBrush = theBrush;
            }
            else
            {
                Img = img;
            }
            Local = local;

            if (RectPen != null)
            {
                DrawPen = true;
                Localrect = new Rectangle((int) Local.X, (int) Local.Y, (int) Local.Width, (int) Local.Height);
            }

            if (flash != null)
            {
                NeedFlash = true;
                Flash = flash;
            }
        }

        /// <summary>
        /// 构造方法-颜色
        /// </summary>
        /// <param name="theBrush">颜色</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectBrushOrImage(SolidBrush theBrush, RectangleF local, TimeCounter flash)
            : this(true, theBrush, null, local, flash)
        {

        }

        /// <summary>
        /// 构造方法-图片
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectBrushOrImage(Image img, RectangleF local, TimeCounter flash)
            : this(false, null, img, local, flash)
        {

        }

        /// <summary>
        /// 画图元
        /// </summary>
        /// <param name="e"></param>
        public override void DrawPelObject(Graphics e)
        {
            if (NeedFlash)
            {
                if (Flash.FlashBegin())
                {
                    Draw(e);
                }
            }
            else
            {
                Draw(e);
            }
        }

        private void Draw(Graphics g)
        {
            if (DrawBrush)
            {
                g.FillRectangle(TheBrush, Local);
            }
            else
            {
                g.DrawImage(Img, Local);
            }

            if (DrawPen)
            {
                g.DrawRectangle(RectPen, Localrect);
            }
        }

        protected bool DrawBrush;
        protected SolidBrush TheBrush;
        protected Image Img;
        protected Pen RectPen;
        protected RectangleF Local;
        protected TimeCounter Flash;

        protected bool DrawPen;
        protected bool NeedFlash;
        protected Rectangle Localrect;

    }

    /// <summary>
    /// 图元中包含一种画刷颜色或者一张图片加文字
    /// </summary>
    public class PelObjectOneByOne : PelObjectBrushOrImage
    {
        /// <summary>
        /// 构造方法-完整
        /// </summary>
        /// <param name="textInfo">文字信息</param>
        /// <param name="drawBrush">判断画颜色还是图片,true为颜色</param>
        /// <param name="theBrush">颜色</param>
        /// <param name="img">图片</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectOneByOne(TextInfo textInfo, bool drawBrush, SolidBrush theBrush, Image img, RectangleF local,
            TimeCounter flash)
            : base(drawBrush, theBrush, img, local, flash)
        {
            m_TextInfo = textInfo;
        }

        /// <summary>
        /// 构造方法-文字加底色
        /// </summary>
        /// <param name="textInfo">文字信息</param>
        /// <param name="theBrush">颜色</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectOneByOne(TextInfo textInfo, SolidBrush theBrush, RectangleF local, TimeCounter flash)
            : base(theBrush, local, flash)
        {
            m_TextInfo = textInfo;
        }

        /// <summary>
        /// 构造方法-文字加底图
        /// </summary>
        /// <param name="textInfo">文字信息</param>
        /// <param name="img">图片</param>
        /// <param name="local">范围</param>
        /// <param name="flash">需要闪烁就添加对象</param>
        public PelObjectOneByOne(TextInfo textInfo, Image img, RectangleF local, TimeCounter flash)
            : base(true, null, img, local, flash)
        {
            m_TextInfo = textInfo;
        }

        /// <summary>
        /// 绘图
        /// </summary>
        /// <param name="e"></param>
        public override void DrawPelObject(Graphics e)
        {
            if (NeedFlash)
            {
                if (Flash.FlashBegin())
                {
                    Draw(e);
                }
            }
            else
            {
                Draw(e);
            }
        }

        private void Draw(Graphics g)
        {
            if (DrawBrush)
            {
                g.FillRectangle(TheBrush, Local);
            }
            else
            {
                g.DrawImage(Img, Local);
            }

            if (DrawPen)
            {
                g.DrawRectangle(RectPen, Localrect);
            }

            m_TextInfo.DrawText(g);
        }

        private readonly TextInfo m_TextInfo;
    }

    /// <summary>
    /// 图元中的元素属于复合型
    /// </summary>
    public class PelObjectRecombination : PelObject
    {
        public override void DrawPelObject(Graphics e)
        {
            throw new NotImplementedException();
        }
    }
}