using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.停靠站界面;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class StopStaionButon:NewQBaseclass
    {
        readonly StopStation m_Stopstion = new StopStation();
        private readonly List<Region> m_Regions = new List<Region>();
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            m_Stopstion.Init();
            Inita();
            return true;
        }
        public override bool mouseDown(Point point)
        {
            for (int i=0;i<4;i++)
            {
                if (m_Regions[i].IsVisible(point))
                {
                    for (int j=0;j<4;j++)
                    {
                        StopStation.m_DownId[j] = false;
                    }                
                    StopStation.m_DownId[i] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_Regions[i].IsVisible(point))
                {
                    //StopStation.DownId[i] = false;
                }
            }
            int index = 0;
            for(; index< 3; index++)
            {
                if (StopStation.m_DownId[2])
                {
                    if (m_Regions[index + 4].IsVisible(point))
                    {
                        switch (index)
                        {
                            case 1:
                                if (StopStation.m_BypasedIdList[0]<StopStation.m_BypasedIdList[StopStation.m_BypassedId])
                                {

                                    StopStation.m_BypassedId--;
                                }
                                break;
                            case 2:
                                if (StopStation.m_BypasedIdList[StopStation.m_BypassedId]<StopStation.m_BypasedIdList[StopStation.m_BypasedIdList.Count-1])
                                {
                                    StopStation.m_BypassedId++;
                                }
                                break;
                            case 3:
                                append_postCmd(CmdType.SetBoolValue, StopStation.m_BypassedId+651, 0, 1);
                                break;
                        }

                    }
                }
            if (StopStation.m_DownId[3])
                {
                    if (m_Regions[index + 4].IsVisible(point))
                    {
                        switch (index)
                        {
                           case 1:
                                if (StopStation.m_Staionslist[0]<StopStation.m_Staionslist[StopStation.m_StationId])
                                {

                                    StopStation.m_StationId--;
                                }
                                break;
                            case 2:
                                if (StopStation.m_Staionslist[StopStation.m_StationId]<StopStation.m_Staionslist[StopStation.m_Staionslist.Count-1])
                                {
                                    StopStation.m_StationId++;
                                }
                                break;
                            case 3:
                                append_postCmd(CmdType.SetBoolValue, StopStation.m_BypassedId + 691, 0, 1);
                                break;

                        }
                    }
                }
            }
                return true;
        }

        private void Inita()
        {
            for (int i=0;i<4;i++)
            {
                m_Regions.Add(new Region(m_Stopstion.m_Rects[i]));
            }
            //
            for (int i=0;i<3;i++)
            {
                m_Regions.Add(new Region(m_Stopstion.m_Rects[5 + i]));
            }
          
        }

    }
}
