using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BatteryScreen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "蓄电池画面";
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
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str203);
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
        /// 长条框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_BValue[3 + i])
                {
                    e.FillRectangle(Common.GreenBrush, m_Rects[11 + i]);
                }
            }
            e.DrawImage(m_Img[0], m_Rects[0]);
        }

        /// <summary>
        /// 电流电压值
        /// </summary>
        /// <param name="g"></param>
        private void DrawForm(Graphics g)
        {
            for (int i = 0; i < 9; i++)
            {
                g.DrawString(Convert.ToInt32(m_FValue[i]).ToString(), Common.Txt12FontB,
                    Common.BlackBrush, m_Rects[1 + i], Common.RightFormat);
            }

            if (m_BValue[0])
                g.DrawLine(Common.WhitePen2, m_PDrawPoint[0], m_PDrawPoint[1]);
            else
                g.DrawLine(Common.WhitePen2, m_PDrawPoint[0], m_PDrawPoint[2]);

            for (int i = 0; i < 2; i++)
            {
                if (m_BValue[1 + i])
                    g.DrawImage(m_Img[1 + i], m_Rects[10]);
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            DrawRect(g);
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
            m_PDrawPoint[0] = new Point(380, 345);
            m_PDrawPoint[1] = new Point(380, 388);
            m_PDrawPoint[2] = new Point(360, 385);

            m_PDrawPoint[3] = new Point(142, 385);
            m_PDrawPoint[4] = new Point(142, 415);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            #endregion#
            m_Rects = new Rectangle[20];
            m_Rects[0] = new Rectangle(20, 195, 470, 335);

            m_Rects[1] = new Rectangle(70, 215, 50, 25);
            m_Rects[2] = new Rectangle(255, 220, 50, 25);
            m_Rects[3] = new Rectangle(365, 220, 50, 25);
            m_Rects[4] = new Rectangle(70, 450, 50, 25);
            m_Rects[5] = new Rectangle(255, 455, 50, 25);

            m_Rects[6] = new Rectangle(35, 240, 45, 30);
            m_Rects[7] = new Rectangle(255, 243, 50, 30);
            m_Rects[8] = new Rectangle(35, 475, 45, 30);
            m_Rects[9] = new Rectangle(255, 475, 50, 30);

            m_Rects[10] = new Rectangle(370, 195, 51, 24);

            m_Rects[11] = new Rectangle(140, 205, 110, 100);
            m_Rects[12] = new Rectangle(140, 445, 110, 85);
        }

        #endregion#

        #region :::::::::::::::::::::::: value init :::::::::::::::::::::::::::::::#

        #region:::::::::::::::::::::::::::传值部分::::::::::::::::::::::::::::::::::#
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[20];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[20];

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

        /// <summary>
        /// 矩形框
        /// </summary>
        Rectangle[] m_Rects;
        #endregion#
    }
}