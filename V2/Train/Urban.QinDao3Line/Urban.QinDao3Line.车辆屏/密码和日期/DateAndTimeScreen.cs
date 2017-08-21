using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface.Attribute;
using Urban.QingDao3Line.MMI.底层共用;

namespace Urban.QingDao3Line.MMI.密码
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DateAndTimeScreen:QingDaoBaseClass 
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "日期和时间界面";
        }

        //6
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }
        #endregion
        #region ::::::::::::::::::::::::: event override :::::::::::::::::::::::::
        public override bool mouseDown(Point point)
        {
            int index = 26;
            for (; index < 37; index++)
            {
                if (m_BtnArea[index].IsVisible(point))
                {
                    m_BtnIsDown[index] = true;
                    if (index < 36)
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
            for (; index < 37; index++)
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
            if (m_PassWordDictionary.Count > 0)
            {
                foreach (var i in m_PassWordDictionary.Keys)
                {
                    g.DrawString(m_PassWordDictionary[i].ToString(),
                        FontItems.Fonts_Regular(FontName.宋体, 10, false), SolidBrushsItems.BlackBrush, m_RectsList[2 + i],
                        FontItems.TheAlignment(FontRelated.居中));
                }
            }
            for (int i = 26; i < 36; i++)
            {
                if (m_BtnIsDown[i])
                {
                    m_Btnlist[i].Cursors();
                }
            }
        }
        private void Draw(Graphics e)
        {
            PaintState(e);
        }
        #region ::::::::::::::::::::::::::::: ex funes :::::::::::::::::::::::::::
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
            e.DrawString("Date and time", FontItems.Fonts_Regular(FontName.宋体, 12, false), SolidBrushsItems.BlackBrush,
                m_RectsList[0], FontItems.TheAlignment(FontRelated.居中));
            e.DrawRectangle(new Pen(Color.Green, 2), m_RectsList[1].X, m_RectsList[1].Y, m_RectsList[1].Width, m_RectsList[1].Height);
            for (int i = 0; i < 3; i++)
            {
                e.DrawString(m_Str[i], FontItems.Fonts_Regular(FontName.宋体, 12, false), SolidBrushsItems.BlackBrush,
                m_RectsList[14+i], FontItems.TheAlignment(FontRelated.居中));
            }
            for (int i = 0; i < 36; i++)
            {
                if (m_BtnIsDown[i])
                    e.FillRectangle(new SolidBrush(Color.Yellow), m_RectsList[55 + i]);
                else
                    e.FillRectangle(new SolidBrush(Color.DarkGray), m_RectsList[55 + i]);
                e.DrawRectangle(new Pen(Color.Black), m_RectsList[19 + i].X, m_RectsList[19+ i].Y, m_RectsList[19 + i].Width,
                    m_RectsList[19 + i].Height);
                e.DrawString(m_Btn[i], FontItems.Fonts_Regular(FontName.宋体, 12, false), SolidBrushsItems.BlackBrush,
                    m_RectsList[19 + i], FontItems.TheAlignment(FontRelated.居中));
            }
            //确认
            e.DrawRectangle(new Pen(Color.Black), m_RectsList[18].X, m_RectsList[18].Y, m_RectsList[18].Width, m_RectsList[18].Height);
            e.DrawString("√", FontItems.Fonts_Regular(FontName.宋体, 28, false), new SolidBrush(Color.Blue),
                m_RectsList[18], FontItems.TheAlignment(FontRelated.居中));

            e.FillRectangle(new SolidBrush(Color.Yellow), m_RectsList[2 + index]);
            if (m_BtnIsDown[36] && index < 11)
            {
                index++;
                m_Index++;
                //if (index == 12)
                //    append_postCmd(CmdType.ChangePage, 8, 0, 0);
            }
        }

        #endregion

        #endregion
        #region ::::::::::::::::::::::::::: init funes :::::::::::::::::::::::::::
        private void InitData()
        {
            m_BValue = new bool[this.UIObj.InBoolList.Count];
            m_FValue = new float[this.UIObj.InFloatList.Count];

            m_ImgsList = new List<Image>();
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_ImgsList.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            if (!Coordinate.ClassIsInited) Coordinate.InitData();
            if (Coordinate.RectangleFLists(ViewIDName.DateAndTimeScreen , ref m_RectsList))
            {
                for (int i = 0; i < 36; i++)
                {
                    m_BtnArea.Add(new Region(m_RectsList[19 + i]));
                    m_Btnlist.Add(new CursorMovecs(i, m_Btn[i]));
                }
                m_BtnArea.Add(new Region(m_RectsList[17]));
            }

        }
        #endregion
        #region:::::::::::::::init values:::::::::::::

        private int index = 0;

        private bool[] m_BValue;
        private float[] m_FValue;
        private List<Image> m_ImgsList;
        private List<RectangleF> m_RectsList;
        private readonly List<Region> m_BtnArea = new List<Region>();
        private readonly bool[] m_BtnIsDown = new bool[37];
        private readonly string[] m_Btn =
        {
            "A","B","C","D","E","F","G","H","I",
            "J","K","L","M","N","O","P","Q","R",
            "S","T","U","V","W","X","Y","Z","0",
            "1","2","3","4","5","6","7","8","9"
        };
        private readonly string[] m_Str = {"-","-",":"};
        public static int m_Index = 0;
        public static int m_View = -1;
        //private string[] Password = new string[6];
        public static List<CursorMovecs> m_Btnlist = new List<CursorMovecs>();

        public static Dictionary<int, string> m_PassWordDictionary = new Dictionary<int, string>();
        public static Dictionary<int, int> m_ContentDictionary = new Dictionary<int, int>();
        #endregion
    }
}
