using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Subway.ATC.THALES.VOBC
{
    public class MessageInfo
    {
        public int LogicID { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }

        public bool IsOK { get; set; }
    }

    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C10_Button_DriverID : baseClass
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
            dcGs.DrawString("司机 车", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(690, 505));
            
        }

        void _btn_Driver_MouseUpEvent(int obj)
        {
            append_postCmd(CmdType.ChangePage, 105, 0, 0);
        }
    }
}
