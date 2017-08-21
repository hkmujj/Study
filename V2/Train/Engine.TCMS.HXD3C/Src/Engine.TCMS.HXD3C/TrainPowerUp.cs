using System;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class TrainPowerUp : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "列车供电画面";
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
        /// 框架线条
        /// 文字
        /// </summary>
        /// <param name="e"></param>
        private void DrawFrame(Graphics e)
        {
            //供电钥匙
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Str1[i], Common.Txt12FontB, Common.WhiteBrush, m_Rects[i]);
                if (m_BValue[i])
                    e.FillRectangle(Common.GreenBrush, m_Rects[2 + i]);
                e.DrawString(m_Str1[i + 2], Common.Txt10FontB, Common.WhiteBrush, m_Rects[2 + i], Common.DrawFormat);
                e.DrawRectangle(Common.WhitePen1, m_Rects[2 + i]);
            }
            //左侧标题
            for (int i = 0; i < 5; i++)
            {
                e.DrawString(m_Str2[i], Common.Txt13FontB, Common.WhiteBrush, m_Rects[i + 5], Common.LeftFormat);
            }
            //0 1280
            e.DrawString("0", Common.Txt13FontR, Common.WhiteBrush, m_Rects[4], Common.LeftFormat);
            e.DrawString("1280", Common.Txt13FontR, Common.WhiteBrush, m_Rects[4], Common.RightFormat);
            //0 100000000
            e.DrawString("0", Common.Txt13FontR, Common.WhiteBrush, m_Rects[10], Common.LeftFormat);
            e.DrawString("100000000", Common.Txt13FontR, Common.WhiteBrush, m_Rects[10], Common.RightFormat);
            //1 2
            for (int i = 0; i < 5; i++)
            {
                e.DrawString("1", Common.Txt13FontR, Common.WhiteBrush, m_Rects[11 + i * 2], Common.DrawFormat);
                e.DrawString("2", Common.Txt13FontR, Common.WhiteBrush, m_Rects[12 + i * 2], Common.DrawFormat);
            }
            //
            e.DrawLine(Common.WhitePen1, m_PDrawPoint[0], m_PDrawPoint[1]);
            e.DrawLine(Common.WhitePen1, m_PDrawPoint[2], m_PDrawPoint[3]);
            for (int i = 0; i < 15; i++)
            {
                e.DrawLine(Common.WhitePen1, m_PDrawPoint[4 + i], m_PDrawPoint[19 + i]);
            }

        }

        /// <summary>
        /// 画值
        /// </summary>
        /// <param name="e"></param>
        private void DrawRecvValue(Graphics e)
        {
            for (int i = 0; i < 8; i++)
            {
                m_GrogressBar[i].DrawRectangle(e, ref m_FValue[i], 2);
            }

            e.FillRectangle(Common.BlueBrush, 
                new RectangleF(m_PDrawPoint[16].X, m_PDrawPoint[16].Y + 20, Title.m_DicNumb1 / 100000000 * 220, 20));

            e.FillRectangle(Common.BlueBrush,
                new RectangleF(m_PDrawPoint[17].X, m_PDrawPoint[17].Y + 20, Title.m_DicNumb2 / 100000000 * 220, 20));

            for (int i = 0; i < 8; i++)
            {
                e.DrawString(Convert.ToInt32(m_FValue[i]).ToString() + m_Str3[i], Common.Txt12FontB,
                    Common.WhiteBrush, m_Rects[21 + i], Common.DrawFormat);
            }

            e.DrawString(Convert.ToInt32(Title.m_DicNumb1).ToString() + m_Str3[8], Common.Txt12FontB,
                Common.WhiteBrush, m_Rects[29], Common.DrawFormat);

            e.DrawString(Convert.ToInt32(Title.m_DicNumb2).ToString() + m_Str3[9], Common.Txt12FontB,
                Common.WhiteBrush, m_Rects[30], Common.DrawFormat);
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRecvValue(e);

            DrawFrame(e);
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
            m_PDrawPoint[0] = new Point(150, 263);
            m_PDrawPoint[1] = new Point(150, 445);

            m_PDrawPoint[2] = new Point(150, 463);
            m_PDrawPoint[3] = new Point(150, 506);

            for (int i = 0; i < 3; i++)
            {
                m_PDrawPoint[4 + i] = new Point(150, 265 + i * 20);
                m_PDrawPoint[7 + i] = new Point(150, 310 + i * 20);
                m_PDrawPoint[10 + i] = new Point(150, 355 + i * 20);
                m_PDrawPoint[13 + i] = new Point(150, 400 + i * 20);
                m_PDrawPoint[16 + i] = new Point(150, 465 + i * 20);

                m_PDrawPoint[19 + i] = new Point(370, 265 + i * 20);
                m_PDrawPoint[22 + i] = new Point(370, 310 + i * 20);
                m_PDrawPoint[25 + i] = new Point(370, 355 + i * 20);
                m_PDrawPoint[28 + i] = new Point(370, 400 + i * 20);
                m_PDrawPoint[31 + i] = new Point(370, 465 + i * 20);
            }

            #endregion#

            for (int i = 0; i < 2; i++)
            {
                m_Rects[i] = new Rectangle(0, 195 + i * 25, 100, 25);
                m_Rects[2 + i] = new Rectangle(95, 190 + i * 25, 65, 25);
            }
            m_Rects[4] = new Rectangle(140, 240, 260, 25);
            for (int i = 0; i < 4; i++)
            {
                m_Rects[5 + i] = new Rectangle(0, 265 + 45 * i, 130, 40);
            }
            m_Rects[9] = new Rectangle(0, 470, 120, 40);
            m_Rects[10] = new Rectangle(140, 445, 280, 25);
            for (int i = 0; i < 2; i++)
            {
                m_Rects[11 + i] = new Rectangle(130, 265 + i * 20, 20, 20);
                m_Rects[13 + i] = new Rectangle(130, 310 + i * 20, 20, 20);
                m_Rects[15 + i] = new Rectangle(130, 355 + i * 20, 20, 20);
                m_Rects[17 + i] = new Rectangle(130, 400 + i * 20, 20, 20);
                m_Rects[19 + i] = new Rectangle(130, 470 + i * 20, 20, 20);

                m_Rects[21 + i] = new Rectangle(370, 265 + i * 20, 90, 20);
                m_Rects[23 + i] = new Rectangle(370, 310 + i * 20, 90, 20);
                m_Rects[25 + i] = new Rectangle(370, 355 + i * 20, 90, 20);
                m_Rects[27 + i] = new Rectangle(370, 400 + i * 20, 90, 20);
                m_Rects[29 + i] = new Rectangle(370, 470 + i * 20, 110, 20);
            }
            


            m_GrogressBar[0] = new NeedChangeLength(m_PDrawPoint[4], 20, 20000f, 220f / 1280, Common.BlueBrush);
            m_GrogressBar[1] = new NeedChangeLength(m_PDrawPoint[5], 20, 20000f, 220f / 1280, Common.BlueBrush);

            m_GrogressBar[2] = new NeedChangeLength(m_PDrawPoint[7], 20, 20000f, 220f / 1280, Common.BlueBrush);
            m_GrogressBar[3] = new NeedChangeLength(m_PDrawPoint[8], 20, 20000f, 220f / 1280, Common.BlueBrush);

            m_GrogressBar[4] = new NeedChangeLength(m_PDrawPoint[10], 20, 20000f, 220f / 1280, Common.BlueBrush);
            m_GrogressBar[5] = new NeedChangeLength(m_PDrawPoint[11], 20, 20000f, 220f / 1280, Common.BlueBrush);

            m_GrogressBar[6] = new NeedChangeLength(m_PDrawPoint[13], 20, 20000f, 220f / 1280, Common.BlueBrush);
            m_GrogressBar[7] = new NeedChangeLength(m_PDrawPoint[14], 20, 20000f, 220f / 1280, Common.BlueBrush);

            m_GrogressBar[8] = new NeedChangeLength(m_PDrawPoint[16], 20, 20000f, 220f / 100000000, Common.BlueBrush);
            m_GrogressBar[9] = new NeedChangeLength(m_PDrawPoint[17], 20, 20000f, 220f / 100000000, Common.BlueBrush);

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
        public Point[] m_PDrawPoint = new Point[80];

        /// <summary>
        /// 图片集
        /// </summary>
        public static Image[] m_Img = new Image[30];
        #endregion#

        public Rectangle[] m_Rects = new Rectangle[50];

        /// <summary>
        /// 进度条
        /// </summary>
        public NeedChangeLength[] m_GrogressBar = new NeedChangeLength[10];

        public string[] m_Str1 = new string[4] { "供电钥匙1", "供电钥匙2", "SA105", "SA106" };
        public string[] m_Str2 = new string[5] { "供电电压", "供电电流", "交流输出电压", "交流输出电流", "供电柜已使用\n电度量" };
        public string[] m_Str3 = new string[10] { "V", "V", "A", "A", "V", "V", "A", "A", "kWh", "kWh" };
        #endregion#
    }
}