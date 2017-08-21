using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class ZhouWen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "轴温状态画面";
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
            for (int i = 0; i < 2; i++)
            {
                if (m_BValue[i * 2])
                {
                    e.FillRectangle(Common.GreenBrush, new Rectangle(m_PDrawPoint[1 + i * 4], new Size(70, 25)));
                    e.DrawString("正常", Common.Txt12FontB, Common.WhiteBrush,
                        new Rectangle(m_PDrawPoint[1 + i * 4], new Size(70, 30)), Common.DrawFormat);
                }
                if (m_BValue[i * 2 + 1])
                {
                    e.FillRectangle(Common.RedBrush, new Rectangle(m_PDrawPoint[1 + i * 4], new Size(70, 25)));
                    e.DrawString("异常", Common.Txt12FontB, Common.WhiteBrush,
                        new Rectangle(m_PDrawPoint[1 + i * 4], new Size(70, 30)), Common.DrawFormat);
                }
            }
            //不知道轴温有多少种状态
            for (int i = 0; i < 2; i++)
            {
                e.DrawRectangle(Common.WhitePen1, new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + i * 40, 70, 25));
            }
        }

        private void DrawForm(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawString((i + 1).ToString() + "架轴温", Common.Txt12FontB, Common.WhiteBrush,
                    new Point(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + i * 40), Common.DrawFormat);
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
            m_PDrawPoint[0] = new Point(80, 230);
            m_PDrawPoint[1] = new Point(130, 215);
            m_PDrawPoint[2] = new Point(0, 380);

            m_PDrawPoint[3] = new Point(142, 385);
            m_PDrawPoint[4] = new Point(142, 415);
            m_PDrawPoint[5] = new Point(130, 255);
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
        public bool[] m_BValue = new bool[10];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[10];

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

        #endregion#
    }
}