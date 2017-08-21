using System.Collections.Generic;
using System.Drawing;
using Engine.TCMS.HXD3C.Utils;
using MMI.Facility.Interface;

namespace Engine.TCMS.HXD3C.Fault
{
    public partial class DefaultState : baseClass
    {
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
            #region::::::::::::::::::: 当前故障 :::::::::::::::::::::::::#
            m_PDrawPoint[0] = new Point(500, 170);

            m_PDrawPoint[1] = new Point(500, 205);

            #endregion#

            #region::::::::::::::::::: 故障履历 :::::::::::::::::::::::::#
            m_PDrawPoint[2] = new Point(0, 130);
            m_PDrawPoint[3] = new Point(40, 130);
            m_PDrawPoint[4] = new Point(240, 130);
            m_PDrawPoint[5] = new Point(385, 130);
            m_PDrawPoint[6] = new Point(505, 130);
            m_PDrawPoint[7] = new Point(755, 130);

            m_PDrawPoint[8] = new Point(0, 130);
            m_PDrawPoint[9] = new Point(0, 130);
            m_PDrawPoint[10] = new Point(0, 130);

            m_PDrawPoint[11] = new Point(0, 0);
            m_PDrawPoint[12] = new Point(0, 0);

            m_PDrawPoint[13] = new Point(0, 0);
            m_PDrawPoint[14] = new Point(0, 0);

            //上一条外框
            m_PDrawPoint[40] = new Point(520, 502);
            m_PDrawPoint[41] = new Point(480, 517);
            m_PDrawPoint[42] = new Point(480, 547);
            m_PDrawPoint[43] = new Point(560, 547);
            m_PDrawPoint[44] = new Point(560, 517);
            //上一条内框
            m_PDrawPoint[45] = new Point(520, 507);
            m_PDrawPoint[46] = new Point(485, 522);
            m_PDrawPoint[47] = new Point(485, 542);
            m_PDrawPoint[48] = new Point(555, 542);
            m_PDrawPoint[49] = new Point(555, 522);
            //下一条外框
            m_PDrawPoint[50] = new Point(560, 502);
            m_PDrawPoint[51] = new Point(560, 532);
            m_PDrawPoint[52] = new Point(600, 547);
            m_PDrawPoint[53] = new Point(640, 532);
            m_PDrawPoint[54] = new Point(640, 502);
            //下一条内框
            m_PDrawPoint[55] = new Point(565, 507);
            m_PDrawPoint[56] = new Point(565, 527);
            m_PDrawPoint[57] = new Point(600, 542);
            m_PDrawPoint[58] = new Point(635, 527);
            m_PDrawPoint[59] = new Point(635, 507);
            //上一页外框
            m_PDrawPoint[60] = new Point(680, 502);
            m_PDrawPoint[61] = new Point(640, 517);
            m_PDrawPoint[62] = new Point(640, 547);
            m_PDrawPoint[63] = new Point(720, 547);
            m_PDrawPoint[64] = new Point(720, 517);
            //上一页内框
            m_PDrawPoint[65] = new Point(680, 507);
            m_PDrawPoint[66] = new Point(645, 522);
            m_PDrawPoint[67] = new Point(645, 542);
            m_PDrawPoint[68] = new Point(715, 542);
            m_PDrawPoint[69] = new Point(715, 522);
            //下一页外框
            m_PDrawPoint[70] = new Point(720, 502);
            m_PDrawPoint[71] = new Point(720, 532);
            m_PDrawPoint[72] = new Point(760, 547);
            m_PDrawPoint[73] = new Point(800, 532);
            m_PDrawPoint[74] = new Point(800, 502);
            //下一页内框
            m_PDrawPoint[75] = new Point(725, 507);
            m_PDrawPoint[76] = new Point(725, 527);
            m_PDrawPoint[77] = new Point(760, 542);
            m_PDrawPoint[78] = new Point(795, 527);
            m_PDrawPoint[79] = new Point(795, 507);
            #endregion#

