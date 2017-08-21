using System;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Clock;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class StandbyView : HMIBase
    {
        public override string GetInfo()
        {
            return "´ý»ú";
        }

        private readonly ClockControl m_ClockControl;
        public static bool IsJump { get; set; }

        public StandbyView()
        {
            m_ClockControl = new ClockControl()
            {
                OutLineRectangle = Rectangle.Inflate(new Rectangle(400, 300, 0, 0), 150, 150)
            };
            m_ClockControl.Init();
        }

        public override void paint(Graphics g)
        {
            m_ClockControl.OnPaint(g);
        }

        public override bool mouseUp(Point point)
        {
            if (!IsJump)
            {
                ChangedPage(IranViewIndex.StartLogin);
            }

            return true;
        }
    }
}