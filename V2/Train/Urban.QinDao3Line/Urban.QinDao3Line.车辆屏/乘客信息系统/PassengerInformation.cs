using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class PassengerInformation : NewQBaseclass
    {
        private List<string> m_Dictionary;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            PassengerData.InitData();

            m_Dictionary = new List<string>()
            {
                string.Empty,                                                    //0
                ResourceFacade.MaintenanceResourcePassengerInformation,          //1    维护：乘客信息系统
                ResourceFacade.MaintenancePassengerInformationPISState,          //2    PIS状态
                ResourceFacade.MaintenanceResourceHostAndGuest,                  //3    主/从
                ResourceFacade.MaintenanceTractionResourceMVBCorrespond,         //4    MVB通信
                ResourceFacade.MaintenanceTractionResourceCriticalFault,         //5    致命故障
                ResourceFacade.MaintenanceTractionResourceMajorFault,            //6    重大故障
                ResourceFacade.MaintenanceResourceControllerFault,               //7    控制单元故障 
                ResourceFacade.MaintenanceBrakeResourceListoffaults,             //8    故障清单
                "PIDS1",                                                         //9    PIS1
                "PIDS2",                                                         //10   PIS2
                "M","S"
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawRcts(g);
            DrawWords(g);
            DrawStates(g);
        }

        private void DrawRcts(Graphics e)
        {
            for (int i = 0; i < 31; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, PassengerData.Rects[1 + i]);
            }

        }

        private void DrawWords(Graphics e)
        {
            //维护：乘客信息系统
            e.DrawString(m_Dictionary[1], Common.m_Font14B, Common.m_BlackBrush,
                           PassengerData.Rects[0], Common.m_MFormat);
            //PIS状态
            e.DrawString(m_Dictionary[2], Common.m_Font11B, Common.m_BlackBrush,
                           PassengerData.Rects[1], Common.m_MFormat);
            //信息
            for (int i = 0; i < 5; i++)
            {
                e.DrawString(m_Dictionary[3 + i], Common.m_Font11, Common.m_BlackBrush,
                           PassengerData.Rects[2 + i], Common.m_LeftCenterFormat);
            }
            //故障清单
            e.DrawString(m_Dictionary[8], Common.m_Font11B, Common.m_BlackBrush,
                           PassengerData.Rects[7], Common.m_MFormat);
            //PIS1、PIS2
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    e.DrawString(m_Dictionary[9 + j], Common.m_Font11B, Common.m_BlackBrush,
                                               PassengerData.Rects[17 + j + 12 * i], Common.m_MFormat);
                }  
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Dictionary[11 + i], Common.m_Font11, Common.m_BlackBrush,
                                           PassengerData.Rects[19 + i], Common.m_MFormat);
            }  
        }

        private void DrawStates(Graphics e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[2], PassengerData.Rects[21 + i].X + 13,
                            PassengerData.Rects[21 + i].Y + 1, 24,
                            24);
                }
                else
                {
                    if (BoolList[m_BoolIds[8 + i]])
                    {
                        e.DrawImage(m_Imgs[1], PassengerData.Rects[21 + i].X + 13,
                            PassengerData.Rects[21 + i].Y + 1, 24,
                            24);
                    }
                    else
                        e.DrawImage(m_Imgs[0], PassengerData.Rects[21 + i].X + 13,
                            PassengerData.Rects[21 + i].Y + 1, 24,
                            24);
                }
            }
        }
    }
}
