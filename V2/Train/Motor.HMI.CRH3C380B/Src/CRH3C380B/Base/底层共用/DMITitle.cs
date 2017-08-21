using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

using Motor.HMI.CRH3C380B.Base.底层共用.TitleChirldren;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    /// <summary>
    /// 标题类
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMITitle : CRH3C380BBase
    {
        private float[] m_FValue;

        private List<RectangleF> m_RectsList = new List<RectangleF>();

        private readonly bool[] m_TitleAllow = {true, true, true};

        private readonly RectangleF m_TitleNameRegion = new RectangleF(175, 0, 300, 25);

        private Dictionary<int, int> m_Dictionary;

        private ICourseService m_CourseService;

        private LampStateControl m_LampStateControl;

        private string m_TrainLine;

        private int[] m_TrainLineDataIndexs;

        /// <summary>
        /// 是否自动速度
        /// </summary>
        public bool IsASCModel { set; get; }

        /// <summary>
        /// 夜晚模式
        /// </summary>
        public bool NightMode
        {
            set
            {
                var old = m_NightMode;
                m_NightMode = value;
                if (old != value)
                {
                    NotifyNightModelChanged();
                }

            }
            get { return m_NightMode; }
        }

        /// <summary>
        /// 16车编组模式
        /// </summary>
        public bool MarshallMode
        {
            private set
            {
                var old = m_MarshallMode;
                if (value != old)
                {
                    m_MarshallMode = value;
                    OnMarshallModeChanged(this);
                }
            }
            get { return m_MarshallMode; }
        }

        public event Action<DMITitle> MarshallModeChanged;

        public event Action<DMITitle> TrainInsideChanged;

        /// <summary>
        /// 车辆换端
        /// </summary>
        public bool TrainInSide
        {
            private set
            {
                var old = TrainInSide;
                if (old != value)
                {
                    m_TrainInSide = value;
                    OnTrainInsideChanged(this);
                }
            }
            get { return m_TrainInSide; }
        }

        /// <summary>
        /// 当前屏上显示的是哪种MMI
        /// </summary>
        public MMIType CurrentMMIName = MMIType.左侧MMI屏;

        public MMIType MMIType { get; private set; }

        /// <summary>
        /// 下标题中按钮内容
        /// </summary>
        public List<RectOfButtonState> BtnContentList { get; set; }

        /// <summary>
        /// 当前视图ID
        /// </summary>
        public int CurrentViewID { get; private set; }

        private List<BoolCorrespodenceGraphic> m_BoolCorGraphicList;

        /// <summary>
        /// 车次好是否显示字符
        /// </summary>
        private bool m_IsChar;

        public string TitleName { set; get; }

        /// <summary>
        /// 瞬时量字典
        /// </summary>
        private readonly Dictionary<int, int> m_InstantValueDict = new Dictionary<int, int>();

        private DateTime m_LastTime;

        /// <summary>
        /// 列车编号
        /// </summary>
        public static string[] CarIdArr =
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"
        };

        /// <summary>
        /// 车辆编号
        /// </summary>
        private readonly List<string[]> m_CarIdList = new List<string[]>
        {
            new[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16"
            },

            new[]
            {
                "8",
                "7",
                "6",
                "5",
                "4",
                "3",
                "2",
                "1",
                "16",
                "15",
                "14",
                "13",
                "12",
                "11",
                "10",
                "9",
            },

            new[]
            {
                "16",
                "15",
                "14",
                "13",
                "12",
                "11",
                "10",
                "9",
                "8",
                "7",
                "6",
                "5",
                "4",
                "3",
                "2",
                "1",
            }
        };

        private bool m_NightMode;
        private bool m_MarshallMode;

        public event Action<DMITitle> NightModelChanged;


        private readonly bool[] m_AscState = new bool[2];

        private readonly Image[] m_TitleImages =
        {
            TitleImages.Title_1_0,
            TitleImages.Title_2_0,
            TitleImages.Title_3_0,
            TitleImages.Title_1_1,
            TitleImages.Title_2_1,
            TitleImages.Title_3_1,

        };

        private bool m_TrainInSide;

        public DMITitle()
        {
            NightMode = false;
            BtnContentList = new List<RectOfButtonState>(10);
        }

        protected void NotifyNightModelChanged()
        {
            Action<DMITitle> handler = NightModelChanged;
            if (handler != null)
            {
                handler(this);
            }
        }

        private void OnMarshallModeChanged(DMITitle obj)
        {
            Action<DMITitle> handler = MarshallModeChanged;
            if (handler != null)
            {
                handler(obj);
            }
        }

        //2
        public override string GetInfo()
        {
            return "DMI标题";
        }

        //6
        public override bool Initalize()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();

            m_TrainLineDataIndexs = new[]
            {
                GetOutFloatIndex("车次号0位"),
                GetOutFloatIndex("车次号1位")
            };
            InitData();
            CurrentMMIName = RecPath.Contains("1D") ? MMIType.左侧MMI屏 : MMIType.右侧MMI屏;
            MMIType = CurrentMMIName;
            m_LampStateControl = new LampStateControl(this) {OutLineRectangle = Rectangle.Ceiling(m_RectsList[13])};

            UIObj.InFloatList.AddRange(m_TrainLineDataIndexs);

            return true;
        }


        public override void Paint(Graphics g)
        {
            m_AscState[0] = GetInBoolValue(InBoolKeys.Inb信息状态条ASC设置0为关闭1为开启);

            GetValue();

            RefreshData();

            Draw(g);
        }

        private void RefreshData()
        {
            var char1 = ((char) OutFloatList[m_TrainLineDataIndexs[0]]);
            string trainNoHead;
            if (m_IsChar)
            {
                if (char1 < 65 || char1 > 90)
                {
                    return;
                }

                trainNoHead = char1.ToString();
            }
            else
            {
                trainNoHead = char1.Equals('D') ? "8" : char1.Equals('G') ? "6" : "";
                if (string.IsNullOrEmpty(trainNoHead))
                {
                    return;
                }
            }


            var trainNoBody = OutFloatList[m_TrainLineDataIndexs[1]].ToString("0");

            m_TrainLine = trainNoHead + trainNoBody;

        }

        /// <summary>
        /// 计算对象
        /// </summary>
        public override void computValue()
        {
            //瞬时量判断
            foreach (int tmp in m_InstantValueDict.Keys.Where(tmp => BoolList[tmp]))
            {
                append_postCmd(CmdType.SetBoolValue, m_InstantValueDict[tmp], 0, 0);
            }

            var flag = IsLeftScreen ? GetInBoolAt(InBoolKeys.Inb非制动屏黑屏) : GetInBoolAt(InBoolKeys.Inb制动屏黑屏);
            //黑屏
            if (!flag ||
                m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }

            TrainInSide = GetInBoolValue(InBoolKeys.Inb换端标志);
            MarshallMode = GetInBoolValue(InBoolKeys.Inb16编接口);
        }

        public void Draw(Graphics g)
        {
            PaintGroundImage(g);

            PaintValue(g);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                if (TrainInSide)
                {

                }

                CurrentViewID = nParaB;
                CarIdArr = null;
                if (MarshallMode && TrainInSide)
                {
                    CarIdArr = m_CarIdList[2];
                }
                else if (!MarshallMode && TrainInSide)
                {
                    CarIdArr = m_CarIdList[1];
                }
                else
                {
                    CarIdArr = m_CarIdList[0];
                }

                TitleName = string.Empty;

                UpdateBtnContent();
            }
        }

        private void GetValue()
        {
            //for (int i = 0; i < m_OutbValue.Length; i++)
            //{
            //    if (UIObj.OutBoolList[i] < 4800)
            //    {
            //        m_OutbValue[i] = BoolList[UIObj.OutBoolList[i]];
            //    }
            //    else
            //    {
            //        m_OutbValue[i] = OutBoolList[UIObj.OutBoolList[i]];
            //    }
            //}
            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }

            foreach (BoolCorrespodenceGraphic tmp in m_BoolCorGraphicList)
            {
                tmp.StateChange();
            }

            DmiParkingBrakes.RefreshTag(true);
            if (!BoolOldList[GetInBoolIndex("自动切换到ASC速度设定为2km/h的连挂界面")] &&
                BoolList[GetInBoolIndex("自动切换到ASC速度设定为2km/h的连挂界面")] &&
                CurrentMMIName == MMIType.左侧MMI屏)
            {
                append_postCmd(CmdType.ChangePage, 126, 0, 0);
            }
        }


        private void PaintValue(Graphics g)
        {
            #region :::::::::::::::::: 头标题 :::::::::::::::::::::::::::::


            g.DrawString(CurrentTime.ToString("yy.MM.dd"),
                FontsItems.FontE12,
                NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[5],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawString(CurrentTime.ToString("HH:mm:ss"),
                FontsItems.FontE12,
                NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[6],
                FontsItems.TheAlignment(FontRelated.居中));

            #endregion

            #region :::::::::::::::::: 中标题 :::::::::::::::::::::::::::::

            //ASC
            //e.DrawImage(_bValue[0] ? _imgsList[7] : _imgsList[6], _rectsList[7]);
            m_BoolCorGraphicList[0].Draw(g);
            g.DrawString(
                GetInBoolValue(InBoolKeys.Inb信息状态条ASC设置0为关闭1为开启)
                    ? Convert.ToInt32(m_FValue[0]).ToString(CultureInfo.InvariantCulture)
                    : "---",
                FontsItems.FontE12,
                NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_RectsList[8],
                FontsItems.TheAlignment(FontRelated.居中));
            //整备运行
            if (GetInBoolValue(InBoolKeys.Inb信息状态条整备运行激活))
            {
                g.DrawImage(TitleImages.StatusBar2, m_RectsList[9]);
            }
            //主断
            if (GetInBoolValue(InBoolKeys.Inb信息状态条至少一个主断路器能够闭合))
            {
                g.DrawImage(TitleImages.StatusBar3, m_RectsList[10]); //至少1台主断能被闭合
            }
            else if (GetInBoolValue(InBoolKeys.Inb信息状态条全部主断路器已闭合))
            {
                g.DrawImage(TitleImages.StatusBar4, m_RectsList[10]); //所有主断都断开
            }
            //0位
            if (GetInBoolValue(InBoolKeys.Inb信息状态条制动手柄应置于0位))
            {
                g.DrawImage(TitleImages.StatusBar5, m_RectsList[11]); //制动手柄
            }
            else if (GetInBoolValue(InBoolKeys.Inb信息状态条牵引手柄应置于0位))
            {
                g.DrawImage(TitleImages.StatusBar6, m_RectsList[11]); //牵引手柄
            }
            else if (GetInBoolValue(InBoolKeys.Inb信息状态条运行速度设定应置于0位))
            {
                g.DrawImage(TitleImages.StatusBar7, m_RectsList[11]); //运行速度
            }
            //列车就绪
            if (GetInBoolValue(InBoolKeys.Inb信息状态条列车已就绪))
            {
                g.DrawImage(TitleImages.StatusBar8, m_RectsList[12]);
            }
            // 灯状态
            DrawLampState(g);

            //列车管充风已被切断--------------(速度限制还没找到，找到再改)
            if (BoolList[m_Dictionary[1]])
            {
                g.DrawImage(CommonImages.StateUnkown, m_RectsList[14]);
            }
            else if (GetInBoolValue(InBoolKeys.Inb信息状态条列车管充风已被切断))
            {
                g.DrawImage(TitleImages.StatusBar10, m_RectsList[14]);
            }

            //时速限制
            if (GetInBoolValue(InBoolKeys.Inb信息状态条时速限制_公里小时))
            {
                g.FillRectangle(SolidBrushsItems.YellowBrush, m_RectsList[14]);
                g.DrawString("最高\n时速",
                    FontsItems.FontC10B,
                    SolidBrushsItems.BlackBrush,
                    m_RectsList[14],
                    FontsItems.TheAlignment(FontRelated.居中));

                g.FillRectangle(SolidBrushsItems.YellowBrush, m_RectsList[15]);
                g.DrawString(Convert.ToInt32(m_FValue[1]).ToString(CultureInfo.InvariantCulture),
                    FontsItems.FontC10B,
                    SolidBrushsItems.BlackBrush,
                    m_RectsList[15],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            //至少1们仍开启
            if (GetInBoolValue(InBoolKeys.Inb制动测试1375) && CompareTime.Compare(CurrentTime, m_LastTime, 1))
            {
                m_LastTime = CurrentTime;
                g.DrawImage(TitleImages.StatusBar11, m_RectsList[15]);
            }
            else if (GetInBoolValue(InBoolKeys.Inb信息状态条至少一门仍开启))
            {
                g.DrawImage(TitleImages.StatusBar11, m_RectsList[15]);
            }

            //自动安全装置
            if (GetInBoolValue(InBoolKeys.Inb信息状态条自动安全装置))
            {
                g.DrawImage(TitleImages.StatusBar12, m_RectsList[16]);
            }
            //司机室变更模式
            if (GetInBoolValue(InBoolKeys.Inb信息状态条司机室变更模式))
            {
                g.DrawImage(TitleImages.StatusBar13, m_RectsList[17]);
            }
            //与门通讯故障
            if (GetInBoolValue(InBoolKeys.Inb信息状态条与门通讯故障))
            {
                g.DrawImage(TitleImages.StatusBar14, m_RectsList[18]);
            }
            //火警(闪烁)
            if (GetInBoolValue(InBoolKeys.Inb信息状态条火警闪烁) && CurrentTime.Second%2 == 0)
            {
                g.DrawImage(TitleImages.StatusBar15, m_RectsList[19]);
            }
            //司机室门已释放，可操作
            if (GetInBoolValue(InBoolKeys.Inb信息状态条司机室门已释放可操作))
            {
                g.DrawImage(TitleImages.StatusBar16, m_RectsList[20]);
            }

            #endregion

            #region :::::::::::::::::: 下标题 :::::::::::::::::::::::::::::

            g.FillRectangle(DMITitle.NightMode ? SolidBrushsItems.Grey3 : SolidBrushsItems.BlackBrush,
                m_RectsList[28]);

            for (int i = 0; i < 10; i++)
            {
                BtnContentList[i].Draw(this, g, DMIButton.BtnDownList.Count != 0 && DMIButton.BtnDownList[0] - 6 == i);
            }

            #endregion
        }

        public override void Clear()
        {
            IsASCModel = false;
        }

        private void DrawLampState(Graphics g)
        {
            m_LampStateControl.OnPaint(g);
        }

        private void PaintGroundImage(Graphics g)
        {
            g.DrawString(TitleName,
                FontsItems.FontC11,
                DMITitle.NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                m_TitleNameRegion,
                FontsItems.TheAlignment(FontRelated.靠左));


            if (GetOutBoolValue(OutBoolKeys.Oub维护0为关闭1为开启))
            {
                g.FillRectangle(SolidBrushsItems.RedBrush1, m_RectsList[3]);
                g.DrawString("维护",
                    FontsItems.FontC11,
                    SolidBrushsItems.WhiteBrush,
                    m_RectsList[3],
                    FontsItems.TheAlignment(FontRelated.居中));
            }
            else
            {
                g.DrawString("列车:   " + m_TrainLine,
                    FontsItems.FontC11,
                    NightMode ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    m_RectsList[3],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }

            for (int i = 0; i < 3; i++)
            {
                if (m_TitleAllow[i])
                {
                    g.DrawImage(NightMode ? m_TitleImages[3 + i] : m_TitleImages[i], m_RectsList[i]);
                }
            }
        }

        public void UpdateBtnContent(IList<string> contents = null)
        {
            if (contents == null)
            {
                foreach (RectOfButtonState btn in BtnContentList)
                {
                    btn.BtnStr = string.Empty;
                }

                return;
            }

            var cnt = Math.Min(contents.Count, BtnContentList.Count);
            for (int i = 0; i < cnt; i++)
            {
                BtnContentList[i].BtnStr = contents[i];
            }
        }

        private void InitData()
        {
            m_FValue = new float[UIObj.InFloatList.Count];
            m_Dictionary = new Dictionary<int, int> {{1, GetInBoolIndex("列车管充风已被切断状态未知（信息状态栏）")}};
            m_IsChar =
                GlobalParam.Instance.TrainDiffConfig.AllTrainConfig.Find(f => f.Type == GlobalParam.Instance.ProjectType)
                    .IsChar;

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMITitle, ref m_RectsList))
            {
                for (int i = 0; i < 10; i++)
                {
                    m_RectsList.Add(m_RectsList[i + 21]);
                }
            }

            m_BoolCorGraphicList = new List<BoolCorrespodenceGraphic>
            {
                //ASC
                new BoolCorrespodenceGraphic(m_AscState, 0, 1,
                    new PelObject[]
                    {
                        new PelObjectBrushOrImage(TitleImages.StatusBar0, m_RectsList[7], null),
                        new PelObjectBrushOrImage(TitleImages.StatusBar1, m_RectsList[7], null)
                    })
            };

            //整备

            for (int i = 0; i < 10; i++)
            {
                BtnContentList.Add(new RectOfButtonState(m_RectsList[22 + i], string.Empty));
            }


            var startIn = IndexDescriptionConfig.InBoolDescriptionDictionary[InBoolKeys.Inb受电弓2隔离];
            var startOut = IndexDescriptionConfig.OutBoolDescriptionDictionary[OutBoolKeys.Oub受电弓1隔离];
            for (int i = 0; i < 80; ++i)
            {
                m_InstantValueDict.Add(startIn + i, startOut + i);
            }


            var t = m_InstantValueDict.FirstOrDefault(
                f => f.Value == IndexDescriptionConfig.OutBoolDescriptionDictionary[OutBoolKeys.Oub制动试验间接试验成功]);
            m_InstantValueDict.Remove(t.Key);
        }

        protected virtual void OnTrainInsideChanged(DMITitle obj)
        {
            if (TrainInsideChanged != null)
            {
                TrainInsideChanged.Invoke(obj);
            }
        }
    }
}