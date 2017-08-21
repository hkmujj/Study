using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;

namespace Motor.HMI.CRH1A.Alarm.FaultPopupView
{
    /// <summary>
    /// 弹框性的错误信息
    /// </summary>
    // ReSharper disable once InconsistentNaming
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class GT_FaultPopupView : CRH1BaseClass
    {
        /// <summary>
        /// 当前故障
        /// </summary>
        public ExceptionData CurrntExceptionData
        {
            set
            {
                m_CurrntExceptionData = value;
                m_CurrentImage = FaultPopupImageRes.GetImage(this);
                m_TitleText.SetText(CurrntExceptionData.ExText);
                m_InfoText.SetText(EventStatic.HelpInfos[CurrntExceptionData.ExSuggestId].HelpDescription);
            }
            get { return m_CurrntExceptionData; }
        }

        private Image m_CurrentImage;

        /// <summary>
        /// 重启
        /// </summary>
        private bool Restart
        {
            get { return BoolList[FaultPopupInBoolRes.GetRestartIndex(this)]; }
        }

        private static readonly SolidBrush BackBrush = new SolidBrush(Color.FromArgb(65, 65, 65));
        private static Pen _titlePen = new Pen(Color.Black, 3);
        private static Pen _infoPen = new Pen(Color.Black, 2);

        private readonly Point m_ImageLocation;
        private static readonly Size ImageSize = new Size(120, 120);
        private readonly GDIRectText m_TitleText;
        private readonly GDIRectText m_InfoText;

        public Point Location
        {
            set
            {
                m_Location = value;
                m_OutLineRectangle.X = value.X;
                m_OutLineRectangle.Y = value.Y;

            }
            get { return m_Location; }
        }

        public Size Size
        {
            private set
            {
                m_Size = value;
                m_OutLineRectangle.Width = value.Width;
                m_OutLineRectangle.Height = value.Height;
            }
            get { return m_Size; }
        }

        private Rectangle m_OutLineRectangle;
        private Point m_Location;
        private Size m_Size;
        private ExceptionData m_CurrntExceptionData;

        public bool IsActive { private set; get; }

        public GT_FaultPopupView()
        {
            Location = new Point(30, 30);
            Size = new Size(720, 500);
            m_ImageLocation = new Point(Location.X + 50, Location.Y + Size.Height / 2 - 50);
            m_TitleText = new GDIRectText();
            m_TitleText.SetTextRect(Location.X, Location.Y + 50, Size.Width, 20);
            m_TitleText.SetBkColor(BackBrush.Color);
            m_TitleText.SetTextStyle(13, FormatStyle.Center, true, "Arial");

            m_InfoText = new GDIRectText();
            m_InfoText.SetTextRect(ImageSize.Width + m_ImageLocation.X + 40,
                m_ImageLocation.Y,
                m_OutLineRectangle.Width - m_ImageLocation.X + m_OutLineRectangle.X - ImageSize.Width - 50,
                ImageSize.Height);
            m_InfoText.SetBkColor(BackBrush.Color);
            m_InfoText.SetTextStyle(10, FormatStyle.Center, true, "Arial");
            IsActive = false;
        }

        /// <summary>
        /// 单例
        /// </summary>
        public static GT_FaultPopupView Instance { private set; get; }

        public override string GetInfo()
        {
            return "弹出的故障信息";
        }


        public override bool Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            var images = UIObj.ParaList.Select(file => Image.FromFile(Path.Combine(RecPath, file))).ToArray();
            FaultPopupImageRes.Images = images;

            return true;
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                IsActive = true;
            }
        }

        public override void paint(Graphics g)
        {
            if (CurrntExceptionData.ExLogNo == 687)
            {
                g.DrawImage(FaultPopupImageRes.Images[2], m_OutLineRectangle);
            }
            else
            {
                //背景
                g.FillRectangle(BackBrush, m_OutLineRectangle);

                g.DrawImage(FaultPopupImageRes.GetImage(this), m_ImageLocation);

                m_TitleText.OnDraw(g);
                m_InfoText.OnDraw(g);
            }

            if (Restart)
            {
                GT_GalobalFaultManage.Instance.CanResponseFaultA = false;
                OnPost(CmdType.SetBoolValue, FaultPopupInBoolRes.GetRestartIndex(this), 0, 0);
                if (!GT_GalobalFaultManage.Instance.HasPupupFault)
                {
                    //CurrntExceptionData.IsConfirm = true;
                    OnPost(CmdType.ChangePage, (int)ViewConfig.Login, 0, 0);
                    IsActive = false;
                }

            }
        }
    }
}
