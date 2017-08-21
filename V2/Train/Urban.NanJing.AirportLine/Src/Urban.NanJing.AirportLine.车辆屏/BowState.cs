using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class BowState : baseClass
    {
        private List<IGridRect> m_BowRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "受电弓状态";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;

            m_BowRectList.Add(new DefaultRect1(new Point(2, 150 + 22 * i++), "高压供电", null));
            m_BowRectList.Add(new DefaultRect2(new Point(2, 150 + 22 * i++), "高压供电维护", null));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "降弓位故障", new List<int>() { -1, -1, 1173, 1174, -1, -1, -1, -1, 1175, 1176, -1, -1 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "升弓位故障", new List<int>() { -1, -1, 1177, 1178, -1, -1, -1, -1, 1179, 1180, -1, -1 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "未知未知故障", new List<int>() { -1, -1, 1181, 1182, -1, -1, -1, -1, 1183, 1184, -1, -1 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "受电弓高压缺失故障", new List<int>() { -1, -1, 1185, 1186, -1, -1, -1, -1, 1187, 1188, -1, -1 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "受电弓高压存在故障", new List<int>() { -1, -1, 1189, 1190, -1, -1, -1, -1, 1191, 1192, -1, -1 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "受电弓控制断路器故障", new List<int>() { 1193, 1194, -1, -1, -1, -1, -1, -1, -1, -1, 1195, 1196 }));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "车间电源插座继电器冲突", new List<int>() { 1197, 1198, -1, -1, -1, -1, -1, -1,  -1, -1 ,1199, 1200,}));
            m_BowRectList.Add(new DefaultRect(new Point(2, 150 + 22 * i++), "主熔断器分配故障", new List<int>() { -1, -1, 1201, 1202, -1, -1, -1, -1, 1203, 1204, -1, -1 }));

            return true;
        }

        public override void paint(Graphics g)
        {


            foreach (var v in m_BowRectList)
            {
                v.OnDraw(g);
            }
        }
    }

    public interface IGridRect
    {
        void OnDraw(Graphics g);
    }

    public class DefaultRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private int m_Index;
        public List<RectText> m_RectText = new List<RectText>();

        private List<int> m_BoolIndexList;

        public DefaultRect(Point point, string label, List<int> boolIndexList, int index = 0)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_Index = index;
            m_BoolIndexList = boolIndexList;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 380, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true,true,false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 380, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 430, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false,true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 480, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 530, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 580, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 630, m_InitialPoint.Y, 50, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
        }

        public void OnDraw(Graphics g)
        {
            if (m_BoolIndexList != null)
            {
                for (int i = 0; i < m_BoolIndexList.Count; i++)
                {
                    if (m_BoolIndexList[i] > 0)
                    {
                        if (m_Index == 0)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_StateColorImageArray[i % 2];
                            }
                        }
                        else
                        {
                            m_RectText[i + 1].Text = BasicClass.m_FloatSortedList[m_BoolIndexList[i]].ToString();
                        }
                    }
                }
            }

            foreach (var v in m_RectText)
            {
                v.OnDraw(g);
            }
        }
    }



    public class DefaultRect1 : DefaultRect
    {
        public DefaultRect1(Point point, string label, List<int> boolIndexList, int index = 0)
            : base(point, label, boolIndexList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;

            m_RectText[1].Text = "A1";
            m_RectText[1].FontTextStyle = FontStyle.Bold;
            m_RectText[1].IsImmutable = true;

            m_RectText[2].Text = "B1";
            m_RectText[2].FontTextStyle = FontStyle.Bold;
            m_RectText[2].IsImmutable = true;

            m_RectText[3].Text = "C1";
            m_RectText[3].FontTextStyle = FontStyle.Bold;
            m_RectText[3].IsImmutable = true;

            m_RectText[4].Text = "C2";
            m_RectText[4].FontTextStyle = FontStyle.Bold;
            m_RectText[4].IsImmutable = true;

            m_RectText[5].Text = "B2";
            m_RectText[5].FontTextStyle = FontStyle.Bold;
            m_RectText[5].IsImmutable = true;

            m_RectText[6].Text = "A2";
            m_RectText[6].FontTextStyle = FontStyle.Bold;
            m_RectText[6].IsImmutable = true;
        }
    }

    public class DefaultRect2 : DefaultRect
    {
        public DefaultRect2(Point point, string label, List<int> boolIndexList, int index = 0)
            : base(point, label, boolIndexList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;
        }
    }

    public class DefaultRect3 : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private int m_Index;
        private List<int> m_BoolIndexList;
        public List<RectText> m_RectText = new List<RectText>();


        public DefaultRect3(Point point, string label, List<int> boolIndexList, int index = 0)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolIndexList;
            m_Index = index;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 380, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true,true,false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 380, m_InitialPoint.Y, 300, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));

        }

        public void OnDraw(Graphics g)
        {
            if (m_BoolIndexList != null)
            {
                for (int i = 0; i < m_BoolIndexList.Count; i++)
                {
                    if (m_BoolIndexList[i] > 0)
                    {
                        if (m_Index == 0)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_StateColorImageArray[i % 2];
                            }
                        }
                        else
                        {
                            m_RectText[i + 1].Text = BasicClass.m_FloatSortedList[m_BoolIndexList[i]].ToString();
                        }
                    }
                }
            }

            foreach (var v in m_RectText)
            {
                v.OnDraw(g);
            }
        }
    }
}
