using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;


namespace Motor.HMI.CRH1A.Comfort
{
    /// <summary>
    /// 舒适度
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class GT_Comfort : CRH1BaseClass
    {
        #region 私有字段

        private readonly GT_MenuTitle m_Title = new GT_MenuTitle("舒适度"); //菜单标题
        public Rectangle Recposition = new Rectangle(0, 133, 800, 100);

        /// <summary>
        /// 设置温度和外部温度
        /// </summary>
        public GDIRectText[] TemperRect = new GDIRectText[3]; //显示温度信息的文本框

        private GDIRectText m_AirFlowStateText;

        private Rectangle m_Rect; //页脚区域

        public bool[] Valueb;

        /// <summary>
        /// 司机室可调节的温度
        /// </summary>
        private int m_DriverSetTemper;

        #endregion

        public string ResPath
        {
            get { return RecPath; }
        }

        private readonly ComfortButtonMgr m_ComfortButtonMgrMgr;

        private readonly ComfortUnit[] m_ComfortUnits;

        /// <summary>
        /// 司机室
        /// </summary>
        private ComfortUnit Driver
        {
            get { return m_ComfortUnits[0]; }
        }

        /// <summary>
        /// 外部温度
        /// </summary>
        private float ExternTemperature
        {
            set
            {
                UpdateDriverTemperature();
                m_ExternTemperature = value;
            }
            get { return m_ExternTemperature; }
        }

        /// <summary>
        /// 选中的单元
        /// </summary>
        private ComfortUnit m_SelectedUnit;

        private readonly string[] m_StrText = new string[8]
        {
            "司机室温度设定值", "02号乘客室温度设定值", "03号乘客室温度设定值", "04号乘客室温度设定值",
            "05号乘客室温度设定值", "06号乘客室温度设定值", "07号乘客室温度设定值", "08号乘客室温度设定值"
        };

        // ReSharper disable once InconsistentNaming
        private static readonly List<string> m_AirFlowStateList = new List<string>() { "关", "1", "2", "3", "4" };

        private int CurrentAirFlowStateIndex
        {
            set
            {
                m_CurrentAirFlowStateIndex = value;
                if (m_AirFlowStateText != null)
                {
                    m_AirFlowStateText.SetText(m_AirFlowStateList[value]);
                }
            }
            get { return m_CurrentAirFlowStateIndex; }
        }


        private float m_ExternTemperature;
        private int m_CurrentAirFlowStateIndex;

        public GT_Comfort()
        {
            m_ComfortButtonMgrMgr = new ComfortButtonMgr(this);
            m_ComfortUnits = new ComfortUnit[8];
            m_DriverSetTemper = 0;
        }

        #region 重载方法

        public override string GetInfo()
        {
            return "舒适度系统";
        }


        protected override void NavigateTo(ViewConfig to)
        {
            base.NavigateTo(to);

            ReselectUnit(null);
        }

        private void ReselectUnit(ComfortUnit unit)
        {
            if (m_SelectedUnit != null)
            {
                m_SelectedUnit.IsSelected = false;
            }

            if (unit != null )
            {
                unit.IsSelected = true;
                if (unit == m_SelectedUnit)
                {
                    unit.IsSelected = false;
                    unit = null;
                }
            }
            m_SelectedUnit = unit;
        }

        public override bool Initialize()
        {
            //////////////////加 载 图 片
            //if (this.UIObj.ParaList.Count >= 6)
            {
                Debug.Assert(UIObj.ParaList.Count == 16);
                var images = new Image[UIObj.ParaList.Count];
                for (int i = 0; i < images.Length; i++)
                {
                    images[i] = Image.FromFile(RecPath + "//" + UIObj.ParaList[i]);
                }
                ComfortImageRes.Instance.Images = images;
            }

            CurrentAirFlowStateIndex = 0;

            InitData();

            //3
            // 初始化按键
            InitButton();

            OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);



            return true;
        }

        private void UpdateDriverTemperature()
        {
            if (Driver != null)
            {
                Driver.Temperature = new[] { ExternTemperature + m_DriverSetTemper, ExternTemperature + m_DriverSetTemper };
            }
        }

        private void InitButton()
        {
            m_ComfortButtonMgrMgr.Init();
            m_ComfortButtonMgrMgr.AllLightHandler += (sender, args) => SetAllLight();
            m_ComfortButtonMgrMgr.SigalLightHandler += (sender, args) => SetSigleLight();
            m_ComfortButtonMgrMgr.TemperatureUpHandler += (sender, args) =>
            {
                if (Driver != null)
                {
                    if (m_DriverSetTemper < 5)
                    {
                        ++m_DriverSetTemper;
                        UpdateDriverTemperature();
                        OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[Driver.Id], 0, Driver.Temperature[0]);
                    }
                }
            };

