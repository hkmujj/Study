using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1C4StationIndicator : baseClass
    {
        private String[] m_DoorStates;
        private String[] m_StationStates;
        private List<String> m_StationList = new List<String>();
        private String m_NextStation = String.Empty;
        public override string GetInfo()
        {
            return "车站指示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_DoorStates = new String[2] { "< ", " >" };
            m_StationStates = new String[4] { "StationHold", "", "StationSkip", "" };
            LoadStationInfo();
            return true;
        }

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadLines(file, Encoding.Default).ToArray();
            m_StationList.Add(all[0]);
            foreach (var cStr in all)
            {
                m_StationList.Add(cStr);
            }
        }
        public override void paint(Graphics g)
        {
            g.DrawString(
                "下一站",
                Global.m_FontArial12B, 
                Brushes.White,
                new RectangleF(504, 346 + 5, 142, 59),
                Global.m_SfCn
                );
            g.DrawString(
                "终点站",
                Global.m_FontArial12B,
                Brushes.White,
                new RectangleF(650, 346 + 5, 142, 59),
                Global.m_SfCn
                );

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    g.DrawString(
                        m_StationStates[i],
                        Global.m_FontArial15B,
                        Brushes.Red,
                        new RectangleF(504, 413, 142, 45),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    g.DrawString(
                        m_StationStates[i + 2],
                        Global.m_FontArial15B,
                        Brushes.Red,
                        new RectangleF(650, 413, 142, 45),
                        Global.m_SfCc
                        );
                    break;
                }
            }

            //if ((Int32) FloatList[UIObj.InFloatList[0]] == 0) return;

            if (m_StationList != null && m_StationList.Count != 0)
            {
                if ((Int32) FloatList[UIObj.InFloatList[0]] != 0)
                {

                    if ((Int32) FloatList[UIObj.InFloatList[0]] < m_StationList.Count)
                    {

                        m_NextStation = m_StationList[(Int32) FloatList[UIObj.InFloatList[0]]];

                        for (int i = 0; i < 2; i++)
                        {
                            if (BoolList[UIObj.InBoolList[0] + i])
                            {
                                if (i == 0)
                                    m_NextStation = "< " + m_NextStation;
                                else
                                    m_NextStation += " >";
                                break;
                            }
                        }


                        g.DrawString(
                            m_NextStation,
                            Global.m_FontArial11B,
                            Global.m_SbBlue,
                            new RectangleF(504, 367, 142, 37),
                            Global.m_SfCc
                            );
                    }
                }

                if ((Int32) FloatList[UIObj.InFloatList[1]] != 0)
                {
                    if ((Int32)FloatList[UIObj.InFloatList[1]] < m_StationList.Count)
                    g.DrawString(
                        m_StationList[(Int32) FloatList[UIObj.InFloatList[1]]],
                        Global.m_FontArial11B,
                        Global.m_SbBlue,
                        new RectangleF(650, 367, 142, 37),
                        Global.m_SfCc
                        );
                }
            }

            base.paint(g);
        }
    }
}