            #region ::::::::::::::::::: buttonRect ::::::::::::::::::::::::::
            //上一条
            m_Points1 = new Point[5] { m_PDrawPoint[40], m_PDrawPoint[41], m_PDrawPoint[42], m_PDrawPoint[43], m_PDrawPoint[44] };
            m_Points11 = new Point[5] { m_PDrawPoint[45], m_PDrawPoint[46], m_PDrawPoint[47], m_PDrawPoint[48], m_PDrawPoint[49] };
            //下一条
            m_Points2 = new Point[5] { m_PDrawPoint[50], m_PDrawPoint[51], m_PDrawPoint[52], m_PDrawPoint[53], m_PDrawPoint[54] };
            m_Points21 = new Point[5] { m_PDrawPoint[55], m_PDrawPoint[56], m_PDrawPoint[57], m_PDrawPoint[58], m_PDrawPoint[59] };
            //上一页
            m_Points3 = new Point[5] { m_PDrawPoint[60], m_PDrawPoint[61], m_PDrawPoint[62], m_PDrawPoint[63], m_PDrawPoint[64] };
            m_Points31 = new Point[5] { m_PDrawPoint[65], m_PDrawPoint[66], m_PDrawPoint[67], m_PDrawPoint[68], m_PDrawPoint[69] };
            //下一页
            m_Points4 = new Point[5] { m_PDrawPoint[70], m_PDrawPoint[71], m_PDrawPoint[72], m_PDrawPoint[73], m_PDrawPoint[74] };
            m_Points41 = new Point[5] { m_PDrawPoint[75], m_PDrawPoint[76], m_PDrawPoint[77], m_PDrawPoint[78], m_PDrawPoint[79] };
            #endregion

            m_Rects = new Rectangle[20];
            //说明处置
            m_Rects[0] = new Rectangle(0, 502, 120, 40);
            m_Rects[1] = new Rectangle(5, 507, 110, 30);
            //按发生顺序
            m_Rects[2] = new Rectangle(240, 502, 120, 45);
            m_Rects[3] = new Rectangle(245, 507, 110, 35);
            //按故障等级
            m_Rects[4] = new Rectangle(360, 502, 120, 45);
            m_Rects[5] = new Rectangle(365, 507, 110, 35);
            //上一条
            m_Rects[6] = new Rectangle(480, 517, 80, 30);
            //下一条
            m_Rects[7] = new Rectangle(560, 502, 80, 45);
            //上一页
            m_Rects[8] = new Rectangle(640, 517, 80, 30);
            //下一页
            m_Rects[9] = new Rectangle(720, 502, 80, 45);
            #endregion#

            m_Rect = new List<Region>();
            m_Rect.Add(new Region(m_Rects[0]));
            m_Rect.Add(new Region(m_Rects[6]));
            m_Rect.Add(new Region(m_Rects[7]));
            m_Rect.Add(new Region(m_Rects[8]));
            m_Rect.Add(new Region(m_Rects[9]));
            m_Rect.Add(new Region(m_Rects[2]));
            m_Rect.Add(new Region(m_Rects[4]));

            #region

            m_ButtonStr = new string[7] { "说明处置", "上一条", "下一条", "上一页", "下一页", "按发生顺序", "按等级顺序" };
            #endregion

            m_ButtonIsDown = new bool[7];

            //
            m_FaultButtons = new HXD3Button[7];
            m_FaultButtons[0] = new HXD3Button(m_Rects[0], m_Rects[1]);

            m_FaultButtons[1] = new HXD3Button(m_Points1, m_Points11);
            m_FaultButtons[2] = new HXD3Button(m_Points2, m_Points21);
            m_FaultButtons[3] = new HXD3Button(m_Points3, m_Points31);
            m_FaultButtons[4] = new HXD3Button(m_Points4, m_Points41);

