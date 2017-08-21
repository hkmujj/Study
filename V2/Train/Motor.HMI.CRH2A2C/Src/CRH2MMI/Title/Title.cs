using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Fault;
using CRH2MMI.Notify;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using CRH2MMI.Resource.Images;


namespace CRH2MMI.Title
{
    /// <summary>
    /// 界面顶部标题信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Title : CRH2BaseClass
    {
        /// <summary>
        /// 标题菜单被点击
        /// </summary>
        public EventHandler<TitleMenuClickArgs> TitleMenuClicking;

        public EventHandler<TitleMenuClickArgs> TitleMenuClicked;

        private TitleResource m_Resource;

        private List<CommonInnerControlBase> m_Infos;
        private bool m_HasFault;
        private TitleShowStrategy m_TitleShowStrategy;

        private Image[] m_Img = new Image[4]; //标题背景图
        private string m_StrtimeY;
        private string m_StrtimeH;

        public string TitleText { set; get; }

        public ViewConfig LastView { get; private set; }

        private ViewConfig m_CurrentView;

        private FaultCollector m_FaultCollector;

        private Image m_RectBKImage;

        /// <summary>
        /// 菜单标题
        /// </summary>
        private GDIButton m_MenuButton;

        private FaultMgr.ICanDeleteFaultGetter m_FaultGetter;

        private FaultMgr.IViewFaultGetter m_ViewFaultGetter;

        private EventHandler TitleShowStrategyChanged;

        private NotifyGetter m_NotifyGetter;

        /// <summary>
        /// 故障发生
        /// </summary>
        private CRH2Button m_FaultOccuse;

        private bool m_HasNotify;
        private bool m_HasPackingBrakeCutEvent;

        public Title()
        {
            LastView = 0;
        }


        public static Title Instance { private set; get; }

        public TitleShowStrategy TitleShowStrategy
        {
            set
            {
                if (value == null)
                {
                    const string msg =
                        "Can not set TitleMenuTextStrategy = null, force let TitleMenuTextStrategy = last value";
                    LogMgr.Error(msg);
                }
                else
                {
                    if (m_TitleShowStrategy != value)
                    {
                        m_TitleShowStrategy = value;
                        HandleUtil.OnHandle(TitleShowStrategyChanged, this, null);
                    }
                }
            }
            get { return m_TitleShowStrategy; }
        }

        public void ResetTitleShowStrategy()
        {
            LogMgr.Debug("Reset the title menu text strategty.");
            TitleShowStrategy = new DefaultTitleShowStrategy();
        }

        public bool HasPackingBrakeCutEvent
        {
            set
            {
                RefreshBKImage();
                if (value == m_HasPackingBrakeCutEvent)
                {
                    return;
                }
                m_HasPackingBrakeCutEvent = value;
                if (value)
                {
                    ActiveFault();
                }
                else
                {
                    PackingBrakeCutManager.Instance.HasAnyNotConfirm = false;
                }
            }
            get { return m_HasPackingBrakeCutEvent; }
        }


        public bool HasNotify
        {
            set
            {
                RefreshBKImage();

                if (value)
                {
                    if (NotifyManager.Instance.HasNewNotify && !m_NotifyGetter.HasResponse)
                    {
                        append_postCmd(CmdType.ChangePage, (int) ViewConfig.Notify, 0, 0);
                        m_NotifyGetter.HasResponse = true;
                    }
                }

                m_HasNotify = value;
            }
            get { return m_HasNotify; }
        }

        private bool HasFault
        {
            set
            {
                RefreshBKImage();
                if (m_HasFault == value)
                {
                    if (value && m_ViewFaultGetter.HasFaultToBeActived)
                    {
                        ActiveFault();
                    }
                    return;
                }
                m_HasFault = value;
                if (value)
                {
                    // 来自蜂鸣器
                    if (!m_FaultGetter.HasFault)
                    {
                        append_postCmd(CmdType.ChangePage, (int) ViewConfig.Notify, 0, 0);
                    }
                    else
                    {
                        ActiveFault();
                    }
                }
            }
            get { return m_HasFault; }
        }

        /// <summary>
        /// 激活故障,显示
        /// </summary>
        private void ActiveFault()
        {
            append_postCmd(CmdType.ChangePage, (int) ViewConfig.FaultInfo, 0, 0);
        }

        private void RefreshBKImage()
        {
            m_RectBKImage = (HasFault || HasNotify || HasPackingBrakeCutEvent) &&
                            TitleShowStrategy.FaultColorVisible()
                ? m_Img[3]
                : m_Img[0];
        }

        public override string GetInfo()
        {
            return "标题";
        }

        public override bool Init()
        {
            Instance = this;
            PackingBrakeCutManager.Instance.PackingBrakeCutChangedEvent += () =>
            {
                HasPackingBrakeCutEvent = PackingBrakeCutManager.Instance.HasAnyNotConfirm;
            };

            m_Img = new Image[]
            {
                ImageResource.title,
                ImageResource.titlered1,
                ImageResource.titlered2,
                ImageResource.title1
            };
            HasFault = false;

            m_Resource = new TitleResource(this);
            m_Resource.Init();

            m_FaultCollector = new FaultCollector();

            m_FaultGetter =
                FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.NotDel) as FaultMgr.ICanDeleteFaultGetter;

            m_ViewFaultGetter =
                FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.ForView) as FaultMgr.IViewFaultGetter;

