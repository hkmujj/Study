using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C2Door : baseClass
    {
        private String[] m_DoorStatesTrain;
        private String[] m_DoorStatesStation;
        public override string GetInfo()
        {
            return "门状态";
        }



        public override bool init(ref int nErrorObjectIndex)
        {
            m_DoorStatesTrain = new String[4] { "X", "X", "O", "O" };
            m_DoorStatesStation = new String[4] { "", "X", "O", "" };
            return true;
        }

        private Brush[] m_Brushes = new Brush[]
            {
                Brushes.Lime, 
                Brushes.Lime,
                Brushes.Red,
                Brushes.Red
            };
        public override void paint(Graphics g)
        {
            g.DrawString(
                "列车门",
                Global.m_FontArial12B,
                Brushes.White,
                new RectangleF(504, 218+5, 142, 122),
                Global.m_SfCn
                );
            g.DrawString(
                "站台门",
                Global.m_FontArial12B,
                Brushes.White,
                new RectangleF(650, 218+5, 142, 122),
                Global.m_SfCn
                );

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    g.DrawString(
                        m_DoorStatesTrain[i],
                        Global.m_FontArial30B,
                        m_Brushes[i],
                        new RectangleF(504, 218, 142, 122),
                        Global.m_SfCf
                        );
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    g.DrawString(
                        m_DoorStatesStation[i],
                        Global.m_FontArial30B,
                        m_Brushes[i],
                        new RectangleF(650, 218, 142, 122),
                        Global.m_SfCf
                        );
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    g.DrawString(
                        "使能",
                        Global.m_FontArial12B,
                        Brushes.Lime,
                        new RectangleF(504+146*i, 210, 142, 122),
                        Global.m_SfCc
                        );
                }
            }

            base.paint(g);
        }
    }
}
