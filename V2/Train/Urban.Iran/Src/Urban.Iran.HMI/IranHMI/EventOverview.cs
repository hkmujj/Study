using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class EventOverview : HMIBase
    {
        private Bitmap[] m_BmpArr;

        private Rectangle[,] m_EventItemRectangles;

        private List<GDIButton> m_EventItemBtns;

        // ReSharper disable once InconsistentNaming
        private static readonly Color m_TextColor = Color.FromArgb(109, 121, 137);

        public override string GetInfo()
        {
            return "EventOverview";
        }

        protected override bool Initalize()
        {
            InitalizeItemRegions();

            InitalizeEventItemBtns();

            m_BmpArr = new[]
            {
                new Bitmap(RecPath + "\\frame/btnWarning.jpg"),
                new Bitmap(RecPath + "\\frame/btnErr.jpg"),
                new Bitmap(RecPath + "\\frame/btnBkNormal.jpg")
            };

            return true;
        }

        private void InitalizeItemRegions()
        {
            var location = new Point(60, 140);
            var size = new Size(98, 64);
            const int hInterval = 88;
            const int vInterval = 46;
            m_EventItemRectangles = new Rectangle[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var rec = new Rectangle(location.X + j*(hInterval + size.Width),
                        location.Y + i*(vInterval + size.Height), size.Width, size.Height);
                    m_EventItemRectangles[i, j] = rec;
                }
            }
        }

        private void InitalizeEventItemBtns()
        {
            m_EventItemBtns = EventGroupExtension.AllEventGroups.Select(s =>
            {
                var dl = s.GetDisplayLocation();
                return ButtonFactory.CreateShadowButton(m_EventItemRectangles[dl.Horizontal, dl.Vertical], s.GetDescription(),
                    btn =>
                    {
                        // 缓存策略，避免paint 每次都new .
                        btn.Tag = s;
                        btn.TextColor = m_TextColor;
                        btn.RefreshAction = OnBtnRefreshAction;
                        btn.ButtonUpEvent = (sender, args) =>
                        {
                            var b = (GDIButton)sender;
                            var group = (EventGroup) b.Tag;
                            AcitveEvents.Instance.SetFilter(el => el.EventInfo.Group == group);
                            ChangedPage(IranViewIndex.ActiveEvents);;
                        };
                    });
            }).ToList();
        }

        private void OnBtnRefreshAction(object o)
        {
            var btn = (GDIButton) o;
            var group = (EventGroup)  btn.Tag;

            var actived =
                EventManager.Instance.ActivedEventCollection.Where(w => w.Value.EventInfo.Group == group)
                    .Select(s => s.Value.EventInfo.Color);

            if (actived.Contains(EventColor.Red))
            {
                //btn.BtnBehavierStrategy = (IBtnBehavierStrategy) ((object[]) btn.Tag)[1];
                btn.TextColor = Color.White;
                btn.BackImage = m_BmpArr[1];
                if (!btn.IsEnable)
                {
                    btn.IsEnable = true;
                }
            }
            else if (actived.Contains(EventColor.Yellow))
            {
                //btn.BtnBehavierStrategy = (IBtnBehavierStrategy) ((object[]) btn.Tag)[1];
                btn.TextColor = m_TextColor;
                btn.BackImage = m_BmpArr[0];
                if (!btn.IsEnable)
                {
                    btn.IsEnable = true;
                }
            }
            else if (actived.Contains(EventColor.White))
            {
                //Todo 
                btn.TextColor = m_TextColor;
                btn.BackImage = m_BmpArr[0];
                if (!btn.IsEnable)
                {
                    btn.IsEnable = true;
                }
            }
            else 
            {
                //btn.BtnBehavierStrategy = (IBtnBehavierStrategy) ((object[]) btn.Tag)[2];
                btn.BackImage = m_BmpArr[2];
                btn.TextColor = m_TextColor;
                if (btn.IsEnable)
                {
                    btn.IsEnable = false;
                }
            }
        }

        public override void paint(Graphics g)
        {
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 11;
            m_EventItemBtns.ForEach(e => e.OnPaint(g));
        }

        public override bool mouseDown(Point point)
        {
            return m_EventItemBtns.Any(a => a.OnMouseDown(point));
        }

        public override bool mouseUp(Point point)
        {
            return m_EventItemBtns.Any(a => a.OnMouseUp(point));
        }
    }
}