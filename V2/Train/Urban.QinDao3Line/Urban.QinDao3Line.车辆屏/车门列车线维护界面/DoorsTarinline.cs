using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DoorsTarinline : NewQBaseclass
    {
        private List<string> m_Dictionary;

        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            DoorsTrainlineData.Initdata();

            m_Dictionary = new List<string>()
            {
                String.Empty,
                ResourceFacade.MaintenanceTrainLineResourceDoorclosestatussideA,
                ResourceFacade.MaintenanceTrainLineResourceDoorclosestatussideB,
                ResourceFacade.MaintenanceTrainLineResourceTc1,
                ResourceFacade.MaintenanceTrainLineResourceM1,
                ResourceFacade.MaintenanceTrainLineResourceM2,
                ResourceFacade.MaintenanceTrainLineResourceM3,
                ResourceFacade.MaintenanceTrainLineResourceM4,
                ResourceFacade.MaintenanceTrainLineResourceTc2,
                ResourceFacade.MaintenanceTrainLineResourceRequestrightdooropened,
                ResourceFacade.MaintenanceTrainLineResourceRequestleftdooropened,
                ResourceFacade.MaintenanceTrainLineResourceRequestrightdoorclosed,
                ResourceFacade.MaintenanceTrainLineResourceRequestleftdoorclosed,
                ResourceFacade.MaintenanceTrainLineResourceAuthorizationtoopenrightdoors,
                ResourceFacade.MaintenanceTrainLineResourceAuthorizationtoopenleftdoors,
                ResourceFacade.MaintenanceTrainLineResourceAlldoorsclosedandlocked
            };
            return true;
        }

        public override void paint(Graphics g)
        {
            Fillrects(g);
            DrawRects(g);
            DrawWords(g);
            DrawStates(g);
        }

        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 43; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, DoorsTrainlineData.Rects[i]);
            }

        }

        private void Fillrects(Graphics e)
        {
            for (int i = 0; i < 43; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, DoorsTrainlineData.Rects[i]);
            }
        }

        private void DrawWords(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Dictionary[1 + i], Common.m_Font11, Common.m_BlackBrush, DoorsTrainlineData.Rects[i], Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(m_Dictionary[3 + i], Common.m_Font11, Common.m_BlackBrush,
                    DoorsTrainlineData.Rects[2 + i], Common.m_MFormat);
            }
            for (int i = 0; i < 7; i++)
            {
                e.DrawString(m_Dictionary[9 + i], Common.m_Font11, Common.m_BlackBrush,
                    DoorsTrainlineData.Rects[i + 20], Common.m_LeftCenterFormat);
            }
            //Tc1、Tc2
            for (int i = 0; i < 2;i++ )
            {
                e.DrawString(m_Dictionary[3+5*i], Common.m_Font11, Common.m_BlackBrush,
                    DoorsTrainlineData.Rects[27+i], Common.m_MFormat);
            }
        }

        private void DrawStates(Graphics e)
        {
            //A侧、B侧车门关闭状态
            for (int i = 0; i < 12; i++)
            {
                    if (BoolList[m_BoolIds[i]])
                    {
                        e.DrawImage(m_Imgs[1], DoorsTrainlineData.Rects[8 + i].X+22,
                            DoorsTrainlineData.Rects[8 + i].Y+2,23,23);
                    }
                    else
                    {
                        if(BoolList[m_BoolIds[i+26]])
                        e.DrawImage(m_Imgs[2], DoorsTrainlineData.Rects[8 + i].X + 22,
                            DoorsTrainlineData.Rects[8 + i].Y + 1, 23, 23);
                        else
                            e.DrawImage(m_Imgs[0], DoorsTrainlineData.Rects[8 + i].X + 22,
                                                        DoorsTrainlineData.Rects[8 + i].Y + 1, 23, 23);
                    }
            }
            //开门请求
            for (int i = 0; i < 14; i++)
            {
                if (BoolList[m_BoolIds[12 + i]])
                {
                    e.DrawImage(m_Imgs[1], DoorsTrainlineData.Rects[29 + i].X + 22,
                            DoorsTrainlineData.Rects[29 + i].Y + 1, 23, 23);
                }
                else
                {
                    if (BoolList[m_BoolIds[38 + i]])
                    {
                        e.DrawImage(m_Imgs[2], DoorsTrainlineData.Rects[29 + i].X + 22,
                                DoorsTrainlineData.Rects[29 + i].Y + 1, 23, 23);
                    }
                    else
                    e.DrawImage(m_Imgs[0], DoorsTrainlineData.Rects[29 + i].X + 22,
                            DoorsTrainlineData.Rects[29 + i].Y + 1, 23, 23);
                }        
            }
        }

    }
}
