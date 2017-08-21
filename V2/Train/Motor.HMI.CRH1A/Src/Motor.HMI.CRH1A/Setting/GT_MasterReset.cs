using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.Train;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;



namespace Motor.HMI.CRH1A.Setting
{
    /// <summary>
    ///  主控复位设定菜单 此菜单的功能是实现主控 TC CUU复位
    ///  可以从设置菜单进入该子菜单
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_MasterReset : CRH1BaseClass
    {
        public GT_MenuTitle Title = new GT_MenuTitle("主控复位");//菜单标题
        public Rectangle Recposition = new Rectangle(0, 170, 791, 280);
        public SolidBrush BackBrush = new SolidBrush(Color.FromArgb(119, 136, 152));
        public Pen BackPen = new Pen(Color.FromArgb(199, 215, 232), 3);
        public SolidBrush StrBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        public Font Strfont = new Font("Arial", 11);
        public CRH1AButton GButton;

        /// <summary>
        /// 提示信息
        /// </summary>
        private const string Info = "只有在列车处于停车状态时,主控才可以复位\n\n       司机可执行的主控复位次数受限\n\n (超过主控复位限定次数后,此按键失效)";

        private string m_CurrentInfo;

        private int[] m_ResetCount = { 0, 0, 0 };

        private int[] m_MaxResetCount = new[] { 3, 3, 3 };
        private int m_Unit = 0;
        /// <summary>
        /// 是否选中了列车
        /// </summary>
        private bool m_IsSelectedTrain = false;

        #region ::::::::::::::::::::::::init override::::::::::::::::::::::::::::::#
        public override string GetInfo()
        {
            return "主控复位设定";
        }

        protected override void NavigateInCurrent(ViewConfig current)
        {
            if (CommonTrain.Instance.SelectedChanged == null)
            {
                CommonTrain.Instance.SelectedChanged = OnTrainSelectedChanged;
                CommonTrain.Instance.FaultChanged = OnCommonTrainFaultChanged;
                CommonTrain.Instance.CanSelect = true;
                CommonTrain.Instance.TrainvView.DeselectAll();
            }
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                CommonTrain.Instance.SelectedChanged = OnTrainSelectedChanged;
                CommonTrain.Instance.FaultChanged = OnCommonTrainFaultChanged;
                CommonTrain.Instance.CanSelect = true;
                CommonTrain.Instance.TrainvView.DeselectAll();
            }
        }

        private void OnCommonTrainFaultChanged(object sender, EventArgs eventArgs)
        {
            var sen = sender as TrainvView;

            if (sen == null)
            {
                LogMgr.Warn(string.Format("Train fault changed event' sender is not typeof({0})", typeof(TrainvView)));
                return;
            }

            if (sen.IsAnyFault)
            {
                m_IsSelectedTrain = false;
                GButton.IsEnable = m_IsSelectedTrain;
                GButton.CurrentMouseState = MouseState.MouseDown;
            }
        }

        private void OnTrainSelectedChanged(object sender, EventArgs eventArgs)
        {
            var sen = sender as TrainvView;
            var args = eventArgs as TrainSelectedChangedArgs;

            if (sen == null)
            {
                LogMgr.Warn(string.Format("Train fault changed event' sender is not typeof({0})", typeof(TrainvView)));
                return;
            }
            m_Unit = BaseTrainUnit.CurrentUnit - 1;
            m_IsSelectedTrain = true;
            var currBtnState = MouseState.MouseUp;
            var isEnable = m_IsSelectedTrain;
            if (sen.IsAnyFault)
            {
                m_IsSelectedTrain = false;
                isEnable = false;
                currBtnState = MouseState.MouseDown;
            }
            else
            {
                var isAll = args.NewSelected.All(b => !b);
                if (isAll)
                {
                    m_IsSelectedTrain = false;
                    isEnable = false;
                    currBtnState = MouseState.MouseDown;
                }

                if (args.From == TrainSelectedChangedArgs.ChangedFrom.UiClick)
                {
                    var selectIdx = args.NewSelected.IndexOf(true);
                    sen.SelectOfIndex(BaseTrainUnit.GetUnitOfIndex(selectIdx));
                }
            }
            if (m_Unit >= 0)
            {
                if (m_ResetCount[m_Unit] >= m_MaxResetCount[m_Unit])
                {
                    currBtnState = MouseState.MouseDown;
                    isEnable = false;
                }
                m_Buttons[m_Unit].IsEnable = isEnable;
                m_Buttons[m_Unit].CurrentMouseState = currBtnState;
            }
            else
            {
                GButton.IsEnable = isEnable;
                GButton.CurrentMouseState = currBtnState;
            }
            m_Unit = BaseTrainUnit.CurrentUnit - 1;

        }


