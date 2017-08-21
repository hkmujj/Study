using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class BaterryMaintance:NewQBaseclass
    {
        private List<string> m_Dictionary;
        private float a = 0.0f;
        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            BaterryData.InitData();
            m_Dictionary = new List<string>()
            {
                string.Empty,
                ResourceFacade.MaintenanceBaterryResourceBattery1capacity,
                ResourceFacade.MaintenanceBaterryResourceBattery2capacity,
                ResourceFacade.MaintenanceBaterryResourceOK,
                ResourceFacade.MaintenanceBaterryResourceNOK,
                ResourceFacade.MaintenanceBaterry 
            };
            return true;
        }

        public override void paint(Graphics g)
        {
            
            DrawImgs(g);
            Fillrects(g);
            DrawRcts(g);
            DrawWords(g);

        }

        private void DrawImgs(Graphics e)
        {
            e.DrawImage(m_Imgs[1], BaterryData.Rects[0]);
        }

        private void DrawRcts(Graphics e)
        {
            for (int i=0;i<10;i++)
            {
                e.DrawRectangle(Common.m_BlackPen, BaterryData.Rects[1+i]);
            }
           
        }

        private void Fillrects(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                if (FloatList[m_FoolatIds[i]] < 100)
                    a = FloatList[m_FoolatIds[i]] / 100;
                else
                    a = 1.0f;
                e.FillRectangle(Common.m_GreenBrush, BaterryData.Rects[i + 1].X, BaterryData.Rects[i + 1].Y,
                              a * BaterryData.Rects[i + 1].Width, BaterryData.Rects[i + 1].Height);
            }
            for (int i = 0; i < 8; i++)
            {
                e.FillRectangle(Common.m_GreenBrush, BaterryData.Rects[i + 3]);
            }

        }

        private void DrawWords(Graphics e)
        {
            for (int i=0;i<2;i++)
            {
                e.DrawString(m_Dictionary[1 + i], Common.m_Font11B, Common.m_BlackBrush, BaterryData.Rects[i + 11],
                    Common.m_MFormat);
            }
            //CVS中Bat状态
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[i]])
                {
                    e.DrawString(m_Dictionary[3], Common.m_Font10B, Common.m_BlackBrush, BaterryData.Rects[5+4*i],
                                       Common.m_MFormat);
                }
                else if (BoolList[m_BoolIds[2+i]])
                {
                    e.DrawString(m_Dictionary[4], Common.m_Font10B, Common.m_BlackBrush, BaterryData.Rects[5 + 4 * i],
                                       Common.m_MFormat);
                }
            }
            //输出电压信息
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds [4+3*i]].ToString ("0")+" V", Common.m_Font10B, Common.m_BlackBrush, BaterryData.Rects[6 + 4 * i],
                                       Common.m_MFormat);
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[2+ i]].ToString("0") + " A", Common.m_Font10B, Common.m_BlackBrush, BaterryData.Rects[3+i],
                                       Common.m_MFormat);
                e.DrawString(FloatList[m_FoolatIds[5 + i]].ToString("0") + " A", Common.m_Font10B, Common.m_BlackBrush, BaterryData.Rects[7 + i],
                                       Common.m_MFormat);
            }
            //维护：蓄电池
            e.DrawString(m_Dictionary[5], Common.m_Font12B, Common.m_BlackBrush, BaterryData.Rects[13],
                    Common.m_MFormat);
        }
    }
}
