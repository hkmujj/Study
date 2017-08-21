using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;
using MMI.Facility.Interface.Data;

namespace Urban.QingDao3Line.MMI.紧急消息界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class EmergencyMessagesScreen : QingDaoBaseClass
    {
        private int m_SelectedIdx = 0;
        private int m_GetDown = 0;
        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[3];

        private List<string> m_Str;
        private bool sendStyle = false;
        //2
        public override string GetInfo()
        {
            return "紧急消息界面";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override bool mouseDown(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                    if (!sendStyle&&index!=0)
                        append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 0);
                    break;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (m_RectsList[1 + i].Contains(point))
                {
                    m_SelectedIdx = i;
                    if (!sendStyle)
                        append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 0);
                    break;
                }
            }
            return true;
        }

        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 3; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = false;
                    break;
                }
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            GetValue();
            Draw(g);
            if (m_BtnIsDown[1]) //上翻
            {
                if (m_SelectedIdx <= 0)
                {
                    if (m_GetDown > 0)
                    {
                        m_GetDown -= 1;
                    }
                    else
                    {
                        m_GetDown = m_Str.Count - 9;
                        m_SelectedIdx = 8;
                    }
                }
                else
                {
                    m_SelectedIdx -= 1;
                }
            }
            if (m_BtnIsDown[2]) //下翻
            {
                if (m_SelectedIdx == 8)
                {
                    if (m_GetDown + m_SelectedIdx + 1 < m_Str.Count)
                    {
                        m_GetDown += 1;
                    }
                    else
                    {
                        m_SelectedIdx = 0;
                        m_GetDown = 0;
                    }
                }
                else
                {
                    m_SelectedIdx += 1;
                }
            }
            if (m_BtnIsDown[0])
            {
                append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, m_GetDown + m_SelectedIdx + 1);
            }
            else
            {
                if (sendStyle)
                {
                    append_postCmd(CmdType.SetFloatValue, UIObj.OutFloatList[0], 0, 0);
                }
            }
        }

        private void Draw(Graphics g)
        {
            if (m_SelectedIdx != -1 && m_SelectedIdx < 9)
            {
                g.FillRectangle(new SolidBrush(Color.White), m_RectsList[m_SelectedIdx + 1]);
            }
            PaintState(g);
        }

        private void GetValue()
        {
            for (int i = 0; i < m_BValue.Length; i++)
            {
                m_BValue[i] = BoolList[this.UIObj.InBoolList[i]];
            }
            for (int i = 0; i < m_FValue.Length; i++)
            {
                m_FValue[i] = FloatList[this.UIObj.InFloatList[i]];
            }
        }

        private void PaintState(Graphics e)
        {
            e.DrawString(ResourceFacade.PISResourceMessage, FontItems.Fonts_Regular(FontName.宋体, 16, false), SolidBrushsItems.BlackBrush,
                m_RectsList[0], FontItems.TheAlignment(FontRelated.居中));
            for (int i = 0; i < 9; i++)
            {
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[1 + i].X, m_RectsList[1 + i].Y,
                    m_RectsList[1 + i].Width,
                    m_RectsList[1 + i].Height);
                e.DrawString(m_Str[m_GetDown + i], Common.m_Font10B, SolidBrushsItems.BlackBrush,
                    m_RectsList[1 + i], FontItems.TheAlignment(FontRelated.靠左));
            }
            //Send按钮
            if (m_BtnIsDown[0])
                e.DrawImage(m_ImgsList[3], m_RectsList[10]);
            else
                e.DrawImage(m_ImgsList[2], m_RectsList[10]);
            e.DrawString(ResourceFacade.PISResourceSEND, Common.m_Font9B, SolidBrushsItems.BlackBrush,
                m_RectsList[13], FontItems.TheAlignment(FontRelated.居中));
            //上翻 下翻
            for (int i = 0; i < 2; i++)
            {
                if (m_BtnIsDown[1 + i])
                {
                    e.DrawImage(m_ImgsList[3], m_RectsList[11 + i]);
                }
                else
                    e.DrawImage(m_ImgsList[2], m_RectsList[11 + i]);
                e.DrawImage(m_ImgsList[i], m_RectsList[14 + i]);
            }
        }

        private void InitData()
        {
            m_BValue = new bool[this.UIObj.InBoolList.Count];
            m_FValue = new float[this.UIObj.InFloatList.Count];
            var fileValue = File.ReadAllLines(Path.Combine(AppConfig.AppPaths.ConfigDirectory, "紧急广播.txt"), Encoding.Default);
            m_Str = new List<string>(fileValue);
            if (m_Str[0] == "#0#")
            {
                sendStyle = true;
            }
            else
            {
                sendStyle = false;
            }
            m_Str.RemoveAt(0);
            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();

            if (Coordinate.RectangleFLists(ViewIDName.EmergencyMessagesScreen, ref m_RectsList))
            {
                for (int i = 0; i < 3; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[13 + i]));
                }
            }
        }
    }
}
