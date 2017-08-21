using System.Collections.ObjectModel;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;

namespace Engine.TCMS.HXD3C.Fault
{
    //故障说明
    [GksDataType(DataType.isMMIObjectClass)]
    public class DefaultInstruction : baseClass
    {
        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "故障说明";
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
                ButtomButtonView.Instance.ButtonStr = new ReadOnlyCollection<string>(Common.Str204);
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
            for (int i = 0; i < 14; i++)
            {
                e.DrawRectangle(Common.WhitePen1, 
                    new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + i * 30, 60, 30));
                e.DrawRectangle(Common.WhitePen1, 
                    new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + i * 30, 600, 30));
            }
            e.DrawString("处置", Common.Txt12FontB, Common.WhiteBrush, 
                new Rectangle(m_PDrawPoint[0], new Size(60, 30)), Common.DrawFormat);
            e.DrawString("说明", Common.Txt12FontB, Common.WhiteBrush,
                new Rectangle(m_PDrawPoint[1], new Size(600, 30)), Common.DrawFormat);
            e.DrawString("", Common.Txt12FontB, Common.WhiteBrush, 
                new Rectangle(m_PDrawPoint[1], new Size(600, 30)), Common.DrawFormat);
            for (int i = 0; i < 13; i++)
            {
                e.DrawString((i + 1).ToString(), Common.Txt12FontB, Common.WhiteBrush, 
                    new Rectangle(m_PDrawPoint[0].X, m_PDrawPoint[0].Y + (i + 1) * 30, 60, 30), Common.DrawFormat);
                e.DrawString(m_Str1[i], Common.Txt12FontB, Common.WhiteBrush,
                    new Rectangle(m_PDrawPoint[1].X, m_PDrawPoint[1].Y + (i + 1) * 30, 600, 30), Common.LeftFormat);
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
            m_PDrawPoint[0] = new Point(50, 110);
            m_PDrawPoint[1] = new Point(110, 110);
            m_PDrawPoint[2] = new Point(0, 0);
            m_PDrawPoint[3] = new Point(0, 0);
            m_PDrawPoint[4] = new Point(0, 0);
            m_PDrawPoint[5] = new Point(0, 0);
            m_PDrawPoint[6] = new Point(0, 0);
            m_PDrawPoint[7] = new Point(0, 0);
            m_PDrawPoint[8] = new Point(0, 0);
            m_PDrawPoint[9] = new Point(0, 0);
            m_PDrawPoint[10] = new Point(0, 0);
            m_PDrawPoint[11] = new Point(0, 0);
            m_PDrawPoint[12] = new Point(0, 0);
            m_PDrawPoint[13] = new Point(0, 0);
            m_PDrawPoint[14] = new Point(0, 0);
            m_PDrawPoint[15] = new Point(0, 0);
            m_PDrawPoint[16] = new Point(0, 0);
            m_PDrawPoint[17] = new Point(0, 0);
            m_PDrawPoint[18] = new Point(0, 0);
            m_PDrawPoint[19] = new Point(0, 0);
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


        /// <summary>
        /// 进度条
        /// </summary>
        public NeedChangeLength[] m_GrogressBar = new NeedChangeLength[9];

        public string[] m_Str1 = new string[13] { "按复位开关", "再开动通风机", "停车。如下降温度，再开动。",
            "停车。如下降油压，再开动。", "再开动风扇", "状态复位后，按复位开关。", "复位开关按下后，再投入主断路器。",
            "将司控器手柄置“0”位", "如电压恢复，按复位开关。", "再投入蓄电池开关", "通风机自动开关闭合后，按复位开关。",
            "将对象CI开放", "会段后，请清扫该APU滤网" };

        public SizeF m_FSize = new SizeF(50, 30);
        #endregion#
    }
}
