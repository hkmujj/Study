using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;

namespace Motor.HMI.CRH1A.Alarm.VigilantFault
{
    [GksDataType(DataType.isMMIObjectClass)]
    class VigilantFaultView : CRH1BaseClass
    {
        public static VigilantFaultView Instance { private set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title  { protected set { m_Title.Title = value; } get { return m_Title.Title; } }

        #region 私有字段

        private readonly GT_MenuTitle m_Title = new GT_MenuTitle();

        private const string EventDetailBackGround = "";
        private const string EventDetailXianXiang = "";

        #endregion

        #region 故障信息
        /// <summary>
        /// 当前显示的A类故障
        /// </summary>
        /// <returns></returns>
        protected ExceptionData CurrentExceptionData;

        /// <summary>
        /// 当前故障提示信息
        /// </summary>
        private FaultHelpInfo m_CurrentHelpInfo;

        /// <summary>
        /// 标题文本框
        /// </summary>
        private Rectangle m_TitleRect;

        /// <summary>
        /// 底部区域
        /// </summary>
        private Rectangle m_BottomRect;

        /// <summary>
        /// 事件发生显示文本框
        /// </summary>
        private readonly GDIRectText[] m_EventOccurtInfoTextBoxs = new GDIRectText[4];

        /// <summary>
        /// 事件详细信息文本框
        /// </summary>
        private readonly GDIRectText[] m_EventDetailInfoTextBoxs = new GDIRectText[3];

        /// <summary>
        /// 从哪个视图切换过来的
        /// </summary>
        private int m_ChangeFromView;

        #endregion

        #region  重载方法


        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool Initialize()
        {
            Instance = this;

            InitData();

            return true;
        }

        /// <summary>
        /// 绘制方法(该方法会被定时器按一定的时间间隔重复调用) 
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            RefreshShowData();
            OnDraw(g);
        }

