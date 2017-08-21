using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;


namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SoftWareVersion:NewQBaseclass
    {
        private List<string> m_String;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            SoftWareData.InitData();
            m_String = new List<string>() 
            {
                ResourceFacade .MaintenanceAuxiliaryResourceMaintenanceSoftWareVersion,
                ResourceFacade .MaintenanceBrakeResourceTc1,
                ResourceFacade .MaintenanceBrakeResourceM1,
                ResourceFacade .MaintenanceBrakeResourceM2,
                ResourceFacade .MaintenanceBrakeResourceM3,
                ResourceFacade .MaintenanceBrakeResourceM4,
                ResourceFacade .MaintenanceBrakeResourceTc2,              //6
                "PIS1","BCU1","MPU1","VAC1","ACE1","BB01","DDU1","DDCU01","","DDCU02",       //7-16
                "","VAC2","PCE1","DCU03","DCU04",                                            //17-21
                "BCU2","VAC3","PCE2","DCU05","DCU06",                                        //22-26                                                        
                "BCU3","VAC4","PCE3","DCU07","DCU08",                                        //27-31
                "","VAC5","PCE4","DCU09","DCU10",                                            //32-36
                "PIS2","BCU4","MPU2","VAC6","ACE2","BBO2","DCU11","DDU2","DCU12","",         //37-46
                "1.0.1","14.0.3","0.0.2.3","1.2.1","1.0.7","0.4.0","4.0.3","G.0.1","","G.0.1",  //47-56                                    //38-47
                "","1.2.1","1.3.0","G.0.1","G.0.1",                                                 //57-61
                "14.0.3","1.2.1","1.3.0","G.0.1","G.0.1",                                            //62-66
                "14.0.3","1.2.1","1.3.0","G.0.1","G.0.1",                                       //67-71
                "","1.2.1","1.3.0","G.0.1","G.0.1",                                            //72-76
                "1.0.1","14.0.3","0.0.2.3","1.2.1","1.0.7","0.4.0","G.0.1","4.0.3","G.0.1","",  //77-86
                "—"                                                                           //87
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            //维护：软件版本
            g.DrawString(m_String[0],Common.m_Font12B,Common.m_BlackBrush,SoftWareData .Rects [0],Common.m_MFormat);
            DrawDoors (g);
            DrawWords(g);
        }
         public void DrawDoors(Graphics e)
        {
            //车头
            for (int i=0;i<8;i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[i], i<4?SoftWareData.Rects[1]:SoftWareData.Rects[2]);
                }
                else
                {
                    if (!BoolList[0] && !BoolList[1] && !BoolList[2] && !BoolList[3])
                    {
                        e.DrawImage(m_Imgs[0], SoftWareData.Rects[1]);
                    }
                    if (!BoolList[4] && !BoolList[5] && !BoolList[6] && !BoolList[7])
                    {
                        e.DrawImage(m_Imgs[4], SoftWareData.Rects[2]);
                    }
                }
            }
            //车厢
            for (int i = 0; i < 6; i++)
            {
                e.FillRectangle(SolidBrushsItems.SoftYellow, SoftWareData.Rects[3 + i]);
                e.DrawRectangle(Common.m_BlackPen,SoftWareData.Rects[3 + i].X, SoftWareData.Rects[3 + i].Y,
                    SoftWareData.Rects[3 + i].Width, SoftWareData.Rects[3 + i].Height);
                e.DrawString(m_String[1+i], Common.m_Font11B, Common.m_BlackBrush, SoftWareData.Rects[3+i], Common.m_MFormat);
            }
        }
         public void DrawWords(Graphics e)
         {
             for (int i = 0; i < 6; i++)
             {
                 e.FillRectangle(Common.m_WhiteBrush, SoftWareData.Rects[9 + i]);
                 e.DrawRectangle(Common.m_BlackPen, SoftWareData.Rects[9 + i]);
             }
             //PIS1等文字
             for (int i = 0; i < 40; i++)
             {
                 e.DrawString(m_String[7 + i], FontItems.Fonts_Regular(FontName.Arial, 9, true), Common.m_BlackBrush, SoftWareData.Rects[15 + i], Common.m_BottomFormat);
             }
             //1.0.1等文字
             for (int i = 0; i < 40; i++)
             {
                 e.DrawString(m_String[47 + i], FontItems.Fonts_Regular(FontName.Arial, 9, true), Common.m_BlackBrush, SoftWareData.Rects[55 + i], Common.m_TopFormat);
             }
             //横杠
             for (int i = 0; i < 40; i++)
             {
                 if(i!=8 &&i!=10 &&i!=25&&i!=39)
                 e.DrawString(m_String[87], Common.m_Font24, Common.m_BlackBrush, SoftWareData.Rects[95 + i], Common.m_MFormat);
             }
         }
    }
}

