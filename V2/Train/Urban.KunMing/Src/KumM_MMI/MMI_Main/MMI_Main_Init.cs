using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.PublicUI;

namespace KumM_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public partial class MMI_Main : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "MMI屏主界面";
        }

        public MMI_Main()
        {
            
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFile();
            InitData();
            return true;
        }

        private void LoadFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\车站信息.txt");
            LogMgr.Info(file);
            foreach (var line in File.ReadAllLines(file, Encoding.Default))
            {
                addStringByLine(line);
            }
        }

        public void addStringByLine(string cStr)
        {

            var split = cStr.Split(new char[] { '\t', ' ' });
            var tmp = new string[2];
            var i = 0;
            foreach (var s in split)
            {
                if (s.Trim() != "")
                {
                    if (i < 2)
                        tmp[i] = s;
                    i++;
                }
                if (i == 2)
                {
                    _StationList.Add(int.Parse(tmp[0]), tmp[1]);
                    return;
                }
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
            LeftFormat.Alignment = (StringAlignment)0;

            bValue = new bool[100];
            oldbValue = new bool[100];
            setBValue = new bool[10];
            setBValueNumb = new int[10];

            fValue = new float[20];
            oldfValue = new float[20];
            setFValue = new float[5];
            setFValueNumb = new int[5];

            pDrawPoint = new Point[20];

            rects = new Rectangle[110];

            Img = new Image[60];

            buttonIsDown = new bool[35];

            Rect = new List<Region>();

            for (var i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }

            dial = new Dial(new PointF(310f, 240f),
                new string[7] { "0", "20", "40", "60", "80", "100", "120" },
                25, 12.5, -240, 160, 30, 15, 4, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);

            dialRM = new Dial(new PointF(310f, 240f), new string[2] { "0", "25" },
                6, 12.5, -240, 160, 30, 15, 5, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);

            dialTX = new Dial(new PointF(310f, 240f), new string[2] { "0", "5" },
                2, 12.5, -240, 160, 30, 15, 1, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);

            speedPointer = new SpeedPointer(300f, -60f, 120f, new PointF(160f, 90f), new PointF(310f, 240f));
            speedPointerRM = new SpeedPointer(62.5f, -60, 25f, new PointF(160f, 90f), new PointF(310f, 240f));
            speedPointerTX = new SpeedPointer(12.5f, -60, 5f, new PointF(160f, 90f), new PointF(310f, 240f));
            xianSuPointer = new SpeedPointer(300, -60, 120f, new PointF(125f, 55f), new PointF(310f, 240f));
            zhiDongPointer = new SpeedPointer(300, -60, 120f, new PointF(125f, 55f), new PointF(310f, 240f));

            flashTimers = new FlashTimer[10];
            for (var i = 0; i < 5; i++)
            {
                flashTimers[i] = new FlashTimer(1);
            }

            juli = new string[11] { "1000", "500", "200", "100", "50", "20", "10", "5", "2", "1", "0" };
            str1 = new string[2] { "ATO-BM", "ATO-CBTC" };
            str2 = new string[4] { "ATO", "ATP", "RM", "EUM" };
            str3 = new string[3] { "CBI", "BM", "CBTC" };
            str4 = new string[3] { "MM", "AM", "AA" };
            str5 = new string[3] { "下一站", "终点站", "发车时间" };
            str6 = new string[13] { "清 除", "1", "2", "3", "4", "5", "6", "7", "8", "9", "", "0", "" };
            str7 = new string[7] { "语言", "测试", "状态", "重新启动", "空白", "空白", "关闭" };
            str8 = new string[4] { "-", "+", "×", "auto" };
            str9 = new string[2] { "强制", "非强制" };

            #region :::::::::::::::::::::::::::: point :::::::::::::::::::::::::::::::::::
            pDrawPoint[0] = new Point(10, 150);
            pDrawPoint[1] = new Point(47, 150);
            pDrawPoint[2] = new Point(480, 0);
            pDrawPoint[3] = new Point(542, 528);
            pDrawPoint[4] = new Point(60, 413);
            #endregion

            #region :::::::::::::::::::::::::::: rects ::::::::::::::::::::::::::::::::::::
            //A1
            rects[0] = new Rectangle(5, 5, 50, 50);
            //A2
            rects[1] = new Rectangle(0, 85, 85, 325);
            //B
            rects[2] = new Rectangle(85, 70, 450, 340);
            //C1
            rects[3] = new Rectangle(0, 410, 140, 65);
            //C2/C3/C4
            for (var i = 0; i < 3; i++)
            {
                rects[4 + i] = new Rectangle(140 + 106 * i, 410, 106, 65);
            }
            //C5
            rects[7] = new Rectangle(458, 410, 77, 65);
            //D
            rects[8] = new Rectangle(711, 70, 88, 65);
            //E
            rects[9] = new Rectangle(0, 475, 535, 75);
            //F
            rects[10] = new Rectangle(535, 475, 264, 125);
            //G1/G2/G3
            for (var i = 0; i < 3; i++)
            {
                rects[11 + i] = new Rectangle(0 + i * 178, 550, 178, 50);
            }
            //K1/K2/K3
            for (var i = 0; i < 3; i++)
            {
                rects[14 + i] = new Rectangle(60 + i * 140, 0, 140, 70);
            }
            //M1/M2/M3/M4/M5/M6/M7/M8/M9/M10
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    rects[17 + j + i * 5] = new Rectangle(535 + i * 132, 135 + j * 68, 132, 68);
                }
            }
            //N
            rects[27] = new Rectangle(535, 70, 176, 65);
            //T1/T2/T3
            for (var i = 0; i < 3; i++)
            {
                rects[28 + i] = new Rectangle(480 + i * 106, 5, 106, 48);
            }

            //
            for (var i = 0; i < 11; i++)
            {
                rects[31 + i] = new Rectangle(5, 145 + i * 25, 30, 10);
            }

            //
            rects[42] = new Rectangle(130, 410, 126, 65);
            //
            rects[43] = new Rectangle(482, 410, 28, 65);
            //
            for (var i = 0; i < 4; i++)
            {
                rects[44 + i] = new Rectangle(15 + i * 11, 570, 10, 10);
            }
            rects[45] = new Rectangle(rects[45].X + 1, rects[45].Y + 1, rects[45].Width - 2, rects[45].Height - 2);
            rects[46] = new Rectangle(rects[46].X + 2, rects[46].Y + 2, rects[46].Width - 4, rects[46].Height - 4);
            rects[47] = new Rectangle(rects[47].X + 1, rects[47].Y + 1, rects[47].Width - 2, rects[47].Height - 2);

            for (var i = 0; i < 3; i++)
            {
                rects[48 + i] = new Rectangle(60 + i * 140, 0, 140, 35);
                rects[51 + i] = new Rectangle(60 + i * 140, 35, 140, 35);
            }

            rects[54] = new Rectangle(80, 550, 120, 50);
            rects[55] = new Rectangle(230, 550, 100, 50);

            rects[56] = new Rectangle(85, 480, 390, 80);

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    //上、下、声音、亮度
                    rects[57 + j + i * 2] = new Rectangle(480 + i * 265, 475 + j * 53, 50, 48);
                }
            }
            //菜单
            rects[61] = new Rectangle(540, 528, 195, 48);

            for (var i = 0; i < 3; i++)
            {
                rects[62 + i] = new Rectangle(90, 480 + i * 25, 390, 25);
            }

            rects[65] = new Rectangle(65, 483, 19, 20);

            //
            rects[66] = new Rectangle(694, 0, 106, 48);

            //工号
            //工号输入
            rects[66] = new Rectangle(532, 97, 220, 74);
            //清除
            rects[67] = new Rectangle(615, 185, 137, 61);
            //
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    rects[68 + i * 3 + j] = new Rectangle(532 + j * 83, 260 + i * 75, 57, 61);
                }
            }

            //菜单
            rects[80] = new Rectangle(500, 0, 280, 100);
            for (var i = 0; i < 7; i++)
            {
                rects[81 + i] = new Rectangle(500, 100 + 70 * i, 280, 48);
            }

            rects[88] = new Rectangle(479, 470, 320, 110);
            rects[89] = new Rectangle(483, 528, 50, 48);//下1
            rects[90] = new Rectangle(745, 528, 50, 48);//下3
            rects[91] = new Rectangle(745, 473, 50, 48);//上2
            rects[92] = new Rectangle(679, 473, 50, 48);//上1
            rects[93] = new Rectangle(542, 528, 195, 48);//下2

            rects[94] = new Rectangle(270, 205, 70, 70);

            //确认
            for (var i = 0; i < 2; i++)
            {
                rects[95 + i] = new Rectangle(542 + i * 133, 528, 118, 48);
            }

            rects[97] = new Rectangle(535, 475, 265, 53);
            rects[98] = new Rectangle(535, 475, 260, 120);

            //目标点速度
            rects[99] = new Rectangle(5, 125, 55, 20);

            //生命显示添加
            rects[100] = new Rectangle(59, 570, 10, 10);
            #endregion

            //////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////

            #region ::::::::::::::::::: 按键区域 :::::::::::::::::::::::::
            //上、下、声音、亮度、菜单按钮
            //0、 1、 2、   3、   4
            for (var i = 0; i < 5; i++)
            {
                Rect.Add(new Region(rects[57 + i]));
            }
            //工号
            //5、
            Rect.Add(new Region(rects[30]));
            //清除、1、2、3、 4、 5、 6、 7、 8、 9、×、 0、 勾
            //   6、7、8、9、10、11、12、13、14、15、16、17、18
            for (var i = 0; i < 13; i++)
            {
                Rect.Add(new Region(rects[67 + i]));
            }

            //////////////////////////////////菜单内容
            //-------------菜单--------------btnViewId == 1
            //语言、测试、状态、重启、空白、空白、关闭
            //  19、  20、  21、  22、  23、  24、  25

            //-------------语言--------------btnViewId == 12
            //英文、中文、空白、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------测试--------------btnViewId == 13
            //声音、信息、空白、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------SOUNDS--------------btnViewId == 14
            //空白、空白、空白、空白、空白、NEXT、CLOSE
            //  19、  20、  21、  22、  23、  24、  25

            //-------------信息--直接跳页面--------------

            //-------------状态--------------btnViewId == 15
            //配置、网络、内部配置、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------配置--直接跳页面--------------

            //-------------DMI NETWORK--------------btnViewId == 151
            //空白、空白、空白、空白、空白、空白、CLOSE
            //  19、  20、  21、  22、  23、  24、  25

            //-------------内部配置--直接跳页面--------------
            for (var i = 0; i < 7; i++)
            {
                Rect.Add(new Region(rects[81 + i]));
            }

            //声音亮度
            // -、 +、×、auto
            //88、89、90、91
            for (var i = 0; i < 4; i++)
            {
                Rect.Add(new Region(rects[89 + i]));
            }

            //确认状态信息
            for (var i = 0; i < 2; i++)
            {
                Rect.Add(new Region(rects[95 + i]));
            }
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        /// <summary>
        /// 目标距离
        /// </summary>
        internal string[] juli;

        internal string[] str1;

        internal string[] str2;

        internal string[] str3;

        internal string[] str4;

        internal string[] str5;

        internal string[] str6;

        internal string[] str7;

        internal string[] str8;

        internal string[] str9;
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

        #region :::::::: 表盘相关 :::::::::::
        /// <summary>
        /// 速度表盘
        /// </summary>
        internal Dial dial;

        /// <summary>
        /// RM模式下表盘
        /// </summary>
        internal Dial dialRM;

        /// <summary>
        /// 退行模式
        /// </summary>
        private Dial dialTX;

        /// <summary>
        /// 指针
        /// </summary>
        internal SpeedPointer speedPointer;

        /// <summary>
        /// RM模式下的指针
        /// </summary>
        internal SpeedPointer speedPointerRM;

        /// <summary>
        /// 退行模式下的指针
        /// </summary>
        private SpeedPointer speedPointerTX;

        /// <summary>
        /// 限速3角
        /// </summary>
        internal SpeedPointer xianSuPointer;

        /// <summary>
        /// 制动3角
        /// </summary>
        internal SpeedPointer zhiDongPointer;

        /// <summary>
        /// 速度指针
        /// </summary>
        internal Image pointer;
        #endregion

        #region :::::::: 车站相关 :::::::::::
        /// <summary>
        /// 车站列表
        /// </summary>
        public SortedDictionary<int, string> _StationList = new SortedDictionary<int, string>();

        /// <summary>
        /// 下一站
        /// </summary>
        internal string nextStation = "";

        /// <summary>
        /// 终点站
        /// </summary>
        internal string endStation = "";
        #endregion

        #region :::::::: 消息相关 :::::::::::
        /// <summary>
        /// 读取文本信息的编号
        /// </summary>
        private int readTxtID;

        /// <summary>
        /// 消息框从消息列表中
        /// 读取消息的位置
        /// </summary>
        private int RowId;

        /// <summary>
        /// 新消息指示
        /// </summary>
        private bool showMsgSign;

        /// <summary>
        /// 新消息指示开始计时
        /// </summary>
        private bool timeIn;

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
        private int soundProgress = 0;

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
        /// 上、下、声音、亮度、菜单、工号
        /// 
        /// </summary>
        private bool[] btnCanDown = new bool[6] { false, false, true, true, true, true };

        /// <summary>
        /// 当前所在页面
        /// 用来区分按键的有效性
        /// 0为主界面、1为菜单界面、2为声音、3为亮度、4为工号、5为确认状态信息
        /// </summary>
        private int btnViewId = 0;

        /// <summary>
        /// 键是否按下
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
