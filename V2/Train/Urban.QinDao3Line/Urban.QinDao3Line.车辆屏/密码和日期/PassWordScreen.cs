using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.QingDao3Line.MMI.Resource;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI.密码
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class PassWordScreen:QingDaoBaseClass 
    {
        private int m_Index = 0;
        private List<Image> m_Images=new List<Image>();
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[38];
        private FlashTimer m_Flasher = new FlashTimer(1);
        private readonly string[] m_Btn =
        {
            "1","2","3","4","5","6","7","8","9","0",
            "Q","W","E","R","T","Y","U","I","O","P",
            "A","S","D","F","G","H","J","K","删除",
            "L","Z","X","C","V","B","N","M","确认"  
        };
        public static int Index { set; get; }
        public static int m_View = -1;
        private readonly string[] m_Password = new string[6];
        public static List<CursorMovecs> m_Btnlist = new List<CursorMovecs>();

        public static Dictionary<int, string> m_PassWordDictionary = new Dictionary<int, string>();
        public static Dictionary<int, int> m_ContentDictionary = new Dictionary<int, int>();

        //2
        public override string GetInfo()
        {
            return "密码界面";
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
            for (; index < 38; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                    if (index != 28 && index != 37)
                    {
                        string strTemp = m_Btn[index];
                        m_PassWordDictionary[m_Index] = strTemp;
                    }
                    break;
                }
            }
            return true;
        }
        public override bool mouseUp(Point point)
        {
            int index = 0;
            for (; index < 38; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = false;

                    if (index != 37 && index != 28)
                    {
                        if (m_PassWordDictionary.Count < 6)
                        {
                            m_Index++;
                        }
                    }
                    if (index == 28 && m_PassWordDictionary.Count > 0)
                    {
                        if (m_PassWordDictionary.Count < 6)
                        {
                            m_Index -= 1;
                        }
                        m_PassWordDictionary.Remove(m_Index);
                    }
                    if (index == 37 && m_PassWordDictionary.Count == 6)
                    {
                        append_postCmd(CmdType.ChangePage, 8, 0, 0);
                    }
                    break;
                }
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            Draw(g);

            if (m_PassWordDictionary.Count > 0)
            {
                foreach (var i in m_PassWordDictionary.Keys)
                {
                    g.DrawString(m_PassWordDictionary[i].ToString(),
                        FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[2 + i],
                        FontItems.TheAlignment(FontRelated.居中));
                    m_Password[i] = Convert.ToString(m_PassWordDictionary[i]);
                }
            }
        }

        private void Draw(Graphics e)
        {
            PaintState(e);
        }

        private void PaintState(Graphics g)
        {
            //"输入密码"
            g.DrawString(ResourceFacade.SettingResourceEnterPassword, FontItems.Fonts_Regular(FontName.宋体, 12, false), SolidBrushsItems.BlackBrush,
                m_RectsList[0], FontItems.TheAlignment(FontRelated.居中));
            //密码框
            g.DrawRectangle(LinePen, m_RectsList[1].X, m_RectsList[1].Y, m_RectsList[1].Width, m_RectsList[1].Height);
            //键盘
            for (int i = 0; i < 38; i++)
            {
                g.FillRectangle(m_BtnIsDown[i] ? new SolidBrush(Color.Yellow) : new SolidBrush(Color.White),
                    m_RectsList[46 + i]);
                g.DrawRectangle(new Pen(Color.Black), m_RectsList[8 + i].X, m_RectsList[8 + i].Y, m_RectsList[8 + i].Width,
                    m_RectsList[8 + i].Height);
                g.DrawString(m_Btn[i], FontItems.Fonts_Regular(FontName.宋体, 12, false), SolidBrushsItems.BlackBrush,
                    m_RectsList[8 + i], FontItems.TheAlignment(FontRelated.居中));
            }
            //黄色的密码填充矩形
            if (m_Flasher.IsNeedFlash())
            {
                g.FillRectangle(new SolidBrush(Color.Yellow), m_RectsList[2 + m_Index]);
            }
        }

        private static Pen LinePen
        {
            get { return new Pen(Color.Green,2); }
        }

        private void InitData()
        {
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Images.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.PassWordScreen, ref m_RectsList))
            {
                for (int i = 0; i < 38; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[8 + i]));
                }
            }
        }
    }
}
