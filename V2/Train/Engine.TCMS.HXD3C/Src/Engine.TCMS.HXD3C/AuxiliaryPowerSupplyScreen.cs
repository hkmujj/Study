using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{

    /// <summary>
    /// 辅助电源画面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AuxiliaryPowerSupplyScreen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "辅助电源画面";
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

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str202);
            }
        }
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
        /// 车
        /// </summary>
        /// <param name="g"></param>
        private void DrawCar(Graphics g)
        {
            //车
            g.DrawLines(Common.WhitePen1, new Point[5] { m_PDrawPoint[0], m_PDrawPoint[1], m_PDrawPoint[3], m_PDrawPoint[2], m_PDrawPoint[0] });
            g.DrawLine(Common.WhitePen1, m_PDrawPoint[4], m_PDrawPoint[5]);
            //车轮
            g.FillEllipse(m_BValue[14] ? Common.PinkBrush : Common.YellowBrush, m_Rects[0]);
            g.FillEllipse(m_BValue[15] ? Common.PinkBrush : Common.MarineBlueBrush, m_Rects[1]);
            g.FillEllipse(m_BValue[16] ? Common.PinkBrush : Common.GreenBrush, m_Rects[2]);
            g.FillEllipse(m_BValue[17] ? Common.PinkBrush : Common.YellowBrush, m_Rects[3]);
            g.FillEllipse(m_BValue[18] ? Common.PinkBrush : Common.MarineBlueBrush, m_Rects[4]);
            g.FillEllipse(m_BValue[19] ? Common.PinkBrush : Common.GreenBrush, m_Rects[5]);
            for (int i = 0; i < 6; i++)
            {
                g.DrawEllipse(Common.WhitePen1, m_Rects[i]);
            }
            //
            for (int i = 0; i < 2; i++)
            {
                //1端2端
                g.DrawString(m_Str[i], Common.Txt12FontB, Common.WhiteBrush, m_PDrawPoint[i + 6], Common.DrawFormat);
                if (m_BValue[2 + 2 * i])
                    g.DrawString(m_StrRect2[i], Common.Txt14FontR, Common.WhiteBrush, m_Rects[20 + i], Common.DrawFormat);
                else
                    g.DrawString("-", Common.Txt14FontR, Common.WhiteBrush, m_Rects[20 + i], Common.DrawFormat);
                if (m_BValue[i])
                    g.DrawImage(m_Img[0], m_Rects[22 + i]);
            }

        }

        /// <summary>
        /// APU
        /// </summary>
        /// <param name="g"></param>
        private void DrawApu(Graphics g)
        {
            //APU1 APU2
            for (int i = 0; i < 2; i++)
            {
                if (m_BValue[6 + i * 2])
                {
                    g.FillEllipse(Common.GreenBrush, m_Rects[10 + i]);
                    g.DrawString(m_StrRect1[i], Common.Txt16FontB, Common.BlackBrush, m_Rects[10 + i], Common.DrawFormat);
                }
                else if (m_BValue[7 + i * 2])
                {
                    g.FillEllipse(Common.RedBrush, m_Rects[10 + i]);
                    g.DrawString(m_StrRect1[i], Common.Txt16FontB, Common.WhiteBrush, m_Rects[10 + i], Common.DrawFormat);
                }
                else
                    g.DrawString(m_StrRect1[i], Common.Txt16FontB, Common.WhiteBrush, m_Rects[10 + i], Common.DrawFormat);
                g.DrawEllipse(Common.WhitePen1, m_Rects[10 + i]);

                if (m_BValue[20 + i])
                {
                    g.FillRectangle(Common.GreenBrush, m_Rects[24 + i]);
                }

                if (m_BValue[22 + i])
                {
                    g.DrawImage(m_Img[1 + i], m_Rects[26 + i]);
                }
            }
            if (BoolList[UIObj.InBoolList[24]])
            {
                g.FillRectangle(Common.GreenBrush, m_Rects[28]);
            }
           
            //12条线段组成的电路图
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(Common.WhitePen1, m_PDrawPoint[9 + i * 2], m_PDrawPoint[10 + i * 2]);
            }
        }

        private void DrawForm(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(m_Str[i + 2], Common.Txt12FontB, Common.WhiteBrush,
                    m_Rects[6 + i], Common.LeftFormat);

                //辅助变流器输出电压+电流
                g.DrawString(Convert.ToInt32(m_FValue[i]).ToString(), Common.Txt14FontB,
                    Common.WhiteBrush, m_Rects[14 + i], Common.RightFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                //辅助变流器工作状态
                if (m_BValue[10 + i * 2])
                {
                    g.FillRectangle(Common.GreenBrush, m_Rects[12 + i]);
                    g.DrawString("ON", Common.Txt14FontB, Common.BlackBrush, m_Rects[12 + i], Common.DrawFormat);
                }
                else if (m_BValue[11 + i * 2])
                {
                    g.FillRectangle(Common.RedBrush, m_Rects[12 + i]);
                    g.DrawString("OFF", Common.Txt14FontB, Common.WhiteBrush, m_Rects[12 + i], Common.DrawFormat);
                }
                g.DrawRectangle(Common.WhitePen1, m_Rects[12 + i]);

                //辅助变流器输出频率
                g.DrawString(m_FValue[4 + i].ToString("0.0"), Common.Txt14FontB,
                    Common.WhiteBrush, m_Rects[18 + i], Common.RightFormat);
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            DrawCar(g);
            DrawApu(g);
            DrawForm(g);
        }

        #endregion#

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
            m_PDrawPoint[0] = new Point(230, 200);
            m_PDrawPoint[1] = new Point(400, 200);
            m_PDrawPoint[2] = new Point(210, 250);
            m_PDrawPoint[3] = new Point(420, 250);

            m_PDrawPoint[4] = new Point(315, 200);
            m_PDrawPoint[5] = new Point(315, 250);

            m_PDrawPoint[6] = new Point(200, 200);
            m_PDrawPoint[7] = new Point(430, 200);

            m_PDrawPoint[8] = new Point(20, 420);


            m_PDrawPoint[9] = new Point(260, 345);
            m_PDrawPoint[10] = new Point(260, 320);

            m_PDrawPoint[11] = new Point(250, 320);
            m_PDrawPoint[12] = new Point(270, 320);

            m_PDrawPoint[13] = new Point(250, 300);
            m_PDrawPoint[14] = new Point(270, 300);

            m_PDrawPoint[15] = new Point(260, 300);
            m_PDrawPoint[16] = new Point(260, 280);

            m_PDrawPoint[17] = new Point(260, 280);
            m_PDrawPoint[18] = new Point(335, 280);

            m_PDrawPoint[19] = new Point(335, 270);
            m_PDrawPoint[20] = new Point(335, 290);

            m_PDrawPoint[21] = new Point(350, 270);
            m_PDrawPoint[22] = new Point(350, 290);

            m_PDrawPoint[23] = new Point(350, 280);
            m_PDrawPoint[24] = new Point(425, 280);

            m_PDrawPoint[25] = new Point(425, 300);
            m_PDrawPoint[26] = new Point(425, 280);

            m_PDrawPoint[27] = new Point(415, 300);
            m_PDrawPoint[28] = new Point(435, 300);

            m_PDrawPoint[29] = new Point(415, 320);
            m_PDrawPoint[30] = new Point(435, 320);

            m_PDrawPoint[31] = new Point(425, 320);
            m_PDrawPoint[32] = new Point(425, 345);
            #endregion#
            m_Rects = new Rectangle[30];
            for (int i = 0; i < 3; i++)
            {
                m_Rects[i] = new Rectangle(210 + 16 * i, 250, 15, 15);
                m_Rects[3 + i] = new Rectangle(370 + 16 * i, 250, 15, 15);
            }
            for (int i = 0; i < 4; i++)
            {
                m_Rects[6 + i] = new Rectangle(20, 420 + i * 30, 200, 25);
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects[10 + i] = new Rectangle(225 + i * 165, 345, 70, 70);
                m_Rects[12 + i] = new Rectangle(225 + i * 165, 420, 70, 25);
                m_Rects[14 + i] = new Rectangle(225 + i * 165, 450, 70, 25);
                m_Rects[16 + i] = new Rectangle(225 + i * 165, 480, 70, 25);
                m_Rects[18 + i] = new Rectangle(225 + i * 165, 510, 70, 25);
                m_Rects[20 + i] = new Rectangle(230 + i * 140, 180, 30, 20);
                m_Rects[22 + i] = new Rectangle(230 + i * 140, 200, 30, 50);
            }
            m_Rects[24] = new Rectangle(252, 300, 17, 20);
            m_Rects[25] = new Rectangle(417, 300, 17, 20);

            m_Rects[26] = new Rectangle(150, 210, 50, 30);
            m_Rects[27] = new Rectangle(430, 210, 50, 30);
            m_Rects[28] = new Rectangle(336, 270, 15, 20);
        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[30];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[30];

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
        public float[] m_FValue = new float[30];

        /// <summary>
        /// 坐标集
        /// </summary>
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#

        public Rectangle[] m_Rects;
        public string[] m_Str = new string[6] { "Ⅰ端", "Ⅱ端", "辅助变流器工作状态", "辅助变流器输出电压(V)", "辅助变流器输出电流(A)", "辅助变流器输出频率(Hz)" };
        public string[] m_StrRect1 = new string[2] { "APU1", "APU2" };
        public string[] m_StrRect2 = new string[2] { "<", ">" };
        #endregion#
    }
}
