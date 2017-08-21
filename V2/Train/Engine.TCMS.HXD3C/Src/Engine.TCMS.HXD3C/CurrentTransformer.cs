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
    public class CurrentTransformer : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "主变流器";
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
        /// 长条框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRect(Graphics e)
        {
            for (int i = 0; i < 5; i++)
            {
                e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + i * 30, 120, 30));
                e.DrawString(m_Str1[i], Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + i * 30, 120, 30), Common.LeftFormat);
                for (int j = 0; j < 6; j++)
                {
                    e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[1].X + j * 60, m_PDrawPoint[1].Y + i * 30, 60, 30));
                    e.DrawString((j + 1).ToString() + "位", Common.Txt12FontB, Common.WhiteBrush,
                        new Rectangle(m_PDrawPoint[1].X + j * 60, m_PDrawPoint[1].Y, 60, 30), Common.DrawFormat);
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    e.DrawString(Convert.ToInt32(m_FValue[j + i * 6]).ToString(), Common.Txt12FontB, Common.WhiteBrush,
                        new Rectangle(m_PDrawPoint[1].X + j * 60, m_PDrawPoint[1].Y + 30 + i * 30, 60, 30), Common.RightFormat);
                }
            }
        }

        private void DrawForm(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Str2[i], Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[2].X, m_PDrawPoint[2].Y + i * 30, 120, 30), Common.LeftFormat);
                for (int j = 0; j < 6; j++)
                {
                    if (m_BValue[i * 6 + j])
                    {
                        e.FillRectangle(Common.GreenBrush, new Rectangle(m_PDrawPoint[3 + i].X + 60 * j, m_PDrawPoint[3 + i].Y, 16, 20));
                    }

                    e.DrawLine(Common.WhitePen1, new Point(m_PDrawPoint[3 + i].X + 60 * j, m_PDrawPoint[3 + i].Y),
                        new Point(m_PDrawPoint[3 + i].X + 60 * j, m_PDrawPoint[3 + i].Y + 20));
                    e.DrawLine(Common.WhitePen1, new Point(m_PDrawPoint[3 + i].X + 60 * j + 16, m_PDrawPoint[3 + i].Y),
                        new Point(m_PDrawPoint[3 + i].X + 60 * j + 16, m_PDrawPoint[3 + i].Y + 20));
                    e.DrawLine(Common.WhitePen1, new Point(m_PDrawPoint[3 + i].X - 5 + 60 * j, m_PDrawPoint[3 + i].Y + 10),
                        new Point(m_PDrawPoint[3 + i].X + 60 * j, m_PDrawPoint[3 + i].Y + 10));
                    e.DrawLine(Common.WhitePen1, new Point(m_PDrawPoint[3 + i].X + 16 + 60 * j, m_PDrawPoint[3 + i].Y + 10),
                        new Point(m_PDrawPoint[3 + i].X + 16 + 5 + 60 * j, m_PDrawPoint[3 + i].Y + 10));

                }
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);
            DrawForm(e);
            DrawForm(e);
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
            m_PDrawPoint[0] = new Point(0, 200);
            m_PDrawPoint[1] = new Point(120, 200);
            m_PDrawPoint[2] = new Point(0, 380);

            m_PDrawPoint[3] = new Point(142, 385);
            m_PDrawPoint[4] = new Point(142, 415);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            #endregion#

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
        /// 进度条
        /// </summary>
        public NeedChangeLength[] m_GrogressBar = new NeedChangeLength[9];

        public string[] m_Str1 = new string[5] { "", "中间电压(V)", "电机电流(A)", "电机频率(HZ)", "牵引/制动(kN)" };
        public string[] m_Str2 = new string[2] { "工作接触器", "充电接触器" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}