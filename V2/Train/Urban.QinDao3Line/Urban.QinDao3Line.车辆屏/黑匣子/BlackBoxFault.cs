using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackBoxFault : NewQBaseclass
    {
        private List<string> m_String;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            BlackBoxData.InitData();
            m_String = new List<string>() 
            {
                string.Empty,                                                  //0
                ResourceFacade.MaintenanceResourceMaintenanceBlackBox,         //1     维护：黑匣子
                ResourceFacade.MaintenanceBrakeResourceStates,                 //2     状态
                ResourceFacade.MaintenanceBasicResourceMVBcommunication,       //3     MVB通信
                ResourceFacade.MaintenanceBrakeResourceMajorFault,             //4     重大故障
                ResourceFacade.MaintenanceBrakeResourceMinorFault,             //5     轻微故障
                ResourceFacade.MaintenanceResourceMaintenanceBBO1,             //6     BBO1
                ResourceFacade.MaintenanceResourceMaintenanceBBO2,             //7     BBO2
                ResourceFacade.MaintenanceBrakeResourceListoffaults,           //8     故障清单
                ResourceFacade.MaintenanceBrakeResourceTc1,                    //9     Tc1
                ResourceFacade.MaintenanceBrakeResourceTc2,                    //10    Tc2
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            //维护：黑匣子
            g.DrawString(m_String[1], Common.m_Font14B, Common.m_BlackBrush, SoftWareData.Rects[0], Common.m_MFormat);
            DrawRects(g);
            DrawWords(g);
            DrawImages(g);
        }
        public void DrawRects(Graphics e)
        {
            for (int i = 0; i < 25; i++)
            {
                e.DrawRectangle(Common.m_BlackPen ,BlackBoxData.Rects[7 + i].X, BlackBoxData.Rects[7 + i].Y,
                    BlackBoxData.Rects[7 + i].Width, BlackBoxData.Rects[7 + i].Height);
            }
        }
        public void DrawWords(Graphics e)
        {
            //状态
            e.DrawString(m_String[2], Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[7], Common.m_MFormat);
            //MVB通信等
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_String[3 + i], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[8 + i], Common.m_LeftCenterFormat);
            }
            //BBO1
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[6 + i], Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[11 + i], Common.m_MFormat);
            }
            //故障清单
            e.DrawString(m_String[8], Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[19], Common.m_MFormat);
            //Tc1、Tc2
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[9 + i], Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[29 + i], Common.m_MFormat);
            }
        }
        public void DrawImages(Graphics e)
        {
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[2], BlackBoxData.Rects[13 + i].X + 19, BlackBoxData.Rects[13 + i].Y+1,23,23);
                }
                else
                {
                    if (BoolList[m_BoolIds[i + 6]])
                    {
                        e.DrawImage(m_Imgs[1], BlackBoxData.Rects[13 + i].X + 19, BlackBoxData.Rects[13 + i].Y + 1, 23, 23);
                    }
                    else
                        e.DrawImage(m_Imgs[0], BlackBoxData.Rects[13 + i].X + 19, BlackBoxData.Rects[13 + i].Y + 1, 23, 23);
                }
            }
        }
    }
}