            m_ComfortButtonMgrMgr.TemperatureDownHandler += (sender, args) =>
            {
                if (Driver != null)
                {
                    if (m_DriverSetTemper > -5)
                    {
                        --m_DriverSetTemper;

                        UpdateDriverTemperature();
                        OnPost(CmdType.SetFloatValue, UIObj.OutFloatList[Driver.Id], 0, Driver.Temperature[0]);
                    }
                }
            };

            m_ComfortButtonMgrMgr.HvacSwithHandler += state => OnPost(CmdType.SetBoolValue,
                UIObj.OutBoolList[
                    (int)ComfortOutBoolDefineHelper.GetOutBoolDefine(m_SelectedUnit.Id, ComfortButtonType.HvacControl)
                    ], state == MouseState.MouseDown ? 1 : 0, 0);

            m_ComfortButtonMgrMgr.TrainCutOffHandler += state => OnPost(CmdType.SetBoolValue,
                UIObj.OutBoolList[
                    (int)ComfortOutBoolDefineHelper.GetOutBoolDefine(m_SelectedUnit.Id, ComfortButtonType.TrainCutOff)],
                state == MouseState.MouseDown ? 1 : 0, 0);

            m_ComfortButtonMgrMgr.AirFlowUpHandler = (sender, args) =>
            {
                if (CurrentAirFlowStateIndex < m_AirFlowStateList.Count - 1)
                {
                    CurrentAirFlowStateIndex++;
                }
            };

