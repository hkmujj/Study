using System;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.View;

namespace Motor.HMI.CRH1A.UserController
{
    class LoginKeyboard : CommonInnerControlBase
    {
        public EventHandler<LoginInputEventArgs> InputCompletedEventHandler { set; get; }

        public static readonly Size DefaultSize = new Size(200, 285);

        private static readonly Rectangle DefaultRecposition = new Rectangle(Point.Empty, DefaultSize);

        private CRH1AButton[] m_GButton = new CRH1AButton[12];
        private Rectangle[] m_Rect = new Rectangle[3];
        private readonly SolidBrush m_Rectbrush = new SolidBrush(Color.FromArgb(119, 135, 158));
        private readonly Pen m_Rectpen = new Pen(Color.FromArgb(165, 177, 185), 3);
        private const int Buttonweight = 60;
        private const int Buttonheight = 60;
        private GDIRectText m_GText;//输入的ID显示框
        string m_DriverId = string.Empty;
        readonly Font m_Strfont = new Font("Arial", 13);
        readonly SolidBrush m_Whitebrush = new SolidBrush(Color.FromArgb(255, 255, 255));

        public LoginKeyboard()
        {
            Init();

            OutLineChanged += OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            for (int i = 0; i < m_Rect.Length; i++)
            {
                m_Rect[i] = new Rectangle(LocationChangeMatrix.TransformPoint(m_Rect[i].Location), m_Rect[i].Size);
            }

            m_GText.Translate(LocationChangeMatrix.OffsetX, LocationChangeMatrix.OffsetY);

            foreach (var btn in m_GButton)
            {
                btn.Translate(LocationChangeMatrix.OffsetX, LocationChangeMatrix.OffsetY);
            }
        }

        public override void Init()
        {
            //3
            InitData();

        }

        public override void OnDraw(Graphics g)
        {
            DrawOn(g);
        }
        public void InitData()
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < 2)
                    m_Rect[i] = new Rectangle(DefaultRecposition.X + (DefaultRecposition.Width + 5) * i + 5, DefaultRecposition.Y, DefaultRecposition.Width, DefaultRecposition.Height);
                else
                    m_Rect[i] = new Rectangle(DefaultRecposition.X + DefaultRecposition.Width * i + 15, DefaultRecposition.Y, DefaultRecposition.Width + 170, DefaultRecposition.Height);
            }

            m_GText = new GDIRectText();
            m_GText.SetBkColor(0, 0, 0);
            m_GText.SetTextColor(255, 255, 255);
            m_GText.SetTextStyle(16, FormatStyle.Center, true, "Arial");
            m_GText.Isdrawrectfrm = true;
            m_GText.SetTextRect(m_Rect[1].X + 30, m_Rect[1].Y + 120, 140, 40);

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                {
                    m_GButton[i * 3 + j] = new CRH1Button();
                    m_GButton[i * 3 + j].SetButtonColor(192, 192, 192);
                    if (i * 3 + j == 11)
                    {
                        m_GButton[i * 3 + j].SetButtonRect(m_Rect[2].X + j * (Buttonweight + 8) + 35, m_Rect[2].Y + i * (Buttonheight + 8) + 15, 2 * Buttonweight, Buttonheight);
                    }
                    else
                    {
                        m_GButton[i * 3 + j].SetButtonRect(m_Rect[2].X + j * (Buttonweight + 8) + 35, m_Rect[2].Y + i * (Buttonheight + 8) + 15, Buttonweight, Buttonheight);
                    }

                }
            m_GButton[0].SetButtonText("7");
            m_GButton[0].ButtonUpEvent += (sender, args) => RecordInput(7);

            m_GButton[1].SetButtonText("8");
            m_GButton[1].ButtonUpEvent += (sender, args) => RecordInput(8);

            m_GButton[2].SetButtonText("9");
            m_GButton[2].ButtonUpEvent += (sender, args) => RecordInput(9);

            m_GButton[3].SetButtonText("4");
            m_GButton[3].ButtonUpEvent += (sender, args) => RecordInput(4);

            m_GButton[4].SetButtonText("5");
            m_GButton[4].ButtonUpEvent += (sender, args) => RecordInput(5);

            m_GButton[5].SetButtonText("6");
            m_GButton[5].ButtonUpEvent += (sender, args) => RecordInput(6);

            m_GButton[6].SetButtonText("1");
            m_GButton[6].ButtonUpEvent += (sender, args) => RecordInput(1);

            m_GButton[7].SetButtonText("2");
            m_GButton[7].ButtonUpEvent += (sender, args) => RecordInput(2);

            m_GButton[8].SetButtonText("3");
            m_GButton[8].ButtonUpEvent += (sender, args) => RecordInput(3);

            m_GButton[9].SetButtonText("清除");
            m_GButton[9].ButtonUpEvent += (sender, args) => ClearInput();

            m_GButton[10].SetButtonText("0");
            m_GButton[10].ButtonUpEvent += (sender, args) => RecordInput(0);

            m_GButton[11].SetButtonText("输 入");
            m_GButton[11].ButtonUpEvent = (sender, args) =>
                                             {
                                                 HandleUtil.OnHandle(InputCompletedEventHandler, this, new LoginInputEventArgs(m_DriverId));
                                                 DriverIdClear();
                                                 m_GText.Text = "";
                                             };
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                foreach (var crh1AButton in m_GButton)
                {
                    crh1AButton.IsEnable = GlobalInfo.Instance.ButtonEnable;
                }
            };
        }
        public void DrawOn(Graphics g)
        {
            //绘制菜单标题
            //title.OnDraw(g);

            for (int i = 0; i < 3; i++)
            {
                g.FillRectangle(m_Rectbrush, m_Rect[i]);
                g.DrawRectangle(m_Rectpen, m_Rect[i]);
            }
            g.DrawString("请输入你的ID", m_Strfont, m_Whitebrush, m_Rect[0].X + 45, m_Rect[0].Y + 130);
            m_GText.OnDraw(g);
            for (int i = 0; i < 12; i++)
            {
                m_GButton[i].OnDraw(g);
            }
        }

        public override bool OnMouseDown(Point point)
        {
            return m_GButton.Any(a => a.OnMouseDown(point));
        }
        public override bool OnMouseUp(Point point)
        {
            return m_GButton.Any(a => a.OnMouseUp(point));
        }

        /// <summary>
        /// 密码填写
        /// -ycl-
        /// </summary>
        /// <param name="keyId"></param>
        private void DriverIdContent(int keyId)
        {
            if (m_DriverId.Length < 4)
            {
                m_DriverId += (keyId).ToString();
                m_GText.Text += "*";
            }
        }

        /// <summary>
        /// 密码清除
        /// -ycl-
        /// </summary>
        private void DriverIdClear()
        {
            m_DriverId = string.Empty;
            m_GText.Text = "";
        }

        private void RecordInput(int input)
        {
            DriverIdContent(input);
        }

        private void ClearInput()
        {
            DriverIdClear();
        }

    }
}