        protected override void NavigateFrom(ViewConfig from)
        {
            m_ChangeFromView = (int)from;
            if (from == ViewConfig.Login || from == ViewConfig.BlackScreen || from == ViewConfig.Start)
            {
                m_ChangeFromView = (int)ViewConfig.MainView;
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 数据初始化方法
        /// </summary>
        private void InitData()
        {
            m_TitleRect = new Rectangle(0, 0, 800, 90);

            m_BottomRect = new Rectangle(0, 510, 800, 90);

            #region 初始化事件发生信息文本框
            for (int index = 0; index < 4; index++)
            {
                m_EventOccurtInfoTextBoxs[index] = new GDIRectText();
                m_EventOccurtInfoTextBoxs[index].SetBkColor(255, 255, 255);
                m_EventOccurtInfoTextBoxs[index].SetTextColor(0, 0, 0);
                m_EventOccurtInfoTextBoxs[index].SetLinePen(210, 210, 210, 1);
                m_EventOccurtInfoTextBoxs[index].Isdrawrectfrm = true;
            }

            m_EventOccurtInfoTextBoxs[0].SetTextRect(m_TitleRect.X + 1, m_TitleRect.Bottom + 3, 90, 30);
            m_EventOccurtInfoTextBoxs[1].SetTextRect(m_EventOccurtInfoTextBoxs[0].OutLineRectangle.Right + 1, m_EventOccurtInfoTextBoxs[0].OutLineRectangle.Y, 180, 30);
            m_EventOccurtInfoTextBoxs[2].SetTextRect(m_EventOccurtInfoTextBoxs[1].OutLineRectangle.Right + 1, m_EventOccurtInfoTextBoxs[1].OutLineRectangle.Y, 180, 30);
            m_EventOccurtInfoTextBoxs[3].SetTextRect(m_EventOccurtInfoTextBoxs[2].OutLineRectangle.Right + 1, m_EventOccurtInfoTextBoxs[0].OutLineRectangle.Y, 340, 30);

            m_EventOccurtInfoTextBoxs[0].SetTextStyle(12, FormatStyle.DirectionRightToLeft, true, "宋体");
            m_EventOccurtInfoTextBoxs[1].SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_EventOccurtInfoTextBoxs[2].SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_EventOccurtInfoTextBoxs[3].SetTextStyle(12, FormatStyle.Center, true, "宋体");

            #endregion

            #region 事件详细信息文本框
            for (int index = 0; index < 3; index++)
            {
                m_EventDetailInfoTextBoxs[index] = new GDIRectText();
                m_EventDetailInfoTextBoxs[index].SetBkColor(255, 255, 255);
                m_EventDetailInfoTextBoxs[index].SetTextColor(0, 0, 0);

                m_EventDetailInfoTextBoxs[index].SetTextStyle(10, FormatStyle.DirectionLeftToRight, true, "宋体");
                // _eventDetailInfoTextBoxs[index].SetLinePen(192, 192, 192,1);
                //_eventDetailInfoTextBoxs[index].isdrawrectfrm = true;
            }

            m_EventDetailInfoTextBoxs[0].SetTextRect(m_EventOccurtInfoTextBoxs[0].OutLineRectangle.X + 2, m_EventOccurtInfoTextBoxs[0].OutLineRectangle.Bottom + 3, 795, 45);
            m_EventDetailInfoTextBoxs[1].SetTextRect(m_EventDetailInfoTextBoxs[1].OutLineRectangle.X + 2, m_EventDetailInfoTextBoxs[0].OutLineRectangle.Bottom + 3, 795, 90);
            m_EventDetailInfoTextBoxs[2].SetTextRect(m_EventDetailInfoTextBoxs[1].OutLineRectangle.X + 2, m_EventDetailInfoTextBoxs[1].OutLineRectangle.Bottom + 3, 795, 120);
            #endregion

        }

        /// <summary>
        /// 刷新显示数据
        /// </summary>
        private void RefreshShowData()
        {
            if (CurrentExceptionData == null)
            {
                OnPost(CmdType.ChangePage, m_ChangeFromView, 0, 0);
                return;
            }

            //故障详细信息
            m_EventOccurtInfoTextBoxs[0].SetText(Title);
            m_EventOccurtInfoTextBoxs[1].SetText(CurrentExceptionData.Startdate.ToString());
            m_EventOccurtInfoTextBoxs[2].SetText(" ");
            string unitName = string.Empty;
            if (GT_GalobalFaultManage.Instance.FaultUinitDictionary.ContainsKey(CurrentExceptionData.ExUnit))
            {
                unitName = GT_GalobalFaultManage.Instance.FaultUinitDictionary[CurrentExceptionData.ExUnit];
            }
            m_EventOccurtInfoTextBoxs[3].SetText(unitName);
            SetCurrentHelpInfo();
            if (m_CurrentHelpInfo == null)
            {
                return;
            }

            //故障描述信息
            m_EventDetailInfoTextBoxs[0].SetText(m_CurrentHelpInfo.HelpDescription);
            m_EventDetailInfoTextBoxs[1].SetText(EventDetailBackGround + m_CurrentHelpInfo.HelpBackground);
            m_EventDetailInfoTextBoxs[2].SetText(EventDetailXianXiang + m_CurrentHelpInfo.HelpXianXiang);

            //  _eventDetailInfoTextBoxs[0].SetText(
        }
        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        private void OnDraw(Graphics g)
        {
            g.FillRectangle(Brushes.Gray, m_TitleRect);

            g.FillRectangle(Brushes.Gray, m_BottomRect);
            //绘制菜单标题
            m_Title.OnDraw(g);

            //绘制事件信息文本框
            for (int index = 0; index < 4; index++)
            {
                m_EventOccurtInfoTextBoxs[index].OnDraw(g);
            }

            //绘制事件详细信息文本框
            for (int index = 0; index < 3; index++)
            {
                m_EventDetailInfoTextBoxs[index].OnDraw(g);
            }
        }
        #endregion

        /// <summary>
        /// 设置当前故障对应的提示信息
        /// </summary>
        private void SetCurrentHelpInfo()
        {
            if (CurrentExceptionData == null)
            {
                return;
            }

            if (EventStatic.HelpInfos.ContainsKey(CurrentExceptionData.ExSuggestId))
            {
                m_CurrentHelpInfo = EventStatic.HelpInfos[CurrentExceptionData.ExSuggestId];
            }
            else
            {
                m_CurrentHelpInfo = null;
            }
        }

        /// <summary>
        /// 清空当前A级故障显示
        /// </summary>
        public void ClearCurrentFault()
        {
            CurrentExceptionData = null;
            GT_AlarmInfo.CurrentShowExceptionData = null;
        }

        /// <summary>
        /// 设置当前显示的A类故障
        /// </summary>
        public void SetCurrentShowFault(ExceptionData exData)
        {
            if (exData.ExType == FaultType.OperError)
            {
                CurrentExceptionData = exData;
                GT_AlarmInfo.CurrentShowExceptionData = exData;
                Title = "信息";
            }
            if (exData.ExType == FaultType.A)
            {
                CurrentExceptionData = exData;
                GT_AlarmInfo.CurrentShowExceptionData = exData;
                Title = "A-警报";
            }
        }
    }
}