        public override bool Initialize()
        {
            //3
            InitDate();

            return true;
        }


        public override void paint(Graphics g)
        {
            GlobalEvent.Instance.StartCompleted += (sender, args) =>
            {
                m_ResetCount = new int[] { 0, 0, 0 };
                m_Unit = -1;
            };

            DrawOn(g);

        }
        private List<CRH1AButton> m_Buttons;
        public void InitDate()
        {
            m_CurrentInfo = Info;
            m_Buttons = new List<CRH1AButton>();
            for (int i = 0; i < 3; i++)
            {
                m_MaxResetCount[i] =
                    GlobalInfo.Instance.CRH1ADetailConfig.MainNode.Find(f => Convert.ToInt32(f.Unit) == i).Num;
            }
            for (int i = 0; i < 3; i++)
            {
                GButton = new CRH1AButton();
                GButton.SetButtonColor(192, 192, 192);
                GButton.SetButtonRect(Recposition.X + 380, Recposition.Y + 160, 100, 60);
                GButton.SetButtonText("复位");
                GButton.ButtonDownEvent = (sender, args) =>
                {
                    m_CurrentInfo = string.Format("当前是第 {0} 次复位,\r\n复位中", m_ResetCount[m_Unit]);
                    CommonTrain.Instance.CanSelect = false;
                    ((CRH1AButton)sender).IsEnable = false;
                    var timermodel = new ResetTimerModel(this);
                    var timer = new Timer(cnt =>
                    {
                        var model = (ResetTimerModel)cnt;
                        if (model.TimerCount == 6)
                        {
                            model.Timer.Dispose();
                            if (model.Content.m_ResetCount[m_Unit] < m_MaxResetCount[m_Unit])
                            {
                                model.Content.m_Buttons[m_Unit].CurrentMouseState = MouseState.MouseUp;
                                model.Content.m_Buttons[m_Unit].IsEnable = true;
                            }
                            else
                            {
                                model.Content.m_Buttons[m_Unit].IsEnable = false;
                            }
                            m_CurrentInfo = Info;
                            CommonTrain.Instance.CanSelect = true;
                        }
                        else
                        {
                            model.Content.m_CurrentInfo += "· ";
                            ++model.TimerCount;
                        }
                    }, timermodel, 300, 1000);
                    timermodel.Timer = timer;
                };
                m_Buttons.Add(GButton);
            }
            GButton = new CRH1AButton();
            GButton.SetButtonColor(192, 192, 192);
            GButton.SetButtonRect(Recposition.X + 380, Recposition.Y + 160, 100, 60);
            GButton.SetButtonText("复位");
        }

        class ResetTimerModel
        {
            public Timer Timer { set; get; }

            public int TimerCount { set; get; }

            public GT_MasterReset Content { private set; get; }

            public ResetTimerModel(GT_MasterReset gt)
            {
                Content = gt;
                TimerCount = 0;
            }
        }

        public void DrawOn(Graphics e)
        {
            //绘制菜单标题
            Title.OnDraw(e);

            e.FillRectangle(BackBrush, Recposition);
            e.DrawRectangle(BackPen, Recposition);
            if (m_Unit >= 0)
            {
                m_Buttons[m_Unit].OnDraw(e);
            }
            else
            {
                GButton.IsEnable = false;
                GButton.OnDraw(e);
            }
            e.DrawString(m_CurrentInfo,
                 Strfont, StrBrush, Recposition.X + 270, Recposition.Y + 55);
        }
        #endregion#

        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }

        public void OnButtonDown(int x, int y)
        {
            if (m_Unit == -1)
            {
                return;
            }
            else
            {
                if (m_Buttons[m_Unit].Contains(x, y) && m_Buttons[m_Unit].IsEnable)
                {
                    if (m_ResetCount[m_Unit] < m_MaxResetCount[m_Unit])
                    {
                        ++m_ResetCount[m_Unit];
                        m_Buttons[m_Unit].OnButtonDown();
                        HandleUtil.OnHandle(m_Buttons[m_Unit].ButtonDownEvent, m_Buttons[m_Unit], null);
                    }
                }
            }
        }
    }
}