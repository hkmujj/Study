using System;
using System.Drawing;
using Motor.HMI.CRH1A.Common;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Comfort
{
    public class ComfortButtonMgr : IInnerControl
    {
        private GT_Comfort m_GtComfort;

        public CRH1AButton[] AllButton { get { return m_GButton; } }

        private readonly CRH1AButton[] m_GButton = new CRH1AButton[Enum.GetValues(typeof(ComfortButtonType)).Length];//底部按钮

        /// <summary>
        /// 选中的房间, -1 代表没有选中
        /// </summary>
        public int SelectedRoomIdx { set; get; }

        private bool IsSelectedRoom { get { return -1 != SelectedRoomIdx; } }

        #region 页脚的按键大小

        private const int FootButtonWidth = 95;

        private const int FootButtonHight = 55;

        /// <summary>
        /// 按键最小间隔
        /// </summary>
        private const int ButtonInterval = 5;

        #endregion

        #region 按键响应事件

        public EventHandler AutoAirFlowHandler;
        public EventHandler AllLightHandler;
        public EventHandler SigalLightHandler;
        public EventHandler TemperatureUpHandler;
        public EventHandler TemperatureDownHandler;
        public EventHandler AirFlowUpHandler;
        public EventHandler AirFlowDownHandler;
        //public EventHandler HVACSwithHandler;
        //public EventHandler TrainCutOffHandler;

        public Action<MouseState> HvacSwithHandler;
        public Action<MouseState> TrainCutOffHandler;

        #endregion


        public ComfortButtonMgr(GT_Comfort gtComfort)
        {
            m_GtComfort = gtComfort;
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化所有的button
        /// </summary>
        public void Init()
        {
            #region ;;;;;;;;;;;;;;;; 底部按钮初始化:::::::::::::::::::::::

            var recposition = m_GtComfort.Recposition;
            var recPath = m_GtComfort.ResPath;
            var uiObj = m_GtComfort.UIObj;

            //const int sys = (int)ComfortButtonType.SystemBtn;
            //m_GButton[sys] = new CRH1AButton();
            //m_GButton[sys].SetButtonRect(recposition.X + 454, recposition.Y + 375, 80, 50);
            //m_GButton[sys].SetButtonColor(192, 192, 192);
            //m_GButton[sys].SetButtonText("系统");
            #endregion


            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::
            var rect = new Rectangle(recposition.X, recposition.Y + 240, 790, 80);
            const int AUTO_AIR = (int)ComfortButtonType.AutoAirflow;
            m_GButton[AUTO_AIR] = new CRH1AButton();
            m_GButton[AUTO_AIR].SetButtonColor(192, 192, 192);
            m_GButton[AUTO_AIR].SetButtonRect(rect.X + 8, rect.Y + 10, FootButtonWidth, FootButtonHight);
            m_GButton[AUTO_AIR].SetButtonText("自动气流");

            const int ALL_LIGHT = (int)ComfortButtonType.AllLight;
            var allLightX = rect.X + 590;
            m_GButton[ALL_LIGHT] = new CRH1AButton();
            m_GButton[ALL_LIGHT].SetButtonColor(192, 192, 192);
            m_GButton[ALL_LIGHT].SetButtonRect(allLightX, rect.Y + 10, FootButtonWidth, FootButtonHight);
            m_GButton[ALL_LIGHT].SetButtonText("全局照明");

            const int SIG_LIGHT = (int)ComfortButtonType.SigleLight;
            m_GButton[SIG_LIGHT] = new CRH1AButton();
            m_GButton[SIG_LIGHT].SetButtonColor(192, 192, 192);
            m_GButton[SIG_LIGHT].SetButtonRect(allLightX + ButtonInterval + FootButtonWidth, rect.Y + 10, FootButtonWidth, FootButtonHight);
            m_GButton[SIG_LIGHT].SetButtonText("客室照明");
            #endregion

            #region ::::::::::::::::::温 度 控 制 区 域:::::::::::::::::::

            const int T_UP = (int)ComfortButtonType.TemperatureUp;
            m_GButton[T_UP] = new CRH1AButton();
            m_GButton[T_UP].SetButtonColor(192, 192, 192);
            m_GButton[T_UP].SetButtonRect(rect.X + 320, rect.Y - 60, 55, 55);
            m_GButton[T_UP].SetButtonPic(ComfortImageRes.Instance.GetTemperAdjustImage(ComfortButtonType.TemperatureUp));


            const int T_DOWN = (int)ComfortButtonType.TemperatureDown;
            m_GButton[T_DOWN] = new CRH1AButton();
            m_GButton[T_DOWN].SetButtonColor(192, 192, 192);
            m_GButton[T_DOWN].SetButtonRect(rect.X + 440, rect.Y - 60, 55, 55);
            m_GButton[T_DOWN].SetButtonPic(ComfortImageRes.Instance.GetTemperAdjustImage(ComfortButtonType.TemperatureDown));


            #endregion

            var hvacControlBtn = new CRH1AButton();
            var hvacX = rect.X + 290;
            hvacControlBtn.SetButtonColor(192, 192, 192);
            hvacControlBtn.SetButtonRect(hvacX, rect.Y + 10, FootButtonWidth, FootButtonHight);
            hvacControlBtn.SetButtonText("HVAC\r\n开/关");
            m_GButton[(int)ComfortButtonType.HvacControl] = hvacControlBtn;

            var trainCutOffBtn = new CRH1AButton();
            trainCutOffBtn.SetButtonColor(192, 192, 192);
            trainCutOffBtn.SetButtonRect(hvacX + ButtonInterval + FootButtonWidth, rect.Y + 10, FootButtonWidth, FootButtonHight);
            trainCutOffBtn.SetButtonText("车辆切除");
            m_GButton[(int)ComfortButtonType.TrainCutOff] = trainCutOffBtn;

            const int AIR_FLOW_UP = (int)ComfortButtonType.AirFlowUp;
            m_GButton[AIR_FLOW_UP] = new CRH1AButton();
            m_GButton[AIR_FLOW_UP].SetButtonColor(192, 192, 192);
            m_GButton[AIR_FLOW_UP].SetButtonRect(rect.X + 8, rect.Y - 60, 55, 55);
            m_GButton[AIR_FLOW_UP].SetButtonPic(ComfortImageRes.Instance.GetTemperAdjustImage(ComfortButtonType.TemperatureUp));


            const int AIR_FLOW_DOWN = (int)ComfortButtonType.AirFlowDown;
            m_GButton[AIR_FLOW_DOWN] = new CRH1AButton();
            m_GButton[AIR_FLOW_DOWN].SetButtonColor(192, 192, 192);
            m_GButton[AIR_FLOW_DOWN].SetButtonRect(rect.X + 128, rect.Y - 60, 55, 55);
            m_GButton[AIR_FLOW_DOWN].SetButtonPic(ComfortImageRes.Instance.GetTemperAdjustImage(ComfortButtonType.TemperatureDown));
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var button in m_GButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
                foreach (var button in AllButton)
                {
                    button.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }

        public bool OnMouseDown(Point point)
        {
            //  按 钮 响 应 事 件

            for (int index = 0; index < m_GButton.Length; index++)
            {
                var btn = m_GButton[index];
                if (btn.Contains(point) && btn.IsEnable)
                {
                    btn.OnButtonDown();
                    if (IsSelectedRoom)
                    {
                        if ((ComfortButtonType)index == ComfortButtonType.TrainCutOff)
                        {
                            if (TrainCutOffHandler != null)
                            {
                                TrainCutOffHandler(MouseState.MouseDown);
                            }
                        }
                        else if ((ComfortButtonType)index == ComfortButtonType.HvacControl)
                        {
                            if (HvacSwithHandler != null)
                            {
                                HvacSwithHandler(MouseState.MouseDown);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public bool OnMouseUp(Point point)
        {
            var btnCnt = m_GButton.Length;
            for (int i = 0; i < btnCnt; i++)
            {
                if (m_GButton[i].Contains(point) && m_GButton[i].IsEnable)
                {
                    var selBtn = (ComfortButtonType)i;
                    switch (selBtn)
                    {
                        //case ComfortButtonType.SystemBtn:
                        //    OnHandle(this.SwithSystemHandler);
                        //    break;
                        case ComfortButtonType.AutoAirflow:
                            OnHandle(AutoAirFlowHandler);
                            //待定
                            break;
                        case ComfortButtonType.AllLight:
                            OnHandle(AllLightHandler);
                            //待定
                            break;
                        case ComfortButtonType.SigleLight:
                            OnHandle(SigalLightHandler);
                            //待定
                            break;
                        case ComfortButtonType.TemperatureUp:
                            OnHandle(TemperatureUpHandler);
                            break;
                        case ComfortButtonType.TemperatureDown:
                            OnHandle(TemperatureDownHandler);
                            m_GButton[(int)ComfortButtonType.TemperatureDown].OnButtonDown();
                            break;
                        case ComfortButtonType.HvacControl:
                            if (IsSelectedRoom)
                            {
                                if (IsSelectedRoom)
                                {
                                    if (HvacSwithHandler != null)
                                    {
                                        HvacSwithHandler(MouseState.MouseUp);
                                    }
                                }
                            }
                            break;
                        case ComfortButtonType.TrainCutOff:
                            if (IsSelectedRoom)
                            {
                                if (TrainCutOffHandler != null)
                                {
                                    TrainCutOffHandler(MouseState.MouseUp);
                                }
                            }
                            break;
                        case ComfortButtonType.AirFlowUp:
                            OnHandle(AirFlowUpHandler, this, null);
                            break;
                        case ComfortButtonType.AirFlowDown:
                            OnHandle(AirFlowDownHandler, this, null);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    m_GButton[i].OnButtonUp();
                    return true;
                }
            }
            return false;
        }

        public CRH1AButton this[ComfortButtonType btnType] { get { return m_GButton[(int)btnType]; } }

        public CRH1AButton this[int idx] { get { return m_GButton[idx]; } }


        public void OnDraw(Graphics g)
        {
            for (int i = 0; i < m_GButton.Length; i++)
            {
                if (i == (int)ComfortButtonType.HvacControl || i == (int)ComfortButtonType.TrainCutOff)
                {
                    if (-1 != SelectedRoomIdx)
                    {
                        m_GButton[i].OnDraw(g);
                    }
                }
                else
                {
                    m_GButton[i].OnDraw(g);
                }
            }
        }

        public void OnPaint(Graphics g)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public Action<object> RefreshAction { get; set; }
        public object Tag { get; set; }

        private void OnHandle(EventHandler handler, object sender, EventArgs e)
        {
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private void OnHandle(EventHandler handler)
        {
            OnHandle(handler, null, null);
        }


    }
}
