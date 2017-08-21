using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultBottomTitle : NewQBaseclass
    {
        private List<RectangleF> m_Rects = new List<RectangleF>();
        private readonly List<Point> m_Points = new List<Point>();
        private readonly List<Region> m_Regions = new List<Region>();
        public bool[] m_BtnIsDown = new bool[7];
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

        private void PaintButtonState(Graphics e)
        {
            if (m_BtnIsDown[0])
            {
                e.DrawImage(m_Imgs[8], m_Rects[7]);
            }
            else
            {
                e.DrawImage(m_Imgs[7], m_Rects[7]);
            }
            e.DrawImage(m_Imgs[0], m_Rects[0]);
            for (int i = 0; i < 6; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    if (m_BtnIsDown[1+i])
                    {
                        e.DrawImage(m_Imgs[8], m_Rects[8 + i]);
                    }
                    else
                    {
                        e.DrawImage(m_Imgs[7], m_Rects[8 + i]);
                    }
                    e.DrawImage(m_Imgs[1+i], m_Rects[1+i]);
                }
            }
        }

        public override bool mouseDown(Point point)
        {
            if (m_Regions[0].IsVisible(point))
            {
                m_BtnIsDown[0] = true;
                return true;
            }
            for (int index=0; index < 6; index++)
            {
                if (m_Regions[index+1].IsVisible(point) && BoolList[m_BoolIds[index]])
                {
                    m_BtnIsDown[index+1] = true;
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            if (m_Regions[0].IsVisible(point))
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
                m_BtnIsDown[0] = false;
                return true ;
            }
            for (int index=0; index < 6; index++)
            {
                if (m_Regions[index+1].IsVisible(point))
                {
                    switch (index+1)
                    {
                        case 1:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 9, 0, 0);
                                m_BtnIsDown[1] = false;
                            }
                            break;
                        case 2:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 5, 0, 0);
                                m_BtnIsDown[2] = false;
                            }
                            break;
                        case 3:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 4, 0, 0);
                                m_BtnIsDown[3] = false;
                            }
                            break;
                        case 4:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 2, 0, 0);
                                m_BtnIsDown[4] = false;
                            }
                            break;

                        case 5:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 10, 0, 0);
                                m_BtnIsDown[5] = false;
                            }
                            break;

                        case 6:
                            if (BoolList[m_BoolIds[index]])
                            {
                                append_postCmd(CmdType.ChangePage, 1, 0, 0);
                                m_BtnIsDown[6] = false;
                            }
                            break;
                    }
                    return true;
                }
            }
            return true;
        }

        private void DrawLines(Graphics e)
        {
            e.DrawLine(Common.m_BlackPen1, m_Points[0], m_Points[1]);
        }

        private void Init()
        {
            //前4个按钮区域/1-4
            for (int i = 0; i < 4; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects,26+i);
            }
            //后3个按钮区域
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 32 + i);
            }
            //前4个按钮图片
            for (int i = 0; i < 4; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 36 + i);
            }
            //后3个按钮图片
            for (int i = 0; i < 3; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TitleScreen, ref m_Rects, 42 + i);
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
