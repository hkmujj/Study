using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class PropulsionMaintennance : NewQBaseclass
    {
        private List<string> m_Resources;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            PmData.InitData();
            m_Resources = new List<string>()
            {
                string.Empty,
                ResourceFacade.MaintenanceTractionResourcePCEStates,
                ResourceFacade.MaintenanceTractionResourceMVBCorrespond,
                ResourceFacade.MaintenanceTractionResourceCriticalFault,
                ResourceFacade.MaintenanceTractionResourceMajorFault,
                ResourceFacade.MaintenanceTractionResourceMinorFault,                                  //5
                ResourceFacade.MaintenanceTractionResourceMVBchannelAandchannelBstatus,
                ResourceFacade.MaintenanceTractionResourceListoffaults,
                ResourceFacade.MaintenanceTractionResourcePCE1PCE2PCE3PCE4,
                ResourceFacade.MaintenanceTractionResourceABABABAB,
                ResourceFacade.MaintenanceTractionResourceAccesstoauthonisedmaintenancemodeconditionsfast,    //10
                ResourceFacade.MaintenanceTractionResourcebrakeappliedandvellocitynull,
                ResourceFacade.MaintenanceResourceMaintenanceTraction,
                "PCE1",
                "PCE2",
                "PCE3",         //15
                "PCE4",
                "A B"
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawRects(g);
            DrawWords(g);
            DrawImgs(g);
        }

        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 45; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, PmData.Rects[i]);
            }
        }

        private void DrawWords(Graphics e)
        {
            // PCE状态
            e.DrawString(m_Resources[1], Common.m_Font12B, Common.m_BlackBrush,
                          PmData.Rects[0], Common.m_MFormat);
            //MVB通信、致命故障、重大故障、轻微故障
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Resources[2 + i], Common.m_Font12, Common.m_BlackBrush,
                         PmData.Rects[1 + i]);
            }
            //MVB网络通道A和通道B状态
            e.DrawString(m_Resources[6], Common.m_Font12, Common.m_BlackBrush,
                         PmData.Rects[5], Common.m_MFormat);
            //故障清单
            e.DrawString(m_Resources[7], Common.m_Font12B, Common.m_BlackBrush,
                          PmData.Rects[6], Common.m_MFormat);
            //PCE1 PCE2 PCE3 PCE4
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Resources[13 + i], Common.m_Font12B, Common.m_BlackBrush,
                                           PmData.Rects[16 + i], Common.m_MFormat);
                e.DrawString(m_Resources[13 + i], Common.m_Font12B, Common.m_BlackBrush,
                                           PmData.Rects[40 + i], Common.m_MFormat);
                //A B
                e.DrawString(m_Resources[17], Common.m_Font12, Common.m_BlackBrush,
                                           PmData.Rects[36 + i], Common.m_MFormat);
            }
            //

            //e.DrawString(m_Resources[8], Common.m_Font12B, Common.m_BlackBrush,
            //           PmData.Rects[10], Common.m_MFormat);
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[10 + i], Common.m_Font8B, Common.m_BlackBrush,
                      PmData.Rects[46 + i]);
            }
            //
            e.DrawString(m_Resources[12], Common.m_Font12B, Common.m_BlackBrush,
                     PmData.Rects[45]);
        }

        private void DrawImgs(Graphics e)
        {
            e.DrawImage(m_Imgs[6], PmData.Rects[48]);
            for (int i = 0; i < 4; i++)
            {
                for (int m = 0; m < 4; m++)
                {
                    if (BoolList[m_BoolIds[m + 8 * i]])
                    {
                        e.DrawImage(m_Imgs[7], PmData.Rects[20 + 4 * i + m].X + 13, PmData.Rects[20 + 4 * i + m].Y,
                            25,25);
                    }
                    else
                    {
                        if (BoolList[m_BoolIds[m + 8 * i + 4]])
                        {
                            e.DrawImage(m_Imgs[8], PmData.Rects[20 + 4 * i + m].X + 13, PmData.Rects[20 + 4 * i + m].Y,
                            25, 25);
                        } 
                        else
                            e.DrawImage(m_Imgs[9], PmData.Rects[20 + 4 * i + m].X + 13, PmData.Rects[20 + 4 * i + m].Y,
                            25, 25);
                    }
                }
            }
        }
    }
}
