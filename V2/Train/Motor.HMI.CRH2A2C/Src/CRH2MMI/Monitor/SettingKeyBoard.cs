using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CommonUtil.Util;using CommonUtil.Util.Extension;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CommonUtil.Controls;
using CRH2MMI.Common.Util;


namespace CRH2MMI.Monitor
{
    internal sealed class SettingKeyBoard : CommonInnerControlBase
    {
        public EventHandler<KeyBoardNumberPressedEventArgs> NumberPressed;

        public EventHandler<KeyBoardControlPressedEventArgs> ControlPressed;

        private List<CRH2Button> m_AllButtons;

        public Color BkColor { set; get; }

        /// <summary>
        /// 边界到button的间隙
        /// </summary>
        public int Interval { set; get; }

        /// <summary>
        /// 默认大小 
        /// </summary>
        public static readonly Size DefaultSeze;

        private static readonly Size DefaultBtnSize = new Size(50,50);

        private const int DefaultInterval = 10;

        static SettingKeyBoard()
        {
            DefaultSeze = new Size(DefaultBtnSize.Width * 3 + 2 * DefaultInterval, DefaultInterval*2 + DefaultBtnSize.Height*5);
        }

        public SettingKeyBoard()
        {
            BkColor = CRH2Resource.WWBrush.Color;
            Interval = DefaultInterval;
            Size = DefaultSeze;

            Init();

            OutLineChanged += OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            var scalx = (float) Size.Width/DefaultSeze.Width;
            var scaly = (float) Size.Height/DefaultSeze.Height;
            var scalMat = new Matrix();
            scalMat.Scale(scalx, scaly);
            var mat = new Matrix();
            mat.Translate(Location.X, Location.Y);
            mat.Scale(scalx, scaly);

            m_AllButtons.ForEach(e => e.OutLineRectangle = new Rectangle(mat.TransformPoint(e.Location),new Size( scalMat.TransformPoint(new Point(e.Size.Width, e.Size.Height)))));

        }

        public override void Init()
        {
            m_AllButtons = new List<CRH2Button>();
            var point = new Point(Location.X + Interval, Location.Y + Interval);
            var hoffset = 0;
            for (int i = 7; i > 0; i-=3)
            {
                for (int offset = 0; offset < 3; offset++)
                {
                    var number = i + offset;
                    m_AllButtons.Add(new CRH2Button()
                    {
                        OutLineRectangle =
                            new Rectangle(point.X + offset*DefaultBtnSize.Width, point.Y + hoffset* DefaultBtnSize.Height, DefaultBtnSize.Width,
                                DefaultBtnSize.Height),
                        TextColor = Color.White,
                        Text = number.ToString(),
                        DownImage = GlobalResource.Instance.BtnDownImage,
                        UpImage = GlobalResource.Instance.BtnUpImage,
                        ButtonDownEvent =
                            (sender, args) =>
                                HandleUtil.OnHandle(NumberPressed, this, new KeyBoardNumberPressedEventArgs(number)),
                    });
                }
                ++hoffset;
            }

            m_AllButtons.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X, point.Y + 3 * DefaultBtnSize.Height, DefaultBtnSize.Width,
                    DefaultBtnSize.Height),
                TextColor = Color.White,
                Text = "0",
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(NumberPressed, this, new KeyBoardNumberPressedEventArgs(0)),
            });

            m_AllButtons.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X + DefaultBtnSize.Width, point.Y + 3 * DefaultBtnSize.Height, DefaultBtnSize.Width * 2,
                    DefaultBtnSize.Height),
                TextColor = Color.White,
                Text = "删  除",
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ControlPressed, this,new KeyBoardControlPressedEventArgs(KeyBoardControlType.Delete)),
            });

            m_AllButtons.Add(new CRH2Button()
            {
                OutLineRectangle =
                    new Rectangle(point.X + DefaultBtnSize.Width, point.Y + 4 * DefaultBtnSize.Height, DefaultBtnSize.Width * 2,
                    DefaultBtnSize.Height),
                TextColor = Color.White,
                Text = "设  定",
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                ButtonDownEvent =
                    (sender, args) =>
                        HandleUtil.OnHandle(ControlPressed, this, new KeyBoardControlPressedEventArgs(KeyBoardControlType.Set)),
            });
        }

        public override void OnDraw(Graphics g)
        {
            g.FillRectangle(CRH2Resource.WWBrush, OutLineRectangle);

            m_AllButtons.ForEach(e => e.OnPaint(g));
        }

        public override bool OnMouseDown(Point point)
        {
            return m_AllButtons.Any(a => a.OnMouseDown(point));
        }
    }
}
