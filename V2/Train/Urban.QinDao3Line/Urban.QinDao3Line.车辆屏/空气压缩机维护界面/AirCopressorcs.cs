using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class AirCopressorcs:NewQBaseclass
    {
        private List<string> m_Dictionnary;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            AirData.InitData();
            m_Dictionnary = new List<string>()
            {
                string.Empty,
                ResourceFacade.MaintenanceAirCopressorcsResourceCompressorcontrolTc1,
                ResourceFacade.MaintenanceAirCopressorcsResourceCompressorcontrolTc2,
                ResourceFacade.MaintenanceAirCopressorcsResourceCommand,
                ResourceFacade.MaintenanceAirCopressorcsResourceRuning,
                ResourceFacade.MaintenanceAirCopressorcsResourceInfault,
                ResourceFacade.MaintenanceAuxiliaryResourceMaintenanceAirCompressor
            };
            return true;
        }

        public override void paint(Graphics g)
        {
            Fillrects(g);
            DrawRects(g);
            DrawWords(g);
            DrawState(g);
        }

        private void DrawRects(Graphics e)
        {
            for (int i=0;i<6;i++)
            {
                e.DrawRectangle(Common.m_BlackPen, AirData.Rects[0 + i]);
            }
            
        }

        private void Fillrects(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                 e.FillRectangle(Common.m_GreyWhite,AirData.Rects[i+0]);
            }
           
        }

        private void DrawWords(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                e.DrawString(m_Dictionnary[1 + i], Common.m_Font10B, Common.m_BlackBrush, AirData.Rects[2 + i],
                                Common.m_MFormat);
            }
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Dictionnary[3+i ], Common.m_Font10B, Common.m_BlackBrush, AirData.Rects[12+i],
                                Common.m_MFormat);
            }
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Dictionnary[3 + i], Common.m_Font10B, Common.m_BlackBrush, AirData.Rects[15 + i],
                                Common.m_MFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i]].ToString("0.0") + " Bar", Common.m_Font10B, Common.m_BlackBrush, AirData.Rects[4 + i],
                                Common.m_MFormat);
            }
            //维护：空气压缩机
            e.DrawString(m_Dictionnary [6], Common.m_Font12B, Common.m_BlackBrush, AirData.Rects[18],
                                Common.m_MFormat);
        }

        private void DrawState(Graphics e)
        {
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[1], AirData.Rects[6 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[6+i]])
                    {
                        e.DrawImage(m_Imgs[2], AirData.Rects[6 + i]);
                    }
                    else
                        e.DrawImage(m_Imgs[0], AirData.Rects[6 + i]);
                }   
            }
        }
    }
}
