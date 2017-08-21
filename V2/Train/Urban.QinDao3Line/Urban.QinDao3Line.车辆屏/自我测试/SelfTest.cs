using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SelfTest:NewQBaseclass
    {
        private List<string> m_Resources;
        private bool[] m_BtnIsdown = new bool[4];
        public override bool init(ref int nErrorObjectIndex)
        {
            SelftestData.InitData();
            IntiData();
            return true;
        }
        public override void paint(Graphics g)
        {
            m_Resources = new List<string>()
            {
                string.Empty,
                ResourceFacade.MaintenanceBrakeResourceStartingcondition,
                ResourceFacade.MaintenanceBrakeResourceTestinprogress,
                ResourceFacade.MaintenanceBrakeResourceTestresult,
                ResourceFacade.MaintenanceBrakeResourceGTW1,
                ResourceFacade.MaintenanceBrakeResourceGTW2,
                ResourceFacade.MaintenanceBrakeResourceGTW3,
                ResourceFacade.MaintenanceBrakeResourceGTW4,
                ResourceFacade.MaintenanceBrakeResourceFalutEvent,
                ResourceFacade.MaintenanceBrakeResourceTrainLines,
                ResourceFacade.MaintenanceBrakeResourceSelfTest,
                ResourceFacade.MaintenanceBrakeResourceSTART,
                ResourceFacade.MaintenanceBrakeResourceSelfTestCondition,
                ResourceFacade.MaintenanceBrakeResourceTobedefine,
                ResourceFacade.MaintenanceResourceMaintenanceBrake           //14
            };
            Fillrects(g);
            DrawImgs(g);
            DarwWords(g);
            DrawRects(g);
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (;index<4;index++)
            {
                if (SelftestData.Regions[index].IsVisible(point))
                {
                    BtnisDownValue(index);
                    if (index == 3)
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                    }
                }
            }
            return base.mouseDown(point);
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 4; index++)
            {
                if (SelftestData.Regions[index].IsVisible(point))
                {
                    switch (index)
                    {
                        case 0:
                            m_BtnIsdown[0]=false;
                            append_postCmd(CmdType.ChangePage, 12, 1, 0);
                            break;
                        case 1:
                            m_BtnIsdown[1] = false;
                            append_postCmd(CmdType.ChangePage, 50, 1, 0);
                            break;
                        case 2:
                            break;
                        case 3:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                            break;
                    }
                }
            }
            return base.mouseUp(point);
        }
        private void DrawRects(Graphics e)
        {
            for (int i=0;i<19;i++)
            {
                e.DrawRectangle(Common.m_BlackPen, SelftestData.Rects[1 + i]);
            }  
        }

        private void Fillrects(Graphics e)
        {
            for (int i=0;i<19;i++)
            {
                e.FillRectangle(Common.m_GreyWhite, SelftestData.Rects[1 + i]);
            }
        }
        private void DrawImgs(Graphics e)
        {
            //左侧四个按钮
            for (int i=0;i<3;i++)
            {
                if (m_BtnIsdown[i])
                {
                    e.DrawImage(m_Imgs[1], SelftestData.Rects[i + 32]);
                }
                else
                {
                    e.DrawImage(m_Imgs[0], SelftestData.Rects[i + 32]);
                }
            }
            if (m_BtnIsdown[3])
            {
                e.DrawImage(m_Imgs[3], SelftestData.Rects[35]);
            }
            else
            { 
                e.DrawImage(m_Imgs[2], SelftestData.Rects[35]);
            }

            //测试状态显示
            for (int i=0;i<4;i++)
            {
                //启动条件
                if (BoolList[m_BoolIds[0+i]])
                {
                    e.DrawImage(m_Imgs[4], SelftestData.Rects[8 + i].X+11, SelftestData.Rects[8 + i].Y,
                                  m_Imgs[4].Width,m_Imgs[4].Height);
                }
                else if (BoolList[m_BoolIds[4+ i]])
                {
                    e.DrawImage(m_Imgs[5], SelftestData.Rects[8 + i].X+11, SelftestData.Rects[8 + i].Y,
                                 m_Imgs[5].Width, m_Imgs[5].Height);
                }
                //测试进行中
                if (BoolList[m_BoolIds[8+ i]])
                {
                    e.DrawImage(m_Imgs[6], SelftestData.Rects[12 + i].X+10, SelftestData.Rects[12 + i].Y,
                                 m_Imgs[6].Width, m_Imgs[6].Height);
                }
                else  if (BoolList[m_BoolIds[12 + i]])
                {
                    e.DrawImage(m_Imgs[7], SelftestData.Rects[12 + i].X+10, SelftestData.Rects[12 + i].Y,
                                  m_Imgs[7].Width, m_Imgs[7].Height);
                }
                //测试结果
                //测试失败
                if (BoolList[m_BoolIds[16 + i]])
                {
                    e.DrawImage(m_Imgs[8], SelftestData.Rects[16 + i].X+15, SelftestData.Rects[16 + i].Y,
                                  m_Imgs[8].Width, m_Imgs[8].Height);
                    m_BtnIsdown[3] = false;
                }
                //测试中止
                else if (BoolList[m_BoolIds[20 + i]])
                {
                    e.DrawImage(m_Imgs[9], SelftestData.Rects[16 + i].X+15, SelftestData.Rects[16 + i].Y,
                                   m_Imgs[9].Width, m_Imgs[9].Height);
                    m_BtnIsdown[3] = false;
                }
                //测试成功
                else if (BoolList[m_BoolIds[24 + i]])
                {
                    e.DrawImage(m_Imgs[10], SelftestData.Rects[16 + i].X+15, SelftestData.Rects[16 + i].Y,
                        m_Imgs[10].Width, m_Imgs[10].Height);
                    m_BtnIsdown[3] = false;
                }
                //测试中
                else if (BoolList[m_BoolIds[28 + i]])
                {
                    e.DrawImage(m_Imgs[11], SelftestData.Rects[16 + i].X+10, SelftestData.Rects[16 + i].Y,
                                   m_Imgs[11].Width-5, m_Imgs[11].Height);
                } 
            }         
        }

        private void DarwWords(Graphics e)
        {
            for (int i=0;i<4;i++)
            {
                e.DrawString(m_Resources[4 + i], Common.m_Font10B, Common.m_BlackBrush,
                    SelftestData.Rects[28 + i], Common.m_MFormat);
            }
            for (int i=0;i<3;i++)
            {
                e.DrawString(m_Resources[1 + i], Common.m_Font10B, Common.m_BlackBrush,
                   SelftestData.Rects[25 + i]);
            }
            for (int i = 0; i < 4; i++)
            {
                e.DrawString(m_Resources[8 + i], Common.m_Font10, Common.m_BlackBrush,
                    SelftestData.Rects[20+ i], Common.m_MFormat);
            }
            e.DrawString(m_Resources[12], Common.m_Font10B, Common.m_BlackBrush,
                   SelftestData.Rects[0]);
            e.DrawString(m_Resources[13], Common.m_Font10, Common.m_BlackBrush,
                 SelftestData.Rects[24]);
            //维护：制动
            e.DrawString(m_Resources[14], Common.m_Font12B, Common.m_BlackBrush,
                 SelftestData.Rects[36], Common.m_MFormat);
        }
        /// <summary>
        /// 把所有btnisDown的值制为false
        /// </summary>
        private void BtnisDownValue(int value)
        {
            for (int i = 0; i < 4; i++)
            {
                m_BtnIsdown[i] = false;

            }
            m_BtnIsdown[value] = true;
        }
    }
}
