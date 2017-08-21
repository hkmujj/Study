using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.底层共用;
using Urban.QingDao3Line.MMI.Resource;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MainScreen : NewQBaseclass
    {
        private List<RectangleF> m_RectsList = new List<RectangleF>();

        private readonly List<Rectangle> m_Rects = new List<Rectangle>();

        public override bool init(ref int nErrorObjectIndex)
        {
            IntiData();
            Init();
            return true;
        }

        public override void paint(Graphics g)
        {
            PainttState(g);
            DrawWords(g);
        }

        private void PainttState(Graphics e)
        {
            //转向架制动
            for (int i = 0; i < 6; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (BoolList[m_BoolIds[j + k * 4 + i * 12]])
                        {
                            if (k == 1)
                            {
                                e.DrawImage(m_Imgs[4 + j], m_RectsList[81 + k + i * 3]);
                            }
                            else
                            {
                                e.DrawImage(m_Imgs[j], m_RectsList[81 + k + i * 3]);
                            }
                        }
                    }
                    if (!BoolList[m_BoolIds[k * 4 + i * 12]] && !BoolList[m_BoolIds[1 + k * 4 + i * 12]]
                        && !BoolList[2 + m_BoolIds[k * 4 + i * 12]] && !BoolList[m_BoolIds[3 + k * 4 + i * 12]])
                    {
                        if (k == 1)
                        {
                            e.DrawImage(m_Imgs[5], m_RectsList[81 + k + i * 3]);
                        }
                        e.DrawImage(m_Imgs[1], m_RectsList[81 + k + i * 3]);
                    }
                }
            }
            //司机室旁路
            for (int i = 0; i < 24; i++)
            {
                if (BoolList[m_BoolIds[108 + i]])
                {
                    e.DrawImage(m_Imgs[8], m_RectsList[113 + i]);
                }
                else
                {
                    e.DrawImage(m_Imgs[9], m_RectsList[113 + i]);
                }
            }
            //TC1辅助变流器

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_BoolIds[132 + i]])
                {
                    e.DrawImage(m_Imgs[10 + i], m_RectsList[71]);
                }
            }
            if (!BoolList[m_BoolIds[132]] && !BoolList[m_BoolIds[133]] && !BoolList[m_BoolIds[134]] && !BoolList[m_BoolIds[135]])
            {
                e.DrawImage(m_Imgs[13], m_RectsList[71]);
            }
            //Tc2辅助变流器
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[m_BoolIds[164 + i]])
                {
                    e.DrawImage(m_Imgs[10 + i], m_RectsList[72]);
                }
            }
            if (!BoolList[m_BoolIds[164]] && !BoolList[m_BoolIds[165]] && !BoolList[m_BoolIds[166]] && !BoolList[m_BoolIds[167]])
            {
                e.DrawImage(m_Imgs[13], m_RectsList[72]);
            }
            //HSCB
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[m_BoolIds[i * 7 + j + 140]])
                    {
                        e.DrawImage(m_Imgs[18 + j], m_RectsList[73 + i * 2]);
                    }
                }
                if (!BoolList[m_BoolIds[i * 7 + 140]] && !BoolList[m_BoolIds[i * 7 + 141]]
                    && !BoolList[m_BoolIds[i * 7 + 142]])
                {
                    e.DrawImage(m_Imgs[20], m_RectsList[73 + i * 2]);
                }
            }
            //PCE
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[m_BoolIds[i * 7 + j + 136]])
                    {
                        e.DrawImage(m_Imgs[14 + j], m_RectsList[74 + i * 2]);
                    }
                }
                if (!BoolList[m_BoolIds[i * 7 + 136]] && !BoolList[m_BoolIds[i * 7 + 137]]
                    && !BoolList[m_BoolIds[i * 7 + 138]] && !BoolList[m_BoolIds[i * 7 + 139]])
                {
                    e.DrawImage(m_Imgs[17], m_RectsList[74 + i * 2]);
                }
            }
            //总风压力正常
            for (int i = 0; i < 2; i++)
            {
                if (BoolList[m_BoolIds[168 + i]])
                {
                    e.DrawImage(m_Imgs[22], m_RectsList[69 + i]);

                }
                else
                {
                    e.DrawImage(m_Imgs[21], m_RectsList[69 + i]);
                }
            }
            //制动压力正常
            for (int i = 0; i < 12; i++)
            {
                if (FloatList[m_FoolatIds[i + 2]] == 0)
                {
                    e.DrawImage(m_Imgs[25], m_Rects[2 + i]);
                }
                else
                {
                    if (BoolList[m_BoolIds[170 + i]])
                    {
                        e.DrawImage(m_Imgs[24], m_Rects[2 + i]);
                    }
                    else
                    {
                        e.DrawImage(m_Imgs[23], m_Rects[2 + i]);
                    }
                }
            }
            //高加速已启动
            if (HighAccelerate.m_BtnIsDown[0])
            {
                e.FillRectangle(Common.m_RedBrush1, m_Rects[14]);
                e.DrawRectangle(Common.m_BlackPen1, m_Rects[14]);
                e.DrawString(ResourceFacade.AccelerationHasActivation, Common.m_Font12B, Common.m_BlackBrush,
                    m_Rects[14], Common.m_MFormat);
            }
        }

        private void DrawWords(Graphics e)
        {
            //MPR1、MPR2中的风缸压力
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i]].ToString("0.0"), FontItems.Fonts_Regular(FontName.Arial, 11, false), Common.m_BlackBrush,
                    m_Rects[i], Common.m_MFormat);
            }
            //制动缸压力数值
            for (int i = 0; i < 12; i++)
            {
                if ((FloatList[m_FoolatIds[i + 2]]) == 0)
                {
                    e.DrawString("——", FontItems.Fonts_Regular(FontName.Arial, 11, true), Common.m_BlackBrush,
                                      m_Rects[i + 2], Common.m_BottomFormat);
                }
                else
                    e.DrawString((FloatList[m_FoolatIds[i + 2]]).ToString("0.0"), FontItems.Fonts_Regular(FontName.Arial, 11, true), Common.m_BlackBrush,
                       m_Rects[i + 2], Common.m_BottomFormat);
            }
            //车辆载荷
            for (int i = 0; i < 6; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[14 + i]].ToString("0.0") + " %", FontItems.Fonts_Regular(FontName.宋体, 10, false), Common.m_BlackBrush,
                    m_RectsList[63 + i], Common.m_MFormat);
            }
            //牵引/制动力
            for (int i = 0; i < 4; i++)
            {
                if (FloatList[m_FoolatIds[20 + i]].ToString("0.0") == "0.0")
                {
                    e.DrawString("----", FontItems.Fonts_Regular(FontName.宋体, 14, false), Common.m_BlackBrush,
                    m_Rects[15 + i], Common.m_MFormat);
                }
                else
                    e.DrawString(FloatList[m_FoolatIds[20 + i]].ToString("0.0"), FontItems.Fonts_Regular(FontName.宋体, 12, false), Common.m_BlackBrush,
                         m_Rects[15 + i], Common.m_MFormat);
                e.DrawString(" KN", FontItems.Fonts_Regular(FontName.宋体, 12, false), Common.m_BlackBrush,
                    m_RectsList[100 + i], Common.m_RightCenterFormat);
            }
        }

        private void Init()
        {
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            Coordinate.RectangleFLists(ViewIDName.MainDrivingScreen, ref m_RectsList);
            //总风压力  /0-1
            for (int i = 0; i < 2; i++)
            {
                m_Rects.Add(new Rectangle(11 + i * 734, 208, 40, 18));
            }
            //制动压力  /2-13
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                    m_Rects.Add(new Rectangle(81 + i * 110 + j * 42, 439, 40, 24));
            }
            //高加速   /14
            m_Rects.Add(new Rectangle(305, 515, 200, 30));
            //牵引/制动力   /15-18
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    m_Rects.Add(new Rectangle(260 + 350 * i, 349 + 24 * j, 65, 22));
                }
            }
        }

        public string R { get; set; }
    }
}
