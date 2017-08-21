using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using Subway.ATC.Casco.Common;
using Subway.ATC.Casco.Config;
using Subway.ATC.Casco.Resource;

namespace Subway.ATC.Casco.MMI_Main
{
    [GksDataType(DataType.isMMIObjectClass)]
    public partial class MMI_Main
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "MMI屏主界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();

            InitData();

            ChangedScreensaverStatus(ElectricKeyOff);


            return true;
        }
        private Screensaver Screensaver = new Screensaver();
        private Timer m_ScreensaverTimer;
        private DateTime LastMouseDownTime;

        /// <summary>
        /// 电钥匙关闭
        /// </summary>
        private bool ElectricKeyOff
        {
            get
            {
                return m_ElectricKeyOff;
            }
            set
            {
                if (m_ElectricKeyOff == value)
                {
                    return;
                }
                m_ElectricKeyOff = value;
                ChangedScreensaverStatus(value);
            }
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(this.AppConfig.AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split(new char[] { '\t', ' ' });
                string[] tmp = new string[2];
                int i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 2)
                            tmp[i] = s;
                        i++;
                    }
                    if (i == 2)
                    {
                        StationList.Add(int.Parse(tmp[0]), tmp[1]);
                    }
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
            m_ScreensaverTimer = new Timer((state =>
           {
               var sec = (DateTime.Now - LastMouseDownTime).TotalSeconds;
               if (sec >= 10)
               {
                   Screensaver.IsScreensaver = true;
               }
               else
               {
                   Screensaver.IsScreensaver = false;
               }

           }), null, int.MaxValue, int.MaxValue);

            DrawFormat.LineAlignment = (StringAlignment)1;
            DrawFormat.Alignment = (StringAlignment)1;

            RightFormat.LineAlignment = (StringAlignment)1;
            RightFormat.Alignment = (StringAlignment)2;

            LeftFormat.LineAlignment = (StringAlignment)1;
            LeftFormat.Alignment = (StringAlignment)0;

            BValue = new bool[100];
            OldbValue = new bool[100];
            SetBValue = new bool[10];
            SetBValueNumb = new int[10];

            FValue = new float[UIObj.InFloatList.Count];
            OldfValue = new float[20];
            SetFValue = new float[5];
            SetFValueNumb = new int[5];

            PDrawPoint = new Point[20];

            Rects = new Rectangle[110];

            Img = new Image[60];

            ButtonIsDown = new bool[35];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
            }
            ConfigManager.Instance.Initalize(DataPackage.Config.SystemDicrectory.SystemConfigDirectory,
                AppConfig.AppPaths.ConfigDirectory);

            m_DefaultServiceID = " " +
                                 new string('-',
                                     ConfigManager.Instance.DifferenceConfig.ContentDictionary[
                                         DifferenceResource.RowServiceIDFormat].Count()) + " ";

            var diagCOnfig = ConfigManager.Instance.DiagConfig;

            var tmp = diagCOnfig.AllDiagConfig.Find(f => f.Name == "Dial");
            Dial = new Dial(tmp.CenterPoint,
                tmp.GraduatValue,
                tmp.AllLineNum, tmp.GraduatAngle, tmp.StartAngle, tmp.Radiud, tmp.LongGraduat, tmp.ShortGraduat, tmp.LongShortGraduatMultiple, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);
            tmp = diagCOnfig.AllDiagConfig.Find(f => f.Name == "dialRM");
            DialRm = new Dial(tmp.CenterPoint,
                tmp.GraduatValue,
                tmp.AllLineNum, tmp.GraduatAngle, tmp.StartAngle, tmp.Radiud, tmp.LongGraduat, tmp.ShortGraduat, tmp.LongShortGraduatMultiple, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);
            tmp = diagCOnfig.AllDiagConfig.Find(f => f.Name == "dialTX");
            m_DialTx = new Dial(tmp.CenterPoint,
                tmp.GraduatValue,
                tmp.AllLineNum, tmp.GraduatAngle, tmp.StartAngle, tmp.Radiud, tmp.LongGraduat, tmp.ShortGraduat, tmp.LongShortGraduatMultiple, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);
            tmp = diagCOnfig.AllDiagConfig.Find(f => f.Name == "m_WmDial");
            m_WmDial = new Dial(tmp.CenterPoint,
                tmp.GraduatValue,
                tmp.AllLineNum, tmp.GraduatAngle, tmp.StartAngle, tmp.Radiud, tmp.LongGraduat, tmp.ShortGraduat, tmp.LongShortGraduatMultiple, new Pen(Color.White, 2), new Pen(Color.White, 3),
                FormatStyle.WhiteSolidBrush);
            var tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "SpeedPointer");
            SpeedPointer = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);
            tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "speedPointerRM");
            SpeedPointerRm = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);
            tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "speedPointerTX");
            m_SpeedPointerTx = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);
            tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "m_SpeedPointerWM");
            m_SpeedPointerWm = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);
            tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "xianSuPointer");
            XianSuPointer = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);
            tmp1 = diagCOnfig.AllSpeedPointerConfig.Find(f => f.Name == "zhiDongPointer");
            ZhiDongPointer = new SpeedPointer(tmp1.MaxAngle, tmp1.InitAngle, tmp1.MaxVlaue, tmp1.ApexPoint, tmp1.CenterPoint);

            FlashTimers = new FlashTimer[10];
            for (int i = 0; i < 5; i++)
            {
                FlashTimers[i] = new FlashTimer(1);
            }

            Juli = new string[11] { "1000", "500", "200", "100", "50", "20", "10", "5", "2", "1", "0" };
            PreselectionModeNormal = new string[2] { "ATO-BM", "ATO-CBTC" };
            PreselectionModeXiaMne = new string[2] { "AMC-BM", "AMC-CBTC" };
            CurrentModelNormal = new string[] { "ATO", "ATP", "RM", "EUM", "RM" };
            CurrentModelXiaMen = new string[] { "AMC", "MCS", "RM", "EUM", "RM" };
            Str3 = new string[] { "CBI", "BM", "CBTC", "CBI" };
            Str4 = new string[3] { "MM", "AM", "AA" };
            Str5 = new string[3] { "下一站", "终点站", "发车时间" };
            Str6 = new string[13] { "清 除", "1", "2", "3", "4", "5", "6", "7", "8", "9", "", "0", "" };
            Str7 = new string[7] { "语言", "测试", "状态", "重新启动", "空白", "空白", "关闭" };
            Str8 = new string[4] { "-", "+", "×", "auto" };
            Str9 = new string[2] { "强制", "非强制" };

            #region :::::::::::::::::::::::::::: point :::::::::::::::::::::::::::::::::::
            PDrawPoint[0] = new Point(10, 150);
            PDrawPoint[1] = new Point(47, 150);
            PDrawPoint[2] = new Point(480, 0);
            PDrawPoint[3] = new Point(542, 528);
            PDrawPoint[4] = new Point(60, 413);
            #endregion

            #region :::::::::::::::::::::::::::: rects ::::::::::::::::::::::::::::::::::::
            //A1
            Rects[0] = new Rectangle(5, 5, 50, 50);
            //A2
            Rects[1] = new Rectangle(0, 85, 85, 325);
            //B
            Rects[2] = new Rectangle(85, 70, 450, 340);
            //C1
            Rects[3] = new Rectangle(0, 410, 140, 65);
            //C2/C3/C4
            for (int i = 0; i < 3; i++)
            {
                Rects[4 + i] = new Rectangle(140 + 106 * i, 410, 106, 65);
            }
            //C5
            Rects[7] = new Rectangle(458, 410, 77, 65);
            //D
            Rects[8] = new Rectangle(711, 70, 88, 65);
            //E
            Rects[9] = new Rectangle(0, 475, 535, 75);
            //F
            Rects[10] = new Rectangle(535, 475, 264, 125);
            //G1/G2/G3
            for (int i = 0; i < 3; i++)
            {
                Rects[11 + i] = new Rectangle(0 + i * 178, 550, 178, 50);
            }
            //K1/K2/K3
            for (int i = 0; i < 3; i++)
            {
                Rects[14 + i] = new Rectangle(60 + i * 140, 0, 140, 70);
            }
            //M1/M2/M3/M4/M5/M6/M7/M8/M9/M10
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Rects[17 + j + i * 5] = new Rectangle(535 + i * 132, 135 + j * 68, 132, 68);
                }
            }
            //N
            Rects[27] = new Rectangle(535, 70, 176, 65);
            //T1/T2/T3
            for (int i = 0; i < 2; i++)
            {
                Rects[28 + i] = new Rectangle(480 + i * 106, 0, 106, 40);
            }
            Rects[30] = new Rectangle(480 + 212, 5, 106, 48);

            //
            for (int i = 0; i < 11; i++)
            {
                Rects[31 + i] = new Rectangle(5, 145 + i * 25, 30, 10);
            }

            //
            Rects[42] = new Rectangle(130, 410, 126, 65);
            //
            Rects[43] = new Rectangle(482, 410, 28, 65);
            //
            for (int i = 0; i < 4; i++)
            {
                Rects[44 + i] = new Rectangle(15 + i * 11, 570, 10, 10);
            }
            Rects[45] = new Rectangle(Rects[45].X + 1, Rects[45].Y + 1, Rects[45].Width - 2, Rects[45].Height - 2);
            Rects[46] = new Rectangle(Rects[46].X + 2, Rects[46].Y + 2, Rects[46].Width - 4, Rects[46].Height - 4);
            Rects[47] = new Rectangle(Rects[47].X + 1, Rects[47].Y + 1, Rects[47].Width - 2, Rects[47].Height - 2);

            for (int i = 0; i < 3; i++)
            {
                Rects[48 + i] = new Rectangle(60 + i * 140, 0, 140, 35);
                Rects[51 + i] = new Rectangle(60 + i * 140, 35, 140, 35);
            }

            Rects[54] = new Rectangle(80, 550, 120, 50);
            Rects[55] = new Rectangle(230, 550, 100, 50);

            Rects[56] = new Rectangle(85, 480, 390, 80);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    //上、下、声音、亮度
                    Rects[57 + j + i * 2] = new Rectangle(480 + i * 265, 475 + j * 53, 50, 48);
                }
            }
            //菜单
            Rects[61] = new Rectangle(540, 528, 195, 48);

            for (int i = 0; i < 3; i++)
            {
                Rects[62 + i] = new Rectangle(90, 480 + i * 25, 390, 25);
            }

            Rects[65] = new Rectangle(65, 483, 19, 20);

            //
            Rects[66] = new Rectangle(694, 0, 106, 48);

            //工号
            //工号输入
            Rects[66] = new Rectangle(532, 97, 220, 74);
            //清除
            Rects[67] = new Rectangle(615, 185, 137, 61);
            //
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rects[68 + i * 3 + j] = new Rectangle(532 + j * 83, 260 + i * 75, 57, 61);
                }
            }

            //菜单
            Rects[80] = new Rectangle(500, 0, 280, 100);
            for (int i = 0; i < 7; i++)
            {
                Rects[81 + i] = new Rectangle(500, 100 + 70 * i, 280, 48);
            }

            Rects[88] = new Rectangle(479, 470, 320, 110);
            Rects[89] = new Rectangle(483, 528, 50, 48);//下1
            Rects[90] = new Rectangle(745, 528, 50, 48);//下3
            Rects[91] = new Rectangle(745, 473, 50, 48);//上2
            Rects[92] = new Rectangle(679, 473, 50, 48);//上1
            Rects[93] = new Rectangle(542, 528, 195, 48);//下2

            Rects[94] = new Rectangle(270, 205, 70, 70);

            //确认
            for (int i = 0; i < 2; i++)
            {
                Rects[95 + i] = new Rectangle(542 + i * 133, 528, 118, 48);
            }

            Rects[97] = new Rectangle(535, 475, 265, 53);
            Rects[98] = new Rectangle(535, 475, 260, 120);

            //目标点速度
            Rects[99] = new Rectangle(5, 125, 55, 20);

            //生命显示添加
            Rects[100] = new Rectangle(59, 570, 10, 10);
            Rects[101] = new Rectangle(280, 330, 70, 30);
            #endregion

            //////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////

            #region ::::::::::::::::::: 按键区域 :::::::::::::::::::::::::
            //上、下、声音、亮度、菜单按钮
            //0、 1、 2、   3、   4
            for (int i = 0; i < 5; i++)
            {
                Rect.Add(new Region(Rects[57 + i]));
            }
            //工号
            //5、
            Rect.Add(new Region(Rects[30]));
            //清除、1、2、3、 4、 5、 6、 7、 8、 9、×、 0、 勾
            //   6、7、8、9、10、11、12、13、14、15、16、17、18
            for (int i = 0; i < 13; i++)
            {
                Rect.Add(new Region(Rects[67 + i]));
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
            for (int i = 0; i < 7; i++)
            {
                Rect.Add(new Region(Rects[81 + i]));
            }

            //声音亮度
            // -、 +、×、auto
            //88、89、90、91
            for (int i = 0; i < 4; i++)
            {
                Rect.Add(new Region(Rects[89 + i]));
            }

            //确认状态信息
            for (int i = 0; i < 2; i++)
            {
                Rect.Add(new Region(Rects[95 + i]));
            }

            StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rects.txt"), false);

            for (int i = 0; i < Rects.Length; ++i)
            {
                sw.WriteLine($"当前序号:{i}  区域大小为：{Rects[i].X},{Rects[i].Y},{Rects[i].Width},{Rects[i].Height}");
            }
            sw.Close();
            #endregion
        }
        #endregion

        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::

        public static StringFormat DrawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        public static bool ClassBegin = false;

        /// <summary>
        /// 目标距离
        /// </summary>
        internal string[] Juli;

        internal string[] PreselectionModeNormal;
        internal string[] PreselectionModeXiaMne;

        internal string[] CurrentModelNormal;
        internal string[] CurrentModelXiaMen;

        internal string[] Str3;

        internal string[] Str4;

        internal string[] Str5;

        internal string[] Str6;

        internal string[] Str7;

        internal string[] Str8;

        internal string[] Str9;
        /// <summary>
        /// 服务号
        /// </summary>
        internal string CarId = string.Empty;

        /// <summary>
        /// 目的号
        /// </summary>
        internal string DestId = string.Empty;

        /// <summary>
        /// 司机工号
        /// </summary>
        internal string CrowNumb = string.Empty;

        /// <summary>
        /// 闪烁判断
        /// </summary>
        internal FlashTimer[] FlashTimers;

        /// <summary>
        /// 生命显示
        /// </summary>
        internal int LiftIndic = 1;

        #region :::::::: 表盘相关 :::::::::::
        /// <summary>
        /// 速度表盘
        /// </summary>
        internal Dial Dial;

        /// <summary>
        /// RM模式下表盘
        /// </summary>
        internal Dial DialRm;

        /// <summary>
        /// 退行模式
        /// </summary>
        private Dial m_DialTx;

        /// <summary>
        /// WM 表盘。洗车模式
        /// </summary>
        private Dial m_WmDial;

        /// <summary>
        /// 指针
        /// </summary>
        internal SpeedPointer SpeedPointer;

        /// <summary>
        /// RM模式下的指针
        /// </summary>
        internal SpeedPointer SpeedPointerRm;

        /// <summary>
        /// 退行模式下的指针
        /// </summary>
        private SpeedPointer m_SpeedPointerTx;

        /// <summary>
        /// 洗车模式指针
        /// </summary>
        private SpeedPointer m_SpeedPointerWm;

        /// <summary>
        /// 限速3角
        /// </summary>
        internal SpeedPointer XianSuPointer;

        /// <summary>
        /// 制动3角
        /// </summary>
        internal SpeedPointer ZhiDongPointer;

        #endregion

        #region :::::::: 车站相关 :::::::::::
        /// <summary>
        /// 车站列表
        /// </summary>
        public SortedDictionary<int, string> StationList = new SortedDictionary<int, string>();

        /// <summary>
        /// 下一站
        /// </summary>
        internal string NextStation = "";

        /// <summary>
        /// 终点站
        /// </summary>
        internal string EndStation = "";
        #endregion

        #region :::::::: 消息相关 :::::::::::
        /// <summary>
        /// 读取文本信息的编号
        /// </summary>
        private int m_ReadTxtId;

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
        private bool m_TimeUp;
        #endregion

        #region :::::::: 按键相关 :::::::::::
        /// <summary>
        /// 声音进度条
        /// </summary>
        private int m_SoundProgress = 0;

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
        private bool[] m_BtnCanDown = new bool[6] { false, false, true, true, true, true };

        /// <summary>
        /// 当前所在页面
        /// 用来区分按键的有效性
        /// 0为主界面、1为菜单界面、2为声音、3为亮度、4为工号、5为确认状态信息
        /// </summary>
        private int m_BtnViewId = 0;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] ButtonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;
        #endregion

        #region:::::::::传值部分::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        internal bool[] BValue;

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        internal bool[] OldbValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        internal bool[] SetBValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        internal int[] SetBValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] FValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] OldfValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        internal float[] SetFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        internal int[] SetFValueNumb;

        /// <summary>
        /// 坐标集
        /// </summary>
        internal Point[] PDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal Rectangle[] Rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;

        private bool m_ElectricKeyOff;

        #endregion#
        #endregion
    }
}
