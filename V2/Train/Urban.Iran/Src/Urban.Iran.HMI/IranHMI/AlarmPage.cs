using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class AlarmPage : HMIBase
    {
        private IranViewIndex m_LastPage;

        private GDIRectText m_ViewTitle;

        private List<GDIButton> m_ControlBtns;

        private GDIRectText m_ContentText;

        public static AlarmPage Instance { private set; get; }

        public EventLife Event { set; get; }

        private Tuple<Bitmap, Rectangle>[] m_BackgroudImages;

        private Tuple<string, Point>[] m_TxtPoints;

        private List<GDIRectText> m_GridContentTexts;

        protected override bool Initalize()
        {
            Instance = this;

            m_ViewTitle = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 0, 800, 20),
                Text = Resource_AR.Title0039,
                TextBrush = GdiCommon.MediumGreyBrush,
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = GdiCommon.Txt12Font,
                TextFormat = GdiCommon.CenterFormat,
            };

            m_ContentText = new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 200, 800, 345),
                TextBrush = GdiCommon.MediumGreyBrush,
                NeedDarwOutline = false,
                BackColorVisible = false,
                DrawFont = GdiCommon.Txt12Font,
                TextFormat = new StringFormat() {Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near},
            };

            m_BackgroudImages = new[]
            {
                new Tuple<Bitmap, Rectangle>(new Bitmap(RecPath + "\\frame/eventGrid.jpg"),
                    new Rectangle(0, 92, 800, 93)),
                new Tuple<Bitmap, Rectangle>(new Bitmap(RecPath + "\\frame/eventScrollbar.jpg"),
                    new Rectangle(771, 186, 28, 349)),
                new Tuple<Bitmap, Rectangle>(new Bitmap(RecPath + "\\frame/mark.jpg"), new Rectangle(771, 203, 28, 28)),
            };

            m_TxtPoints = new[]
            {
                new Tuple<string, Point>("Priority", new Point(3, 98)),
                new Tuple<string, Point>("Start", new Point(104, 98)),
                new Tuple<string, Point>("End", new Point(300, 98)),
                new Tuple<string, Point>("Group", new Point(492, 98)),
                new Tuple<string, Point>("Car", new Point(3, 142)),
                new Tuple<string, Point>("Event code", new Point(104, 142)),
                new Tuple<string, Point>("Description", new Point(300, 142)),
            };

            InitalizeControlBtns();

            InitalizeGridContents();

            return true;
        }

        private void InitalizeGridContents()
        {
            m_GridContentTexts = new List<GDIRectText>()
            {
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(1, 98, 102, 40),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.RightFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        string type = string.Empty;
                        switch (Event.EventInfo.Priority)
                        {
                            case EventPriority.AAlarm:
                            case EventPriority.FireAlarm:
                            case EventPriority.Fault:
                                type = "A-Alarm";
                                break;
                            case EventPriority.BAlarm:
                                type = "B-Alarm";
                                break;
                            case EventPriority.Event:
                                type = "Info";
                                break;
                        }
                        txt.Text = type;
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(105, 98, 190, 40),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.RightFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.StartTime.ToString();
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(296, 98, 190, 40),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.RightFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.EndTime.ToString();
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(490, 98, 310, 40),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.CenterFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.EventInfo.Group.GetDescription();
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(1, 138, 102, 46),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.RightFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.EventInfo.CarName;
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(105, 138, 190, 46),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.RightFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.EventInfo.EventCode.ToString();
                    },
                },
                new GDIRectText()
                {
                    OutLineRectangle = new Rectangle(297, 138, 504, 46),
                    Text = Resource_AR.Title0039,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    NeedDarwOutline = false,
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt12Font,
                    TextFormat = GdiCommon.CenterFormat,
                    RefreshAction = o =>
                    {
                        var txt = (GDIRectText) o;
                        txt.Text = Event.EventInfo.Description;
                    },
                },
            };
        }

        private void InitalizeControlBtns()
        {
            m_ControlBtns = new List<GDIButton>()
            {
                ButtonFactory.CreateShadowButton(new Rectangle(2, 536, 97, 62), "1",
                    btn => { btn.ButtonUpEvent = (sender, args) => m_ContentText.Text = Event.EventInfo.MsgPage1; }),
                ButtonFactory.CreateShadowButton(new Rectangle(102, 536, 97, 62), "2",
                    btn => { btn.ButtonUpEvent = (sender, args) => m_ContentText.Text = Event.EventInfo.MsgPage2; }),
                ButtonFactory.CreateShadowButton(new Rectangle(302, 536, 97, 62), Resource_AR.Button0015,
                    btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(m_LastPage);
                    }),
                ButtonFactory.CreateShadowButton(new Rectangle(402, 536, 97, 62), Resource_AR.Button0014,
                    btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) =>
                        {
                            EventManager.Instance.AcknownleageAllActivingEvents();
                            ChangedPage(IranViewIndex.Doors);
                        };

                    }),

                ButtonFactory.CreateShadowButton(new Rectangle(702, 536, 97, 62),
                    btn =>
                    {
                        btn.ButtonUpEvent = (sender, args) => ChangedPage(IranViewIndex.ActiveEvents);
                        // "Active events"
                        btn.BackImage = Images[0];
                    }),
            };
        }

        public override string GetInfo()
        {
            return "AlarmPage";
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView,
            IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {

                if (Event != null)
                {
                    m_ContentText.Text = Event.EventInfo.MsgPage1;
                    Train.Instance.ShowingEvents.Add(Event);
                }

                var temp = (int) lastView;
                if (temp != 39)
                {
                    m_LastPage = (IranViewIndex) lastView;
                }
            }
        }

        public override void paint(Graphics g)
        {
            m_ViewTitle.OnDraw(g);

            foreach (var backgroudImage in m_BackgroudImages)
            {
                g.DrawImage(backgroudImage.Item1, backgroudImage.Item2);
            }

            m_ControlBtns.ForEach(e => e.OnDraw(g));

            DrawGridTitles(g);

            if (Event == null)
            {
                return;
            }

            DrawEventGridContent(g, Event);

            m_ContentText.OnDraw(g);

        }

        private void DrawEventGridContent(Graphics g, EventLife current)
        {

            m_GridContentTexts.ForEach(e => e.OnPaint(g));
        }

        private void DrawGridTitles(Graphics g)
        {
            foreach (var txtPoint in m_TxtPoints)
            {
                g.DrawString(txtPoint.Item1, GdiCommon.Txt10Font, GdiCommon.MediumGreyBrush, txtPoint.Item2);
            }
        }

        public override bool mouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseUp(point));
        }
    }
}