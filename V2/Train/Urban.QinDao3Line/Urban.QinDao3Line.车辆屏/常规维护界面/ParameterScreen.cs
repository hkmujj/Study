using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI.参数界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ParameterScreen : QingDaoBaseClass
    {
        //2
        private float m_Numb = 25;
        private int[] Btn_Numb = new int[]{0,0};

        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[6];
        private readonly string[] m_Str = { ResourceFacade.SettingResourceHVACModeSelection, ResourceFacade.SettingResourceAirTemperature };
        private readonly string[] m_Txt = { ResourceFacade.SettingResourceOUTSIDFIREMODE, ResourceFacade.SettingResourceAIRPURIFICATION };

        private void InitData()
        {
            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.ParameterScreen, ref m_RectsList))
            {
                for (int i = 0; i < 6; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[i]));
                }
            }

        }

        public override string GetInfo()
        {
            return "参数界面";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_Numb);
            return true;
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 6; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 6; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = false;
                    switch (index)
                    {
                        case 0:
                            append_postCmd(CmdType.ChangePage, 6, 0, 0);
                            break;
                        case 1:
                            if (m_Numb < 28) 
                            {
                                m_Numb = m_Numb + 1;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0],0,m_Numb);
                            }
                            break;
                        case 2:
                            if (m_Numb >= 12) 
                            { 
                                m_Numb = m_Numb - 1;
                                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_Numb);
                            }
                            break;
                        case 3:
                            if (Btn_Numb[0] == 0)
                            {
                                m_BtnIsDown[index] = true;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                                Btn_Numb[0] = 1;
                            }
                            else
                            {
                                Btn_Numb[0] = 0;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                            }
                            break;
                        case 4:
                            if (Btn_Numb[1] == 0)
                            {
                                m_BtnIsDown[index] = true;
                                Btn_Numb[1] = 1;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                            }
                            else
                            {
                                Btn_Numb[1] = 0;
                                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                            }
                            break;
                        case 5:
                            append_postCmd(CmdType.ChangePage, 7, 0, 0);
                            break;
                    }
                }
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            PaintState(g);
            DrawWord(g);
        }

        private void DrawWord(Graphics e)
        {
            e.FillRectangle(new SolidBrush(Color.DimGray), m_RectsList[0]);
            e.DrawRectangle(new Pen(Color.Black), m_RectsList[0].X, m_RectsList[0].Y, m_RectsList[0].Width,
                m_RectsList[0].Height);
            e.DrawString(ResourceFacade.SettingResourceHVACModeSelection, Common.m_Font12B,
                SolidBrushsItems.BlackBrush,
                m_RectsList[0], FontItems.TheAlignment(FontRelated.居中));

            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Txt[i], Common.m_Font12B, SolidBrushsItems.BlackBrush,
                    m_RectsList[3 + i], FontItems.TheAlignment(FontRelated.居中));
            }

            for (int i = 0; i < 2; i++)
            {
                e.DrawString(m_Str[i], Common.m_Font12B, SolidBrushsItems.BlackBrush,
                    m_RectsList[13 + i], FontItems.TheAlignment(FontRelated.靠右));
            }
            e.DrawString(ResourceFacade.SettingResourceProtectedMenu, FontItems.Fonts_Regular(FontName.宋体, 10, false),
                SolidBrushsItems.BlackBrush,
                m_RectsList[15], FontItems.TheAlignment(FontRelated.居中));

            //温度
            e.DrawString(m_Numb.ToString("0.0") + "℃", FontItems.Fonts_Regular(FontName.宋体, 14, false),
                SolidBrushsItems.BlackBrush,
                m_RectsList[12], FontItems.TheAlignment(FontRelated.居中));
        }
        private void PaintState(Graphics e)
        {
            //外部火灾模式及空气净化
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[3 + i])
                {
                    e.DrawImage(m_ImgsList[6], m_RectsList[3 + i]);
                }
                else
                    e.DrawImage(m_ImgsList[5], m_RectsList[3 + i]);
            }

            //上下按钮图
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[1 + i])
                {
                    e.DrawImage(m_ImgsList[4], m_RectsList[1 + i]);
                }
                else
                {
                    e.DrawImage(m_ImgsList[3], m_RectsList[1 + i]);
                }
                e.DrawImage(m_ImgsList[i], m_RectsList[7 + i]);
            }
            //锁图
            if (m_BtnIsDown[5])
            {
                e.DrawImage(m_ImgsList[4], m_RectsList[5]);
            }
            else
            {
                e.DrawImage(m_ImgsList[3], m_RectsList[5]);
            }
            e.DrawImage(m_ImgsList[2], m_RectsList[11]);
        }
    }
}
