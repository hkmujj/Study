using System;
using System.Drawing;
using CommonUtil.Util;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace CRH2MMI.Common.Screen
{
    /// <summary>
    /// 屏的上电
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class TMSPower : CRH2BaseClass
    {
        private TMSPowerState CurrentPowerState
        {
            set
            {
                var old = m_CurrentPowerState;
                m_CurrentPowerState = value;
                if (old != m_CurrentPowerState)
                {
                    switch (m_CurrentPowerState)
                    {
                        case TMSPowerState.PowerOn:
                            LogMgr.Info("The screen has power on  ------------- ");
                            break;
                        case TMSPowerState.PowerOff:
                            LogMgr.Info("The screen has power off ============= ");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

            }
            get { return m_CurrentPowerState; }
        }

        private TMSPowerState m_LastPowerState;

        private int m_PowerOnIndex;

        private bool m_IsFirstPowOn;

        private readonly StartingView m_StartingView;

        private ICourseService m_CourseService;
        private TMSPowerState m_CurrentPowerState;

        public TMSPower()
        {
            CurrentPowerState = TMSPowerState.PowerOff;
            m_LastPowerState = CurrentPowerState;
            m_IsFirstPowOn = true;
            m_StartingView = new StartingView();
        }

        public override bool Init()
        {
            m_CourseService = DataPackage.ServiceManager.GetService<ICourseService>();
            m_CourseService.CourseStateChanged += CourseServiceOnCourseStateChanged;
            m_StartingView.Init(DataPackage);

            m_PowerOnIndex =
                GetInBoolIndex(this.ProjectName == "CRH2"
                    ? GlobalInfo.CurrentCRH2Config.TMSPowerConfig.PowerOn.InBoolColoumNames[0]
                    : GlobalInfo.CurrentCRH2Config.TMSPowerConfig.PowerOn.InBoolColoumNames[1]);

            InitUIObj(new[] {GlobalInfo.CurrentCRH2Config.TMSPowerConfig.PowerOn});

            SetPowerOnFlag(true);

            m_StartingView.Icon = ImageResource.Railway;
            m_StartingView.StartViewConfig = GlobalInfo.CurrentCRH2Config.StartViewConfig;

            return true;
        }

        private void CourseServiceOnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (m_CourseService.CurrentCourseState == CourseState.Stoped)
            {
                append_postCmd(CmdType.ChangePage, 0, 0, 0);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                if (nParaB == (int) ViewConfig.Black)
                {
                    SetPowerOnFlag(false);
                }
            }
        }

        private void SetPowerOnFlag(bool flag)
        {
            //AppDomain.CurrentDomain.
            if (GlobalInfo.Instance.CRH2ConfigAdpt.CRH2Config.DebugConfig.AutoLightUpScreen)
            {
                append_postCmd(CmdType.SetInBoolValue, m_PowerOnIndex, flag ? 1 : 0, 0);
            }
        }

        public override void paint(Graphics g)
        {
            CheckState();

            Notify();

            m_StartingView.OnDraw(g);
        }

        private void Notify()
        {
            switch (CurrentPowerState)
            {
                case TMSPowerState.PowerOn:
                    if (m_LastPowerState == TMSPowerState.PowerOff)
                    {
                        if (!m_IsFirstPowOn)
                        {
                            LogMgr.Debug("Restart screen.");
                            HandleUtil.OnHandle(GlobalEvent.Instance.RestartEvent, this, null);
                        }
                        else
                        {
                            LogMgr.Debug("Start the screen .");
                            HandleUtil.OnHandle(GlobalEvent.Instance.StartEvent, this, null);
                        }
                        m_StartingView.OnRestart();
                    }
                    m_IsFirstPowOn = false;
                    break;
                case TMSPowerState.PowerOff:
                    m_StartingView.OnShutdown();
                    HandleUtil.OnHandle(GlobalEvent.Instance.ShutdownEvent, this, null);
                    append_postCmd(CmdType.ChangePage, 0, 0, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckState()
        {
            var state = BoolList[m_PowerOnIndex];
            m_LastPowerState = CurrentPowerState;
            if (state && m_CourseService.CurrentCourseState != CourseState.Stoped)
            {
                if (CurrentPowerState == TMSPowerState.PowerOff)
                {
                    CurrentPowerState = TMSPowerState.PowerOn;
                    m_StartingView.OnRestart();
                    m_StartingView.StateChanged += OnStateChanged;
                }
            }
            else
            {
                if (CurrentPowerState == TMSPowerState.PowerOn)
                {
                    CurrentPowerState = TMSPowerState.PowerOff;
                    append_postCmd(CmdType.ChangePage, (int) ViewConfig.Black, 0, 0);
                }
            }
        }

        private void OnStateChanged(object sender, EventArgs eventArgs)
        {
            if (m_StartingView.CurretnState == StartingState.Complete)
            {
                append_postCmd(CmdType.ChangePage, (int) ViewConfig.InitView, 0, 0);
                m_StartingView.StateChanged -= OnStateChanged;
            }
            else if (m_StartingView.CurretnState == StartingState.Shutdown)
            {
                m_StartingView.StateChanged -= OnStateChanged;
            }
        }
    }
}
