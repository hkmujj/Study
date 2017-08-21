using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Resources;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class DoorsMaintenance : NewQBaseclass
    {
        private bool[] m_BtnIsdown = new bool[4];
        private List<string> m_Dictionary;
        private int m_TrainIndex = 0;                                  //记录车厢号
        private int m_SitIndex = 0;                                    //记录座位号
        static public int CarChoose = 0;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();

            m_Dictionary = new List<string>()
            {
                String.Empty, 
                ResourceFacade.MaintenanceResourceMaintenanceDoors,           //1   维护：车门
                ResourceFacade.MaintenanceBrakeResourceTc1,                   //2   Tc1
                ResourceFacade.MaintenanceBrakeResourceM1,                    //3   M1
                ResourceFacade.MaintenanceBrakeResourceM2,                    //4   M2
                ResourceFacade.MaintenanceBrakeResourceM3,                    //5   M3
                ResourceFacade.MaintenanceBrakeResourceM4,                    //6   M4
                ResourceFacade.MaintenanceBrakeResourceTc2,                   //7   Tc2
                "门驱电机电路故障",                                           //8
                "门关好并锁闭限位开关故障",
                "车门在无允许的情况下离开锁闭位",
                "车门在3秒内未解锁",
                "车门位置传感器故障",
                "车门关闭过程中检测到障碍物",
                "车门代开过程中检测到的电机电流",
                "DCU内部的安全继电器故障",
                "DCU输出A6短路",
                "DCU输出A7短路",
                "DCU输出A8短路",
                "数据总线通信故障",
                "诊断内存备用电池故障",
                "DCU上的服务按钮故障",
                "DCU输出A3短路",
                "DCU输出A4短路",
                "软件版本与主控不一致",
                "司机按钮故障",
                "在预订时间内未检测到打开位",
                "在预订时间内未检测到闭合位",
                "DCU1",                                                   //28  DCU1
                "DCU2",                                                   //29  DCU2
                "DCU3",                                                   //30  DCU3
                "DCU4",                                                   //31  DCU4
                "DCU5",                                                   //32  DCU5
                "DCU6",                                                   //33  DCU6
                "DCU7",                                                   //34  DCU7
                "DCU8"                                                    //35  DCU8                               
            };

            DoorsData.InitData();
            return true;
        }

        public override void paint(Graphics g)
        {
            FillRects(g);
            DrawWords(g);
            DrawRects(g);
            DrawImgs(g);
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (DoorsData.Regions[index].IsVisible(point))
                {
                    m_BtnIsdown[index] = true;
                    switch (index)
                    {
                        case 0:
                            if (m_TrainIndex <= 0)
                            {
                                m_TrainIndex = 5;
                                CarChoose = 5;
                            }
                            else
                            {
                                m_TrainIndex = m_TrainIndex - 1;
                                CarChoose -= 1;
                            }
                               
                            break;
                        case 1:
                            if (m_TrainIndex >= 5)
                            {
                                m_TrainIndex = 0;
                                CarChoose = 0;
                            }
                            else
                            {
                                m_TrainIndex = m_TrainIndex + 1;
                                CarChoose += 1;
                            }
                                
                            break;
                        case 2:
                            if (m_TrainIndex < 3)
                            {
                                if (m_SitIndex <= 0)
                                {
                                    m_SitIndex = 7;
                                }
                                else
                                    m_SitIndex = m_SitIndex - 1;
                            }
                            else
                            {
                                if (m_SitIndex >= 7)
                                {
                                    m_SitIndex = 0;
                                }
                                else
                                    m_SitIndex = m_SitIndex + 1;
                            }
                            break;
                        case 3:
                            if (m_TrainIndex < 3)
                            {
                                if (m_SitIndex >= 7)
                                {
                                    m_SitIndex = 0;
                                }
                                else
                                    m_SitIndex = m_SitIndex + 1;
                            }
                            else
                            {
                                if (m_SitIndex <= 0)
                                {
                                    m_SitIndex = 7;
                                }
                                else
                                    m_SitIndex = m_SitIndex - 1;
                            }
                            break;
                    }
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (DoorsData.Regions[index].IsVisible(point))
                {
                    m_BtnIsdown[index] = false;
                }
            }
            return true;
        }

        private void DrawImgs(Graphics g)
        {
            //按钮图片
            for (int i = 0; i < 4; i++)
            {
                if (m_BtnIsdown[i])
                {
                    g.DrawImage(m_Imgs[1], DoorsData.Rects[i]);
                }
                else
                {
                    g.DrawImage(m_Imgs[0], DoorsData.Rects[i]);
                }
                if (i == 1 || i == 3)
                {
                    g.DrawImage(m_Imgs[2], DoorsData.Rects[i + 4]);
                }
                else
                {
                    g.DrawImage(m_Imgs[3], DoorsData.Rects[i + 4]);
                }
            }
            //显示灯图片
            #region:::::::::::::前三个车厢::::::::::::::::
            if (m_TrainIndex < 3)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * m_SitIndex + i]])
                    {
                        g.DrawImage(m_Imgs[5], DoorsData.Rects[28 + i].X + 19,
                                    DoorsData.Rects[28 + i].Y + 1, 23, 23);                                       //正常
                    }
                    else
                    {
                        if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * m_SitIndex + i + 20]])
                        {
                            g.DrawImage(m_Imgs[6], DoorsData.Rects[28 + i].X + 19,
                                                               DoorsData.Rects[28 + i].Y + 1, 23, 23);            //故障
                        }
                        else
                            g.DrawImage(m_Imgs[4], DoorsData.Rects[28 + i].X + 19,
                                                               DoorsData.Rects[28 + i].Y + 1, 23, 23);            //默认灰色页面
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * m_SitIndex +10+ i]])
                    {
                        g.DrawImage(m_Imgs[5], DoorsData.Rects[49 + i].X + 19,
                                    DoorsData.Rects[49 + i].Y + 1, 23, 23);                                       //正常
                    }
                    else
                    {
                        if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * m_SitIndex + i + 30]])
                        {
                            g.DrawImage(m_Imgs[6], DoorsData.Rects[49 + i].X + 19,
                                                               DoorsData.Rects[49 + i].Y + 1, 23, 23);            //故障
                        }
                        else
                            g.DrawImage(m_Imgs[4], DoorsData.Rects[49 + i].X + 19,
                                                               DoorsData.Rects[49 + i].Y + 1, 23, 23);            //默认灰色页面
                    }
                }
            }
            #endregion
            #region::::::::::::::::后三个车厢:::::::::::::::::::
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 *(7- m_SitIndex) + i]])
                    {
                        g.DrawImage(m_Imgs[5], DoorsData.Rects[28 + i].X + 19,
                                    DoorsData.Rects[28 + i].Y + 1, 23, 23);                                       //正常
                    }
                    else
                    {
                        if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * (7-m_SitIndex) + i + 20]])
                        {
                            g.DrawImage(m_Imgs[6], DoorsData.Rects[28 + i].X + 19,
                                                               DoorsData.Rects[28 + i].Y + 1, 23, 23);            //故障
                        }
                        else
                            g.DrawImage(m_Imgs[4], DoorsData.Rects[28 + i].X + 19,
                                                               DoorsData.Rects[28 + i].Y + 1, 23, 22);            //默认灰色页面
                    }
                }
                for (int i = 0; i < 7; i++)
                {
                    if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 * (7-m_SitIndex) + 10 + i]])
                    {
                        g.DrawImage(m_Imgs[5], DoorsData.Rects[49 + i].X + 19,
                                    DoorsData.Rects[49 + i].Y + 1, 23, 23);                                       //正常
                    }
                    else
                    {
                        if (BoolList[m_BoolIds[320 * m_TrainIndex + 40 *(7- m_SitIndex) + i + 30]])
                        {
                            g.DrawImage(m_Imgs[6], DoorsData.Rects[49 + i].X + 19,
                                                               DoorsData.Rects[49 + i].Y + 1, 23, 23);            //故障
                        }
                        else
                            g.DrawImage(m_Imgs[4], DoorsData.Rects[49 + i].X + 19,
                                                               DoorsData.Rects[49 + i].Y + 1, 23, 23);            //默认灰色页面
                    }
                }
            }
            #endregion
        }
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 37; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, DoorsData.Rects[8 + i]);
            }
            for (int i = 48; i < 56; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, DoorsData.Rects[i]);
            }
        }

        private void FillRects(Graphics g)
        {
            //车厢示意图
            g.FillRectangle(Common.m_SoftYellowBrush, DoorsData.Rects[8]);
            //车厢示意图中车门
            for (int i = 0; i < 8; i++)
            {
                if (i == m_SitIndex)
                {
                    g.FillRectangle(Common.m_GreenBrush, DoorsData.Rects[9 + i]);
                }
                else
                    g.FillRectangle(Common.m_BlackBrush, DoorsData.Rects[9 + i]);
            }
            for (int i = 17; i < 45; i++)
            {
                g.FillRectangle(Common.m_WhiteBrush, DoorsData.Rects[i]);
            }
            for (int i = 48; i < 56; i++)
            {
                g.FillRectangle(Common.m_WhiteBrush, DoorsData.Rects[i]);
            }
        }

        private void DrawWords(Graphics g)
        {
            //列车中显示车厢信息  Tc1至Tc2
            g.DrawString(m_Dictionary[2 + m_TrainIndex], Common.m_Font11B, Common.m_BlackBrush,
                 DoorsData.Rects[8], Common.m_MFormat);
            //列车故障信息  20个
            for (int i = 0; i < 10; i++)
            {
                g.DrawString(m_Dictionary[8 + i], Common.m_Font11, Common.m_BlackBrush,
                DoorsData.Rects[17 + i], Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 7; i++)
            {
                g.DrawString(m_Dictionary[18 + i], Common.m_Font11, Common.m_BlackBrush,
                               DoorsData.Rects[38 + i], Common.m_LeftCenterFormat);
            }
            //DCU
            for (int i = 0; i < 2; i++)
            {
                if (m_TrainIndex < 3)
                {
                    g.DrawString(m_Dictionary[28 + m_SitIndex], Common.m_Font11B, Common.m_BlackBrush,
                                                  DoorsData.Rects[27 + 21 * i], Common.m_MFormat);
                }
                else
                {
                    g.DrawString(m_Dictionary[35 - m_SitIndex], Common.m_Font11B, Common.m_BlackBrush,
                                                  DoorsData.Rects[27 + 21 * i], Common.m_MFormat);
                }
            }
        }
    }
}