            m_FaultButtons[5] = new HXD3Button(m_Rects[2], m_Rects[3]);
            m_FaultButtons[6] = new HXD3Button(m_Rects[4], m_Rects[5]);
        }

        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        public bool[] m_BValue = new bool[500];

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        public bool[] m_OldbValue = new bool[500];

        /// <summary>
        /// 故障在内存的位置
        /// </summary>
        public int[] m_MemoryIndex = new int[500];

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

        /// <summary>
        /// 故障键值列表
        /// </summary>
        public static SortedList<int, string[]> m_FaultList = new SortedList<int, string[]>();

        /// <summary>
        /// 故障开始时间
        /// </summary>
        public string m_StartTime;

        /// <summary>
        /// 故障结束时间
        /// </summary>
        public string m_OverTime;

        /// <summary>
        /// 故障列表(未结束)
        /// </summary>
        private readonly List<ItemInfo> m_TheNoOverDefault = new List<ItemInfo>();

        /// <summary>
        /// 故障列表(所有)
        /// </summary>
        private readonly List<ItemInfo> m_TheAllDefault = new List<ItemInfo>();

        /// <summary>
        /// 1级故障
        /// </summary>
        public List<ItemInfo> m_TheDefaultLevel1 = new List<ItemInfo>();

        /// <summary>
        /// 2级故障
        /// </summary>
        public List<ItemInfo> m_TheDefaultLevel2 = new List<ItemInfo>();

        /// <summary>
        /// 3级故障
        /// </summary>
        public List<ItemInfo> m_TheDefaultLevel3 = new List<ItemInfo>();

        /// <summary>
        /// 排完序的故障
        /// </summary>
        public List<ItemInfo> m_DefaultSort = new List<ItemInfo>();

        /// <summary>
        /// 故障实例
        /// </summary>
        public ItemInfo m_DefaultTmp;

        /// <summary>
        /// 是否有故障
        /// </summary>
        public static bool m_IsDefaultOccur;

        /// <summary>
        /// 当前视图
        /// </summary>
        public string m_CurrentView = "";

        /// <summary>
        /// 故障履历框架的宽
        /// </summary>
        private readonly int[] m_RectWrith = new int[6] { 40, 200, 145, 120, 250, 45 };

        /// <summary>
        /// 故障履历框架标题
        /// </summary>
        private readonly string[] m_RectStr = new string[6] { "NO.", "名称/内容", "发生时间", "恢复时间", "故障时机车状态", "处置" };

        /// <summary>
        /// 故障履历页码
        /// </summary>
        public int m_Page = 0;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int m_CurrentPage = 0;

        /// <summary>
        /// 当前页行数
        /// </summary>
        public int m_Row;

        /// <summary>
        /// 当前行
        /// </summary>
        public int m_CurrentRow;

        /// <summary>
        /// 按键是否显示
        /// </summary>
        public bool[] m_ButtonIsShow = new bool[5] { true, false, false, false, false };

        /// <summary>
        /// 按键上的字符
        /// </summary>
        public string[] m_ButtonStr;

        /// <summary>
        /// 键是否按下
        /// </summary>
        public bool[] m_ButtonIsDown;

        /// <summary>
        /// 下排按键尺寸
        /// </summary>
        private Size m_RectSize1 = new Size(115, 45);

        /// <summary>
        /// 按键列表
        /// </summary>
        public List<Region> m_Rect;

        /// <summary>
        /// 按钮框
        /// </summary>
        public Rectangle[] m_Rects;

        /// <summary>
        /// 按键
        /// </summary>
        public HXD3Button[] m_FaultButtons;

        /// <summary>
        /// 故障图标
        /// </summary>
        public Image m_DefImg;

        /// <summary>
        /// 是否需要排序
        /// </summary>
        public bool m_NeedSort;

        public Point[] m_Points1;
        public Point[] m_Points2;
        public Point[] m_Points3;
        public Point[] m_Points4;

        public Point[] m_Points11;
        public Point[] m_Points21;
        public Point[] m_Points31;
        public Point[] m_Points41;
    }
}
