using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackBoxFigure : NewQBaseclass
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
                ResourceFacade.MaintenanceResourceMaintenanceBBO1,             //2     BBO1
                ResourceFacade.MaintenanceResourceMaintenanceBBO2,             //3     BBO2
                ResourceFacade.MaintenanceBlackBoxResourceTrainDistance,       //4     车辆允许距离
                ResourceFacade.MaintenanceBlackBoxResourceWheelDiameter,       //5     车轮直径
                ResourceFacade.MaintenanceBlackBoxResourceTrainSpeed,          //6     列车速度
                
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            //维护：黑匣子
            g.DrawString(m_String[1], Common.m_Font14B, Common.m_BlackBrush, SoftWareData.Rects[0], Common.m_MFormat);
            DrawRects(g);
            DrawWords(g);
        }
        public void DrawRects(Graphics e)
        {
            for (int i = 0; i < 4; i++)
            {
                e.FillRectangle(Common.m_WhiteBrush, BlackBoxData.Rects[99 + i]);
            }
            for (int i = 0; i < 4; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, BlackBoxData.Rects[99 + i].X, BlackBoxData.Rects[99 + i].Y,
                    BlackBoxData.Rects[99 + i].Width, BlackBoxData.Rects[99 + i].Height);
            }
        }
        public void DrawWords(Graphics e)
        {
            //BBO
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[2 + i], Common.m_Font11B, Common.m_BlackBrush, BlackBoxData.Rects[99 + 2*i], Common.m_MFormat);
            }
            //车辆允许距离
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[4], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[103+i], Common.m_MFormat);
            }
            //Km值
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i]].ToString("0")+" Km", Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[105 + i], Common.m_MFormat);
            }
            //车轮直径
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[5], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[107 + i], Common.m_MFormat);
            }
            //mm值
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[2+i]].ToString("0") + " mm", Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[109 + i], Common.m_MFormat);
            }
            //列车速度
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[6], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[111 + i], Common.m_MFormat);
            }
            //kph值
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[4+i]].ToString("0.0") + " kph", Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[113 + i], Common.m_MFormat);
            }
        }
    }
}


