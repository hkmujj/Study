using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FireAlarmView : HMIBase
    {

        private readonly string[] m_Str =
        {
            "TC1 Cab Fire",
            "TC1 Saloon Fire",
            "MP1 Saloon Fire",
            "M Saloon Fire",
            "MP2 Saloon Fire",
            "TC2 Saloon Fire",
            "TC2 Cab Fire"
        };

        private List<Label> m_ConstTexts;

        private Rectangle m_BackRectangle = new Rectangle(HMICommon.ContentRectangle.X + 60, HMICommon.ContentRectangle.Y + 200, 680, 85);

        private List<RectangleImage> m_FireImages;

        private List<GDIButton> m_Buttons;

        private Image[] m_Img;
        private static readonly Timer _timer = new Timer(2000);
        private static readonly Size FireSize = new Size(51, 51);


        protected override bool Initalize()
        {

            m_FireImages = new List<RectangleImage>();
            m_ConstTexts = new List<Label>();
            m_Img = new Image[2];
            m_Img[0] = Image.FromFile(Path.Combine(RecPath, "frame\\btnWarning.jpg"));
            m_Img[1] = Image.FromFile(Path.Combine(RecPath, "frame\\btnBkNormal.jpg"));
            InitalizeBodys();

            InitalizeCarNames();

            InitalizeBtns();

            return true;
        }

        private void InitalizeBtns()
        {
            var size = new Size(97, 62);
            //702, 470, 97, 62
            var location = new Point(702 - 1 - size.Width, 470 - 1 - size.Height);

            m_Buttons = new List<GDIButton>()
            {
                ButtonFactory.CreateShadowButton(new Rectangle(location, size), GlobleParam.ResManagerText.GetString("Button0003")),
                ButtonFactory.CreateShadowButton(new Rectangle(location.Translate(size.Width + 1,0), size),GlobleParam.ResManagerText.GetString("Button0004")),
            };
            m_Buttons[0].ButtonDownEvent += (sender, args) =>
            {
                var btn = sender as GDIButton;
                if (!btn.IsEnable)
                {
                    return;
                }
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
               // m_Buttons[0].BackImage = m_Img[0];
            };
            m_Buttons[0].TextColor = GdiCommon.MediumGreyBrush.Color;
            m_Buttons[1].TextColor = GdiCommon.MediumGreyBrush.Color;
            m_Buttons[0].ButtonUpEvent += (sender, args) =>
            {
                var btn = sender as GDIButton;
                if (!btn.IsEnable)
                {
                    return;
                }
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
               // m_Buttons[0].BackImage = m_Img[1];
            };
            m_Buttons[1].ButtonDownEvent += (sender, args) =>
            {
                var btn = sender as GDIButton;
                if (!btn.IsEnable)
                {
                    return;
                }
          //      m_Buttons[1].BackImage = m_Img[0];
                //_timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                //_timer.Enabled = true;
            };
            m_Buttons[1].ButtonUpEvent += (sender, args) =>
            {
                
            };
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            m_Buttons[1].BackImage = m_Img[1];
        }

        private void InitalizeCarNames()
        {

            var names =
                Enumerable.Range(25, 5)
                    .Select(s => GlobleParam.ResManagerText.GetString(string.Format("Text{0:0000}", s)));

            var regions = GetNameRectangles().GetEnumerator();
            m_ConstTexts.AddRange(names.Select(s =>
            {
                regions.MoveNext();
                return CreateLabel(s, regions.Current);
            }));
        }

        private void InitalizeBodys()
        {
            int txtHeight = 40;

            var cab = GlobleParam.ResManagerText.GetString("Text0023");
            var saloon = GlobleParam.ResManagerText.GetString("Text0024");

            var names = Enumerable.Repeat(saloon, 7).ToArray();
            names[0] = cab;
            names[6] = cab;

            var bodys = Enumerable.Range(1011, 5).Select(s => s.ToString("00000")).ToArray();

            int i = 0;
            var last = Rectangle.Empty;
            foreach (var rectangle in GetLocations())
            {
                last = rectangle;


                m_ConstTexts.Add(CreateLabel(names[i],
                    new Rectangle(rectangle.X, rectangle.Y - txtHeight, rectangle.Width, txtHeight)));
                m_FireImages.Add(
                    CreateFireImageControl(
                        InflateFireRectangle(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height),
                            rectangle.Width), o => InitFireImageAction(o, m_Str[i])));

                if (i > 0 && i < 6)
                {
                    m_ConstTexts.Add(CreateLabel(bodys[i - 1], rectangle, false));
                }

                ++i;
            }

            m_BackRectangle.Width = last.Right - m_BackRectangle.X + 1;
        }

        private void InitFireImageAction(RectangleImage rectangleImage, object tag)
        {
            rectangleImage.Tag = tag;
            rectangleImage.RefreshAction = o =>
            {
                var img = (RectangleImage)o;
                var name = tag as string;
                img.Visible = BoolList[GlobleParam.Instance.FindInBoolIndex(name)];
            };
        }

        private IEnumerable<Rectangle> GetNameRectangles()
        {
            const int txtHeight = 40;

            var bodys = GetLocations().ToArray();

            var bottom = bodys[0].Bottom;

            yield return new Rectangle(bodys[0].X, bottom, bodys[1].Right - bodys[0].Left, txtHeight);

            for (int i = 2; i < 5; i++)
            {
                yield return new Rectangle(bodys[i].X, bottom, bodys[i].Width, txtHeight);
            }

            yield return new Rectangle(bodys[5].X, bottom, bodys[6].Right - bodys[5].Left, txtHeight);
        }

        private IEnumerable<Rectangle> GetLocations()
        {
            var wid1 = 52;
            var wid2 = 89;
            var wid3 = 123;
            var interal1 = 3;
            var interval2 = 7;

            var x = m_BackRectangle.X + 1;
            var y = m_BackRectangle.Y;

            yield return new Rectangle(x, y, wid1, m_BackRectangle.Height);

            x = x + wid1 + interal1;
            yield return new Rectangle(x, y, wid2, m_BackRectangle.Height);

            x = x + wid2 + interval2;

            for (int i = 0; i < 3; i++)
            {
                yield return new Rectangle(x, y, wid3, m_BackRectangle.Height);
                x = x + (wid3 + interval2);
            }

            x = x - interval2 + interal1;
            yield return new Rectangle(x, y, wid2, m_BackRectangle.Height);

            x = x + wid2 + interal1;
            yield return new Rectangle(x, y, wid1, m_BackRectangle.Height);

        }

        private Rectangle InflateFireRectangle(Rectangle rectangle, int width)
        {
            rectangle.Inflate((FireSize.Width - width) / 2, (-m_BackRectangle.Height + FireSize.Height) / 2);
            return rectangle;
        }

        private RectangleImage CreateFireImageControl(Rectangle outline, Action<RectangleImage> initAction = null)
        {
            var contorl = new RectangleImage() { Image = Images[1], OutLineRectangle = outline };

            if (initAction != null)
            {
                initAction(contorl);
            }

            return contorl;
        }

        private Label CreateLabel(string text, Rectangle outline, bool isWhite = true, Action<Label> initAction = null)
        {
            var label = new Label()
            {
                Text = text,
                Brush = isWhite ? GdiCommon.MediumGreyBrush : GdiCommon.DarkBlueBrush,
                Font = GdiCommon.Txt12Font,
                OutLineRectangle = outline
            };

            if (initAction != null)
            {
                initAction(label);
            }

            return label;
        }

        public override void paint(Graphics g)
        {

           // m_Buttons.ForEach(f => f.IsEnable = GlobleParam.Instance.WorkModel != HMIWorkModel.NoActoveDrive);

            g.DrawImage(Images[0], m_BackRectangle);

            m_ConstTexts.ForEach(e => e.OnDraw(g));

            m_FireImages.ForEach(e => e.OnPaint(g));

            m_Buttons.ForEach(e => e.OnDraw(g));
        }
        public override bool mouseDown(Point point)
        {
            this.m_Buttons.ForEach(btn => btn.OnMouseDown(point));
            return true;
        }

        public override bool mouseUp(Point point)
        {
            this.m_Buttons.ForEach(btn => btn.OnMouseUp(point));
            return true;
        }

    }
}