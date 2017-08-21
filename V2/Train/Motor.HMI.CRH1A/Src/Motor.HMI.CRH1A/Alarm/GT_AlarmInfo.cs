using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Alarm.NotifyFilter;
using Motor.HMI.CRH1A.Alarm.VigilantFault;
using Motor.HMI.CRH1A.Common;

namespace Motor.HMI.CRH1A.Alarm
{
    /// <summary>
    ///  在每个视图的底部显示的B类警报 
    /// </summary>  
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_AlarmInfo : CRH1BaseClass
    {
        #region 私有字段
        private Rectangle m_Recposition = new Rectangle(0, 457, 705, 55);
        private GDIRectText[] m_GText = new GDIRectText[4];
        private CRH1AButton m_ConfigButton;
        private Rectangle[] m_StrRect = new Rectangle[4];//故障信息标题位置
        private string[] m_StrInfo = new string[4] { "单元", "车辆", "代码", "描述" };
        private Font m_Font = new Font("Arial", 10);
        private SolidBrush m_BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));
        private int m_Weight = 50;
        private int m_Height = 25;
        private Pen m_GrayPen = new Pen(Color.FromArgb(179, 179, 179), 2);
        private SolidBrush m_FaultLevelBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        private Region m_Region;//文本框点击
        private INotifyFilter m_NotifyFilter;


        #endregion

        private Color BottomInfoColor { set { m_BlackBrush.Color = value; } get { return m_BlackBrush.Color; } }

        #region 公共静态属性
        /// <summary>
        /// 当前显示的故障
        /// </summary>
        public static ExceptionData CurrentShowExceptionData;


        #endregion

        #region 重载方法
        public override string GetInfo()
        {
            return "警报信息";
        }

        public override bool Initialize()
        {
            m_NotifyFilter = new DefaultNotifyFilter();

            InitData();
            return true;
        }

        public override void paint(Graphics g)
        {
            ReFreshData();
            DrawOn(g);
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override bool OnMouseDown(Point point)
        {
            OnButtonDown(point.X, point.Y);
            return true;
        }

        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override bool OnMouseUp(Point point)
        {
            OnButtonUp(point.X, point.Y);
            return true;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            //故障显示标题位置初始化
            for (int i = 0; i < 4; i++)
            {
                if (i < 3)
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + m_Weight * i, m_Recposition.Y, m_Weight, m_Height);
                }
                else
                {
                    m_StrRect[i] = new Rectangle(m_Recposition.X + m_Weight * i, m_Recposition.Y, 555, m_Height);
                }
            }
            //故障显示框初始化
            for (int i = 0; i < 4; i++)
            {
                m_GText[i] = new GDIRectText();
                m_GText[i].SetBkColor(255, 255, 0);
                m_GText[i].SetTextColor(0, 0, 0);
                m_GText[i].SetTextStyle(10, FormatStyle.DirectionLeftToRight, true, "Arial");
                if (i < 3)
                {
                    m_GText[i].SetTextRect(m_Recposition.X + m_Weight * i, m_Recposition.Y + m_Height, m_Weight, m_Height);
                }
                else
                {
                    m_GText[i].SetTextRect(m_Recposition.X + m_Weight * i, m_Recposition.Y + m_Height, 555, m_Height);
                }
            }
            m_Region = new Region(m_GText[3].OutLineRectangle);
            //初始化确认按钮
            m_ConfigButton = new CRH1AButton();
            m_ConfigButton.SetButtonColor(192, 192, 192);
            m_ConfigButton.SetButtonRect(m_Recposition.X + 708, m_Recposition.Y, 85, 50);
            m_ConfigButton.SetButtonText("确认");
        }

        /// <summary>
        /// 绘制方法
        /// </summary>
        /// <param name="g"></param>
        private void DrawOn(Graphics g)
        {
            if (GT_GalobalFaultManage.Instance.HasFault() && CanResponseUser)
            {
                NotifyFaultHasOccuse(CurrentShowExceptionData);

                for (int i = 0; i < 4; i++)
                {
                    m_GText[i].OnDraw(g);
                    g.FillRectangle(m_FaultLevelBrush, m_StrRect[i]);
                    g.DrawString(m_StrInfo[i], m_Font, m_BlackBrush, m_StrRect[i]);
                    g.DrawLine(m_GrayPen, m_StrRect[i].X, m_StrRect[i].Y, m_StrRect[i].X, m_StrRect[i].Y + 2 * m_Height);
                }
                m_ConfigButton.OnDraw(g);
            }
            else
            {
                NotifyFaultHasConfirm(null);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 刷新活动事件
        /// </summary>
        private void ReFreshData()
        {
            BottomInfoColor = Color.Black;
            if (CurrentShowExceptionData == null)
            {
                ClearTheFault();
            }
            else
            {
                SetTheFault();
            }
        }

        private void ClearTheFault()
        {
            m_GText[0].SetText(" ");
            m_GText[1].SetText(" ");
            m_GText[2].SetText(" ");
            m_GText[3].SetText(" ");

            for (int index = 0; index < 4; index++)
            {
                m_GText[index].SetBkColor(255, 255, 0);
            }

            m_FaultLevelBrush = new SolidBrush(Color.FromArgb(255, 255, 0));
        }

        private void SetTheFault()
        {

            m_GText[0].SetText(CurrentShowExceptionData.ExCarUnit.ToString());
            m_GText[1].SetText(CurrentShowExceptionData.ExCarId.ToString("00"));
            m_GText[2].SetText(CurrentShowExceptionData.ExId.ToString());
            m_GText[3].SetText(CurrentShowExceptionData.ExText);

            //根据故障等级设置不同的故障颜色
            SetColorByFaultType();

            JudgeConfirm();
        }

        /// <summary>
        /// 判断是否解决了
        /// </summary>
        private void JudgeConfirm()
        {
            if (BoolList[582])
            {
                if (CurrentShowExceptionData != null)
                {
                    CurrentShowExceptionData.IsConfirm = true;
                }
            }
        }

        private void SetColorByFaultType()
        {
            m_FaultLevelBrush = new SolidBrush(FaultTypeHelper.GetFaultShowAttribute(CurrentShowExceptionData.ExType).Color);

            switch (CurrentShowExceptionData.ExType)
            {
                case FaultType.A:
                    for (int index = 0; index < 4; index++)
                    {
                        m_GText[index].SetBkColor(m_FaultLevelBrush.Color.R, m_FaultLevelBrush.Color.G, m_FaultLevelBrush.Color.B);
                        m_GText[index].SetTextColor(Color.White.R, Color.White.G, Color.White.B);
                        BottomInfoColor = Color.White;
                    }
                    break;
                case FaultType.OperError:
                    for (int index = 0; index < 4; index++)
                    {
                        m_GText[index].SetBkColor(m_FaultLevelBrush.Color.R, m_FaultLevelBrush.Color.G, m_FaultLevelBrush.Color.B);
                        m_GText[index].SetTextColor(Color.White.R, Color.White.G, Color.White.B);
                        BottomInfoColor = Color.White;
                    }
                    break;
                default:
                    for (int index = 0; index < 4; index++)
                    {
                        m_GText[index].SetBkColor(m_FaultLevelBrush.Color.R, m_FaultLevelBrush.Color.G, m_FaultLevelBrush.Color.B);
                        m_GText[index].SetTextColor(Color.Black.R, Color.Black.G, Color.Black.B);
                    }
                    break;
            }


        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonDown(int x, int y)
        {
            //  按 钮 响 应 事 件
            if (m_ConfigButton.Contains(x, y))
            {
                var data = CurrentShowExceptionData;

                if (CurrentShowExceptionData != null)
                {
                    CurrentShowExceptionData.IsConfirm = true;
                    VigilantFaultView.Instance.ClearCurrentFault();
                    //GT_GalobalFaultManage.Instance.Refresh();
                }

                NotifyFaultHasConfirm(data);
                //OnPost(CmdType.SetBoolValue, this.UIObj.OutBoolList[0], 1, 0);
                m_ConfigButton.OnButtonDown();
            }
            else if (m_Region.IsVisible(x, y) && CurrentShowExceptionData != null && CurrentShowExceptionData.ExType != FaultType.A && EventStatic.HelpInfos.ContainsKey(CurrentShowExceptionData.ExSuggestId))
            {
                OnPost(CmdType.ChangePage, 44, 0, 0);//跳转故障B类子页面
            }
        }

        private void NotifyFaultHasOccuse(ExceptionData data)
        {
            if (m_NotifyFilter.Filter(data))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
            }
        }

        private void NotifyFaultHasConfirm(ExceptionData data)
        {
            if (data == null || m_NotifyFilter.Filter(data))
            {
                OnPost(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
            }
        }

        /// <summary>
        /// 鼠标弹起事件
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OnButtonUp(int x, int y)
        {
            //  按 钮 响 应 事 件
            if (m_ConfigButton.Contains(x, y))
            {
                //OnPost(CmdType.SetBoolValue, this.UIObj.OutBoolList[0], 0, 0);
                m_ConfigButton.OnButtonUp();
            }
        }
        #endregion
    }
}
