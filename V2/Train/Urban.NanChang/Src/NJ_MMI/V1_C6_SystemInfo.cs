using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C6_SystemInfo : baseClass
    {
        public static String DriverID = "---";

        public override string GetInfo()
        {
            return "系统信息（最下一栏）";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            Font f = new Font("Arial", 11, FontStyle.Bold);
            dcGs.DrawString((DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString()+"."+DateTime.Now.Day.ToString()), f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(70, 562));
            dcGs.DrawString(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString(), f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(230, 562));
            //FloatList[UIObj.InFloatList[1]].ToString()
            dcGs.DrawString(DriverID, f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(365, 562));
            dcGs.DrawString(FloatList[UIObj.InFloatList[2]].ToString(), f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(453, 562));
            dcGs.DrawString("6", f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(554, 562));
            dcGs.DrawString(FloatList[UIObj.InFloatList[4]].ToString(), f, new SolidBrush(Color.FromArgb(0, 204, 255)), new PointF(643, 562));
            base.paint(dcGs);
        }
    }
}
