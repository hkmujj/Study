using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;


namespace CRH2MMI.TrainLineNo
{
    class TrainLineSetKeyboard : CommonInnerControlBase
    {
        public EventHandler<ContentPressedEventArgs<Char>> CharContentClick;

        public EventHandler<ContentPressedEventArgs<int>> NumberContentClick;

        public EventHandler<ControlPressedEventArgs> ControlClick;

        static TrainLineSetKeyboard()
        {
            DefaultSeze = new Size(DefaultBtnSize.Width * 11 + 6 * DefaultInterval, DefaultInterval * 2 + DefaultBtnSize.Height * 5);
        }

        public TrainLineSetKeyboard()
        {
            m_BkBrush = new SolidBrush(CRH2Resource.WWBrush.Color);

            OutLineRectangle = new Rectangle(0, 0, DefaultSeze.Width, DefaultSeze.Height);

            Init();

            OutLineChanged = OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var mat = LocationChangeMatrix.Clone();
            mat.Multiply(SizeChangeMatrix);

            m_AllBtns.ForEach(e => e.OutLineRectangle = new Rectangle(mat.TransformPoint(e.Location), new Size(SizeChangeMatrix.TransformPoint(new Point(e.Size.Width, e.Size.Height)))));

        }


        public Color BkColor
        {
            set { m_BkBrush.Color = value; }
            get { return m_BkBrush.Color; }
        }

        public const int DefaultInterval = 20;

        /// <summary>
        /// 默认大小 
        /// </summary>
        public static readonly Size DefaultSeze;

        private static readonly Size DefaultBtnSize = new Size(50, 50);
        private readonly SolidBrush m_BkBrush;
        private List<CRH2Button> m_AllBtns;

        public override void Init()
        {
            m_AllBtns = new List<CRH2Button>();
            var point = new Point(DefaultInterval, DefaultSeze.Height - DefaultInterval - DefaultBtnSize.Height);

            InitChars(point);

            point = new Point(DefaultInterval * 3 + DefaultBtnSize.Width * 6, DefaultSeze.Height - DefaultInterval - DefaultBtnSize.Height);
            InitNumbers(point);

            InitControls();

            var font = CRH2Resource.Font13;
            m_AllBtns.ForEach(e => e.TextControl.DrawFont = new Font(font.FontFamily, 14, FontStyle.Bold));
        }

        private void InitControls()
        {
            var point = new Point(DefaultInterval * 3 + DefaultBtnSize.Width * 7, DefaultSeze.Height - DefaultInterval - DefaultBtnSize.Height);

            m_AllBtns.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X, point.Y,
                        DefaultBtnSize.Width * 2, DefaultBtnSize.Height),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = "删  除",
                TextColor = Color.White,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ControlClick, sender, new ControlPressedEventArgs(ControlPressedEventArgs.ControlType.Delete)),
            });

            point = new Point(point.X + DefaultBtnSize.Width * 2 + DefaultInterval * 2, point.Y - DefaultBtnSize.Height * 2);

            m_AllBtns.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X, point.Y,
                        DefaultBtnSize.Width, DefaultBtnSize.Height),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = "←",
                TextColor = Color.White,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ControlClick, sender, new ControlPressedEventArgs(ControlPressedEventArgs.ControlType.GotoLeft)),
            });


            m_AllBtns.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X + DefaultBtnSize.Width, point.Y,
                        DefaultBtnSize.Width, DefaultBtnSize.Height),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = "→",
                TextColor = Color.White,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ControlClick, sender, new ControlPressedEventArgs(ControlPressedEventArgs.ControlType.GotoRight)),
            });
        }

        private void InitNumbers(Point point)
        {
            m_AllBtns.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X, point.Y,
                        DefaultBtnSize.Width, DefaultBtnSize.Height),
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                Text = "0",
                TextColor = Color.White,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(NumberContentClick, sender, new ContentPressedEventArgs<int>(0)),
            });

            point = new Point(point.X, point.Y - DefaultBtnSize.Height);
            for (int i = 0; i < 3; i++)
            {
                for (int offset = 0; offset < 3; offset++)
                {
                    var num = i * 3 + offset + 1;

                    m_AllBtns.Add(new CRH2Button()
                    {
                        OutLineRectangle =
                            new Rectangle(point.X + offset * DefaultBtnSize.Width, point.Y - i * DefaultBtnSize.Height,
                                DefaultBtnSize.Width, DefaultBtnSize.Height),
                        DownImage = GlobalResource.Instance.BtnDownImage,
                        UpImage = GlobalResource.Instance.BtnUpImage,
                        Text = num.ToString(),
                        TextColor = Color.White,
                        ButtonDownEvent =
                            (sender, args) =>
                                HandleUtil.OnHandle(NumberContentClick, sender, new ContentPressedEventArgs<int>(num)),
                    });

                }
            }
        }

        private void InitChars(Point point)
        {
            const char ca = 'A';
            for (int i = 0; i < 5; i++)
            {
                for (int offset = 0; offset < 6; offset++)
                {
                    var cur = (char)(ca + offset + i * 6);
                    if (cur > 'Z')
                    {
                        return;
                    }
                    m_AllBtns.Add(new CRH2Button()
                    {
                        OutLineRectangle =
                            new Rectangle(point.X + offset * DefaultBtnSize.Width, point.Y - i * DefaultBtnSize.Height,
                                DefaultBtnSize.Width, DefaultBtnSize.Height),
                        DownImage = GlobalResource.Instance.BtnDownImage,
                        UpImage = GlobalResource.Instance.BtnUpImage,
                        Text = cur.ToString(),
                        TextColor = Color.White,
                        ButtonDownEvent =
                            (sender, args) =>
                                HandleUtil.OnHandle(CharContentClick, sender, new ContentPressedEventArgs<char>(cur)),
                    });
                }
            }
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(m_BkBrush, OutLineRectangle);

            m_AllBtns.ForEach(e => e.OnDraw(g));

        }

        public override bool OnMouseDown(Point point)
        {
            return m_AllBtns.Any(a => a.OnMouseDown(point));
        }
    }
}
