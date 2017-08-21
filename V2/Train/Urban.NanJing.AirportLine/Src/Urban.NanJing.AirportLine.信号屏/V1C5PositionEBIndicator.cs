using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class V1C5PositionEBIndicator : baseClass
    {
        public override string GetInfo()
        {
            return "位置与EB指示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {

            return true;
        }
        private Brush[] m_Brushes = new Brush[] { Brushes.Lime, Brushes.Red };
        public override void paint(Graphics g)
        {
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    g.DrawString(
                        "位置",
                        Global.m_FontArial13B,
                        m_Brushes[i],
                        new RectangleF(504, 112, 142, 48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            if (BoolList[UIObj.InBoolList[1] + 1])
            {
                g.DrawString(
                    "紧急制动",
                    Global.m_FontArial13B,
                    Brushes.Red,
                    new RectangleF(650, 112, 142, 48),
                    Global.m_SfCc
                    );
            }

            //for (int i = 0; i < 2; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[1] + i])
            //    {
            //        g.DrawString(
            //            "紧急制动",
            //            Global.Font_Arial_13_B,
            //            brushes[i],
            //            new RectangleF(650, 112, 142, 48),
            //            Global.SF_CC
            //            );
            //        break;
            //    }
            //}

            //车门打开模式
            String[] stringsDoor = new[] {"自动", "手动"};
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    g.DrawString(
                        stringsDoor[i],
                        Global.m_FontArial13B,
                        Brushes.Lime,
                        new RectangleF(504, 165, 142, 48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            //牵引制动指示
            String[] strings = new[] { "牵引", "制动","惰行" };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    g.DrawString(
                        strings[i],
                        Global.m_FontArial13B,
                        Brushes.Lime,
                        new RectangleF(650, 165, 142, 48),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            base.paint(g);
        }
    }
}
