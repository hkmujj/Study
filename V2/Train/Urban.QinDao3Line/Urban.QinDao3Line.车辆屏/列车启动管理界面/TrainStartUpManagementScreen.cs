using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class TrainStartUpManagementScreen : NewQBaseclass
    {
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[2];
        private readonly string[] m_Str = { "Tc1", "M1", "M2", "M3", "M4", "Tc2" };
        private string[] m_Txt = { "110V", "144V", "7.3Bar", "110V", "144V", "7.3Bar" };

        ///// <summary>
        ///// start按键的按键次数
        ///// </summary>
        //private static int m_SatrtNum = 0;
        ///// <summary>
        ///// stop按键的次数
        ///// </summary>
        //private static int m_StopNum = 0;

        //2
        public override string GetInfo()
        {
            return "列车启动管理界面";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            IntiData();
            return true;
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;

            for (; index < 2; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {

                    switch (index)
                    {

                        case 0:
                            m_BtnIsDown[0] = true;
                            m_BtnIsDown[1] = false;
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0);
                            break;
                        case 1:
                            m_BtnIsDown[1] = true;
                            m_BtnIsDown[0] = false;
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 1, 0);
                            break;
                    }
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 2; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    switch (index)
                    {

                        case 0:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0);
                            break;
                        case 1:
                            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1], 0, 0);
                            break;
                    }
                }
            }
            return true;
        }
        public override void paint(Graphics g)
        {
            Draw(g);
            DrawButton(g);
        }
        protected void Draw(Graphics e)
        {
            PaintState(e);
        }

        protected void PaintState(Graphics e)
        {
            for (int i = 0; i < 28; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[i].X, m_RectsList[i].Y, m_RectsList[i].Width, m_RectsList[i].Height);
            }

            for (int i = 0; i < 6; i++)
            {
                e.FillRectangle(new SolidBrush(Color.Orange), m_RectsList[i]);
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.宋体, 20, false), SolidBrushsItems.BlackBrush, m_RectsList[i], FontItems.TheAlignment(FontRelated.居中));
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i]].ToString("0") + " V", FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[28 + i], FontItems.TheAlignment(FontRelated.靠右));
            }
            for (int i = 0; i < 2; i++)
            {
                e.DrawString(FloatList[m_FoolatIds[i + 2]].ToString("0") + " V", FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[31 + i], FontItems.TheAlignment(FontRelated.靠右));
            }
            e.DrawString(FloatList[m_FoolatIds[4]].ToString("0.0") + " Br", FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[30], FontItems.TheAlignment(FontRelated.靠右));

            e.DrawString(FloatList[m_FoolatIds[5]].ToString("0.0") + " Br", FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[33], FontItems.TheAlignment(FontRelated.靠右));
            //辅助变流器

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[m_BoolIds[j + i * 4]])
                    {
                        e.DrawImage(m_ImgsList[j], m_RectsList[34 + i * 2]);
                    }
                    if (!BoolList[m_BoolIds[i * 4]] && !BoolList[m_BoolIds[1 + i * 4]] &&
                        !BoolList[m_BoolIds[2 + i * 4]] && !BoolList[m_BoolIds[3 + i * 4]])
                    {
                        e.DrawImage(m_ImgsList[3], m_RectsList[34 + i * 2]);
                    }
                }
            }
            //空气压缩机
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[m_BoolIds[j + i * 3 + 8]])
                    {
                        e.DrawImage(m_ImgsList[4 + j], m_RectsList[35 + i * 2]);
                    }
                    if (!BoolList[m_BoolIds[i * 3 + 8]] && !BoolList[m_BoolIds[1 + i * 3 + 8]] &&
                        !BoolList[m_BoolIds[2 + i * 3 + 8]])
                    {
                        e.DrawImage(m_ImgsList[6], m_RectsList[35 + i * 2]);
                    }
                }
            }

            //HVAC状态
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[m_BoolIds[j + 14 + i * 3]])
                    {
                        e.DrawImage(m_ImgsList[7 + j], m_RectsList[38 + i]);
                    }
                    if (!BoolList[m_BoolIds[14 + i * 3]] && !BoolList[m_BoolIds[1 + 14 + i * 3]] &&
                        !BoolList[m_BoolIds[2 + 14 + i * 3]])
                    {
                        e.DrawImage(m_ImgsList[9], m_RectsList[38 + i]);
                    }
                }
            }
        }

        public virtual void DrawButton(Graphics e)
        {
            //启动与退出按钮
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[i])
                {
                    e.DrawImage(m_ImgsList[13], m_RectsList[44 + i]);
                }
                else
                {
                    e.DrawImage(m_ImgsList[12], m_RectsList[44 + i]);
                }
                e.DrawImage(m_ImgsList[10 + i], m_RectsList[46 + i]);
            }
        }

        protected void InitData()
        {
            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.TrainStartUpManagementScreen, ref m_RectsList))
            {
                for (int i = 0; i < 2; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[46 + i]));
                }
            }
        }
    }
}
