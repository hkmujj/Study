using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace LightRail.ATC.Casco.MMI_Main
{
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class MMI_Main : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "MMI屏主界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            LoadStationFile();

            SetWhenDebug();

            return true;
        }

        private void SetWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, 1, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 100, 1, 0);
            append_postCmd(CmdType.SetInBoolValue, 101, 1, 0);
        }

        void LoadStationFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\车站信息.txt");
            var allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var slip in allLine.Select(line => line.Split(new[] { '\t', ' ' })).Where(slip => slip.Length == 2))
            {
                StationList.Add(int.Parse(slip[0]), slip[1]);
            }
        }

        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        /// <summary>
        /// 初始化坐标、数组、热区
        /// </summary>
        private void InitData()
        {
            drawFormat.LineAlignment = (StringAlignment)1;
            drawFormat.Alignment = (StringAlignment)1;

            RightFormat.LineAlignment = (StringAlignment)1;
            RightFormat.Alignment = (StringAlignment)2;

            LeftFormat.LineAlignment = (StringAlignment)1;
            LeftFormat.Alignment = 0;
            const int offset = 5;
            bValue = new bool[100];
            oldbValue = new bool[100];

            fValue = new float[20];
            oldfValue = new float[20];
            setFValue = new float[5];
            setFValueNumb = new int[5];
            setBValueNumb = new int[UIObj.OutBoolList.Count];
            pDrawPoint = new Point[20];

            rects = new Rectangle[110];
            //实例化Image 数组
            Img = new Image[40];

            buttonIsDown = new bool[35];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i]));
            }

            speedPointer = new SpeedPointer(280, -50f, 80, new PointF(160f, 90f), new PointF(310f, 240f));

            xianSuPointer = new SpeedPointer(279, -51, 80, new PointF(125f, 55f), new PointF(310f, 240f));
            zhiDongPointer = new SpeedPointer(279, -51, 80, new PointF(125f, 55f), new PointF(310f, 240f));

            flashTimers = new FlashTimer[10];
            for (int i = 0; i < 5; i++)
            {
                flashTimers[i] = new FlashTimer(1);
            }

            juli = new[] { "1000", "500", "200", "100", "50", "20", "10", "5", "2", "1", "0" };
            str6 = new[] { "清 除", "1", "2", "3", "4", "5", "6", "7", "8", "9", "", "0", "" };
            str7 = new[] { "LanguageSelect", "DeviceAdjust", "AlarmTest", "CleanScreen", "DMIStatus", "Reboot", "Close" };
            #region :::::::::::::::::::::::::::: point :::::::::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(10, 150);
            pDrawPoint[1] = new Point(47, 150);
            pDrawPoint[2] = new Point(470, 0);
            pDrawPoint[3] = new Point(542, 528);
            pDrawPoint[4] = new Point(60, 413);
            #endregion

            #region :::::::::::::::::::::::::::: rects ::::::::::::::::::::::::::::::::::::
            //A1
            rects[0] = new Rectangle(5, 5, 50, 50);
            //A2
            rects[1] = new Rectangle(rects[0].X, rects[0].Y + rects[0].Height, 85, 325);
            //B
            rects[2] = new Rectangle(rects[1].X + rects[1].Width, 70, 450, 340);
            //C1
            rects[7] = new Rectangle(488, rects[2].Y + rects[2].Height, 40, 65);
            //E
            rects[9] = new Rectangle(rects[1].X, 475, 535, 75);
            //G1
            rects[11] = new Rectangle(0, 550, 178, 50);
            //G2
            rects[12] = new Rectangle(rects[11].X + rects[11].Width, 550, 356, 50);
            //M1
            rects[13] = new Rectangle(535, 135, 132, 70);
            //M2
            rects[14] = new Rectangle(rects[13].X + rects[13].Width, rects[13].Y, rects[13].Width, rects[13].Height);
            //M3
            rects[15] = new Rectangle(rects[13].X, rects[13].Y + rects[13].Height, rects[13].Width, rects[13].Height);
            //M4
            rects[16] = new Rectangle(rects[15].X + rects[15].Width, rects[15].Y, rects[13].Width, rects[13].Height);
            //M5
            rects[17] = new Rectangle(rects[15].X, rects[15].Y + rects[15].Height, rects[13].Width, rects[13].Height);
            //M6
            rects[18] = new Rectangle(rects[16].X, rects[17].Y + rects[17].Height, rects[13].Width, rects[13].Height);
            //M7
            rects[19] = new Rectangle(rects[18].X, rects[18].Y + rects[18].Height, rects[18].Width, rects[18].Height);
            //T1 工号
            rects[20] = new Rectangle(rects[18].X + 20, 5, rects[18].Width - 20, 65);
            //D  Menu
            rects[8] = new Rectangle(rects[18].X - 20, rects[20].Y + rects[20].Height, 150, 65);
            //紧急消息显示区域
            rects[21] = new Rectangle(85, 480, 390, 80);
            //上
            rects[22] = new Rectangle(480, 475, 50, 48);
            //下
            rects[23] = new Rectangle(480, 528, 50, 48);
            //消息指示 
            rects[24] = new Rectangle(65, 483, 19, 20);
            //消息区域用到
            for (int i = 0; i < 3; i++)
            {
                rects[25 + i] = new Rectangle(90, 480 + i * 25, 390, 25);
            }
            //G1-G2
            for (int i = 0; i < 4; i++)
            {
                rects[28 + i] = new Rectangle(15 + i * 11, 570, 10, 10);
            }
            //生命显示添加
            rects[32] = new Rectangle(59, 570, 10, 10);
            //G1 G2 时间显示
            rects[33] = new Rectangle(80, 550, 120, 50);
            rects[34] = new Rectangle(230, 550, 100, 50);
            for (int i = 0; i < 28; i++)
            {
                rects[35 + i].X = rects[i].X + offset;
                rects[35 + i].Y = rects[i].Y + offset;
                rects[35 + i].Width = rects[i].Width - 2 * offset;
                rects[35 + i].Height = rects[i].Height - 2 * offset;
            }
            for (int i = 0; i < 28; i++)
            {
                rects[i] = rects[35 + i];
            }
            //指针位置处的速度显示区域
            rects[67] = new Rectangle(275, 210, 70, 70);
            //目标点速度
            rects[68] = new Rectangle(5, 125, 55, 20);
            //Km/h字符串位置
            rects[69] = new Rectangle(250, 320, 120, 70);
            var menuLocation = new Point(pDrawPoint[2].X + 15, pDrawPoint[2].Y + 10);
            var menuSize = new Size(260, 52);
            //Menu菜单 子项位置
            for (int i = 0; i < 7; i++)
            {
                rects[70 + i] = new Rectangle(menuLocation, menuSize);
                menuLocation.Offset(0, menuSize.Height);
            }
            var crewNumSize = new Size(60, 50);
            //工号子菜单 图片区域
            rects[77] = new Rectangle(new Point(550, 0), new Size(250, 360));
            //工号菜单区域
            rects[78] = new Rectangle(rects[77].X + 140, rects[77].Y + 5, 40, 40);
            rects[79] = new Rectangle(rects[78].X + rects[78].Width + offset, rects[78].Y, 40, 40);
            const int offsetOfcrew = 10;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[80 + i * 3 + j] = new Rectangle(rects[77].X + 10 + offsetOfcrew * (j + 1) + crewNumSize.Width * j, rects[77].Y + 100 + offsetOfcrew * (i + 1) + crewNumSize.Height * i, crewNumSize.Width, crewNumSize.Height);
                }
            }
            //显示输入工号区域
            rects[92] = new Rectangle(rects[77].X + 100, rects[77].Y + 60, 60, 30);
            // CrewID 字符区域
            rects[93] = new Rectangle(rects[92].X - 90, rects[92].Y, 85, 30);
            // 无线中断
            rects[94] = new Rectangle(rects[17].X, rects[17].Y + rects[17].Height, rects[17].Width, rects[17].Height);
            //Menu 菜单 图片区域
            //TODO rects设置未完善 缺少菜单详细点击区域  缺少工号点击区域
            #endregion
            #region ::::::::::::::::::: 按键区域 :::::::::::::::::::::::::
            //上键按键区域
            Rect.Add(new Region(rects[22]));
            //下键按键区域
            Rect.Add(new Region(rects[23]));
            //Menu按钮区域
            Rect.Add(new Region(rects[8]));
            //工号按钮区域
            Rect.Add(new Region(rects[20]));
            //工号 勾 和叉
            for (int i = 0; i < 2; i++)
            {
                Rect.Add(new Region(rects[78 + i]));
            }
            //工号按键区域
            for (int i = 0; i < 12; i++)
            {
                Rect.Add(new Region(rects[80 + i]));
            }
            //Menu菜单 子项区域
            for (int i = 0; i < 7; i++)
            {
                Rect.Add(new Region(rects[70 + i]));
            }
            //TODO 区域未设置完善  缺少菜单详细点击区域  缺少工号点击区域
            #endregion

        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public static bool ClassBegin = false;

        /// <summary>
        /// 目标距离
        /// </summary>
        internal String[] juli;
        /// <summary>
        /// 工号
        /// </summary>
        internal String[] str6;
        /// <summary>
        /// 菜单
        /// </summary>
        internal String[] str7;
        /// <summary>
        /// 服务号
        /// </summary>
        internal string carID = string.Empty;

        /// <summary>
        /// 目的号
        /// </summary>
        internal string destID = string.Empty;

        /// <summary>
        /// 司机工号
        /// </summary>
        internal string crowNumb = string.Empty;

        /// <summary>
        /// 闪烁判断
        /// </summary>
        internal FlashTimer[] flashTimers;

        /// <summary>
        /// 生命显示
        /// </summary>
        internal int liftIndic = 1;

        private readonly DialPlate m_DialPlate = new DialPlate(new LineDialPlateModel(80), new PointF(310f, 240f), 160f);

        /// <summary>
        /// 指针
        /// </summary>
        internal SpeedPointer speedPointer;

        /// <summary>
        /// RM模式下的指针
        /// </summary>
        internal SpeedPointer speedPointerRM;


        /// <summary>
        /// ATP推荐速度三角
        /// </summary>
        internal SpeedPointer xianSuPointer;

        /// <summary>
        /// 紧急制动触发速度三角
        /// </summary>
        internal SpeedPointer zhiDongPointer;

        /// <summary>
        /// 速度指针
        /// </summary>
        internal Image pointer;

        #region :::::::: 车站相关 :::::::::::
        /// <summary>
        /// 车站列表
        /// </summary>
        public SortedDictionary<int, String> StationList = new SortedDictionary<int, string>();

        /// <summary>
        /// 下一站
        /// </summary>
        internal String NextStation = "";

        /// <summary>
        /// 终点站
        /// </summary>
        internal String EndStation = "";
        #endregion

        #region :::::::: 消息相关 :::::::::::
        /// <summary>
        /// 读取文本信息的编号
        /// </summary>
        private int m_ReadTxtID;

        /// <summary>
        /// 消息框从消息列表中
        /// 读取消息的位置
        /// </summary>
        private int m_RowId;

        /// <summary>
        /// 新消息指示
        /// </summary>
        private bool m_ShowMsgSign;

        /// <summary>
        /// 新消息指示开始计时
        /// </summary>
        private bool m_TimeIn;

        /// <summary>
        /// 消息显示的时间到10秒
        /// 且没有新消息
        /// </summary>
        private bool timeUp;
        #endregion

        #region :::::::: 按键相关 :::::::::::
        /// <summary>
        /// 声音进度条
        /// </summary>
        private int m_SoundProgress;

        /// <summary>
        /// 亮度进度条
        /// </summary>
        private static int _brightProgress = 5;

        /// <summary>
        /// 亮度进度条
        /// </summary>
        public static int BrightProgress { get { return _brightProgress; } }

        /// <summary>
        /// 按键是否能按
        /// 上、下、菜单、工号
        /// 
        /// </summary>
        private readonly bool[] m_BtnCanDown = { false, false, true, true };

        /// <summary>
        /// 当前所在页面
        /// 用来区分按键的有效性
        /// 0为主界面、1为菜单界面、2为工号
        /// </summary>
        private int m_BtnViewId;

        /// <summary>
        /// 键是否按下
        /// 0为向上，1向下，2工号，3Menu 4-16 工号界面按钮 17-23Menu界面选项
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion

        #region:::::::::传值部分::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        internal bool[] bValue;

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        internal bool[] oldbValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        internal bool[] setBValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        internal int[] setBValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] fValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] oldfValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        internal float[] setFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        internal int[] setFValueNumb;

        /// <summary>
        /// 坐标集
        /// </summary>
        internal Point[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal Rectangle[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;
        #endregion#
        #endregion
    }
}
