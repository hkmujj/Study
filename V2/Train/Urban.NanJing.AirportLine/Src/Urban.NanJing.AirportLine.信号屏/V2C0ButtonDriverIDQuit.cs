using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2C0ButtonDriverIDQuit : baseClass
    {
        private Button1 m_BtnDriver;
        public override string GetInfo()
        {
            return "司机ID按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_BtnDriver = new Button1(new Rectangle(683, 500, 95, 35), null, null);
            m_BtnDriver.MouseUpEvent += _btn_Driver_MouseUpEvent;
            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_BtnDriver.MouseUp(point);
            return base.mouseUp(point);
        }

        void _btn_Driver_MouseUpEvent(int obj)
        {
            append_postCmd(CmdType.ChangePage, 104, 0, 0);
        }
    }
}