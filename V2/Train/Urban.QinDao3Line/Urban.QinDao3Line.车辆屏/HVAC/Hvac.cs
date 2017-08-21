using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Hvac : NewQBaseclass
    {
        private List<string> m_Dictionary;

        public override bool init(ref int nErrorObjectIndex)
        {
            HvacData.InitData();
            IntiData();
            //ReadConfigcs read = new ReadConfigcs("HVAC", m_Dictionary);
            //read.ReaFile(Path.Combine(RecPath, "..\\Config"));
            m_Dictionary = new List<string>()
            {
                //0-4
                string.Empty,
                ResourceFacade.MaintenanceHVACResourceEquipmentmode,                     
                ResourceFacade.MaintenanceHVACResourceVACswitchsattus,
                ResourceFacade.MaintenanceHVACResourceFreshairtemperature,
                ResourceFacade.MaintenanceHVACResourceReturnairtemperature,
                //5-9
                ResourceFacade.MaintenanceHVACResourceCondsender1,
                ResourceFacade.MaintenanceHVACResourceCondsender2,
                ResourceFacade.MaintenanceHVACResourceEvaporator,
                ResourceFacade.MaintenanceHVACResourceCompressor1highpressure,
                ResourceFacade.MaintenanceHVACResourceCompressor2highpressure,
                //10-14
                ResourceFacade.MaintenanceHVACResourceCompressor1lowpressure,
                ResourceFacade.MaintenanceHVACResourceCompressor2lowpressure,
                ResourceFacade.MaintenanceHVACResourceHeating1contactor,
                ResourceFacade.MaintenanceHVACResourceHeating2contactor,
                ResourceFacade.MaintenanceHVACResourceCompressor1contactor,
                //15-19
                ResourceFacade.MaintenanceHVACResourceCompressor2contactor,
                ResourceFacade.MaintenanceHVACResourceVAC1,
                ResourceFacade.MaintenanceHVACResourceVAC2,
                ResourceFacade.MaintenanceHVACResourceVAC3,
                ResourceFacade.MaintenanceHVACResourceVAC4,
                //20-24
                ResourceFacade.MaintenanceHVACResourceVAC5,
                ResourceFacade.MaintenanceHVACResourceVAC6,
                ResourceFacade.MaintenanceHVACResourceUnit1,
                ResourceFacade.MaintenanceHVACResourceUnit2,
                ResourceFacade.MaintenanceHVACResourceVentilation,
                //25-29
                ResourceFacade.MaintenanceHVACResourceCooling,
                ResourceFacade.MaintenanceHVACResourceEmergencyVentilation ,
                ResourceFacade.MaintenanceHVACResourceOff ,
                ResourceFacade.MaintenanceHVACResourceReduce,
                ResourceFacade.MaintenanceHVACResourceMaintenance,
                //30-34
                ResourceFacade.MaintenanceHVACResourceVACCentralControl,
                ResourceFacade.MaintenanceHVACResourceVACOff,	
                ResourceFacade.MaintenanceHVACResourceVACMaintenance,
                ResourceFacade.MaintenanceHVACResourceVACVentilation,
                ResourceFacade.MaintenanceHVACResourceAutomatic,
                //35-37
                ResourceFacade.MaintenanceHVACResourceVACHalfCooling,
                ResourceFacade.MaintenanceHVACResourceVACFullCooling,
                ResourceFacade.MaintenanceResourceMaintenanceHVAC
            };
            return true;
        }

        public override void paint(Graphics g)
        {
            FillRects(g);
            DrawRects(g);
            DrawWords(g);
            DrawLines(g);
            DrawState(g);
            DrawImage(g);
        }

        private void DrawImage(Graphics g)
        {
            //新风温度传感器
            for (int i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[78 + i]])
                {
                    g.DrawImage(m_Imgs[5], HvacData.Rects[189 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[90 + i]])
                    {
                        g.DrawImage(m_Imgs[3], HvacData.Rects[189 + i]);
                    }
                    else
                        g.DrawImage(m_Imgs[4], HvacData.Rects[189 + i]);
                }
            }
            //回风温度传感器
            for (int i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[102 + i]])
                {
                    g.DrawImage(m_Imgs[5], HvacData.Rects[201 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[114 + i]])
                    {
                        g.DrawImage(m_Imgs[3], HvacData.Rects[201 + i]);
                    }
                    else
                        g.DrawImage(m_Imgs[4], HvacData.Rects[201 + i]);
                }
            }
            //Condsender1状态
            for (int m = 0; m < 11; m++)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (BoolList[m_BoolIds[126 + j + 12 * i+24*m]])
                        {
                            g.DrawImage(m_Imgs[i], HvacData.Rects[57 + j + 12 * m].X + 12, HvacData.Rects[57 + j + 12 * m].Y + 2,
                                HvacData.Rects[57 + j + 12 * m].Width - 24, HvacData.Rects[57 + j + 12 * m].Height - 4);
                        }
                        //相关设备未运行时，显示为灰色
                        if (!BoolList[m_BoolIds[126 + j + 24 * m]] && !BoolList[m_BoolIds[126 + j + 24 * m + 12]])
                        {
                            g.DrawImage(m_Imgs[2], HvacData.Rects[57 + j + 12 * m].X + 12, HvacData.Rects[57 + j + 12 * m].Y + 2,
                                HvacData.Rects[57 + j + 12*m].Width - 24, HvacData.Rects[57 + j + 12 *m].Height - 4);
                        }
                    }
                }
            }
        }

        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 189; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, HvacData.Rects[i]);
            }
        }

        private void FillRects(Graphics e)
        {
            for (int i = 0; i < 189; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, HvacData.Rects[i]);

            }
        }

        private void DrawLines(Graphics e)
        {
            for (int i = 0; i < 16; i = i + 2)
            {
                e.DrawLine(new Pen(Color.Black, 2), HvacData.myPoints[i], HvacData.myPoints[i + 1]);
            }
        }

        private void DrawWords(Graphics e)
        {
            for (int i = 0; i < 15; i++)
            {
                e.DrawString(m_Dictionary[1 + i], Common.m_Font11, Common.m_BlackBrush,
                    HvacData.Rects[i],Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(m_Dictionary[16 + i], Common.m_Font12B, Common.m_BlackBrush,
                              HvacData.Rects[15 + i], Common.m_MFormat);
            }
            for (int i = 0; i < 12; i = i + 2)
            {
                e.DrawString(m_Dictionary[22], Common.m_Font9B, Common.m_BlackBrush,
                              HvacData.Rects[21 + i], Common.m_MFormat);
            }
            for (int i = 0; i < 12; i = i + 2)
            {
                e.DrawString(m_Dictionary[23], Common.m_Font9B, Common.m_BlackBrush,
                              HvacData.Rects[22 + i], Common.m_MFormat);
            }
            e.DrawString(m_Dictionary[37], Common.m_Font12B, Common.m_BlackBrush,
                              HvacData.Rects[225], Common.m_MFormat);
        }

        private void DrawState(Graphics e)
        {
            //设备模式
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[m_BoolIds[6 * i + j]])
                    {
                        e.DrawString(m_Dictionary[24 + j], Common.m_Font9, Common.m_BlackBrush,
                                      HvacData.Rects[33 + i], Common.m_CenterFormat);
                        break;
                    }
                }
            }
            //VAC开关状态
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (BoolList[m_BoolIds[36 + 7 * i + j]])
                    {
                        e.DrawString(m_Dictionary[30 + j], Common.m_Font9, Common.m_BlackBrush,
                                                         HvacData.Rects[39 + i], Common.m_MFormat);
                        break;
                    }
                }
            }
            //新风及回风温度
            for (int i = 0; i < 12; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i]].ToString("0.0") + " C", Common.m_Font10, Common.m_BlackBrush,
                                                      HvacData.Rects[213 + i], Common.m_MFormat);
            }
        }
    }


}
