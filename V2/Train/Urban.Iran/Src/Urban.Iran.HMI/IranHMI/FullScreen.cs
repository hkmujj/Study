using System.Drawing;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class FullScreen : HMIBase
    {
        private  Bitmap[] m_BmpArr;
        private  Rectangle[] m_RectArr;

        private IranViewIndex m_LastViewIndex;

        public override string GetInfo()
        {
            return "FullScreen";
        }

        protected override bool Initalize()
        {
            m_BmpArr = new[]
                     {
                         new Bitmap(RecPath + "\\frame/fsErr.jpg"),
                         new Bitmap(RecPath + "\\frame/fsInfo.jpg")
                     };
            m_RectArr = new[]
                      {
                          new Rectangle(150, 75, 500, 60),
                          new Rectangle(350, 160, 300, 200)
                      };

            return true;
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView, IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                m_LastViewIndex = (IranViewIndex) lastView;
            }
        }

        public override void paint(Graphics g)
        {
            var info = GetEventInfo();

            if (info == null)
            {
                ChangedPage(m_LastViewIndex);
            }
            else
            {
                g.DrawImage((info.Priority == EventPriority.Fault) ? m_BmpArr[0] : m_BmpArr[1], GlobleRect.m_BKRect);
                g.DrawString(info.MsgPage1, GdiCommon.Txt14Font, GdiCommon.DarkBlueBrush, m_RectArr[0], GdiCommon.CenterFormat);
                g.DrawString(info.MsgPage2, GdiCommon.Txt14Font, GdiCommon.DarkBlueBrush, m_RectArr[1], GdiCommon.CenterFormat);
            }
        }

        private EventInfo GetEventInfo()
        {
            EventInfo info = null;
            foreach (var temp in EventManager.Instance.ActivedEventCollection.Select(kvp => EventManager.Instance.AllEvent[kvp.Key]))
            {
                if (temp.Priority == EventPriority.Fault)
                {
                    info = temp;
                    break;
                }
                if (temp.Priority == EventPriority.Information)
                {
                    info = temp;
                }
            }
            return info;
        }
    }
}