            GlobalEvent.Instance.RestartEvent += (sender, args) => ResetTitleShowStrategy();

            LogMgr.Debug(string.Format("{0} get notify getter, app name = {1}", GetType(), ProjectName));
            m_NotifyGetter = NotifyManager.Instance.GetOrCreateNotifyGetter(ProjectName);

            ResetTitleShowStrategy();

            InitMenuBtn();

            InitDate();

            InitInfos();

            InitStringInfos();

            InitUIObj();

            this.TitleShowStrategyChanged += OnTitleShowStrategyChanged;

            return true;
        }

        private void OnTitleShowStrategyChanged(object sender, EventArgs args)
        {
            RefreshBKImage();
            m_MenuButton.Text = TitleShowStrategy.GetMenuText();
            TitleText = TitleShowStrategy.GetTitleText(m_CurrentView);
        }

        private void InitMenuBtn()
        {
            m_MenuButton = new GDIButton()
            {
                OutLineRectangle = new Rectangle(688, 10, 95, 38),
                RefreshAction = o =>
                {
                    var btn = (GDIButton) o;
                    btn.Text = TitleShowStrategy.GetMenuText();
                },
                ButtonDownEvent = OnMenuButtonDownEvent
            };
            m_MenuButton.TextControl.BkColor = Color.Blue;
            m_MenuButton.TextControl.TextColor = Color.White;
            m_MenuButton.TextControl.TextFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.LineLimit
            };
            m_MenuButton.TextControl.DrawFont = new Font(CRH2Resource.Font14.FontFamily, 14, FontStyle.Bold);
        }

        private void OnMenuButtonDownEvent(object sender, EventArgs eventArgs)
        {
            HandleUtil.OnHandle(this.TitleMenuClicking, this, new TitleMenuClickArgs());
            var menuType = TitleShowStrategy.GetMenuType();
            switch (menuType)
            {
                case TitleMenuType.Menu:
                    TitleShowStrategy.GotoMenuView(this);
                    break;
                case TitleMenuType.Retrun:
                    TitleShowStrategy.TurnBackView(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            HandleUtil.OnHandle(this.TitleMenuClicked, this, new TitleMenuClickArgs());
        }

        private void InitUIObj()
        {
            var titleConfig = GlobalInfo.CurrentCRH2Config.TitleConfig;
            InitUIObj(titleConfig.TitleUnits.Cast<CRH2CommunicationPortModel>());
        }

        private void InitStringInfos()
        {
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.LineLimit
            };
            var titleConfig = GlobalInfo.CurrentCRH2Config.TitleConfig;
            foreach (var unit in titleConfig.TitleUnits.FindAll(f => f.InFloatColoumNames.Any()))
            {
                TitleUnit tunit = unit;
                var strInfo = new GDIRectText()
                {
                    TextColor = unit.TextColor,
                    DrawFont = new Font(CRH2Resource.Font12.FontFamily, unit.FontWidth),
                    OutLineRectangle = unit.Rectangle,
                    TextFormat = format,
                    BackColorVisible = false,
                };
                if (unit.Name == "线路")
                {
                    strInfo.TextFormat = new StringFormat()
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Center
                    };
                    strInfo.RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        var fValue =
                            FloatList[GetInFloatIndex(tunit.InFloatColoumNames.First())];
                        var iValue = (int) Math.Ceiling(Math.Abs(fValue));
                        var conf = m_Resource.TrainLineConfigs.Find(f => f.Index == iValue);
                        txt.Text = conf != null ? conf.Name : "0";
                    };
                }
                else if (unit.Name.Contains("速度"))
                {
                    strInfo.RefreshAction = o =>
                    {
                        var txt = (GDIRectText)o;
                        var fValue =
                            Math.Abs(
                                FloatList[GetInFloatIndex(tunit.InFloatColoumNames.First())] * GlobalInfo.CurrentCRH2Config.EspecialConfig.SpeedScale);
                        txt.Text = fValue.ToString(tunit.ToStringFormat);
                    };
                }
                else
                {
                    strInfo.RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        var fValue =
                            Math.Abs(
                                FloatList[GetInFloatIndex(tunit.InFloatColoumNames.First())]);
                        txt.Text = fValue.ToString(tunit.ToStringFormat);
                    };
                }
                m_Infos.Add(strInfo);
            }
        }

        private void InitInfos()
        {
            var font = new Font(CRH2Resource.Font12.FontFamily, 11, FontStyle.Bold);
            var format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                FormatFlags = StringFormatFlags.LineLimit
            };
            var titleConfig = GlobalInfo.CurrentCRH2Config.TitleConfig;
            m_Infos = new List<CommonInnerControlBase>();
            foreach (var s in titleConfig.TitleUnits.FindAll(f => f.InBoolColoumNames.Any()))
            {
                var unit = s;
                CommonInnerControlBase gditxt = new GDIRectText()
                {
                    Text = s.Name,
                    TextColor = s.TextColor,
                    BkColor = s.BkColor,
                    OutLineRectangle = s.Rectangle,
                    BKImage = s.BkImgaeIndex != -1 ? m_Img[s.BkImgaeIndex] : null,
                    DrawFont = font,
                    TextFormat = format,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Visible = BoolList[GetInBoolIndex(unit.InBoolColoumNames.First())];
                    }
                };

                if (s.Name == "紧急报警")
                {
                    gditxt.RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Visible = NotifyManager.Instance.HasNotify(NotifyType.Enmagerce);
                    };
                }

                if (s.Name == "火灾报警")
                {
                    gditxt.RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Visible = NotifyManager.Instance.HasNotify(NotifyType.Fire);
                    };
                }

                if (s.Name == "停放制动")
                {
                    var txt = (GDIRectText)gditxt;
                    m_Infos.Remove(gditxt);
                    m_FaultOccuse = new CRH2Button
                    {
                        OutLineRectangle = txt.OutLineRectangle,
                        TextColor = txt.TextColor,
                        Text = txt.Text,
                        BackImage = txt.BKImage,
                        DownImage = txt.BKImage,
                        UpImage = txt.BKImage,
                        RefreshAction = o =>
                        {
                            var b = (GDIButton) o;
                            b.Visible = unit.InBoolColoumNames.Any(GetInBoolValue);
                        },
                        ButtonDownEvent =
                            (sender, args) => append_postCmd(CmdType.ChangePage, (int) ViewConfig.DriveState, 0, 0)
                    };
                }

                if (s.Name == "主故障蜂鸣器切除")
                {
                    gditxt.RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Visible = BoolList[GetInBoolIndex(unit.InBoolColoumNames.First())];
                        //出现故障信息，跳转到通知信息视图
                        m_FaultCollector.AddOrRefresh(txt.Text, txt.Visible);
                        HasFault = m_FaultCollector.HasAnyFault;
                    };
                }
                m_Infos.Add(gditxt);

                if (s.Name == "故障发生")
                {
                    var txt = (GDIRectText) gditxt;
                    m_Infos.Remove(gditxt);
                    m_FaultOccuse = new CRH2Button
                    {
                        OutLineRectangle = txt.OutLineRectangle,
                        TextColor = txt.TextColor,
                        Text = txt.Text,
                        BackImage = txt.BKImage,
                        DownImage = txt.BKImage,
                        UpImage = txt.BKImage,
                        RefreshAction = o =>
                        {
                            var b = (GDIButton) o;
                            b.Visible = m_FaultGetter.HasFault;
                            m_FaultCollector.AddOrRefresh(b.Text, b.Visible);
                            HasFault = m_FaultCollector.HasAnyFault;
                        },
                        ButtonDownEvent =
                            (sender, args) => append_postCmd(CmdType.ChangePage, (int) ViewConfig.FaultInfo, 0, 0)
                    };
                }
            }
        }

        public override void paint(Graphics g)
        {
            RefreshDate();
            OnDraw(g);
        }


        /// <summary>
        /// 设置运行参数 根据参数显示不同标题信息
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB">视图编号</param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_CurrentView = (ViewConfig) nParaB;

                TitleText = TitleShowStrategy.GetTitleText(m_CurrentView);

                LastView = (ViewConfig) Convert.ToInt32(nParaC);
            }
        }

        protected override bool OnMouseDown(Point point)
        {
            if (HasPackingBrakeCutEvent)
            {
                return false;
            }

            if (m_MenuButton.OnMouseDown(point))
            {
                m_MenuButton.CurrentMouseState = MouseState.MouseUp;
                return true;
            }
            return m_FaultOccuse.OnMouseDown(point);
        }



        /// <summary>
        /// 刷新时间
        /// </summary>
        public void RefreshDate()
        {
            //m_FaultCollector.Clear();


            m_StrtimeH = string.Format("  {0,3:# 0}时{1,3:# 0}分{2,3:# 0}秒", CurrentTime.Hour, CurrentTime.Minute,
                CurrentTime.Second);
            m_StrtimeY = string.Format("' {0,3:# 0}年{1,3:# 0}月{2,3:# 0}日", CurrentTime.Year%100, CurrentTime.Month,
                CurrentTime.Day);

            HasNotify = NotifyManager.Instance.HasNotify();

        }

        public void InitDate()
        {
            CRH2Resource.DrawFormat.Alignment = (StringAlignment) 1;
            CRH2Resource.DrawFormat.LineAlignment = (StringAlignment) 1;

            CRH2Resource.RightFormat.Alignment = (StringAlignment) 2;
            CRH2Resource.RightFormat.LineAlignment = (StringAlignment) 1;
        }

        public void OnDraw(Graphics g)
        {
            g.DrawImage(m_RectBKImage, 0, 0, m_RectBKImage.Width, m_RectBKImage.Height);

            m_MenuButton.OnPaint(g);

            g.DrawString(TitleText, CRH2Resource.Font14, CRH2Resource.WhiteBrush, 40, 5);
            g.DrawString(m_StrtimeY, CRH2Resource.Font16, CRH2Resource.WhiteBrush, 11, 60);
            g.DrawString(m_StrtimeH, CRH2Resource.Font16, CRH2Resource.WhiteBrush, 11, 85);

            m_FaultOccuse.OnPaint(g);

            m_Infos.ForEach(e => e.OnPaint(g));
        }
    }
}
