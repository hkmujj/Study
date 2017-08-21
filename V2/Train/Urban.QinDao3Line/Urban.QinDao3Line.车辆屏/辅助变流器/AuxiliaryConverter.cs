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
    class AuxiliaryConverter : NewQBaseclass
    {
        private List<string> m_Resources;
        private readonly bool[] m_BtnIsdown = new bool[1];
        public override bool init(ref int nErrorObjectIndex)
        {
            //ReadConfigcs read = new ReadConfigcs("辅助变流器",m_Dictionary);
            //read.ReaFile(Path.Combine(RecPath, "..\\Config"));
            IntiData();
            m_Resources = new List<string>()
            {
                String.Empty,
                ResourceFacade.MaintenanceAuxiliaryResourceCVS1A501,
                ResourceFacade.MaintenanceAuxiliaryResourceCVS2A501,
                ResourceFacade.MaintenanceAuxiliaryResourceFaultEvent,
                ResourceFacade.MaintenanceAuxiliaryResourceCouplingContactor,
                ResourceFacade.MaintenanceAuxiliaryResourceON,
                ResourceFacade.MaintenanceAuxiliaryResourceOFF,
                ResourceFacade .MaintenanceAuxiliaryResourceMaintenanceAuxiliary
            };
            AuxiliaryData.InitData();
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawImgs(g);
            DrawRects(g);
            DrawWords(g);
            DrawLines(g);
        }
        public override bool mouseDown(Point point)
        {

            if (AuxiliaryData.Regions[0].IsVisible(point))
            {
                m_BtnIsdown[0] = true;
            }

            return true;
        }
        public override bool mouseUp(Point point)
        {

            if (AuxiliaryData.Regions[0].IsVisible(point))
            {
                append_postCmd(CmdType.ChangePage, 16, 0, 0);
                m_BtnIsdown[0] = false;

            }
            return true;
        }

        private void DrawImgs(Graphics e)
        {
            if (BoolList[m_BoolIds[12]])
            e.DrawImage(m_Imgs[0], AuxiliaryData.Rects[0]);
            if (BoolList[m_BoolIds[13]])
            e.DrawImage(m_Imgs[3], AuxiliaryData.Rects[0]);
            if (m_BtnIsdown[0])
            {
                e.DrawImage(m_Imgs[2], AuxiliaryData.Rects[21]);
            }
            else
            {
                e.DrawImage(m_Imgs[1], AuxiliaryData.Rects[21]);
            }
        }

        private void DrawWords(Graphics e)
        {
            //维护：辅助逆变器  扩展供电接触 故障事件
            e.DrawString(m_Resources[3], Common.m_Font10, Common.m_BlackBrush, AuxiliaryData.Rects[23], Common.m_MFormat);
            e.DrawString(m_Resources[7], Common.m_Font12B, Common.m_BlackBrush, AuxiliaryData.Rects[24], Common.m_MFormat);
            if (BoolList[m_BoolIds[12]] || BoolList[m_BoolIds[13]])
            {
                e.DrawString(m_Resources[4], Common.m_Font8, Common.m_BlackBrush, AuxiliaryData.Rects[22], Common.m_LeftFormat);
            }
            //CVS1   CVS2
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[12 + i]])
                    e.DrawString(m_Resources[1 + i], Common.m_Font12B, Common.m_BlackBrush, AuxiliaryData.Rects[19 + i],
                        Common.m_MFormat);
            }
            //上半部分电路状态
            if (BoolList[m_BoolIds[12]])
            {
                //OK或NO状态
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(BoolList[m_BoolIds[i]] ? "OK" : "NO", Common.m_Font10, Common.m_BlackBrush, AuxiliaryData.Rects[2 + i], Common.m_MFormat);
                }
                //输入电压
                e.DrawString(FloatList[m_FoolatIds[0]].ToString("0") + " V", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[1], Common.m_MFormat);
                //输出电流
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[m_FoolatIds[1 + 2 * i]].ToString("0") + " A", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[5 + 2 * i], Common.m_MFormat);
                }
                //输出电压
                for (int i = 0; i < 2; i++)
                {
                    e.DrawString(FloatList[m_FoolatIds[2 + 2 * i]].ToString("0") + " V", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[6 + 2 * i], Common.m_MFormat);
                }
            }
            //下半部分电路状态
            if (BoolList[m_BoolIds[13]])
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(BoolList[m_BoolIds[3 + i]] ? "OK" : "NO", Common.m_Font10, Common.m_BlackBrush, AuxiliaryData.Rects[11 + i], Common.m_MFormat);
                }
                //输入电压
                e.DrawString(FloatList[m_FoolatIds[6]].ToString("0") + " V", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[10], Common.m_MFormat);
                //输出电流
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FloatList[m_FoolatIds[7 + 2 * i]].ToString("0") + " A", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[14 + 2 * i], Common.m_MFormat);
                }
                //输出电压
                for (int i = 0; i < 2; i++)
                {
                    e.DrawString(FloatList[m_FoolatIds[8 + 2 * i]].ToString("0") + " V", Common.m_Font10B, Common.m_BlackBrush, AuxiliaryData.Rects[15 + 2 * i], Common.m_MFormat);
                }
            }
        }

        private void DrawRects(Graphics e)
        {
            if (BoolList[m_BoolIds[12]])
            {
                for (int i = 1; i < 10; i++)
                {

                    e.FillRectangle(Common.m_GreenBrush, AuxiliaryData.Rects[i].X, AuxiliaryData.Rects[i].Y,
                                           AuxiliaryData.Rects[i].Width, AuxiliaryData.Rects[i].Height);
                    e.DrawRectangle(Common.m_BlackPen, AuxiliaryData.Rects[i]);
                }
            }
            if (BoolList[m_BoolIds[13]])
            {
                for (int i = 10; i < 19; i++)
                {

                    e.FillRectangle(Common.m_GreenBrush, AuxiliaryData.Rects[i].X, AuxiliaryData.Rects[i].Y,
                                           AuxiliaryData.Rects[i].Width, AuxiliaryData.Rects[i].Height);
                    e.DrawRectangle(Common.m_BlackPen, AuxiliaryData.Rects[i]);
                }
            }
        }

        private void DrawLines(Graphics e)
        {
            //上方电路开关
            if (BoolList[m_BoolIds[12]])
            {
                for (int i = 0; i < 16; )
                {
                    if (BoolList[m_BoolIds[14 + i / 4]])
                        e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[i + 2], AuxiliaryData.Points[i + 3]);
                    else
                        e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[i], AuxiliaryData.Points[i + 1]);
                    i += 4;
                }
            }
            //下方电路开关
            if (BoolList[m_BoolIds[13]])
            {
                for (int i = 0; i < 16; )
                {
                    if (BoolList[m_BoolIds[18 + i / 4]])
                        e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[i + 18], AuxiliaryData.Points[i + 19]);
                    else
                        e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[i+16], AuxiliaryData.Points[i + 17]);
                    i += 4;
                }
            }
            //扩展供电接触器开关
            if (BoolList[m_BoolIds[12]] || BoolList[m_BoolIds[13]])
            {
                if (BoolList[m_BoolIds[22]])
                {
                    e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[34], AuxiliaryData.Points[35]);
                }
                else
                    e.DrawLine(Common.m_BlackPen2, AuxiliaryData.Points[32], AuxiliaryData.Points[33]);
            }
        }

    }
}  
