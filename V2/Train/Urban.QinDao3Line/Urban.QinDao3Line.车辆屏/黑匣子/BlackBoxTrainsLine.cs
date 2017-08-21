using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BlackBoxTrainsLine : NewQBaseclass
    {
        private List<string> m_String;
        private bool IsBtnDown;
        private Region m_Region ;
        private int m_Row = 0;
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
                "INO1  本端司机室激活",                                        //4
                "INO2  远端司机室激活",
                "INO3  前向列车线",
                "INO4  后向列车线",
                "INO5  DSD旁路继电器状态",
                "INO6  蓄电池启动信号",
                "INO7  快速列车线",
                "INO8  快速旁路继电器状态",
                "INO9  备用模式列车线",
                "IN10  列车完整性列车线",
                "IN11  列车完整性旁路继电器状态",
                "IN12  空压机强制启动信号",
                "IN13  ATP旁路继电器状态",
                "IN14  ATO自动折退模式信号",
                "IN15  ATO模式信号"
            };
            m_Region = new Region(BlackBoxData.Rects[98]);
            return true;
        }
        public override void paint(Graphics g)
        {
            //维护：黑匣子
            g.DrawString(m_String[1], Common.m_Font14B, Common.m_BlackBrush, SoftWareData.Rects[0], Common.m_MFormat);
            DrawRects(g);
            DrawWords(g);
            DrawImages(g);
            DrawBtnStates(g);
        }
        public override bool mouseDown(Point point)
        {
            if (m_Region.IsVisible(point))
                IsBtnDown = true;
            return true;
        }
        public override bool mouseUp(Point point)
        {
            if(m_Region.IsVisible(point))
            {
                IsBtnDown = false;
                m_Row = (m_Row + 1) % 15;
            }    
            return true;
        }
        public void DrawRects(Graphics e)
        {
            e.FillRectangle(Common.m_WhiteBrush, BlackBoxData.Rects[50 + m_Row]);
            for (int i = 0; i < 47; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, BlackBoxData.Rects[50 + i].X, BlackBoxData.Rects[50 + i].Y,
                    BlackBoxData.Rects[50 + i].Width, BlackBoxData.Rects[50 + i].Height);
            }
        }
        public void DrawWords(Graphics e)
        {
            //列车线信息
            for (int i = 0; i < 15; i++)
            {
                e.DrawString(m_String[4+i], Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[50+i], Common.m_LeftCenterFormat);
            }
            //BBO1与BBO2
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_String[2 + i], Common.m_Font10B, Common.m_BlackBrush, BlackBoxData.Rects[65 + i], Common.m_MFormat );
            }
            e.DrawString("1/3", Common.m_Font10, Common.m_BlackBrush, BlackBoxData.Rects[115], Common.m_MFormat);
        }
        public void DrawImages(Graphics e)
        {
            for (int i = 0; i < 30; i++) 
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[2], BlackBoxData.Rects[67 + i].X + 19, BlackBoxData.Rects[67 + i].Y + 1, 23, 23);
                }
                else
                {
                    if (BoolList[m_BoolIds[i + 30]])
                    {
                        e.DrawImage(m_Imgs[1], BlackBoxData.Rects[67 + i].X + 19, BlackBoxData.Rects[67 + i].Y + 1, 23, 23);
                    }
                    else
                        e.DrawImage(m_Imgs[0], BlackBoxData.Rects[67 + i].X + 19, BlackBoxData.Rects[67 + i].Y + 1, 23, 23);
                }   
            }
        }
        public void DrawBtnStates(Graphics e)
        {
            if (IsBtnDown)
            {
                e.DrawImage(m_Imgs[4], BlackBoxData.Rects[97]);
            }
            else
                e.DrawImage(m_Imgs[3], BlackBoxData.Rects[97]);
            e.DrawImage(m_Imgs[5], BlackBoxData.Rects[98]);
        }
    }
}