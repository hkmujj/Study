using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Sdi : NewQBaseclass
    {
        private  List<string> m_Dictionary;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            SdiData.InitData();

            m_Dictionary = new List<string>()
            {
                string.Empty,
                ResourceFacade.SDIResourceSDI,
                ResourceFacade.SDIResourceFailurestate,
                ResourceFacade.SDIResourceAlarmdetection,
                ResourceFacade.SDIResourcePre_Alarmdetection,
                ResourceFacade.SDIResourceTc1,
                ResourceFacade.SDIResourceM1,
                ResourceFacade.SDIResourceM2,
                ResourceFacade.SDIResourceM3,
                ResourceFacade.SDIResourceM4,
                ResourceFacade.SDIResourceTc2
            };

            return true;
        }
        public override void paint(Graphics g)
        {
            Fillrects(g);
            DrawRcts(g);
            DrawWords(g);
            DrawStates(g);
        }

        private void DrawRcts(Graphics e)
        {
            for (int i = 0; i < 27; i++)
            {
                e.DrawRectangle(Common.m_BlackPen, SdiData.Rects[1+i]);
            }

        }

        private void Fillrects(Graphics e)
        {
            for (int i = 0; i < 27; i++)
            {
                e.FillRectangle(Common.m_WhiteBrush, SdiData.Rects[1 + i]);
            }
        }

        private void DrawWords(Graphics e)
        {
            //维护：烟火检测
            e.DrawString(m_Dictionary[1], Common.m_Font14B, Common.m_BlackBrush,
                           SdiData.Rects[0], Common.m_MFormat);
            //信息
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Dictionary[2 + i], Common.m_Font11, Common.m_BlackBrush,
                           SdiData.Rects[1 + i], Common.m_LeftCenterFormat);
            }
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(m_Dictionary[5 + i], Common.m_Font11B, Common.m_BlackBrush,
                          SdiData.Rects[4 + i], Common.m_MFormat);
            }
        }

        private void DrawStates(Graphics e)
        {
            for (int i = 0; i < 18; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawImage(m_Imgs[1], SdiData.Rects[10 + i].X + 14,
                            SdiData.Rects[10 + i].Y + 2, 22,
                            22);
                }
                else
                {
                    if (BoolList[m_BoolIds[18 + i]])
                    {
                        e.DrawImage(m_Imgs[2], SdiData.Rects[10 + i].X+14,
                            SdiData.Rects[10 + i].Y+2,22,
                            22);
                    }
                    else
                        e.DrawImage(m_Imgs[0], SdiData.Rects[10 + i].X + 14,
                            SdiData.Rects[10 + i].Y + 2, 22,
                            22);
                }
            }
        }
    }
}
