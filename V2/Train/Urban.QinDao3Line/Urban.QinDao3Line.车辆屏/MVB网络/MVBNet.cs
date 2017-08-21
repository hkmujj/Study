using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MVBNet : NewQBaseclass
    {
        private List<string> m_Resources;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            m_Resources = new List<string>()
            {
                string.Empty,                                                     //0
                ResourceFacade.MaintenanceResourceMaintenanceMVBNet,              //1
                ResourceFacade.MaintenanceBrakeResourceTc1,                       //2
                ResourceFacade.MaintenanceBrakeResourceM1,                        //3
                ResourceFacade.MaintenanceBrakeResourceM2,                        //4
                ResourceFacade.MaintenanceBrakeResourceM3,                        //5
                ResourceFacade.MaintenanceBrakeResourceM4,                        //6
                ResourceFacade.MaintenanceBrakeResourceTc2,                       //7
                "DDU","MPU","ACE","PIS","BBox",                                 //8-12
                "VAC","BCU","RIOM1","RIOM2","GTW","ATC",                            //13-18
                "PCE","VAC",                                                      //19-20
                "BCU","RIOM",                                                     //21-22
                "DCU1","DCU2","DCU3","DCU4",                                      //23-26
                "DCU5","DCU6","DCU7","DCU8",                                      //27-30
                "M"                                                               //31
            };
            MVBNetData.InitData();
            return true;
        }
        public override void paint(Graphics g)
        {
            //维护：MVB网络
            g.DrawString(m_Resources[1], Common.m_Font14B, Common.m_BlackBrush, MVBNetData.Rects[0], Common.m_MFormat);
            DrawDoors(g);
            FillRects(g);
            DrawRects(g);
            DrawWords(g);
            DrawColorLines(g);
        }

        public void DrawDoors(Graphics e)
        {
            //车头
            for (int i = 0; i < 8; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[i], i < 4 ? MVBNetData.Rects[1] : MVBNetData.Rects[2]);
                }
                else
                {
                    if (!BoolList[0] && !BoolList[1] && !BoolList[2] && !BoolList[3])
                    {
                        e.DrawImage(m_Imgs[0], MVBNetData.Rects[1]);
                    }
                    if (!BoolList[4] && !BoolList[5] && !BoolList[6] && !BoolList[7])
                    {
                        e.DrawImage(m_Imgs[4], MVBNetData.Rects[2]);
                    }
                }
            }
            //车厢
            for (int i = 0; i < 6; i++)
            {
                e.FillRectangle(Common.m_SoftYellowBrush, MVBNetData.Rects[3 + i]);
                e.DrawRectangle(Common.m_BlackPen, MVBNetData.Rects[3 + i].X, MVBNetData.Rects[3 + i].Y,
                    MVBNetData.Rects[3 + i].Width, MVBNetData.Rects[3 + i].Height);
                e.DrawString(m_Resources[2 + i], Common.m_Font11B, Common.m_BlackBrush, MVBNetData.Rects[3 + i], Common.m_MFormat);
            }
        }
        private void DrawWords(Graphics e)
        {
            //M
            e.DrawString(m_Resources[31], Common.m_Font12B, Common.m_BlackBrush, MVBNetData.Rects[9], Common.m_MFormat);
            //DDU ACE BBox BCU RIOM2 MPU PIS VAC RIOM1 GTW
            for (int i = 0; i < 10; i++)
            {
                e.DrawString(m_Resources[8 + i], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[10 + i], Common.m_MFormat);
                e.DrawString(m_Resources[8 + i], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[20 + i], Common.m_MFormat);
            }
            //ATC
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[18], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[30 + i], Common.m_MFormat);
            }
            //PCE VAC RIOM
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[19], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[32 + 3 * i], Common.m_MFormat);
                e.DrawString(m_Resources[20], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[33 + 3 * i], Common.m_MFormat);
                e.DrawString(m_Resources[22], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[34 + 3 * i], Common.m_MFormat);
            }
            //PCE VAC BCU RIOM
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Resources[19 + i], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[38 + i], Common.m_MFormat);
                e.DrawString(m_Resources[19 + i], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[42 + i], Common.m_MFormat);
            }
            //DCU1 DCU2 DCU3 DCU4 DCU5 DCU6 DCU7 DCU8
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    e.DrawString(m_Resources[23 + j], Common.m_Font10, Common.m_BlackBrush, MVBNetData.Rects[46 + j + 8 * i], Common.m_MFormat);
                }
            }
        }
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 84; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, MVBNetData.Rects[10 + i]);
            }
        }
        private void FillRects(Graphics e)
        {
            for (int i = 0; i < 84; i++)
            {
                if (BoolList[m_BoolIds[8 + i]])
                {
                    e.FillRectangle(Common.m_RedBrush1, MVBNetData.Rects[10 + i]);
                }
                else
                    e.FillRectangle(Common.m_GreenBrush, MVBNetData.Rects[10 + i]);
            }
        }
        private void DrawColorLines(Graphics e)
        {
            //DDU、MPU线
            for (int i = 0; i < 20; i = i + 2)
            {
                e.DrawLine(Common.m_RedPen3, MVBNetData.myPoints[i], MVBNetData.myPoints[i + 1]);
            }
            //ATC线
            for (int i = 0; i < 4; i = i + 2)
            {
                e.DrawLine(Common.m_BluePen3, MVBNetData.myPoints[20 + i], MVBNetData.myPoints[20 + i + 1]);
            }
            //其余的红线
            for (int i = 0; i < 54; i = i + 2)
            {
                e.DrawLine(Common.m_RedPen3, MVBNetData.myPoints[24 + i], MVBNetData.myPoints[24 + i + 1]);
            }
            //黄色的线
            for (int i = 0; i < 60; i = i + 2)
            {
                e.DrawLine(Common.m_YelloPen3, MVBNetData.myPoints[78 + i], MVBNetData.myPoints[78 + i + 1]);
            }
        }
    }
}
