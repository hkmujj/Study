using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    //牵引制动画面
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class QianYZD : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "牵引制动画面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            //3
            nErrorObjectIndex = -1;

            m_Img = this.GetImages();

            InitData();

            return true;
        }

        #endregion#

        #region ::::::::::::::::::::::::event override:::::::::::::::::::::::::::::#

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
        }

        #endregion#

        #region :::::::::::::::::::::::: ex funes ::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                m_BValue[i] = BoolList[UIObj.InBoolList[i]];
                m_OldbValue[i] = BoolOldList[UIObj.InBoolList[i]];
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }
            for (int i = 0; i < UIObj.OutBoolList.Count; i++)
            {
                m_SetBValue[i] = BoolList[UIObj.OutBoolList[i]];
                m_SetBValueNumb[i] = UIObj.OutBoolList[i];
            }

        }

        /// <summary>
        /// 画刻度
        /// </summary>
        /// <param name="e"></param>
        private void DrawGraduation(Graphics e, int raiseNumb, int maxNumb, float width, bool direction)
        {
            if (direction)
            {
                for (int i = 0; i < maxNumb; i++)
                {
                    if (i % 10 == 0 && i > 0)
                    {
                        e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X + raiseNumb - 10, m_PDrawPoint[0].Y + (i + 1) * width),
                            new PointF(m_PDrawPoint[0].X + raiseNumb, m_PDrawPoint[0].Y + (i + 1) * width));
                    }
                    else
                    {
                        e.DrawLine(Common.WhitePen1, new PointF(m_PDrawPoint[0].X + raiseNumb - 5, m_PDrawPoint[0].Y + (i + 1) * width),
                            new PointF(m_PDrawPoint[0].X + raiseNumb, m_PDrawPoint[0].Y + (i + 1) * width));
                    }
                }
            }
            else
            {
                for (int i = 0; i < maxNumb; i++)
                {
                    if (i % 10 == 0 && i > 0)
                    {
                        e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X + raiseNumb + 10, m_PDrawPoint[0].Y + (i + 1) * width),
                            new PointF(m_PDrawPoint[0].X + raiseNumb, m_PDrawPoint[0].Y + (i + 1) * width));
                    }
                    else
                    {
                        e.DrawLine(Common.WhitePen1, new PointF(m_PDrawPoint[0].X + raiseNumb + 5, m_PDrawPoint[0].Y + (i + 1) * width),
                            new PointF(m_PDrawPoint[0].X + raiseNumb, m_PDrawPoint[0].Y + (i + 1) * width));
                    }
                }
            }

        }

        /// <summary>
        /// 长条框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            #region ::::::::::::::::::::::::::::::::: 前3个刻度条 :::::::::::::::::::::::::::::::::::::::::::::
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    //6条纵向线条
                    e.DrawLine(Common.WhitePen1, new PointF(m_PDrawPoint[0].X + i * 100 + j * 25, m_PDrawPoint[0].Y),
                        new PointF(m_PDrawPoint[0].X + i * 100 + j * 25, m_PDrawPoint[0].Y + 250));
                }
                //3条顶部外延线
                e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X - 15 + i * 100, m_PDrawPoint[0].Y),
                    new PointF(m_PDrawPoint[0].X + i * 100, m_PDrawPoint[0].Y));
                //3条底部封口线
                e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X - 15 + i * 100, m_PDrawPoint[0].Y + 250),
                    new PointF(m_PDrawPoint[0].X + 25 + i * 100, m_PDrawPoint[0].Y + 250));
            }
            DrawGraduation(e, 0, 39, 6.25f, true);
            DrawGraduation(e, 100, 49, 5f, true);
            DrawGraduation(e, 200, 19, 12.5f, true);

            e.DrawString("40", Common.Txt12FontR, Common.WhiteBrush,
                new PointF(m_PDrawPoint[0].X - 35, m_PDrawPoint[0].Y - 10));
            e.DrawString("500", Common.Txt12FontR, Common.WhiteBrush,
                new PointF(m_PDrawPoint[0].X + 100 - 45, m_PDrawPoint[0].Y - 10));
            e.DrawString("200", Common.Txt12FontR, Common.WhiteBrush,
                new PointF(m_PDrawPoint[0].X + 200 - 45, m_PDrawPoint[0].Y - 10));
            #endregion

            #region :::::::::::::::::::::::::::::: 牵引/制动 :::::::::::::::::::::::::::::::::::::
            //
            for (int i = 0; i < 7; i++)
            {
                e.DrawLine(Common.WhitePen1, new PointF(m_PDrawPoint[0].X + 280 + i * 25, m_PDrawPoint[0].Y),
                    new PointF(m_PDrawPoint[0].X + 280 + i * 25, m_PDrawPoint[0].Y + 250));
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X + 280 - 15 + i * 165, m_PDrawPoint[0].Y),
                    new PointF(m_PDrawPoint[0].X + 280 + i * 165, m_PDrawPoint[0].Y));
            }
            e.DrawLine(Common.WhitePen2, new PointF(m_PDrawPoint[0].X + 280 - 15, m_PDrawPoint[0].Y + 250),
                new PointF(m_PDrawPoint[0].X + 280 + 165, m_PDrawPoint[0].Y + 250));

            DrawGraduation(e, 280, 19, 12.5f, true);
            DrawGraduation(e, 430, 19, 12.5f, false);

            e.DrawString("200", Common.Txt12FontR, Common.WhiteBrush,
                new PointF(m_PDrawPoint[0].X + 280 - 45, m_PDrawPoint[0].Y - 10));
            #endregion

            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Str1[i], Common.Txt12FontB, Common.WhiteBrush,
                    new RectangleF(m_PDrawPoint[10 + i], new Size(80, 50)), Common.DrawFormat);
            }
        }

        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="e"></param>
        private void DrawGrogressBar(Graphics e)
        {
            if (m_FValue[0] >= 40) m_FValue[0] = 40;
            e.DrawString(m_FValue[0].ToString("0.0"), Common.Txt12FontR, Common.GreenBrush,
                new RectangleF(m_PDrawPoint[14], m_FSize), Common.DrawFormat);

            if (m_FValue[1] >= 500) m_FValue[1] = 500;

            for (int i = 2; i < 9; i++)
            {
                if (m_FValue[i] >= 200) m_FValue[i] = 200;
            }
            for (int i = 0; i < 3; i++)
            {
                m_GrogressBar[i].DrawRectangle(e, ref m_FValue[i], 3);
            }
            m_GrogressBar[3].DrawRectangle(e, ref m_FValue[3], 3, m_BValue[0] ? Common.PinkBrush : Common.YellowBrush);
            m_GrogressBar[4].DrawRectangle(e, ref m_FValue[4], 3, m_BValue[1] ? Common.PinkBrush : Common.MarineBlueBrush);
            m_GrogressBar[5].DrawRectangle(e, ref m_FValue[5], 3, m_BValue[2] ? Common.PinkBrush : Common.GreenBrush);
            m_GrogressBar[6].DrawRectangle(e, ref m_FValue[6], 3, m_BValue[3] ? Common.PinkBrush : Common.YellowBrush);
            m_GrogressBar[7].DrawRectangle(e, ref m_FValue[7], 3, m_BValue[4] ? Common.PinkBrush : Common.MarineBlueBrush);
            m_GrogressBar[8].DrawRectangle(e, ref m_FValue[8], 3, m_BValue[5] ? Common.PinkBrush : Common.GreenBrush);

            for (int i = 1; i < 3; i++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[i]).ToString(), Common.Txt12FontR, Common.GreenBrush,
                    new RectangleF(m_PDrawPoint[14 + i], m_FSize), Common.DrawFormat);
            }

            e.DrawString(Convert.ToInt32(m_FValue[3]).ToString(), Common.Txt12FontR,
                m_BValue[0] ? Common.PinkBrush : Common.YellowBrush,
                new RectangleF(m_PDrawPoint[17], m_FSize), Common.DrawFormat);
            e.DrawString(Convert.ToInt32(m_FValue[4]).ToString(), Common.Txt12FontR,
                m_BValue[1] ? Common.PinkBrush : Common.MarineBlueBrush,
                new RectangleF(m_PDrawPoint[18], m_FSize), Common.DrawFormat);
            e.DrawString(Convert.ToInt32(m_FValue[5]).ToString(), Common.Txt12FontR,
                m_BValue[2] ? Common.PinkBrush : Common.GreenBrush,
                new RectangleF(m_PDrawPoint[19], m_FSize), Common.DrawFormat);
            e.DrawString(Convert.ToInt32(m_FValue[6]).ToString(), Common.Txt12FontR,
                m_BValue[3] ? Common.PinkBrush : Common.YellowBrush,
                new RectangleF(m_PDrawPoint[20], m_FSize), Common.DrawFormat);
            e.DrawString(Convert.ToInt32(m_FValue[7]).ToString(), Common.Txt12FontR,
                m_BValue[4] ? Common.PinkBrush : Common.MarineBlueBrush,
                new RectangleF(m_PDrawPoint[21], m_FSize), Common.DrawFormat);
            e.DrawString(Convert.ToInt32(m_FValue[8]).ToString(), Common.Txt12FontR,
                m_BValue[5] ? Common.PinkBrush : Common.GreenBrush,
                new RectangleF(m_PDrawPoint[22], m_FSize), Common.DrawFormat);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);

            DrawGrogressBar(e);

            //电压电流
            if (m_BValue[6] || m_BValue[7])
            {
                for (int i = 0; i < 2; i++)
                {
                    e.DrawString(m_Str2[i], Common.Txt12FontR, Common.WhiteBrush,
                        new RectangleF(m_PDrawPoint[23 + i], new SizeF(150, 20)), Common.LeftFormat);
                    e.DrawString(Convert.ToInt32(m_FValue[i + 9]).ToString() + "V", Common.Txt12FontR, Common.WhiteBrush,
                        new RectangleF(m_PDrawPoint[23 + i], new SizeF(150, 20)), Common.RightFormat);
                    e.DrawString(m_Str2[2], Common.Txt12FontR, Common.WhiteBrush,
                        new RectangleF(m_PDrawPoint[25 + i], new SizeF(120, 20)), Common.LeftFormat);
                    e.DrawString(Convert.ToInt32(m_FValue[i + 11]).ToString() + "A", Common.Txt12FontR, Common.WhiteBrush,
                        new RectangleF(m_PDrawPoint[25 + i], new SizeF(120, 20)), Common.RightFormat);
                }
            }
        }

        #endregion#

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str201);
            }
        }

        #region:::::::::::::::所有坐标点的初始化、表盘和进度条的初始化:::::::::::::::#
        /// <summary>
        /// 初始化坐标点
        /// </summary>
        private void InitData()
        {
            Common.DrawFormat.Alignment = (StringAlignment)1;
            Common.DrawFormat.LineAlignment = (StringAlignment)1;

            Common.RightFormat.Alignment = (StringAlignment)2;
            Common.RightFormat.LineAlignment = (StringAlignment)1;

            Common.LeftFormat.Alignment = (StringAlignment)0;
            Common.LeftFormat.LineAlignment = (StringAlignment)1;


            #region ::::::::::::::::::::::::坐标点:::::::::::::::::::::::::::::#
            m_PDrawPoint[0] = new PointF(35f, 235f);

            m_PDrawPoint[1] = new PointF(36f, 485f);
            m_PDrawPoint[2] = new PointF(136f, 485f);
            m_PDrawPoint[3] = new PointF(236f, 485f);
            m_PDrawPoint[4] = new PointF(316f, 485f);
            m_PDrawPoint[5] = new PointF(341f, 485f);
            m_PDrawPoint[6] = new PointF(366f, 485f);
            m_PDrawPoint[7] = new PointF(391f, 485f);
            m_PDrawPoint[8] = new PointF(416f, 485f);
            m_PDrawPoint[9] = new PointF(441f, 485f);

            m_PDrawPoint[10] = new PointF(0, 185f);
            m_PDrawPoint[11] = new PointF(100f, 185f);
            m_PDrawPoint[12] = new PointF(200f, 185f);
            m_PDrawPoint[13] = new PointF(350f, 185f);

            m_PDrawPoint[14] = new PointF(20f, 480f);
            m_PDrawPoint[15] = new PointF(120f, 480f);
            m_PDrawPoint[16] = new PointF(220f, 480f);
            m_PDrawPoint[17] = new PointF(300f, 480f);
            m_PDrawPoint[18] = new PointF(330, 500f);
            m_PDrawPoint[19] = new PointF(350, 480f);
            m_PDrawPoint[20] = new PointF(380, 500f);
            m_PDrawPoint[21] = new PointF(400, 480f);
            m_PDrawPoint[22] = new PointF(430, 500f);

            m_PDrawPoint[23] = new PointF(0, 510f);
            m_PDrawPoint[24] = new PointF(0, 530f);
            m_PDrawPoint[25] = new PointF(180f, 510f);
            m_PDrawPoint[26] = new PointF(180f, 530f);

            #endregion#
            m_GrogressBar[0] = new NeedChangeLength(m_PDrawPoint[1], 24, 2000f, 250f / 40, Common.GreenBrush);
            m_GrogressBar[1] = new NeedChangeLength(m_PDrawPoint[2], 24, 2000f, 250f / 500, Common.GreenBrush);
            m_GrogressBar[2] = new NeedChangeLength(m_PDrawPoint[3], 24, 1000f, 250f / 200, Common.GreenBrush);

            m_GrogressBar[3] = new NeedChangeLength(m_PDrawPoint[4], 24, 1000f, 250f / 200, Common.YellowBrush);
            m_GrogressBar[4] = new NeedChangeLength(m_PDrawPoint[5], 24, 1000f, 250f / 200, Common.MarineBlueBrush);
            m_GrogressBar[5] = new NeedChangeLength(m_PDrawPoint[6], 24, 1000f, 250f / 200, Common.PinkBrush);
            m_GrogressBar[6] = new NeedChangeLength(m_PDrawPoint[7], 24, 1000f, 250f / 200, Common.YellowBrush);
            m_GrogressBar[7] = new NeedChangeLength(m_PDrawPoint[8], 24, 1000f, 250f / 200, Common.MarineBlueBrush);
            m_GrogressBar[8] = new NeedChangeLength(m_PDrawPoint[9], 24, 1000f, 250f / 200, Common.GreenBrush);

        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[120];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[120];

        /// <summary>
        /// 发送的数字量
        /// </summary>
        public bool[] m_SetBValue = new bool[10];

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        public int[] m_SetBValueNumb = new int[10];

        /// <summary>
        /// 接收模拟量
        /// </summary>
        public float[] m_FValue = new float[20];

        /// <summary>
        /// 坐标集
        /// </summary>
        public PointF[] m_PDrawPoint = new PointF[80];

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#


        /// <summary>
        /// 进度条
        /// </summary>
        public NeedChangeLength[] m_GrogressBar = new NeedChangeLength[9];

        public string[] m_Str1 = new string[4] { "原边电压\n(kv)", "原边电流\n(A)", "控制电压\n(V)", "牵引/制动\n(kN)" };
        public string[] m_Str2 = new string[3] { "LG1 电压:", "LG2 电压:", "电流:" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}
