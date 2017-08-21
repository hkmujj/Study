using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class FaultInformation : NewQBaseclass
    {
        private List<string> m_Resources;

        /// <summary>
        /// 左侧3个按钮的状态
        /// </summary>
        public static bool[] m_BtnIsdown = new bool[5];

        /// <summary>
        /// 移动的行号
        /// </summary>
        public static int m_RowId = 0;

        public override bool init(ref int nErrorObjectIndex)
        {

            IntiData();
            FaultInformationData.InitData();

            m_Resources = new List<string>()
            {
                string.Empty,                                                    
                ResourceFacade.MaintenanceBrakeResourceFalutEvent,
                ResourceFacade.MaintenanceBrakeResourceTrainLines,
                ResourceFacade.MaintenanceBrakeResourceSelfTest,
                ResourceFacade.MaintenanceBrakeResourceStates,
                ResourceFacade.MaintenanceBrakeResourceMVBCommunication,
                ResourceFacade.MaintenanceBrakeResourceMajorFault,
                ResourceFacade.MaintenanceBrakeResourceMinorFault,
                ResourceFacade.MaintenanceBrakeResourceGTW1,
                ResourceFacade.MaintenanceBrakeResourceGTW2,
                ResourceFacade.MaintenanceBrakeResourceGTW3,
                ResourceFacade.MaintenanceBrakeResourceGTW4,
                ResourceFacade.MaintenanceBrakeResourceTc1,
                ResourceFacade.MaintenanceBrakeResourceM1,
                ResourceFacade.MaintenanceBrakeResourceM2,
                ResourceFacade.MaintenanceBrakeResourceM3,
                ResourceFacade.MaintenanceBrakeResourceM4,
                ResourceFacade.MaintenanceBrakeResourceTc2,
                ResourceFacade.MaintenanceBrakeResourceListoffaults,
                ResourceFacade.MaintenanceResourceMaintenanceBrake                             //19
            };

            return true;
        }

        public override void paint(Graphics g)
        {
            DrawLine(g);
            DrawRects(g);
            DrawImgs(g);
            Fillrects(g);
            DrawWords(g);

        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 5; index++)
            {
                if (FaultInformationData.Regions[index].IsVisible(point))
                {
                    m_BtnIsdown[index] = true;
                    if (index == 1 || index == 2)
                    {
                        m_BtnIsdown[0] = false;
                    }
                    if (index == 3)
                    {
                        m_RowId = (m_RowId <= 0) ? 8 : (m_RowId-1);
                    }
                    if (index == 4)
                    {
                        m_RowId = (m_RowId + 1) % 9;
                    }
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 5; index++)
            {
                if (FaultInformationData.Regions[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                            BtnisDownValue(0);
                            break;
                        case 1:
                            m_BtnIsdown[1] = false;
                            append_postCmd(CmdType.ChangePage, 50, 1, 0);
                            break;
                        case 2:
                            m_BtnIsdown[2]=false;
                            append_postCmd(CmdType.ChangePage, 13, 1, 0);
                            break;
                        default:
                            m_BtnIsdown[index] = false;
                            break;
                    }
                }
            }
            return true;
        }

        private void DrawLine(Graphics g)
        {
            for (int i = 0; i < 10; i = i + 2)
            {
                g.DrawLine(Common.m_BlackPen, FaultInformationData.Points[i], FaultInformationData.Points[i + 1]);
            }
            g.DrawLine(Common.m_BlackPen, FaultInformationData.Points[0], FaultInformationData.Points[8]);
            for (int i = 0; i < 8; i = i + 2)
            {
                g.DrawLine(Common.m_BlackPen, FaultInformationData.Points[10 + i], FaultInformationData.Points[i + 11]);
            }
            g.DrawLine(Common.m_BlackPen, FaultInformationData.Points[1], FaultInformationData.Points[9]);
        }

        private void DrawRects(Graphics g)
        {
            for (int i = 0; i < 17; i++)
            {
                g.DrawRectangle(Common.m_BlackPen, FaultInformationData.Rects[i]);
            }
        }

        private void DrawImgs(Graphics g)
        {
            //左边3个按钮
            for (int i = 0; i < 3; i++)
            {
                if (m_BtnIsdown[i])
                {
                    g.DrawImage(m_Imgs[1], FaultInformationData.Rects[42 + i]);
                }
                else
                {
                    g.DrawImage(m_Imgs[0], FaultInformationData.Rects[42 + i]);
                }
            }
            //右边两个按钮
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsdown[3+i])
                {
                    g.DrawImage(m_Imgs[1], FaultInformationData.Rects[45 + i]);
                }
                else
                {
                    g.DrawImage(m_Imgs[0], FaultInformationData.Rects[45 + i]);
                }
                g.DrawImage(m_Imgs[i + 2], FaultInformationData.Rects[20 + i]);
            }
            #region//状态
            //Mvb commnucition 正常  Major Fault 正常 Minor Fault 正常
            for (int i = 0; i < 12; i++)
            {
                if (BoolList[m_BoolIds[0 + i]])
                {
                    g.DrawImage(m_Imgs[5], FaultInformationData.Rects[102 + i]);
                }
                else
                {
                    if(BoolList[m_BoolIds[12+i]])
                    g.DrawImage(m_Imgs[4], FaultInformationData.Rects[102 + i]);
                    else
                        g.DrawImage(m_Imgs[6], FaultInformationData.Rects[102 + i]);
                }
            }
            #endregion
        }

        private void DrawWords(Graphics g)
        {
            //左侧3个按钮的文字
            for (int i = 0; i < 3; i++)
            {
                g.DrawString(m_Resources[1 + i], Common.m_Font10, Common.m_BlackBrush, FaultInformationData.Rects[17 + i],
                    Common.m_MFormat);
            }

            //state
            g.DrawString(m_Resources[4], Common.m_Font11B, Common.m_BlackBrush, FaultInformationData.Rects[22],
                Common.m_CenterFormat);
            //MVB conminucation-Minor Falut
            for (int i = 0; i < 3; i++)
            {
                g.DrawString(m_Resources[5 + i], Common.m_Font11B, Common.m_BlackBrush, FaultInformationData.Rects[23 + i]);
            }
            //GTW1-GTW4
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(m_Resources[8 + i], Common.m_Font10B, Common.m_BlackBrush, FaultInformationData.Rects[26 + i],
                    Common.m_CenterFormat); 
            }
            //List of faults
            g.DrawString(m_Resources[18], Common.m_Font12B, Common.m_BlackBrush, FaultInformationData.Rects[0],
                Common.m_MFormat);
            for (int i = 0; i < 6; i++)
            {
                g.DrawString(m_Resources[12 + i], Common.m_Font12B, Common.m_BlackBrush, FaultInformationData.Rects[10 + i],
                    Common.m_MFormat);

            }
            g.DrawString(m_Resources[19], Common.m_Font12B, Common.m_BlackBrush, FaultInformationData.Rects[101],
                    Common.m_MFormat);
        }

        private void Fillrects(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                if (m_BtnIsdown[0])
                {
                    g.FillRectangle(Common.m_WhiteBrush, FaultInformationData.Rects[m_RowId + 1].X + 1,
                        FaultInformationData.Rects[m_RowId + 1].Y + 1,
                        FaultInformationData.Rects[m_RowId + 1].Width - 1, FaultInformationData.Rects[m_RowId + 1].Height - 1);
                }
            }
        }

        /// <summary>
        /// 把所有btnisDown的值制为false
        /// </summary>
        private void BtnisDownValue(int value)
        {
            for (int i = 0; i < 3; i++)
            {
                m_BtnIsdown[i] = false;
            }
            m_BtnIsdown[value] = true;
        }
    }
}