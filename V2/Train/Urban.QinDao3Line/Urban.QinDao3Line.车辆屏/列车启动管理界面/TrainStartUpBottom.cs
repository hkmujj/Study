using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
   public class TrainStartUpBottom:NewQBaseclass
    {
        private List<RectangleF> m_Rects = new List<RectangleF>();
        private readonly List<Point> m_Points = new List<Point>();
        private readonly List<Region> m_Regions = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[7];
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            Init();
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawLines(g);
            PaintButtonState(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (;index<7;index++)
            {
               if (m_Regions[index].IsVisible(point))
               {
                   m_BtnIsDown[index] = true;
               }
            }
            return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
             int index = 0;
            for (; index < 7; index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                            append_postCmd(CmdType.ChangePage, 9, 0, 0);
                            m_BtnIsDown[0] = false;
                            break;
                        case 1:
                            append_postCmd(CmdType.ChangePage, 5, 0, 0);
                            m_BtnIsDown[1] = false;
                            break;
                        case 2:
                            append_postCmd(CmdType.ChangePage, 4, 0, 0);
                            m_BtnIsDown[2] = false;
                            break;
                        case 3:
                            append_postCmd(CmdType.ChangePage, 3, 0, 0);
                            m_BtnIsDown[3] = false;
                            break;
                        case 4:
                            append_postCmd(CmdType.ChangePage, 2, 0, 0);
                            m_BtnIsDown[4] = false;
                            break;

                        case 5:
                            append_postCmd(CmdType.ChangePage, 10, 0, 0);
                            m_BtnIsDown[5] = false;
                            break;

                        case 6:
                            append_postCmd(CmdType.ChangePage, 1, 0, 0);
                            m_BtnIsDown[6] = false;
                            break;
                    }
                }
            }
            return base.mouseUp(point);
        }

        private void PaintButtonState(Graphics e)
        {
            for (int i = 0; i < 7; i++)
            {
                if (m_BtnIsDown[i])
                {
                    e.DrawImage(m_Imgs[8], m_Rects[7 + i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[7], m_Rects[7 + i]);
                }
                e.DrawImage(m_Imgs[i], m_Rects[i]);
            }
        }
        private void DrawLines(Graphics e)
        {
            e.DrawLine(Common.m_BlackPen1, m_Points[0], m_Points[1]);
        }

        private void Init()
        {
            //按钮区域及位置
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 27 + i);
            }
            for (int i = 0; i < 4; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 31 + i);
            }
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 37 + i);
            }
            for (int i = 0; i < 4; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 41 + i);
            }
            for (int i = 0; i < 7; i++)
            {
                m_Regions.Add(new Region(m_Rects[i]));
            }
            //按键上面横线的坐标
            m_Points.Add(new Point(0, 547));
            m_Points.Add(new Point(800, 547));
        }
    }
}
