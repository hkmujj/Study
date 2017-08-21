using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Bottom;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Util;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Title;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;


namespace CRH2MMI.TelentControl
{
    /// <summary>
    /// 远程控制切除
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Telecontr : CRH2BaseClass
    {

        public static bool SelfLocked { set; get; }

        private ICourseService m_CourseService;

        /// <summary>
        /// 背景色的区域 
        /// </summary>
        private List<Rectangle> m_BkRectangleList;

        // ReSharper disable once InconsistentNaming
        private static readonly SolidBrush m_BkRegionBrush = CRH2Resource.WWBrush;

        /// <summary>
        /// 单元
        /// </summary>
        private List<CRH2Button> m_UnitBtns;

        /// <summary>
        /// 所有的控制 = m_AcceptEleBtns + m_VCBBtns + m_ACK2Btns + m_TransformerBtns + m_CompressorBtns
        /// </summary>
        private List<CRH2Button> m_AllCtrlBtns;

        /// <summary>
        /// 选中的单元
        /// </summary>
        private CRH2Button m_SelectedUnitBtn;

        /// <summary>
        /// 选中的控制
        /// </summary>
        private CRH2Button m_SelectedContrlBtn;

        private List<CommonInnerControlBase> m_ConstInfo;

        private TelentCtrlResource m_TelentCtrlResource;
        private TrainView m_TrainView;

        private static readonly Size CtlBtnSize = new Size(100, 60);
        private CommonSetBtn m_CommonSetBtn;
        private Title.Title m_Title;

        private SuggestiveInformation m_SuggestiveInformation;

        private int m_cout;

        public override void paint(Graphics g)
        {

            m_BkRectangleList.ForEach(e => g.FillRectangle(m_BkRegionBrush, e));

            m_TrainView.paint(g);

            m_UnitBtns.ForEach(e => e.OnDraw(g));

            m_AllCtrlBtns.ForEach(e => e.OnDraw(g));

            m_ConstInfo.ForEach(e => e.OnDraw(g));

            m_CommonSetBtn.OnDraw(g);
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            if (m_SuggestiveInformation != null)
            {
                m_SuggestiveInformation.ClearInformation();
                m_SuggestiveInformation = null;
            }
            if (m_UnitBtns.Any(a => a.OnMouseDown(nPoint)))
            {
                return true;
            }

            if (m_AllCtrlBtns.Any(a => a.OnMouseDown(nPoint)))
            {
                return true;
            }

            return m_CommonSetBtn.OnMouseDown(nPoint);
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_CommonSetBtn.OnMouseUp(point);
        }

        public override bool Init()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
            m_CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
            GlobalEvent.Instance.ShutdownEvent += ShutdownEvent;
            m_TrainView = TrainView.Instance;
            m_Title = Title.Title.Instance;

            m_CommonSetBtn = new CommonSetBtn
            {
                SetButtonDown = OnSetButtonDown,
                SetButtonUp = OnSetButtonUp,
            };
            m_TelentCtrlResource = new TelentCtrlResource(this);

            InitUIObj();

            InitBackgroud();

            InitBtns();

            InitConstInfo();

            GlobalEvent.Instance.ReversalChanged += OnReversalChanged;

            return true;
        }