            m_ComfortButtonMgrMgr.AirFlowDownHandler = (sender, args) =>
            {
                if (CurrentAirFlowStateIndex > 0)
                {
                    CurrentAirFlowStateIndex--;
                }
            };

        }

        public override void paint(Graphics dcGs)
        {
            GetValue();
            ReFreshData();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        protected override bool OnMouseDown(Point point)
        {
            // 按键不可能既在火车又在其它按键上.
            if (!OnTrainDown(point))
            {
                m_ComfortButtonMgrMgr.OnMouseDown(point);
            }
            return true;
        }

        protected override bool OnMouseUp(Point point)
        {
            m_ComfortButtonMgrMgr.OnMouseUp(point);
            return true;
        }

        #endregion

        #region 私有方法

        public void GetValue()
        {
            Valueb = new bool[UIObj.InBoolList.Count];
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                Valueb[i] = BoolList[UIObj.InBoolList[i]];
            }

            if (UIObj.InFloatList.Count >= 9)
            {
                ExternTemperature = FloatList[UIObj.InFloatList[8]];
            }

        }

        public void ReFreshData()
        {
            for (int i = 0; i < m_ComfortUnits.Length; i++)
            {
                var unit = m_ComfortUnits[i];
                if (BoolList[UIObj.InBoolList[ComfortInBoolDefine.GetHvacFaultIdx(i)]])
                {
                    unit.HvacState = HvacState.Fault;
                    continue;
                }
                if (BoolList[UIObj.InBoolList[ComfortInBoolDefine.GetHvacCutOffIdx(i)]])
                {
                    unit.HvacState = HvacState.CutOff;
                    continue;
                }
                if (!BoolList[UIObj.InBoolList[ComfortInBoolDefine.GetHvacSwitch(i)]])
                {
                    unit.HvacState = HvacState.TurnOff;
                    continue;
                }
                unit.HvacState = HvacState.Normal;
            }
            TemperRect[2].SetText(ExternTemperature.ToString("F0")); //外部温度
            TemperRect[1].SetText(m_DriverSetTemper.ToString());
        }

        /// <summary>
        /// 设置所有灯的状态亮
        /// </summary>
        private void SetAllLight()
        {
            bool isLight = !IsAllLight();

            foreach (var unit in m_ComfortUnits)
            {
                unit.IsLight = isLight;
            }
        }

        private void SetSigleLight()
        {
            if (m_SelectedUnit != null)
            {
                m_SelectedUnit.IsLight = !m_SelectedUnit.IsLight;
            }

        }

        private bool IsAllLight()
        {
            return m_ComfortUnits.All(unit => unit.IsLight);
        }

        public void InitData()
        {
            // 8 个
            const int UNIT_INTERVAL = 1;
            const int UNIT_X_OFFSET = 25;
            const int UNIT_Y_OFFSET = 40;
            m_ComfortUnits[0] = new TrainHeadComfortUnit(new Point(Recposition.X + UNIT_X_OFFSET, Recposition.Y + UNIT_Y_OFFSET))
            {
                Id = 0,
                Temperature = new float[] { 26, 26 },
                SelectedHandler = (sender, args) => ReselectUnit(sender as ComfortUnit),
            };
            for (int i = 1; i < m_ComfortUnits.Length - 1; i++)
            {
                m_ComfortUnits[i] =
                    new ComfortUnit(new Point(Recposition.X + UNIT_X_OFFSET + UNIT_INTERVAL + i * ComfortUnit.UnitSize.Width,
                        Recposition.Y + UNIT_Y_OFFSET))
                    {
                        Id = i,
                        Temperature = new float[] { 26, 26 },
                        SelectedHandler = (sender, args) => ReselectUnit(sender as ComfortUnit),
                    };
            }
            // 车尾
            m_ComfortUnits[7] = new TrainTailComfortUnit(new Point(Recposition.X + UNIT_X_OFFSET + UNIT_INTERVAL + 7 * ComfortUnit.UnitSize.Width,
                        Recposition.Y + UNIT_Y_OFFSET))
                    {
                        Id = 7,
                        Temperature = new float[] { 26, 26 },
                        SelectedHandler = (sender, args) => ReselectUnit(sender as ComfortUnit),
                    };



            #region ::::::::::::::::::::页 脚 信 息 区 域 位 置::::::::::::::::::::::::::::::::

            m_Rect = new Rectangle(Recposition.X, Recposition.Y + 240, 790, 80);

            #endregion

            #region ::::::::::::::::::温 度 控 制 区 域:::::::::::::::::::

            TemperRect[0] = new GDIRectText();
            TemperRect[0].SetBkColor(192, 192, 192);
            TemperRect[0].SetTextColor(0, 0, 0);
            TemperRect[0].SetTextStyle(8, FormatStyle.Center, true, "Arial");
            TemperRect[0].Isdrawrectfrm = true;
            TemperRect[0].SetText("24.5");
            TemperRect[0].SetTextRect(Recposition.X + 35, Recposition.Y + 60, 25, 20);

            // 设定温度显示
            TemperRect[1] = new GDIRectText();
            TemperRect[1].SetBkColor(255, 255, 225);
            TemperRect[1].SetTextColor(0, 0, 0);
            TemperRect[1].SetTextStyle(10, FormatStyle.Center, true, "Arial");
            TemperRect[1].SetTextRect(m_Rect.X + 380, m_Rect.Y - 60, 55, 55);

            //外部温度显示
            TemperRect[2] = new GDIRectText();
            TemperRect[2].SetBkColor(255, 255, 225);
            TemperRect[2].Isdrawrectfrm = true;
            TemperRect[2].SetTextColor(0, 0, 0);
            TemperRect[2].SetTextStyle(10, FormatStyle.Center, true, "Arial");
            TemperRect[2].SetTextRect(m_Rect.X + 535, m_Rect.Y - 40, 50, 25);

            #endregion


            #region 气流状态

            m_AirFlowStateText = new GDIRectText();
            m_AirFlowStateText.SetBkColor(255, 255, 225);
            m_AirFlowStateText.SetTextColor(0, 0, 0);
            m_AirFlowStateText.SetTextStyle(10, FormatStyle.Center, true, "Arial");
            m_AirFlowStateText.Isdrawrectfrm = true;
            m_AirFlowStateText.SetText(m_AirFlowStateList[CurrentAirFlowStateIndex]);
            m_AirFlowStateText.SetTextRect(m_Rect.X + 65, m_Rect.Y - 60, 60, 55);

            #endregion
        }

        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            m_Title.OnDraw(g);

            //页脚区域绘制
            g.FillRectangle(CommonResouce.BackgroudBrush, m_Rect);
            g.DrawRectangle(CommonResouce.BackgroudPen, m_Rect);

            //绘制底部按钮
            if (m_SelectedUnit != null)
            {
                m_ComfortButtonMgrMgr.SelectedRoomIdx = m_SelectedUnit.Id;
            }
            else
            {
                m_ComfortButtonMgrMgr.SelectedRoomIdx = -1;
            }
            m_ComfortButtonMgrMgr.OnDraw(g);

            foreach (var unit in m_ComfortUnits)
            {
                unit.OnDraw(g);
            }

            //显示温度
            for (int i = 1; i < 3; i++)
            {
                TemperRect[i].OnDraw(g);
            }

            m_AirFlowStateText.OnDraw(g);

            g.DrawString("司机室气流", new Font(CommonResouce.DefalutFont, FontStyle.Bold), CommonResouce.BlackBrush, m_Rect.X + 55, m_Rect.Y - 80);

            //外部温度
            g.DrawString("外部温度", new Font(CommonResouce.DefalutFont, FontStyle.Bold), CommonResouce.BlackBrush, m_Rect.X + 530, m_Rect.Y - 60);

            //温度控制区 文本显示
            g.DrawString(m_StrText[0], new Font(CommonResouce.DefalutFont, FontStyle.Bold), CommonResouce.BlackBrush, m_Rect.X + 340, m_Rect.Y - 80);
        }
        #endregion


        private bool OnTrainDown(Point point)
        {
            return m_ComfortUnits.Any(unit => unit.OnMouseDown(point));
        }
    }
}



