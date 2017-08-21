using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C
{
    [GksDataType(DataType.isMMIObjectClass)]
    class AirBrakeScreen : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "空制状态画面";
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
            for (int i = 0; i < 9; i++)
            {
                e.DrawString(m_Str[i], Common.Txt14FontB, Common.WhiteBrush, m_Rects[i], Common.DrawFormat);
                if (m_BValue[i * 2])
                {
                    e.FillRectangle(Common.GreenBrush, m_Rects[10 + i]);
                    e.DrawString(m_StrRect1[i], Common.Txt14FontB, Common.BlackBrush, m_Rects[10 + i], Common.DrawFormat);
                }
                else if (m_BValue[1 + i * 2])
                {
                    e.FillRectangle(Common.RedBrush, m_Rects[10 + i]);
                    e.DrawString(m_StrRect2[i], Common.Txt14FontB, Common.WhiteBrush, m_Rects[10 + i], Common.DrawFormat);
                }
                else
                    e.DrawString(m_StrRect3[i], Common.Txt14FontB, Common.WhiteBrush, m_Rects[10 + i], Common.DrawFormat);
                e.DrawRectangle(Common.WhitePen1, m_Rects[10 + i]);
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="e"></param>
        private void DrawOn(Graphics e)
        {
            DrawRect(e);
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
            m_PDrawPoint[0] = new Point(20, 230);
            m_PDrawPoint[1] = new Point(150, 230);

            m_PDrawPoint[2] = new Point(20, 310);
            m_PDrawPoint[3] = new Point(150, 310);

            m_PDrawPoint[4] = new Point(20, 420);
            m_PDrawPoint[5] = new Point(150, 420);

            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            #endregion#
            m_Rects = new Rectangle[30];
            m_Rects[0] = new Rectangle(0, 230, 100, 40);
            m_Rects[1] = new Rectangle(140, 270, 100, 30);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects[2 + j * 2 + i] = new Rectangle(0 + i * 260, 300 + j * 45, 100, 40);
                    m_Rects[12 + j * 2 + i] = new Rectangle(140 + i * 240, 300 + j * 45, 100, 40);
                }
                for (int k = 0; k < 2; k++)
                {
                    m_Rects[6 + k * 2 + i] = new Rectangle(0 + i * 260, 405 + k * 45, 100, 40);
                    m_Rects[16 + k * 2 + i] = new Rectangle(140 + i * 240, 405 + k * 45, 100, 40);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                m_Rects[10 + i] = new Rectangle(140 + i * 105, 230, 100, 40);
            }

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
        private readonly string[] m_Str = new string[9] { "压缩器", "栓", "停车闸", "受电弓", "撒沙", "辅助风缸", "转向架Ⅰ", "转向架Ⅱ", "紧急制动" };
        private readonly string[] m_StrRect1 = new string[9] { "CMP1", "CMP2", "正常", "正常", "正常", "正常", "正常", "正常", "正常" };
        private readonly string[] m_StrRect2 = new string[9] { "CMP1", "CMP2", "异常", "异常", "异常", "异常", "异常", "异常", "异常" };
        private readonly string[] m_StrRect3 = new string[9] { "CMP1", "CMP2", "", "", "", "", "", "", "" };
        #endregion#
    }
}