        private void ShutdownEvent(object sender, EventArgs eventArgs)
        {
            SelfLocked = false;
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                SelfLocked = false;
            }
        }

        private void OnSetButtonUp(object sender, EventArgs e)
        {
            if (CanTelentState())
            {
                SetTelentState(0);
                if (!m_SelectedContrlBtn.Text.Contains("ACK"))
                {
                    append_postCmd(CmdType.ChangePage, (int) ViewConfig.RemovalState, 1, 0);

                }
                else
                {
                    append_postCmd(CmdType.ChangePage, (int) ViewConfig.PowerClassfy, 1, 0);
                }
                SelfLocked = false;
                m_Title.TitleShowStrategy = new TeleCtrTitleMenuShowStrategy() {MenuType = TitleMenuType.Retrun};
            }
            else
            {
                CancelSet();
            }
        }

        private void CancelSet()
        {
            ReselectUnit(null);
            ReselectControl(null);
            m_SuggestiveInformation = this.GetSameProjectObjcect<SuggestiveInformation>();
            m_SuggestiveInformation.UpdateInformation(new InformationModel("输入错误，请重新设定")
            {
                InformationType = InformationType.Error,
                Location = InformationLocation.Up
            });
        }

        private void OnSetButtonDown(object sender, EventArgs e)
        {
            if (CanTelentState())
            {
                SetTelentState(1);
            }
        }

        private bool CanTelentState()
        {
            return m_SelectedUnitBtn != null && m_SelectedContrlBtn != null;
        }

        private void SetTelentState(int value)
        {
            var key = new TeleSendKeyModel()
            {
                Type = (TeleControlBtnType) m_SelectedContrlBtn.Tag,
                Unit = (int) m_SelectedUnitBtn.Tag
            };

            m_TelentCtrlResource.SetTelentState(key, value);

        }

        private void InitUIObj()
        {
            var models = GlobalInfo.CurrentCRH2Config.TeleControlConfig.TeleOutBoolConfigs;
            InitUIObj(models);
        }

        private void InitBackgroud()
        {
            m_BkRectangleList = new List<Rectangle>() {new Rectangle(0, 230, 800, 80), new Rectangle(0, 320, 800, 170)};
        }

        private void InitConstInfo()
        {
            m_ConstInfo = new List<CommonInnerControlBase>()
            {

                new GDIRectText()
                {
                    OutLineRectangle =
                        new Rectangle(m_BkRectangleList[0].Left + 5, m_BkRectangleList[0].Top + 5, 100, 18),
                    TextColor = Color.Black,
                    BkColor = Color.FromArgb(0, 255, 255),
                    Text = "单元选择",
                    NeedDarwOutline = false,
                    Bold = true,
                },

                new GDIRectText()
                {
                    OutLineRectangle =
                        new Rectangle(m_BkRectangleList[1].Left + 5, m_BkRectangleList[1].Top + 10, 200, 18),
                    TextColor = Color.Black,
                    BkColor = Color.FromArgb(0, 255, 255),
                    Text = "远程控件切除选择",
                    NeedDarwOutline = false,
                    Bold = true,
                },
            };

            if (GlobalInfo.CurrentCRH2Config.Type == CRH2Type.CRH2A)
            {
                m_ConstInfo.Add(
                    new GDIRectText()
                    {
                        OutLineRectangle =
                            new Rectangle(50 + 2*100, m_BkRectangleList[1].Top + 15, CtlBtnSize.Width, 18),
                        TextColor = Color.Black,
                        BkColor = Color.FromArgb(0, 255, 255),
                        Text = " ┌─ T 车 ─┐",
                        NeedDarwOutline = false,
                        Bold = true,
                    });

                m_ConstInfo.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(50 + 3*100, m_BkRectangleList[1].Top + 15, CtlBtnSize.Width, 18),
                    TextColor = Color.Black,
                    BkColor = Color.FromArgb(0, 255, 255),
                    Text = " ┌─ M 车─┐",
                    NeedDarwOutline = false,
                    Bold = true,
                });
            }
            else if (GlobalInfo.CurrentCRH2Config.Type == CRH2Type.CRH2B)
            {
                m_ConstInfo.Add(
                    new GDIRectText()
                    {
                        OutLineRectangle = new Rectangle(20 + 3*90, m_BkRectangleList[1].Top + 15, 90, 18),
                        TextColor = Color.Black,
                        BkColor = Color.FromArgb(0, 255, 255),
                        Text = "┌─ T 车─┐",
                        NeedDarwOutline = false,
                        Bold = true,
                    });

                m_ConstInfo.Add(new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(20 + 4*90, m_BkRectangleList[1].Top + 15, 95, 18),
                    TextColor = Color.Black,
                    BkColor = Color.FromArgb(0, 255, 255),
                    Text = "┌─ M 车─┐",
                    NeedDarwOutline = false,
                    Bold = true,
                });
            }
        }

        private void OnReversalChanged(object sender, EventArgs eventArgs)
        {
            var uCount = m_UnitBtns.Count;
            for (int i = 0; i < m_UnitBtns.Count/2; i++)
            {
                var loc = m_UnitBtns[uCount - 1 - i].Location;
                m_UnitBtns[uCount - 1 - i].Location = m_UnitBtns[i].Location;
                m_UnitBtns[i].Location = loc;
            }
        }

        private void InitBtns()
        {
            var teleConfig = GlobalInfo.CurrentCRH2Config.TeleControlConfig;

            var unitWidths = m_TrainView.GetTrainUnitRectangles();

            var units = teleConfig.TeleOutBoolConfigs.Select(s => s.Unit).Distinct();

            var unitx = teleConfig.TrainViewLocation.X;
            m_UnitBtns = units.Select((s, i) => new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(unitx += (i > 0 ? unitWidths[i - 1].Width + 4 : 0), m_BkRectangleList[0].Top + 25,
                        unitWidths[i].Width + 4, 48),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = OnUnitBtnDown,
                TextColor = Color.White,
                Text = string.Format("{0} U", s),
                Tag = s
            }).ToList();

            //控制
            m_AllCtrlBtns = teleConfig.TeleRectangles.Select(s => new CRH2Button()
            {
                OutLineRectangle = new Rectangle(s.Rectangle.Location, s.Rectangle.Size),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent = (o, args) => OnControlBtnDown(o, args),
                TextColor = Color.White,
                Text = EnumUtil.GetDescription(s.Type).First(),
                Tag = s.Type
            }).ToList();
        }

        private void OnUnitBtnDown(object sender, EventArgs e)
        {
            ReselectUnit(sender as CRH2Button);
        }

        private void ReselectUnit(CRH2Button unitButton)
        {
            if (m_SelectedUnitBtn != null)
            {
                m_SelectedUnitBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedUnitBtn = unitButton;
            if (m_SelectedUnitBtn != null)
            {
                m_SelectedUnitBtn.CurrentMouseState = MouseState.MouseDown;
            }
        }

        private void ReselectControl(CRH2Button controlButton)
        {
            if (m_SelectedContrlBtn != null)
            {
                m_SelectedContrlBtn.CurrentMouseState = MouseState.MouseUp;
            }
            m_SelectedContrlBtn = controlButton;
            if (m_SelectedContrlBtn != null)
            {
                m_SelectedContrlBtn.CurrentMouseState = MouseState.MouseDown;
            }
        }

        private void OnControlBtnDown(object sender, EventArgs e)
        {
            ReselectControl(sender as CRH2Button);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                SelfLocked = true;
                m_Title.TitleMenuClicking += OnTitleMenuClick;

                ReselectControl(null);
                ReselectUnit(null);

                switch (nParaB)
                {
                    case (int) ViewConfig.Telecontr:
                        InitDate();
                        break;
                }

                if ((ViewConfig) Convert.ToInt32(nParaC) == ViewConfig.RemovalState ||
                    (ViewConfig) Convert.ToInt32(nParaC) == ViewConfig.PowerClassfy)
                {
                    m_Title.ResetTitleShowStrategy();
                }

            }
        }

        private void OnTitleMenuClick(object sender, TitleMenuClickArgs titleMenuClickArgs)
        {
            SelfLocked = false;

            m_Title.TitleMenuClicking -= OnTitleMenuClick;
        }

        public override string GetInfo()
        {
            return "远程控制切除";
        }

        public void InitDate()
        {
            m_TrainView.Location =
                GlobalInfo.CurrentCRH2Config.TeleControlConfig.TrainViewLocation.Rectangle.Location;
            m_TrainView.NeedDrawCarName = true;
            m_TrainView.IsUnitNameVisible = true;
        }
    }
}