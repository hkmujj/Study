using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C6SystemInfo : baseClass
    {
        public static String m_DriverID = String.Empty;

        public override string GetInfo()
        {
            return "系统信息（最下一栏）";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            return true;
        }

        public override void paint(Graphics g)
        {
            g.DrawString(
                "日期",
                Global.m_FontArial11B,
                Brushes.White,
                new RectangleF(6, 563, 164, 30),
                Global.m_SfNc
                );
            g.DrawString(
                (DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString()),
                Global.m_FontArial11B,
                Global.m_SbBlue,
                new RectangleF(48, 563, 164 - 42, 30),
                Global.m_SfCc
                );

            g.DrawString(
                "时间",
                Global.m_FontArial11B, 
                Brushes.White,
                new RectangleF(177, 563, 107, 30),
                Global.m_SfNc
                );
            g.DrawString(
                DateTime.Now.Hour.ToString()+":"+DateTime.Now.Minute.ToString(),
                Global.m_FontArial11B,
                Global.m_SbBlue,
                new RectangleF(177 + 42, 563, 107 - 42, 30),
                Global.m_SfCc
                );

            g.DrawString(
                "司机",
                Global.m_FontArial11B,
                Brushes.White,
                new RectangleF(290, 563, 107, 30),
                Global.m_SfNc
                );
            g.DrawString(
                m_DriverID,//FloatList[UIObj.InFloatList[1]].ToString(),
                Global.m_FontArial11B,
                Global.m_SbBlue,
                new RectangleF(290 + 42, 563, 107 - 42, 30),
                Global.m_SfCc
                );

            g.DrawString(
                "车次",
                Global.m_FontArial11B,
                Brushes.White,
                new RectangleF(413, 563, 170, 30),
                Global.m_SfNc
                );
            g.DrawString(
                FloatList[UIObj.InFloatList[2]].ToString(),
                Global.m_FontArial11B,
                Global.m_SbBlue,
                new RectangleF(413 + 42, 563, 170 - 42, 30),
                Global.m_SfCc
                );

            g.DrawString(
                "编组",
                Global.m_FontArial11B,
                Brushes.White,
                new RectangleF(589, 563, 76, 30),
                Global.m_SfNc
                );
            g.DrawString(
                FloatList[UIObj.InFloatList[3]].ToString(),
                Global.m_FontArial11B,
                Global.m_SbBlue,
                new RectangleF(589 + 42, 563, 76 - 42, 30),
                Global.m_SfCc
                );

            base.paint(g);
        }
    }
}
