using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class EventSatusBar : HMIBase
    {
        private readonly Rectangle m_BarRect = new Rectangle(1, 471, 699, 62);

        private readonly Point[] m_Points =
        {
            new Point(61, 471),
            new Point(61, 533),
            new Point(148, 471),
            new Point(148, 533),
            new Point(598, 471),
            new Point(598, 533),
            new Point(599, 471),
            new Point(599, 533)
        };

        private readonly Point m_CarNamePoint = new Point(5, 500);
        private readonly Point m_EventCodePoint = new Point(65, 500);
        private readonly Point m_DescriptionCotentPoint = new Point(155, 500);
        private readonly Pen m_IntervalLinePen = new Pen(Color.FromArgb(124, 128, 119));

        private GDIButton m_ActiveEventBtn;


        private List<GDIRectText> m_Titles;

        private GDIButton m_AcknownledgeButton;



        public override string GetInfo()
        {
            return "事件状态栏";
        }

        protected override bool Initalize()
        {
            InitalizeTitles();

            m_AcknownledgeButton = ButtonFactory.CreateButton(new Rectangle(600, 471, 104, 63), btn =>
            {
                btn.BackImage = null;
                btn.Text = "Acknow - \r\nledge";
                btn.ContentTextControl.DrawFont = GdiCommon.Txt12Font;
                btn.ContentTextControl.TextBrush = GdiCommon.DarkBlueBrush;
                btn.ButtonDownEvent = AckEvent;
            });

            m_ActiveEventBtn = ButtonFactory.CreateButton(new Rectangle(702, 470, 97, 62),
                btn =>
                {
                    btn.ButtonDownEvent = ActiveEventButtonDownEvent;
                    btn.RefreshAction = ActiveEventBtnRefreshAction;
                });

            UIObj.InBoolList.AddRange(EventManager.Instance.AllEvent.Values.Select(s => s.LogicIndex));

            return true;
        }

        private void AckEvent(object sender, EventArgs eventArgs)
        {
            EventManager.Instance.AcknownleageCurrentActivingEventIfHas();
        }

        private void InitalizeTitles()
        {
            m_Titles = new List<GDIRectText>()
            {
                new GDIRectText()
                {
                    Text = "Car",
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt10Font,
                    TextBrush = GdiCommon.EventTitleBrush,
                    OutLineRectangle = new Rectangle(5, 470, 62, 26)
                },
                new GDIRectText()
                {
                    Text = "Event code",
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt10Font,
                    TextBrush = GdiCommon.EventTitleBrush,
                    OutLineRectangle = new Rectangle(65, 470, 85, 26)
                },
                new GDIRectText()
                {
                    Text = "Description",
                    BackColorVisible = false,
                    DrawFont = GdiCommon.Txt10Font,
                    TextBrush = GdiCommon.EventTitleBrush,
                    OutLineRectangle = new Rectangle(155, 470, 444, 26)
                },
            };
        }

        private void ActiveEventBtnRefreshAction(object o)
        {
            var btn = (GDIButton) o;
            var btnImg = Images[1];
            if (EventManager.Instance.ActivedEventCollection.Any())
            {
                btnImg = Images[2];
            }
            btn.BackImage = btnImg;
        }

        private void ActiveEventButtonDownEvent(object sender, EventArgs eventArgs)
        {
            if (
                EventManager.Instance.ActivedEventCollection.Select(s => s.Value.EventInfo.Priority)
                    .Any(
                        a =>
                            !a.IsFullScreenEvent()))
            {
                AcitveEvents.Instance.SetFilter();
                AcitveEvents.Instance.GotoFirstPage();
                if (GlobleParam.Instance.CurrentIranViewIndex == IranViewIndex.ActiveEvents)
                {
                    AcitveEvents.Instance.RefreshEvents();
                }
                else
                {
                    ChangedPage(IranViewIndex.ActiveEvents);
                }
            }
        }

        public override bool mouseDown(Point point)
        {
            return m_ActiveEventBtn.OnMouseDown(point) || m_AcknownledgeButton.OnMouseDown(point);
        }

        public override void paint(Graphics g)
        {
            m_ActiveEventBtn.OnPaint(g);

            var currentEvent = EventManager.Instance.GetCurrentActivableEvent();
            if (currentEvent == null)
            {
                return;
            }

            m_Titles.ForEach(e => e.OnDraw(g));

            DrawCurrentEvent(g, currentEvent);

            m_AcknownledgeButton.OnPaint(g);
        }

        private void DrawCurrentEvent(Graphics g, EventLife currentEvent)
        {
            DrawBarBackgroud(g, currentEvent);

            DrawCurrentEventContent(g, currentEvent);
        }

        private void DrawCurrentEventContent(Graphics g, EventLife currentEvent)
        {
            g.DrawString(currentEvent.EventInfo.CarName, GdiCommon.Txt12Font,
                GdiCommon.DarkBlueBrush, m_CarNamePoint);

            g.DrawString(currentEvent.EventInfo.EventCode.ToString(), GdiCommon.Txt12Font,
                GdiCommon.DarkBlueBrush, m_EventCodePoint);

            g.DrawString(currentEvent.EventInfo.Description, GdiCommon.Txt12Font,
                GdiCommon.DarkBlueBrush, m_DescriptionCotentPoint);
        }

        private void DrawBarBackgroud(Graphics g, EventLife currentEvent)
        {
            var tempBrush = GetCurrenContentBrushByEvent(currentEvent);

            g.FillRectangle(tempBrush, m_BarRect);
            g.DrawLine(m_IntervalLinePen, m_Points[0], m_Points[1]);
            g.DrawLine(m_IntervalLinePen, m_Points[2], m_Points[3]);
            g.DrawLine(m_IntervalLinePen, m_Points[4], m_Points[5]);
            g.DrawLine(GdiCommon.DarkBluePen, m_Points[6], m_Points[7]);
        }

        private Brush GetCurrenContentBrushByEvent(EventLife currentEvent)
        {
            Brush tempBrush;

            switch (currentEvent.EventInfo.Color)
            {
                case EventColor.Red:
                    tempBrush = GdiCommon.RedBrush;
                    break;
                case EventColor.Yellow:
                    tempBrush = GdiCommon.YellowBrush;
                    break;
                case EventColor.White:
                    tempBrush = GdiCommon.WhiteBrush;
                    break;
                default:
                    return null;
            }
            return tempBrush;
        }
    }
}