
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class TrainLines : NewQBaseclass
    {
        private List<string> m_Resources;
        private bool[] m_BtnIsdown = new bool[3];
        public override bool init(ref int nErrorObjectIndex)
        {
            TrainLinesData.InitData();
            IntiData();
            return true;
        }
        public override void paint(Graphics g)
        {
            m_Resources = new List<string>()
            {
                string.Empty,
                ResourceFacade.MaintenanceBrakeResourceGTW1,
                ResourceFacade.MaintenanceBrakeResourceGTW2,
                ResourceFacade.MaintenanceBrakeResourceGTW3,
                ResourceFacade.MaintenanceBrakeResourceGTW4,
                ResourceFacade.MaintenanceBrakeResourceFalutEvent,              //5
                ResourceFacade.MaintenanceBrakeResourceTrainLines,
                ResourceFacade.MaintenanceBrakeResourceSelfTest,
                ResourceFacade.MaintenanceResourceMaintenanceBrake,
                ResourceFacade.MaintenanceBrakeResourceReserveTrainLines,       //9
                ResourceFacade .MaintenanceBrakeResourceBreakingCommandTrainLines,
                ResourceFacade.MaintenanceBrakeResourceUrgentBreakingTrainLines,
                ResourceFacade.MaintenanceBrakeResourceDraggingAccreditTrainLines,
                ResourceFacade .MaintenanceBrakeResourceDraggingTrainLines
            };
            Fillrects(g);
            DrawRects(g);
            DrawImgs(g);
            DarwWords(g);
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (TrainLinesData.Regions[index].IsVisible(point))
                {
                    BtnisDownValue(index);
                }
            }
            return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (TrainLinesData.Regions[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                            m_BtnIsdown[0] = false;
                            append_postCmd(CmdType.ChangePage, 12, 1, 0);
                            break;
                        case 1:
                            break;
                        case 2:
                            m_BtnIsdown[2] = false;
                            append_postCmd(CmdType.ChangePage, 13, 1, 0);
                            break;
                    }
                }
            }
            return base.mouseUp(point);
        }
        private void DrawRects(Graphics e)
        {
            for (int i = 0; i < 29; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, TrainLinesData.Rects[i]);
            }
        }

        private void Fillrects(Graphics e)
        {
            for (int i = 0; i < 29; i++)
            {
                e.FillRectangle(Common.m_GreyWhite, TrainLinesData.Rects[i]);
            }
        }
        private void DrawImgs(Graphics e)
        {
            //左侧三个按钮
            for (int i = 0; i < 3; i++)
            {
                if (m_BtnIsdown[i])
                {
                    e.DrawImage(m_Imgs[1], TrainLinesData.Rects[i + 41]);
                }
                else
                {
                    e.DrawImage(m_Imgs[0], TrainLinesData.Rects[i + 41]);
                }
            }
            //网关阀状态显示
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                    for (int m = 0; m < 4; m++)
                    {
                        if (BoolList[m_BoolIds[m + j * 4 + i * 8]])
                        {
                            e.DrawImage(m_Imgs[2 + j], TrainLinesData.Rects[45 + m + 4 * i]);
                        }
                        else if (!BoolList[m_BoolIds[m + i * 8]] && !BoolList[m_BoolIds[m + 4 + i * 8]])
                        {
                            e.DrawImage(m_Imgs[4], TrainLinesData.Rects[45 + m + 4 * i]);
                        }
                    }
            }
        }

        private void DarwWords(Graphics e)
        {
            //网关阀
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Resources[1 + i], Common.m_Font10B, Common.m_BlackBrush,
                    TrainLinesData.Rects[37 + i], Common.m_MFormat);
            }
            //列车线信息
            for (int i = 0; i < 5; i++)
            {
                e.DrawString(m_Resources[9 + i], Common.m_Font10, Common.m_BlackBrush,
                                                      TrainLinesData.Rects[32 + i], Common.m_LeftFormat);
            }
            //按钮文字
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Resources[5 + i], Common.m_Font10, Common.m_BlackBrush,
                                   TrainLinesData.Rects[29 + i], Common.m_MFormat);
            }
            //维护制动
            e.DrawString(m_Resources[8], Common.m_Font12B, Common.m_BlackBrush,
                                  TrainLinesData.Rects[44], Common.m_MFormat);
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