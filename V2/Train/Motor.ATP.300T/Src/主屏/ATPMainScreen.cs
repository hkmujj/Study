using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ATPComControl.MRSP;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using Mmi.Common.DateTimeInterpreter;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Motor.ATP._300T.Appraise;
using Motor.ATP._300T.Config;
using Motor.ATP._300T.Constant;
using Motor.ATP._300T.DataManager;
using Motor.ATP._300T.Resources;
using Motor.ATP._300T.Resources.ImageKeys;
using Motor.ATP._300T.共用;
using Motor.ATP._300T.共用.功能键与菜单;
using MsgReceiveMod;
using Coordinate = Motor.ATP._300T.共用.Coordinate;

namespace Motor.ATP._300T.主屏
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPMainScreen : ATPBaseClass, IDataClearable
    {
        private ICourseService m_CourseService;

        // ReSharper disable once InconsistentNaming
        public DateTime m_CurrentTime;

        private IDateTimeInterpreter m_DateTimeInterpreter;

        public IStationNameProviderService StationNameProviderService { private set; get; }

        public override void paint(Graphics g)
        {
            if (BoolList[IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]])
            {
                DataManager.DataManager.Instance.ClearData(DataClearFlag.WaiteForDriver);

                UpdateWrateForDriverDatas();

                DrawBackground(g);

                m_AreaBContent.DrawAreaB(g, AreaBView.ControlModel | AreaBView.SpeedValue);

                OpenFunBtnCtcs300T.DrawMsgList(g);

                m_AreaEContent.DrawAreaEDateTime(g);
            }
            else
            {
                if (CurrentView != ViewConfig.Main)
                {
                    append_postCmd(CmdType.ChangePage, (int)ViewConfig.Main, 0, 0);
                    return;
                }

                UpdateValue();

                m_AppraiseControl.Notify();

                DrawBackground(g);

                if (CurrentTrainMode != TrainMode.隔离)
                {
                    m_AreaAContent.DrawAreaA(g);
                }

                m_AreaBContent.DrawAreaB(g);

                if (CurrentTrainMode != TrainMode.隔离)
                {
                    m_AreaCContent.DrawAreaC(g);

                    m_AreaDContent.DrawAreaD(g);

                    m_AreaEContent.DrawAreaE(g);

                    m_AreaFContent.DrawAreaF(g);
                }
            }

            UpdateStateAfterPaint();
        }

        private void UpdateWrateForDriverDatas()
        {
            ReceiveMsg();

            UpdateControlModel();
        }

        private void DrawBackground(Graphics g)
        {
            g.FillRectangle(SolidBrushsItems.BKGBrush, m_RectsList[19]); //背景色
            g.DrawImage(ComImages.底层框架_粗线, m_RectsList[19]); //背景图
        }

        public int[] m_BrakeIndexs;
        private AreaAContent m_AreaAContent;
        private AreaCContent m_AreaCContent;

        public int[] RbcIndexs
        {
            get
            {
                return new[]
                {
                    GetInBoolIndex(InBoolKeys.InbRBC未连接),
                    GetInBoolIndex(InBoolKeys.InbRBC正在连接),
                    GetInBoolIndex(InBoolKeys.InbRBC已连接),
                };
            }
        }

        public int[] GSMRIndexs
        {
            get
            {
                return new[]
                {
                    GetInBoolIndex(InBoolKeys.InbGSMR灰色),
                    GetInBoolIndex(InBoolKeys.InbGSMR绿2),
                    GetInBoolIndex(InBoolKeys.InbGSMR绿3),
                };
            }
        }

        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::

        //2
        public override string GetInfo()
        {
            return "主视图";
        }

        //6
        public override bool Initalize()
        {
            m_AreaEContent = new AreaEContent(this);
            m_AreaDContent = new AreaDContent(this);
            m_AreaAContent = new AreaAContent(this);
            m_AreaCContent = new AreaCContent(this);
            m_AreaBContent = new AreaBContent(this);
            m_AreaFContent = new AreaFContent(this);
            StationNameProviderService = DataPackage.ServiceManager.GetService<IStationNameProviderService>();
            m_BrakeIndexs = new[]
            {
                GetInBoolIndex(InBoolKeys.Inb紧急制动),
                GetInBoolIndex(InBoolKeys.Inb常用制动),
                GetInBoolIndex(InBoolKeys.Inb中等常用制动),
                GetInBoolIndex(InBoolKeys.Inb弱常用制动),
                GetInBoolIndex(InBoolKeys.Inb允许缓解1),
            };

            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
            m_CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
            m_DateTimeInterpreter = DateTimeInterpreterFactory.Instance.GetInterpreter(RawDataType.DateTime);
            m_AppraiseControl = new AppraiseControl(this);

            DataManager.DataManager.Instance.AddRequest(this);

            LoadAttachedFiles();

            return Init();
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                DataManager.DataManager.Instance.ClearData();
            }
        }

        private void LoadAttachedFiles()
        {
            var file = Path.Combine(RecPath, "..\\Config\\车站信息.txt");
            {
                var all = ReadFileAllLines(file);

                ParserStationInfo(all);
            }

            file = Path.Combine(RecPath, "..\\Config\\消息通知.txt");
            {
                var all = ReadFileAllLines(file);

                ParserMessageInfo(all);
            }
        }


        private void ParserMessageInfo(IEnumerable<string> all)
        {
            try
            {
                foreach (var cStr in all)
                {
                    var split = GetStringSplitArr(cStr, '\t');
                    var tmp = new string[9];
                    var i = 0;
                    foreach (var str in split.Take(9))
                    {
                        if (str.Trim() != string.Empty)
                        {
                            if (i < 9)
                            {
                                tmp[i] = str;
                            }
                            i++;
                        }
                        if (i == 9)
                        {
                            m_MsgDict.Add(int.Parse(tmp[0]), tmp);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ParserStationInfo(IEnumerable<string> all)
        {
            foreach (var split in all.Select(cStr => GetStringSplitArr(cStr, '\t', ' ')))
            {
                int tempIndex;
                if (split.Length == 2 && int.TryParse(split[0], out tempIndex))
                {
                    m_StationsDict.Add(Convert.ToInt32(split[0]), split[1]);
                }
            }
        }

        private IEnumerable<string> ReadFileAllLines(string file)
        {
            if (File.Exists(file))
            {
                return File.ReadAllLines(file, Encoding.Default);
            }

            return new string[0];
        }


        #endregion



        private SpeedProportionConfig SpeedProportionConfig;

        private bool Init()
        {



            SpeedProportionConfig =
                DataSerialization.DeserializeFromXmlFile<SpeedProportionConfig>(Path.Combine(AppPaths.ConfigDirectory,
                    SpeedProportionConfig.FileName));



            var mrspImgs = new Image[]
            {
                PlanImages.y2,
                PlanImages.y3,
                PlanImages.y4,
                PlanImages.y5,
                PlanImages.y6,
                PlanImages.y1,

                PlanImages.加,
                PlanImages.减,
                PlanImages.降至0,
            };

            m_TheMrsp = new ATP300MRSP(new PointF(422, 68), mrspImgs);

            var cout = 30;
            var firstIn = GetInBoolIndex(InBoolKeys.Inb启动确认);
            var fisrtOut = GetOutBoolIndex(OutBoolKeys.Oub启动确认);
            //瞬时量字典
            for (var i = 0; i < cout; ++i)
            {
                m_InstantValueDict.Add(firstIn + i, fisrtOut + i);
            }

            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(View_ID_Name.MainScreen);

            #region ::::::::::::: 制动预警颜色字典

            m_WarningBrushDic = new Dictionary<int, SolidBrush>
            {
                {10, SolidBrushsItems.BlackBrush},
                {20, SolidBrushsItems.BlackBrush},
                {30, SolidBrushsItems.BlackBrush},
                {40, SolidBrushsItems.BlackBrush},
                {11, SolidBrushsItems.GrayBrush},
                {21, SolidBrushsItems.GrayBrush},
                {31, SolidBrushsItems.GrayBrush},
                {41, SolidBrushsItems.GrayBrush},
                {12, SolidBrushsItems.YellowBrush},
                {22, SolidBrushsItems.YellowBrush},
                {32, SolidBrushsItems.YellowBrush},
                {42, SolidBrushsItems.YellowBrush},
                {13, SolidBrushsItems.OrangeBrush},
                {23, SolidBrushsItems.OrangeBrush},
                {33, SolidBrushsItems.OrangeBrush},
                {43, SolidBrushsItems.OrangeBrush},
                {14, SolidBrushsItems.RedBrush},
                {24, SolidBrushsItems.RedBrush},
                {34, SolidBrushsItems.RedBrush},
                {44, SolidBrushsItems.RedBrush}
            };

            #endregion

            m_TargetSpeed = new List<GDIProgress>();
            var loc = new Point(40, 106 + 214 - 143);
            const int width = 18;
            for (var i = 9; i >= 0; i--)
            {
                m_TargetSpeed.Add(new GDIProgress(Direction.Up)
                {
                    CurrentValue = 0,
                    MaxValue = 100,
                    Location = loc,
                    NeedOutLine = false,
                    Size = new Size(width, m_Length[i]),
                    BackgroundColor = Color.White
                });
                loc.Offset(0, i != 0 ? -m_Length[i - 1] : 0);
            }
            m_ThePointerImg = DialPointerImages.灰针;

            m_C3ThePointer[0] = new SpeedPointer(135f, -140f, -140f, 150f, 0f, m_RectsList[18],
                new PointF(m_RectsList[18].X + m_RectsList[18].Width / 2, m_RectsList[18].Y + m_RectsList[18].Height / 2),
                m_ThePointerImg);
            m_C3ThePointer[1] = new SpeedPointer(145f, -5f, -5f, 450f, 150f, m_RectsList[18],
                new PointF(m_RectsList[18].X + m_RectsList[18].Width / 2, m_RectsList[18].Y + m_RectsList[18].Height / 2),
                m_ThePointerImg);

            #region ::::::::: 初始化圆弧曲线

            m_C3TheVtargetCircle = new Circle[2];
            m_C3TheVtargetCircle[0] = new Circle(135f, -230f, 150f, 0f, m_RectsList[21]);
            m_C3TheVtargetCircle[1] = new Circle(145f, -95f, 450f, 150f, m_RectsList[21]);

            m_C3TheVpermCircle = new Circle[2];
            m_C3TheVpermCircle[0] = new Circle(135f, -230f, 150f, 0f, m_RectsList[21]);
            m_C3TheVpermCircle[1] = new Circle(145f, -95f, 450f, 150f, m_RectsList[21]);

            m_C3TheVintCircle = new Circle[2];
            m_C3TheVintCircle[0] = new Circle(135f, -230f, 150f, 0f, m_RectsList[22]);
            m_C3TheVintCircle[1] = new Circle(145f, -95f, 450f, 150f, m_RectsList[22]);

            m_C3TheHookCircle = new Circle[2];
            m_C3TheHookCircle[0] = new Circle(135f, -230f, 150f, 0f, m_RectsList[22]);
            m_C3TheHookCircle[1] = new Circle(145f, -95f, 450f, 150f, m_RectsList[22]);

            m_C3TheOutCircle = new Circle[2];
            m_C3TheOutCircle[0] = new Circle(5f, -235f, 5f, 0, m_RectsList[21]);
            m_C3TheOutCircle[1] = new Circle(55f, 50f, 5f, 0, m_RectsList[21]);

            #endregion

            OpenFunBtnCtcs300T = new OpenFunBtnCTCS300T(this,
                new Image[]
                {
                    ComImages.箭头_上_亮,
                    ComImages.箭头_上_暗,
                    ComImages.箭头_下_亮,
                    ComImages.箭头_下_暗,
                    ComImages.亮度,
                    ComImages.声音
                })
            {
                AppraiseControl = m_AppraiseControl
            };
            OpenFunBtnCtcs300T.OnAppendPostCmd += append_postCmd;

            Ctcs3Entry = new bool[3];
            return true;
        }

        private AppraiseControl m_AppraiseControl;

        public bool[] Ctcs3Entry { get; set; }


        public List<Image> m_ImgsList;

        /// <summary>
        /// 对象涉及的所有坐标
        /// </summary>
        public List<RectangleF> m_RectsList;


        private Dictionary<int, SolidBrush> m_WarningBrushDic;

        /// <summary>
        /// 瞬时量字典
        /// </summary>
        private readonly Dictionary<int, int> m_InstantValueDict = new Dictionary<int, int>();

        #region ::::::::::::::::::: A1

        private readonly int[] m_Length = { 3, 4, 4, 5, 6, 7, 9, 12, 21, 145 };

        /// <summary>
        /// 制动预警设定时间
        /// </summary>
        private const int Tsquare = 8;

        /// <summary>
        /// 列车速度
        /// </summary>
        public float m_Vtrain;

        /// <summary>
        /// 允许速度
        /// </summary>
        public float m_Vperm;

        /// <summary>
        /// 目标速度
        /// </summary>
        public float m_Vtarget;

        /// <summary>
        /// 干预速度
        /// </summary>
        public float m_Vint;

        /// <summary>
        /// 开口速度
        /// </summary>
        private float m_Vrelease;



        #endregion

        #region ::::::::::::::::: A2

        /// <summary>
        /// 目标距离
        /// </summary>
        public float m_TarDistance;

        public List<GDIProgress> m_TargetSpeed;

        #endregion

        #region ::::::::::::::::::: B1

        /// <summary>
        /// 速度指针
        /// </summary>
        public Image m_ThePointerImg;

        /// <summary>
        /// 指针前半部分
        /// </summary>
        public readonly SpeedPointer[] m_C3ThePointer = new SpeedPointer[2];

        /// <summary>
        /// 目标速度曲线
        /// </summary>
        public Circle[] m_C3TheVtargetCircle;

        /// <summary>
        /// 允许速度曲线
        /// </summary>
        public Circle[] m_C3TheVpermCircle;

        /// <summary>
        /// 警报制动速度曲线
        /// </summary>
        public Circle[] m_C3TheVintCircle;

        /// <summary>
        /// 钩子
        /// </summary>
        public Circle[] m_C3TheHookCircle;

        /// <summary>
        /// 多出部分的曲线
        /// </summary>
        public Circle[] m_C3TheOutCircle;

        #endregion

        #region ::::::::::::::: C

        #endregion

        #region ::::::::::::::: D

        public ATP300MRSP m_TheMrsp;

        public readonly string[] m_SingleLampStr =
        {
            "",
            "6",
            "5",
            "4",
            "3",
            "2",
            "",
            "",
            "",
            "",
            "2",
            "2",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };

        public int m_RangeScaleMode;

        /// <summary>
        /// 显示计划区
        /// </summary>
        public bool m_ShowPlanningArea;

        #endregion

        #region ::::::::::::::: E

        //车站
        public readonly Dictionary<int, string> m_StationsDict = new Dictionary<int, string>();

        /// <summary>
        /// 放大镜
        /// </summary>
        public Image[] m_TheGlasses = new Image[2];

        #endregion

        #region :::::::::::: F

        private readonly Dictionary<int, string[]> m_MsgDict = new Dictionary<int, string[]>();

        public OpenFunBtnCTCS300T OpenFunBtnCtcs300T { get; private set; }

        //OpenFunBtn op = new OpenFunBtn();

        #endregion

        public ControlMode m_TheControlMode = ControlMode.Other;
        private AreaEContent m_AreaEContent;
        private AreaDContent m_AreaDContent;
        private AreaBContent m_AreaBContent;
        private AreaFContent m_AreaFContent;


        public ATPMainScreen()
        {
            CurrentSignalSystem = SignalSystem.无信号;
            CurrentTrainMode = TrainMode.无;

        }

        /// <summary>
        /// 当前列车模式
        /// </summary>
        public TrainMode CurrentTrainMode { get; set; }

        public SignalSystem CurrentSignalSystem { get; private set; }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);

            if (nParaA == 2)
            {
                OpenFunBtnCtcs300T.ClearData();
            }
        }

        /// <summary>
        /// 计划信息区横坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float Getx(float x)
        {
            float y = 0;
            if (x < 0)
            {
                x = 0;
            }
            switch (m_RangeScaleMode)
            {
                case 0:
                    y = x < 1000f ? 428 + x * 80 / 1000f : (float)(508 + 40 * Math.Log10(x / 1000f) / Math.Log10(2));
                    break;
                case 1:
                    y = x < 500f ? 428 + x * 80 / 500f : (float)(508 + 40 * Math.Log10(x / 500f) / Math.Log10(2));
                    break;
                case 2:
                    y = x < 250f ? 428 + x * 80 / 1000f : (float)(508 + 40 * Math.Log10(x / 250f) / Math.Log10(2));
                    break;
            }

            return y + 1;

        }

        /// <summary>
        /// 计划信息区纵坐标值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float Gety(float x)
        {
            if (x < 0)
            {
                x = 0;
            }
            else if (x > 450)
            {
                x = 450;
            }
            var y = x * 40 / 100f;

            return 258 - y;

        }

        /// <summary>
        /// 更新指针颜色变化
        /// </summary>
        private void UpdatePointerBrush()
        {
            switch (ReturnModeAndRunStatus(m_TheControlMode, true))
            {
                case 11:
                case 21:
                case 31:
                case 41:
                    m_ThePointerImg = DialPointerImages.灰针; //灰色指针
                    break;
                case 12:
                case 22:
                case 32:
                case 42:
                    m_ThePointerImg = DialPointerImages.黄针; //黄色指针
                    break;
                case 13:
                case 23:
                case 33:
                case 43:
                    m_ThePointerImg = DialPointerImages.橙针; //橙色指针
                    break;
                case 14:
                case 24:
                case 34:
                case 44:
                    m_ThePointerImg = DialPointerImages.红针; //红色指针
                    break;
                default:
                    m_ThePointerImg = DialPointerImages.灰针;
                    break;
            }
        }

        /// <summary>
        /// 信号灯更新
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>信号灯</returns>
        public Image UpdateSignalLamp(int id)
        {
            switch (id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return SignalImages.绿灯;
                case 7:
                    return SignalImages.绿黄灯;
                case 8:
                case 9:
                case 10:
                case 11:
                    return SignalImages.黄灯;
                case 12:
                case 13:
                    return SignalImages.双黄灯;
                case 14:
                case 15:
                    return SignalImages.红黄灯;
                case 16:
                    return SignalImages.红灯;
                case 17:
                case 18:
                    return SignalImages.灰灯;
                case 19:
                    return SignalImages.白灯;
                default:
                    return SignalImages.无色;
            }
        }

        public float GetProportionValue(float value)
        {
            return value * SpeedProportionConfig.Proportion;
        }
        private int[] GetStringToIntArr(string strArr)
        {
            var tmpStrArr = GetStringSplitArr(strArr, ',', '，', ' ');
            var tmpIntArr = new int[tmpStrArr.Length];
            for (var i = 0; i < tmpIntArr.Length; i++)
            {
                tmpIntArr[i] = int.Parse(tmpStrArr[i]);
            }
            return tmpIntArr;
        }

        private int[] GetStringToOutIntArr(string strArr)
        {
            var tmpStrArr = GetStringSplitArr(strArr, ',', '，', ' ');
            var tmpIntArr = new int[tmpStrArr.Length];
            for (var i = 0; i < tmpIntArr.Length; i++)
            {
                var v = int.Parse(tmpStrArr[i]);
                tmpIntArr[i] = v > 0 ? v - this.GetFixOutBoolOffset() : v + this.GetFixOutBoolOffset();
            }
            return tmpIntArr;
        }

        private List<int[]> GetOutBoolList(string cStr)
        {
            var tmpStrArr = GetStringSplitArr(cStr, '|');

            return tmpStrArr.Select(GetStringToOutIntArr).ToList();
        }

        /// <summary>
        /// 消息获取
        /// </summary>
        private void ReceiveMsg()
        {
            if (m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                ClearData();
                return;
            }

            if (BoolList[IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]] &&
                !OpenFunBtnCtcs300T.MsgCatchList.Contains(
                    IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]))
            {
                AddMessage(IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]);
                return;
            }
            if (!BoolList[IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]] &&
                OpenFunBtnCtcs300T.MsgCatchList.Contains(
                    IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]))
            {
                DeleteMessage(IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb驾驶室未激活]);
            }

            foreach (var key in m_MsgDict.Keys)
            {
                var has = key >= LogicInterface.VirOutBoolStartIndex
                    ? OutBoolList[key - this.GetFixOutBoolOffset()]
                    : BoolList[key];
                //获取新消息
                if (has && !OpenFunBtnCtcs300T.MsgCatchList.Contains(key))
                {
                    AddMessage(key);
                }
                //某条消息结束
                else if (!has && OpenFunBtnCtcs300T.MsgCatchList.Contains(key))
                {
                    DeleteMessage(key);
                }
            }
        }

        private void DeleteMessage(int key)
        {
            var msgList = OpenFunBtnCtcs300T.MsgHandlerATPList;
            OpenFunBtnCtcs300T.MsgCatchList.Remove(key);

            if (msgList.SpecialMsgList.Count == 1 &&
                !msgList.SpecialMsgList[0].Jumped && msgList.SpecialMsgList[0].MsgLogicID == key)
            {
                OpenFunBtnCtcs300T.ClearButtonOutline();
                msgList.SpecialMsgList[0].Jumped = true;
                msgList.MsgOver(msgList.SpecialMsgList[0].MsgLogicID, CurrentTime);
                OpenFunBtnCtcs300T.MenuID = FunMenuButtonId.未选择;
            }
            else if (msgList.SpecialMsgList.Count > 1)
            {
                OpenFunBtnCtcs300T.ClearButtonOutline();
                OpenFunBtnCtcs300T.MenuID = (FunMenuButtonId)msgList.SpecialMsgList[1].MsgStartThenJumpView;
                msgList.SpecialMsgList[0].Jumped = true;
                msgList.MsgOver(msgList.SpecialMsgList[0].MsgLogicID, CurrentTime);
            }
            msgList.MsgOver(key, CurrentTime);
        }

        private void AddMessage(int key)
        {
            var msgList = OpenFunBtnCtcs300T.MsgHandlerATPList;
            OpenFunBtnCtcs300T.MsgCatchList.Add(key);
            var msgTmp = new Message
            {
                MsgLogicID = key,
                MsgID = m_MsgDict[key][1],
                MsgContent = m_MsgDict[key][2],
                TheMsgLevel = (MsgLevels)Enum.ToObject(typeof(MsgLevels), (Convert.ToInt32(m_MsgDict[key][3]))),
                MsgStartThenJumpView = Convert.ToInt32(m_MsgDict[key][4]),
                AbnormalMsg = Convert.ToInt32(m_MsgDict[key][5]) == 1,
                TheSpecialHanding =
                    (SpecialHanding)Enum.ToObject(typeof(SpecialHanding), (Convert.ToInt32(m_MsgDict[key][6]))),
                SendValueToCpu = GetOutBoolList(m_MsgDict[key][7]),
                MsgOverThenJumpViewArr = GetStringToIntArr(m_MsgDict[key][8]),
                BeSure = false,
                MsgReceiveTime = CurrentTime
            };

            if (msgTmp.MsgStartThenJumpView > 0 &&
                OpenFunBtnCtcs300T.StateProvider.BtnStrDict.ContainsKey(msgTmp.MsgStartThenJumpView))
            {
                OpenFunBtnCtcs300T.MenuID = (FunMenuButtonId)msgTmp.MsgStartThenJumpView;
            }

            msgList.AddNewMsg(msgTmp);
            msgList.CurrentMsgListSort(SortCriteriaOfMsg.TimeDown);
        }

        /// <summary>
        /// 清除发送数据
        /// </summary>
        private void ClearSsendData()
        {
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub请求手动进入C3模式), 0, 0);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubRBC输入确认), 0, 0);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub电话号码发送确认), 0, 0);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubRBC输入取消), 0, 0);
        }

        private void UpdateValue()
        {
            UpdateCurrentTime();

            ClearSsendData();

            if (ATPButtonScreen.BtnUpState != null)
            {
                OpenFunBtnCtcs300T.ButtonStateChange(ATPButtonScreen.BtnUpState, MouseState.MouseDown);
            }

            ReceiveMsg();

            OpenFunBtnCtcs300T.Update(this);

            m_Vtrain = GetProportionValue(Math.Abs(GetInFloatValue(InFloatKeys.列车速度)));
            m_Vperm = GetProportionValue(Math.Abs(GetInFloatValue(InFloatKeys.允许速度)));
            m_Vtarget = GetProportionValue(Math.Abs(GetInFloatValue(InFloatKeys.目标速度)));
            m_Vint = GetProportionValue(Math.Abs(GetInFloatValue(InFloatKeys.干预速度)));
            m_Vrelease = GetProportionValue(Math.Abs(GetInFloatValue(InFloatKeys.开口速度)));

            m_TarDistance = GetProportionValue(GetInFloatValue(InFloatKeys.目标距离));

            m_ShowPlanningArea = false;
            if ((BoolList[GetInBoolIndex(InBoolKeys.Inb完全)] || BoolList[GetInBoolIndex(InBoolKeys.Inb引导)])) //完全和引导模式;
            {
                m_ShowPlanningArea = true;
                if (ATPButtonScreen.BtnUpState != null)
                {
                    if (ATPButtonScreen.BtnUpState.BtnId == 15 && m_RangeScaleMode < 2)
                    {
                        m_RangeScaleMode++;
                        m_TheMrsp.SpeedAreaScale = (MostSpeedAreaScale)m_RangeScaleMode;
                        ATPButtonScreen.BtnUpState = null;
                    }
                    else if (ATPButtonScreen.BtnUpState.BtnId == 16 && m_RangeScaleMode > 0)
                    {
                        m_RangeScaleMode--;
                        m_TheMrsp.SpeedAreaScale = (MostSpeedAreaScale)m_RangeScaleMode;
                        ATPButtonScreen.BtnUpState = null;
                    }
                }
            }

            //信号模式
            for (var i = 0; i < 7; i++)
            {
                if (BoolList[SignalTypeIndexs[i]])
                {
                    CurrentSignalSystem = (SignalSystem)i;
                    break;
                }
                CurrentSignalSystem = SignalSystem.无信号;
            }

            UpdateSpeedMonitorModel();

            UpdateControlModel();

            UpdatePointerBrush();
        }

        private int[] SignalTypeIndexs
        {
            get
            {
                return new[]
                {
                    GetInBoolIndex(InBoolKeys.InbCTCS0),
                    GetInBoolIndex(InBoolKeys.InbCTCS1),
                    GetInBoolIndex(InBoolKeys.InbTVM430),
                    GetInBoolIndex(InBoolKeys.InbTVM300),
                    GetInBoolIndex(InBoolKeys.InbCTCS2),
                    GetInBoolIndex(InBoolKeys.InbCTCS3),
                    GetInBoolIndex(InBoolKeys.InbCTCS3D),
                };
            }
        }

        private void UpdateSpeedMonitorModel()
        {
            for (var i = 0; i < 3; i++)
            {
                if (BoolList[SpMonitorIndexs[i]])
                {
                    m_TheControlMode = (ControlMode)i;
                    break;
                }
                m_TheControlMode = ControlMode.Other;
            }
        }

        private int[] SpMonitorIndexs
        {
            get
            {
                return new[]
                {
                    GetInBoolIndex(InBoolKeys.Inb顶棚速度监视区CSM),
                    GetInBoolIndex(InBoolKeys.Inb目标速度监视区TSM),
                    GetInBoolIndex(InBoolKeys.Inb开口速度监视区RSM),
                };
            }
        }

        /// <summary>
        /// 模式
        /// </summary>
        private void UpdateControlModel()
        {

            for (var index = 0; index < 11; index++)
            {
                if (BoolList[ControlModelIndexs[index]])
                {
                    CurrentTrainMode = (TrainMode)index;
                    break;
                }
                CurrentTrainMode = TrainMode.无;
            }
        }

        private int[] ControlModelIndexs
        {
            get
            {
                return new[]
                {
                    GetInBoolIndex(InBoolKeys.Inb完全),
                    GetInBoolIndex(InBoolKeys.Inb部分),
                    GetInBoolIndex(InBoolKeys.Inb目视),
                    GetInBoolIndex(InBoolKeys.Inb引导),
                    GetInBoolIndex(InBoolKeys.Inb调车),
                    GetInBoolIndex(InBoolKeys.Inb待机),
                    GetInBoolIndex(InBoolKeys.Inb隔离),
                    GetInBoolIndex(InBoolKeys.Inb机信),
                    GetInBoolIndex(InBoolKeys.Inb休眠),
                    GetInBoolIndex(InBoolKeys.Inb冒进),
                    GetInBoolIndex(InBoolKeys.Inb冒后),
                };
            }
        }

        private void UpdateCurrentTime()
        {
            m_CurrentTime = CurrentTime;
        }

        public string[] GetStringSplitArr(string cStr, params char[] separator)
        {
            var split = cStr.Split(separator);

            return split;
        }


        #region :::::::::::::::::::::::::::::: 制动预警时间模块 ::::::::::::::::::::::::::::::::::::::

        /*
             * 制动预警图标4种尺寸：
             * 制动预警时间大于等于8s显示最小图标；
             * 大于4s并小于8s显示1/2；
             * 小于等于4s并大于0s显示3/4；
             * 等于0s时显示全部
             * 
             * 显示条件与显示颜色对应关系见表(Tint 制动预警时间，Tsquare 设定时间值)
             * ---------------------------------------------------------------------------------
             *    控制模式   |         运行状态          |  Tint<Tsquare时是否显示  |   颜色   
             * --------------|---------------------------|--------------------------|-----------
             *    |          |      Vtrain ≤ Vperm      |  显示，根据Tint逐渐变大  |   灰色
             *    |          |---------------------------|--------------------------|-----------
             *    |   CSM    |    Vperm < Vtrain ≤Vint  |  显示，根据Tint逐渐变大  |   橙色
             *    |          |---------------------------|--------------------------|-----------
             *    |          |       Vtrain > Vint       |  显示，全尺寸显示        |   红色
             *    |----------|---------------------------|--------------------------|-----------
             *    |          |      Vtrain ≤ Vperm      |  显示，根据Tint逐渐变大  |   黄色
             *    |          |---------------------------|--------------------------|-----------
             * FS |   TSM    |    Vperm < Vtrain ≤Vint  |  显示，根据Tint逐渐变大  |   橙色
             *    |          |---------------------------|--------------------------|-----------
             *    |          |        Vtrain > Vint      |  显示，全尺寸显示        |   红色
             *    |----------|---------------------------|--------------------------|-----------
             *    |  RSM     |      Vtrain ≤ Vrelease   |  显示，根据Tint逐渐变大  |   黄色
             *    |          |---------------------------|--------------------------|-----------
             *    |          |       Vtrain > Vrelease   |  显示，全尺寸显示        |   红色
             * --------------|---------------------------|--------------------------|-----------
             *    其它模式   |       所有状态            |         不显示           |  未使用  
             * ---------------------------------------------------------------------------------
             * Vtrain:当前速度      Vperm:允许速度;    Vint:干预速度;    Vrelease:开口速度;     Vtarget:目标速度
             */

        /* 
         * 当列车处于顶棚速度监视区(CSM)时
         * 如果制动预警时间大于设定时间值，DMI在A1区不显示任何图标；
         * 如果制动预警时间小于或等于设定时间值，DMI在A1区按相应比例显示制动预警图标。
         */

        /* 
         * 当列车处于目标速度监视区(TSM)时
         * 如果制动预警时间大于或等于设定时间值，DMI在A1区显示最小制动预警图标（最大图标的10％）；
         * 如果制动预警时间小于设定时间值，DMI在A1区按相应比例显示制动预警图标。
         */

        /// <summary>
        /// 各种模式下运行状态导致的颜色变化
        /// </summary>
        /// <param name="mode"> 尾数为
        /// 0表示黑色，
        /// 1表示灰色，
        /// 2表示黄色，
        /// 3表示橙色，
        /// 4表示红色</param>
        /// <param name="isPointer"></param>
        /// <returns></returns>
        public int ReturnModeAndRunStatus(ControlMode mode, bool isPointer = false)
        {
            switch (mode)
            {
                case ControlMode.CSM:
                    if (m_Vtrain <= m_Vperm)
                    {
                        return 11; //灰色
                    }
                    if (m_Vperm < m_Vtrain && m_Vtrain <= m_Vint)
                    {
                        return 13; //橙色
                    }
                    return m_Vtrain > m_Vint ? 14 : 10;
                case ControlMode.TSM:
                    if (m_Vtrain <= m_Vtarget && isPointer)
                    {
                        return 41; //灰色
                    }
                    if (m_Vtrain <= m_Vperm)
                    {
                        return 22; //黄色
                    }
                    if (m_Vperm < m_Vtrain && m_Vtrain <= m_Vint)
                    {
                        return 23; //橙色
                    }
                    return m_Vtrain > m_Vint ? 24 : 20;
                case ControlMode.RSM:
                    return m_Vtrain <= m_Vrelease ? 32 : 34;
                default: //ControlMode.Other
                    if (m_Vtrain <= m_Vperm)
                    {
                        return 41; //灰色
                    }
                    if (m_Vperm < m_Vtrain && m_Vtrain <= m_Vint)
                    {
                        return 43; //橙色
                    }
                    return m_Vtrain > m_Vint ? 44 : 40;
            }
        }


        private readonly RectangleF m_BreakWarningRectangleF = new RectangleF(0, 0, 0, 0);

        /// <summary>
        /// 制动预警方块大小信息
        /// </summary>
        /// <param name="warningTime">制动预警时间</param>
        /// <param name="modeId">运行状态编号</param>
        /// <returns></returns>
        private RectangleF BreakWarningRect(int warningTime, int modeId)
        {
            if (modeId >= 40) //其他模式
            {
                return m_BreakWarningRectangleF;
            }
            if (modeId == 14 || modeId == 24 || modeId == 34) //红色
            {
                return m_RectsList[0];
            }
            if (m_TheControlMode == ControlMode.TSM && warningTime > Tsquare || warningTime == Tsquare)
            {
                return m_RectsList[3];
            }
            if (warningTime > 4 && warningTime < Tsquare)
            {
                return m_RectsList[2];
            }
            if (warningTime > 0 && warningTime <= 4)
            {
                return m_RectsList[1];
            }
            if (warningTime == 0)
            {
                return m_RectsList[0];
            }
            return m_BreakWarningRectangleF;
        }

        private BreakWarningStatus m_BreakWarningStatus;

        /// <summary>
        /// 获取制动预警时间信息
        /// </summary>
        /// <param name="time">制动预警时间</param>
        /// <returns>结构体BreakWarningStatus信息</returns>
        public BreakWarningStatus GetBreakWarning(int time)
        {
            m_BreakWarningStatus.BreakWarningRect = BreakWarningRect(time, ReturnModeAndRunStatus(m_TheControlMode));
            m_BreakWarningStatus.BreakWarmingBrush = m_WarningBrushDic[ReturnModeAndRunStatus(m_TheControlMode)];
            return m_BreakWarningStatus;
        }

        #endregion

        public static Rectangle GetRec(IList<GDIProgress> lstGdiProgresses)
        {
            var x = lstGdiProgresses[lstGdiProgresses.Count - 1].OutLineRectangle.X;
            var y = lstGdiProgresses[lstGdiProgresses.Count - 1].OutLineRectangle.Y;
            var height = lstGdiProgresses.Sum(t => t.OutLineRectangle.Height);
            var width = lstGdiProgresses[0].OutLineRectangle.Width;
            return new Rectangle(x, y, width, height);
        }

        public void ClearData(DataClearFlag clearFlag = DataClearFlag.All)
        {
            switch (clearFlag)
            {
                case DataClearFlag.All:
                    OpenFunBtnCtcs300T.ClearData();
                    OpenFunBtnCtcs300T.MsgHandlerATPList.ClearAllList();
                    foreach (var i in m_MsgDict.Keys.Where(w => w > LogicInterface.VirOutBoolStartIndex))
                    {
                        append_postCmd(CmdType.SetBoolValue, i - LogicInterface.VirOutBoolStartIndex, 0, 0);
                    }
                    break;
                case DataClearFlag.WaiteForDriver:
                    OpenFunBtnCtcs300T.ClearData();
                    break;
            }
        }

        private void UpdateStateAfterPaint()
        {
            //瞬时量判断
            foreach (var tmp in m_InstantValueDict.Keys.Where(tmp => BoolList[tmp]))
            {
                append_postCmd(CmdType.SetBoolValue, m_InstantValueDict[tmp], 0, 0);
            }

            if (!BoolList[GetInBoolIndex(InBoolKeys.Inb黑屏)] || m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0); //黑屏
            }
        }
    }
}
