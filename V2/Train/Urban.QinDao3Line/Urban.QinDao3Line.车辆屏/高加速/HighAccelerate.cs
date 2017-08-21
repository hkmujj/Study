using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class HighAccelerate : NewQBaseclass
    {
        public static bool[] m_BtnIsDown = new bool[2];
        private List<string> m_string;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            HighAccelerateData.Init();
            m_string = new List<string>()
            {
                string.Empty,                                          //0
                ResourceFacade.AccelerationHasActivation,              //1
                ResourceFacade .CommonUserYes,                         //2
                ResourceFacade.CommonUserNo                            //3
            };
            return true;
        }
        public override void paint(Graphics g)
        {
            PaintButtonState(g);
            DrawString(g);
        }

        private void PaintButtonState(Graphics e)
        {
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[i])
                    e.DrawImage(m_Imgs[1], HighAccelerateData.m_Rects[1 + i]);
                else
                    e.DrawImage(m_Imgs[0], HighAccelerateData.m_Rects[1 + i]);
            }
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (HighAccelerateData.m_Regions[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                    if (index == 0)
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                        m_BtnIsDown[1] = false;
                    }
                    else
                    {
                        append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                        m_BtnIsDown[0] = false;
                    }
                    break;
                }
            }
            return true;
        }

        private void DrawString(Graphics e)
        {
            e.DrawString(m_string[1], Common.m_Font16B, Common.m_BlackBrush, HighAccelerateData.m_Rects[0],
                                Common.m_MFormat);
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_string[2 + i], Common.m_Font10B, Common.m_BlackBrush, HighAccelerateData.m_Rects[1 + i],
                                               Common.m_MFormat);
            }
        }
    }
}
