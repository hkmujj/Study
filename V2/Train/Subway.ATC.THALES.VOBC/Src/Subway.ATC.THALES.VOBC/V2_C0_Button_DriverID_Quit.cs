using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2_C0_Button_DriverID_Quit : baseClass
    {
        private Button _btn_Driver;
        public override string GetInfo()
        {
            return "司机ID按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _btn_Driver = new Button(new Rectangle(683, 500, 95, 35), null, null);
            _btn_Driver.MouseUpEvent += _btn_Driver_MouseUpEvent;
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn_Driver.MouseUp(nPoint);
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString("退出", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(690, 505));
            
        }

        void _btn_Driver_MouseUpEvent(int obj)
        {
            append_postCmd(CmdType.ChangePage, 104, 0, 0);
        }
    }
}
