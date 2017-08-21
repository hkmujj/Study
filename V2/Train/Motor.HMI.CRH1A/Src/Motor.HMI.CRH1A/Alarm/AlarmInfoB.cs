using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.View;
using Motor.HMI.CRH1A.Train;

namespace Motor.HMI.CRH1A.Alarm
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class AlarmInfoB : CRH1BaseClass
    {
        public static AlarmInfoB Instance { private set; get; }
        private List<CRH1AButton> m_ControlBtns;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { protected set { m_Title.Title = value; } get { return m_Title.Title; } }

        #region 私有字段

        private readonly GT_MenuTitle m_Title = new GT_MenuTitle();
        #endregion

        #region 故障信息
        /// <summary>
        /// 当前显示的B类故障
        /// </summary>
        /// <returns></returns>
        protected ExceptionData CurrentExceptionData;

        /// <summary>
        /// 当前故障提示信息
        /// </summary>
        private FaultHelpInfo m_CurrentHelpInfo;

        private readonly SolidBrush m_YellowBrush = new SolidBrush(Color.Yellow);
        private Rectangle m_Recposition = new Rectangle(0, 185, 70, 40);

        /// <summary>
        /// 从哪个视图切换过来的
        /// </summary>
        private int m_ChangeFromView;

        private List<CommonInnerControlBase> m_Collection;
        private List<Rectangle> m_Rct;

        #endregion

        #region  重载方法

        /// <summary>
        /// 图元初始化过程执行过程 (只在初始化过程执行一次)
        /// </summary>
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
            RefreshData();
            OnDraw(g);
        }

        private void OnDraw(Graphics g)
        {
            m_ControlBtns.ForEach(e => e.OnDraw(g));
            m_Collection.ForEach(e => e.OnPaint(g));
        }

        private void RefreshData()
        {
            CurrentExceptionData = GT_AlarmInfo.CurrentShowExceptionData;
            if (CurrentExceptionData == null)
            {
                OnPost(CmdType.ChangePage, m_ChangeFromView, 0, 0);
            }
            else
            {
                Train4.SelectedBrush = m_YellowBrush;
                Train4.SelectedIndex = CurrentExceptionData.ExCarId - 1;
            }
            SetCurrentHelpInfo();
            if (m_CurrentHelpInfo == null)
            {
                return;
            }
            m_ControlBtns.ForEach(f=>f.IsEnable=false);
        }
        /// <summary>
        /// 设置当前故障对应的提示信息
        /// </summary>
        private void SetCurrentHelpInfo()
        {
            if (CurrentExceptionData == null)
            {
                return;
            }

            m_CurrentHelpInfo = EventStatic.HelpInfos.ContainsKey(CurrentExceptionData.ExSuggestId) ? EventStatic.HelpInfos[CurrentExceptionData.ExSuggestId] : null;
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
        /// <summary>
        /// 数据初始化方法
        /// </summary>
        private void InitData()
        {
            InitButton();
            InitText();
        }

        private void InitText()
        {
            var right = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            };
            var center = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            const int HEIGHT = 30;
            m_Rct = new List<Rectangle>();
            m_Rct.Add(new Rectangle(1, 165, 108, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[0].X, m_Rct[0].Y + HEIGHT, m_Rct[0].Width, m_Rct[0].Height));
            m_Rct.Add(new Rectangle(m_Rct[0].X + m_Rct[0].Width, m_Rct[0].Y, 205, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[2].X, m_Rct[2].Y + HEIGHT, 98, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[3].X + m_Rct[3].Width, m_Rct[3].Y, m_Rct[2].Width - m_Rct[3].Width, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[2].X + m_Rct[2].Width, m_Rct[2].Y + HEIGHT, 475, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[m_Rct.Count - 1].X, m_Rct[m_Rct.Count - 1].Y - HEIGHT, 180, HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[m_Rct.Count - 1].X + m_Rct[m_Rct.Count - 1].Width, m_Rct[m_Rct.Count - 1].Y, 295,
                HEIGHT));
            m_Rct.Add(new Rectangle(m_Rct[0].X, m_Rct[m_Rct.Count - 1].Y + 2 * HEIGHT,
                m_Rct[0].Width + m_Rct[2].Width + m_Rct[6].Width + m_Rct[7].Width, 230));
            m_Collection = new List<CommonInnerControlBase>();
            for (int i = 0; i < m_Rct.Count; i++)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text = "ss",
                    TextColor = Color.Black,
                    NeedDarwOutline = true,
                    OutLineRectangle = m_Rct[i],
                    BackColorVisible = true,
                    BkColor = i == 8 ? Color.White : Color.Beige,
                    OutLinePen = new Pen(Color.FromArgb(226, 226, 199), 2f),
                    TextFormat = (i == 2 || i == 5 || i == 7) ? center : i == 8 ? new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center } : right,
                    Tag = i + 1,
                    RefreshAction = o => RefreshItem((GDIRectText)o)
                });
            }
        }

        enum ContentType
        {
            ExType = 1,
            ExCarUnit = 2,
            Startdate = 3,
            ExCarId = 4,
            ExId = 5,
            ExText = 6,
            Content = 9,
        }
        private void RefreshItem(GDIRectText item)
        {
            var name = (ContentType)item.Tag;
            if (CurrentExceptionData == null || m_CurrentHelpInfo == null)
            {
                return;
            }
            switch (name)
            {
                case ContentType.ExType:
                    item.Text = CurrentExceptionData.ExTypeInfo;
                    break;
                case ContentType.ExCarUnit:
                    item.Text = CurrentExceptionData.ExCarUnit.ToString("000");
                    break;
                case ContentType.Startdate:
                    item.Text = CurrentExceptionData.Startdate.ToString("s").Replace("T", " ");
                    break;
                case ContentType.ExCarId:
                    item.Text = CurrentExceptionData.ExCarId.ToString("00");
                    break;
                case ContentType.ExId:
                    item.Text = CurrentExceptionData.ExId.ToString();
                    break;
                case ContentType.ExText:
                    item.Text = CurrentExceptionData.ExText;
                    break;
                case ContentType.Content:
                    item.Text = m_CurrentHelpInfo.HelpDescription + "\n" + m_CurrentHelpInfo.HelpBackground + "\n" +
                                m_CurrentHelpInfo.HelpXianXiang;
                    break;
                default:
                    item.Text = string.Empty;
                    break;
            }
        }

        private void InitButton()
        {
            m_ControlBtns = new List<CRH1AButton>();
            var btn = new CRH1Button()
            {
                ButtonUpEvent = (sender, args) => OnPost(CmdType.ChangePage, (int)ViewConfig.MainView, 0, 0),
            };
            btn.SetButtonRect(m_Recposition.X + 624, m_Recposition.Y + 326, 80, 50);
            btn.SetButtonColor(192, 192, 192);
            btn.SetButtonText("主菜单");
            m_ControlBtns.Add(btn);

            btn = new CRH1Button()
            {
                ButtonUpEvent = (sender, args) =>
                {
                    if (GlobalParam.Instance.TrainInfo.Speed >= 3) //速度高于3km时跳转到运行界面
                        OnPost(CmdType.ChangePage, (int)ViewConfig.Running, 0, 0);
                    else
                    {
                        OnPost(CmdType.ChangePage, (int)ViewConfig.Station, 0, 0);
                    }
                }
            };
            btn.SetButtonColor(192, 192, 192);
            btn.SetButtonRect(m_Recposition.X + 710, m_Recposition.Y + 326, 80, 50);
            btn.SetButtonText("运行/车站");
            m_ControlBtns.Add(btn);
            btn = new CRH1Button()
            {
                ButtonUpEvent = (sender, args) => { OnPost(CmdType.ChangePage, m_ChangeFromView, 0, 0); }
            };
            btn.SetButtonColor(192, 192, 192);
            btn.SetButtonRect(m_Recposition.X + 420, m_Recposition.Y + 326, 80, 50);
            btn.SetButtonText("返回");
            m_ControlBtns.Add(btn);
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseUp(point));
        }
    }
}