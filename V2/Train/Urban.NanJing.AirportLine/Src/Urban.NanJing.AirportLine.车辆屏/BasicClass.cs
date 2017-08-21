using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BasicClass : baseClass
    {
        public static IReadOnlyDictionary<int, bool> m_Boolsortedlist;
        public static IReadOnlyDictionary<int, float> m_FloatSortedList;

        public static Image[] m_StateColorImageArray;
        public static Image[] m_SwitchImageArray;
        public static Image[] m_PanSwitchImageArray;
        public static Image[] m_EventImageArray;

        public static DateTime m_StartTime;
        public override string GetInfo()
        {
            return "基础类";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_Boolsortedlist = BoolList;
            m_FloatSortedList = FloatList;

            m_StateColorImageArray = new Image[2];
            m_SwitchImageArray = new Image[2];
            m_PanSwitchImageArray = new Image[2];
            m_EventImageArray = new Image[3];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    if (i < 2)
                    {
                        m_StateColorImageArray[i] = Image.FromStream(fs);
                    }

                    else if (i < 4)
                    {
                        m_SwitchImageArray[i - 2] = Image.FromStream(fs);
                    }
                    else if (i < 6)
                    {
                        m_PanSwitchImageArray[i - 4] = Image.FromStream(fs);
                    }
                    else if (i < 9)
                    {
                        m_EventImageArray[i - 6] = Image.FromStream(fs);
                    }

                }
            }
            return true;
        }

        private float m_OldViewIndex = 1;

        private bool Switch
        {
            get
            {
                return m_Switch;
            }
            set
            {
                if (m_Switch == value)
                {
                    return;
                }
                m_Switch = value;
                if (!m_Switch)
                {
                    append_postCmd(CmdType.ChangePage, 103, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.ChangePage, (int)m_OldViewIndex, 0, 0);
                }
            }
        }
        private bool m_Switch;


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
                if ((int)nParaC != 103 && nParaB != 103)
                    m_OldViewIndex = nParaB;
            Switch = BoolList[300];
        }

        /// <summary>
        /// 刷新故障
        /// </summary>
        private void GetEventInfo()
        {
            foreach (var v in EventView.m_FaultEventDictionary)
            {
                if (BoolList[v.Key])
                {
                    if (EventView.m_CurrentEventList.Contains(v.Value))
                    {
                        continue;
                    }
                    v.Value.Affirm = false;
                    EventView.m_CurrentEventList.Add(v.Value);
                    if ((int)v.Value.EventFaultLevel > EventView.m_FaultLevel)
                    {
                        EventView.m_FaultLevel = (int)v.Value.EventFaultLevel;
                    }
                }
                else
                {
                    if (!EventView.m_CurrentEventList.Contains(v.Value))
                    {
                        continue;
                    }
                    if (!v.Value.Affirm)
                    {
                        continue;
                    }
                    EventView.m_CurrentEventList.Remove(v.Value);
                    if ((int)v.Value.EventFaultLevel != EventView.m_FaultLevel)
                    {
                        continue;
                    }
                    EventView.m_FaultLevel = 0;
                    EventView.m_CurrentEventList.ForEach((item) =>
                    {
                        if ((int)item.EventFaultLevel > EventView.m_FaultLevel)
                        {
                            EventView.m_FaultLevel = (int)item.EventFaultLevel;
                        }
                    });
                }
            }
        }

        public override void paint(Graphics g)
        {
            GetEventInfo();
        }
    }
}