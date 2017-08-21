using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
     class ManualTrainStartUpManagementScreen:TrainStartUpManagementScreen
    {
        private List<RectangleF> m_Rects = new List<RectangleF>();

        private readonly List<Region> m_Regions = new List<Region>();
        /// <summary>
        /// 按键的状态
        /// </summary>
        private readonly bool[] m_BtnIsDown = new bool[2];
        /// <summary>
        /// 辅助变流器的按键次数
        /// </summary>
        private static int m_AuxiliaryNum;
        /// <summary>
        /// HVAC的按键次数
        /// </summary>
        private static int m_HVACNum;

        public override bool init(ref int nErrorObjectIndex)
        { 
            Init();
            return base.init(ref nErrorObjectIndex);
        }
        public override void paint(Graphics g)
        {
       
            DrawButton(g);
            Draw(g);
           base.paint(g);
        }
        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (;index<2;index++)
            {
                if (m_Regions[index].IsVisible(point))
                {
                     
                    switch (index)
                    {
                        
                        case 0:
                         
                            if (m_AuxiliaryNum%2 == 0)
                            {
                                m_BtnIsDown[0] = true;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                            }
                            else
                            {
                                m_BtnIsDown[0] = false;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                            }

                            m_AuxiliaryNum++;
                            break;
                        case 1:
                           if (m_HVACNum%2 == 0)
                            {
                                m_BtnIsDown[1] = true;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                               
                            }
                            else
                            {
                                m_BtnIsDown[1] = false;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                            }
                            m_HVACNum++;
                            break;
                    }
                }
            }
            return true;
        }

        public override void DrawButton(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                if (m_BtnIsDown[0 + i])
                {
                    e.DrawImage(m_Imgs[13], m_Rects[i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[12], m_Rects[i]);
                }
                e.DrawImage(m_Imgs[10+i], m_Rects[2+i]);
            }
           
        }

        private void Init()
        {
            for (int i = 0; i < 4; i++)
            {
                Coordinate.AddRectangle(ViewIDName.TrainStartUpManagementScreen, ref m_Rects, 48 + i);
            }
            for (int i=0;i<2;i++)
            {
                m_Regions.Add(new Region(m_Rects[2+i]));
            }
        }
    }
    }


