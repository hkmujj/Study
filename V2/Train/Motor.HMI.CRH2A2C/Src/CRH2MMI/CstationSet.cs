using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using CRH2MMI.Common;
using CRH2MMI.Common.Util;
using CRH2MMI.Resource.Images;
using CRH2MMI.TrainLineNo;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI
{
    /// <summary>
    /// 当前站设定
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CstationSet : CRH2BaseClass
    {
        PointF m_Point = new PointF();
        readonly RectangleF[] m_Rec1 = new RectangleF[26];
        readonly String[] m_Str1 = new String[26];
        static readonly SortedList<int, string> StationList = new SortedList<int, string>();
        int m_Flag = 200;

        private string StationFile { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}\\Config\\{1}", ProjectName, "车站信息.txt")); } }

        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        public override bool Init()
        {
            InitDate();

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {

                switch (nParaB)
                {
                    case 18:
                        InitDate();
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "当前站设定";
        }

        protected override bool OnMouseDown(Point point)
        {
           return OnButtonClick(point.X, point.Y);
        }

        public bool OnButtonClick(int X, int Y)
        {

            for (int i = 0; i < 26; i++)
            {
                if (m_Rec1[i].X <= X && X <= m_Rec1[i].X + m_Rec1[i].Width && Y >= m_Rec1[i].Y && Y <= m_Rec1[i].Y + m_Rec1[i].Height)
                {
                    m_Flag = i;
                    return true;
                }

            }
            return false;

        }


        public void InitDate()
        {
            ReadStationFile();
            for (int i = 0; i < 25; i++)
            {
                m_Rec1[i].X = 25 + +(i % 5) * 155;
                m_Rec1[i].Y = 200 + ((int)(i / 5f) * 63);
                m_Rec1[i].Width = 115;
                m_Rec1[i].Height = 40;
            }
            m_Rec1[25].X = 672;
            m_Rec1[25].Y = 529;
            m_Rec1[25].Width = 115;
            m_Rec1[25].Height = 40;

            for (int i = 0; i < StationList.Count; i++)
            {
                if (StationList.ContainsKey(i + 1))
                {
                    m_Str1[i] = StationList[i + 1];
                }
            }
            for (int i = StationList.Count; i < 25; i++)
            {
                m_Str1[i] = "○○○○";
            }
        }

        public void OnDraw(Graphics e)
        {
            m_Point.X = 20;
            m_Point.Y = 114;
            e.DrawString("车次", CRH2Resource.Font12, CRH2Resource.WhiteBrush, m_Point);
            m_Point.X = 104;
            m_Point.Y = 114;
            e.DrawString(TNSET.TrainLine, CRH2Resource.Font12, CRH2Resource.YellowBrush, m_Point);
            m_Point.X = 25;
            m_Point.Y = 140;
            e.DrawString("请设定当前车次。", CRH2Resource.Font12, CRH2Resource.WWBrush, m_Point);

            for (int i = 0; i < 25; i++)
            {
                if (m_Flag == i)
                {
                    e.DrawImage(ImageResource.bluedown, m_Rec1[i]);
                    e.DrawString(m_Str1[i], CRH2Resource.Font12, CRH2Resource.BlackBrush, m_Rec1[i], CRH2Resource.DrawFormat);
                }
                else
                {
                    e.DrawImage(ImageResource.blueb, m_Rec1[i]);
                    e.DrawString(m_Str1[i], CRH2Resource.Font12, CRH2Resource.WhiteBrush, m_Rec1[i], CRH2Resource.DrawFormat);
                }

            }
            e.DrawImage(ImageResource.blueb, m_Rec1[25]);
            e.DrawString("设定", CRH2Resource.Font12, CRH2Resource.WhiteBrush, m_Rec1[25], CRH2Resource.DrawFormat);



        }
        /// <summary>
        /// 读取车站信息
        /// </summary>
        private void ReadStationFile()
        {
            string[] allLine = File.ReadAllLines(StationFile,Encoding.Default);
            foreach (var cStr in allLine)
            {
                string[] split = cStr.Split(new Char[] { ' ', ',' });
                string[] tmp = new string[2];
                int i = 0;
                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        if (i < 2)
                        {
                            tmp[i] = s;
                        }
                        i++;
                    }
                    if (i == 2)
                    {
                        if (!StationList.ContainsKey(int.Parse(tmp[0])))
                        {
                            StationList.Add(int.Parse(tmp[0]), tmp[1]);
                        }
                      break;
                    }
                }
            }
        }
    }
}