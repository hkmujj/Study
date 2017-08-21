using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class AuxiliaryFault : NewQBaseclass
    {
        private List<string> m_Resources;
        private readonly bool[] m_BtnIsdown = new bool[4];
        
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            m_Resources = new List<string>()
            {
                String.Empty,                                                            //0
                ResourceFacade.MaintenanceAuxiliaryResourceStates,
                ResourceFacade.MaintenanceBasicResourceMVBcommunication,
                ResourceFacade.MaintenanceAuxiliaryResourceCriticalFault,
                ResourceFacade.MaintenanceAuxiliaryResourceMajorFault,
                ResourceFacade.MaintenanceAuxiliaryResourceMinorFault,                   //5
                ResourceFacade.MaintenanceAuxiliaryResourceCVSiscorrestlyrunning,
                "ACE1",
                "ACE2",
                ResourceFacade.MaintenanceAuxiliaryResourceResetCVS1,
                ResourceFacade.MaintenanceAuxiliaryResourceResetCVS2,                   //10
                ResourceFacade.MaintenanceAuxiliaryResourceListoffaults,
            };
            
            AuxiliaryFaultDatacs.InitData();
            return true;
        }
        public override void paint(Graphics g)
        {
            DrawRcts(g);
            DrawState(g);
            DrawImgs(g);
            DrawWords(g);
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (AuxiliaryFaultDatacs.Regions[index].IsVisible(point))
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 1, 0); 
                    m_BtnIsdown[index] = true;
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (AuxiliaryFaultDatacs.Regions[index].IsVisible(point))
                {
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[index], 0, 0);
                    m_BtnIsdown[index] = false;
                    break;
                }

            }
            return true;
        }

        private void DrawRcts(Graphics e)
        {
            for (int i = 0; i < 34; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, AuxiliaryFaultDatacs.Rects[0 + i]);
            }

        }

        private void DrawWords(Graphics e)
        {
            //States
            e.DrawString(m_Resources[1], Common.m_Font11B, Common.m_BlackBrush,
                AuxiliaryFaultDatacs.Rects[0], Common.m_MFormat);
            //下面7种状态
            for (int i = 0; i < 5; i++)
            {
                e.DrawString(m_Resources[2 + i], Common.m_Font11B, Common.m_BlackBrush,
                    AuxiliaryFaultDatacs.Rects[1 + i]);
            }
            //故障清单
            e.DrawString(m_Resources[11], Common.m_Font11B, Common.m_BlackBrush,
                AuxiliaryFaultDatacs.Rects[7], Common.m_MFormat);
            //ACE1-ACE2
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[7+i], Common.m_Font11B, Common.m_BlackBrush,
                   AuxiliaryFaultDatacs.Rects[17+i], Common.m_MFormat);
            }
            //ACE1-ACE2
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[7 + i], Common.m_Font11B, Common.m_BlackBrush,
                   AuxiliaryFaultDatacs.Rects[31 + i], Common.m_MFormat);
            }
            //2个按钮上的文字 
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Resources[9 + i], Common.m_Font10B, Common.m_BlackBrush,
                    AuxiliaryFaultDatacs.Rects[34 + i], Common.m_MFormat);
            }
        }

        private void DrawImgs(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsdown[i])
                {
                    e.DrawImage(m_Imgs[2], AuxiliaryFaultDatacs.Rects[i + 34]);
                }
                else
                {
                    e.DrawImage(m_Imgs[1], AuxiliaryFaultDatacs.Rects[i + 34]);
                }
            }
        }

        private void DrawState(Graphics e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[4], AuxiliaryFaultDatacs.Rects[i + 19].X + 13, AuxiliaryFaultDatacs.Rects[i + 19].Y, 24, 24);
                }
                else
                {
                    if(BoolList[m_BoolIds[i+10]])
                        e.DrawImage(m_Imgs[3], AuxiliaryFaultDatacs.Rects[i + 19].X + 13, AuxiliaryFaultDatacs.Rects[i + 19].Y, 24, 24);
                    else
                        e.DrawImage(m_Imgs[5], AuxiliaryFaultDatacs.Rects[i + 19].X + 13, AuxiliaryFaultDatacs.Rects[i + 19].Y, 24, 24);
                }
                    
            }
        }
    }
}
