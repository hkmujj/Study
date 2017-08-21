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
    public class Draught : baseClass
    {
        private List<IGridRect> m_DraughtRectList = new List<IGridRect>();

        public override string GetInfo()
        {
            return "驾驶";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            int i = 0;
            m_DraughtRectList.Add(new DraughtRect1(new Point(2, 150), "PCE", null));
            m_DraughtRectList.Add(new DraughtRect2(new Point(2, 172), "牵引操作", null));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 194), "高压值（V）", new List<int>() { 60, 61, 62, 63 }, 4));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 216), "参考速度（Kph）", new List<int>() { 64, 65, 66, 67 }, 4));
            m_DraughtRectList.Add(new DraughtRect3(new Point(2, 238), "需求力", new List<int>() { 68 }, 4));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 260), "输出力值（制动时为负数）（KN）", new List<int>() { 121, 122, 123, 124 }, 4));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 282), "空转打滑侦测", null));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 304), "线路接触器状态", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }, 1));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 326), "高速开关状态", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }, 1));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 348), "接地隔离开关状态", new List<int> { 1353 + i++, 1353 + i++, -1, -1, -1, -1, 1353 + i++, 1353 + i++ }, 2));
            m_DraughtRectList.Add(new DraughtRect2(new Point(2, 370), "牵引维护", null));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 392), "操作", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));

            m_DraughtRectList.Add(new DraughtRect(new Point(2, 414), "牵引设备隔离", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 436), "牵引设备锁定", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 458), "推进状态", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 480), "电制动状态", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 502), "受电弓牵引高压分布", new List<int> { 1353 + i++, 1353 + i++, -1, -1, -1, -1, 1353 + i++, 1353 + i++ }));
            m_DraughtRectList.Add(new DraughtRect(new Point(2, 524), "牵引风机中压分布", new List<int> { 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++, 1353 + i++ }));
            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var v in m_DraughtRectList)
            {
                v.OnDraw(g);
            }
        }
    }

    public class DraughtRect : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private List<int> m_BoolIndexList;
        private int m_Index;

        public List<RectText> m_RectText = new List<RectText>();

        public DraughtRect(Point point, string label, List<int> boolList, int index = 0)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolList;
            m_Index = index;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 420, 22), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 420, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 485, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 550, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 615, m_InitialPoint.Y, 65, 22), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, false, true, false));

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
                        else if (m_Index == 1)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_SwitchImageArray[i % 2];
                            }
                        }
                        else if (m_Index == 2)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_PanSwitchImageArray[i % 2];
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

    public class DraughtRect1 : DraughtRect
    {
        public DraughtRect1(Point point, string label, List<int> boolList, int index = 0)
            : base(point, label, boolList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;

            m_RectText[1].Text = "B1";
            m_RectText[1].FontTextStyle = FontStyle.Bold;
            m_RectText[1].IsImmutable = true;

            m_RectText[2].Text = "C1";
            m_RectText[2].FontTextStyle = FontStyle.Bold;
            m_RectText[2].IsImmutable = true;

            m_RectText[3].Text = "C2";
            m_RectText[3].FontTextStyle = FontStyle.Bold;
            m_RectText[3].IsImmutable = true;

            m_RectText[4].Text = "B2";
            m_RectText[4].FontTextStyle = FontStyle.Bold;
            m_RectText[4].IsImmutable = true;

        }
    }

    public class DraughtRect2 : DraughtRect
    {
        public DraughtRect2(Point point, string label, List<int> boolList, int index = 0)
            : base(point, label, boolList, index)
        {
            m_RectText[0].AlignmentTextFormat = 1;
            m_RectText[0].FontTextStyle = FontStyle.Bold;
        }
    }

    public class DraughtRect3 : IGridRect
    {
        private Point m_InitialPoint;
        private string m_Label;
        private List<int> m_BoolIndexList;
        private int m_Index;
        public List<RectText> m_RectText = new List<RectText>();

        public DraughtRect3(Point point, string label, List<int> boolList, int index = 0)
        {
            m_InitialPoint = point;
            m_Label = label;
            m_BoolIndexList = boolList;
            m_Index = index;

            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X, m_InitialPoint.Y, 420, 25), m_Label, 12, 0, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));
            m_RectText.Add(new RectText(new Rectangle(m_InitialPoint.X + 420, m_InitialPoint.Y, 260, 25), "", 12, 1, Color.Black, Common.m_BackGroundColor, Color.Black, 1, true, null, true, true, false));

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
                        else if (m_Index == 1)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_SwitchImageArray[i % 2];
                            }
                        }
                        else if (m_Index == 2)
                        {
                            if (BasicClass.m_Boolsortedlist[m_BoolIndexList[i]])
                            {
                                m_RectText[i / 2 + 1].ImagePicture = BasicClass.m_PanSwitchImageArray[i % 2];
                            }
                        }
                        else
                        {
                            m_RectText[i / 2 + 1].Text = BasicClass.m_FloatSortedList[m_BoolIndexList[i]].ToString();
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
