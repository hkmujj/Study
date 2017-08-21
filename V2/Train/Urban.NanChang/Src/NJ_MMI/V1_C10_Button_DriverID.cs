using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    public class MessageInfo
    {
        public Int32 LogicID { get; set; }
        public String Time { get; set; }
        public String Description { get; set; }

        public bool IsOK { get; set; }
    }

    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C10_Button_DriverID : baseClass
    {

        private Button _btn_Driver;
        public override string GetInfo()
        {
            return "司机ID按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            this._btn_Driver = new Button(new Rectangle(683, 500, 95, 35), null, null);
            this._btn_Driver.MouseUpEvent += _btn_Driver_MouseUpEvent;
            return true;
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_Driver.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString("司机 车", new Font("Arial", 13, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(690, 505));
            base.paint(dcGs);
        }

        void _btn_Driver_MouseUpEvent(int obj)
        {
            append_postCmd(CmdType.ChangePage, 105, 0, 0);
        }
    }
}
