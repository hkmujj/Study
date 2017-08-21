using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Bypass : HMIBase
    {
        private Bitmap[] m_BmpArr;

        private List<CommonInnerControlBase> m_CollectionOfPage2;
        private List<CommonInnerControlBase> m_CollectionOfTitle;
        private List<CommonInnerControlBase> m_CollectionOfPage1;

        private int m_MenuNum = 1;

        private FjButtonEx m_Page2;
        private FjButtonEx m_Page1;


        private List<string> m_Lst;

        public override string GetInfo()
        {
            return "Bypass";
        }

        protected override void SetNavigateValue(NavigateType naviType, IranViewIndex currentView,
            IranViewIndex lastView)
        {
            if (naviType == NavigateType.SwitchFromOther)
            {
                m_MenuNum = 1;
            }
        }

        protected override bool Initalize()
        {
            var temp = new Point(5, 152);
            var tempLoc1 = new Point(300, 152);
            const int defeatY = 152;
            m_CollectionOfPage1 = new List<CommonInnerControlBase>();
            m_CollectionOfTitle = new List<CommonInnerControlBase>();
            m_CollectionOfPage2 = new List<CommonInnerControlBase>();
            InitPic();
            int[] tmpint =
            {
                13, 15, 17, 19, 21, 23, 24, 28, 29, 36, 40, 41, 42, 43, 44, 45, 48, 52, 53, 66, 67, 68, 69,
                70, 71
            };

            InitPage2(temp, tempLoc1, tmpint, defeatY);
            temp = new Point(5, 152);
            tempLoc1 = new Point(300, 152);
            InitPage1(tempLoc1, temp, defeatY);

            for (int i = 0; i < m_CollectionOfPage2.Count; i++)
            {
                m_CollectionOfPage2[i].Visible = !tmpint.Contains(i);
            }
            InitTitle();

            m_Page2 = new FjButtonEx(1, GlobleParam.ResManager.GetString("String245") + "▶",
                new Rectangle(701, 402, 97, 62));
            m_Page1 = new FjButtonEx(2, GlobleParam.ResManager.GetString("String244") + "◀",
                new Rectangle(701, 402, 97, 62));
            m_Page2.MouseDown += (sender, arg) =>
            {
                m_MenuNum = 2;
            };
            m_Page1.MouseDown += (sender, arg) =>
            {
                m_MenuNum = 1;
            };



            return true;
        }

        private void InitPic()
        {
            m_BmpArr = new[]
            {
                new Bitmap(RecPath + "\\frame/acy.jpg"),
                new Bitmap(RecPath + "\\frame/acx.jpg"),
                new Bitmap(RecPath + "\\frame/acz.jpg")
            };
        }

        private void InitPage2(Point temp, Point tempLoc1, int[] tmpint, int defeatY)
        {
            for (int k = 1; k <= 5; k++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (k == 1)
                    {
                        m_CollectionOfPage2.Add(new GDIRectText()
                        {
                            Text = GlobleParam.ResManagerText.GetString("Text00" + (57 + i)),
                            TextBrush = GdiCommon.MediumGreyBrush,
                            BackColorVisible = false,
                            NeedDarwOutline = false,
                            Location = temp,
                            DrawFont = GdiCommon.Txt10Font,
                            Size = new Size(200, 15)
                        });
                        temp.Y += 24;
                    }

                    m_CollectionOfPage2.Add(new GDIRectText()
                    {
                        BackColorVisible = true,
                        NeedDarwOutline = false,
                        Location = tempLoc1,
                        Size = m_BmpArr[1].Size,
                        BKImage = m_BmpArr[1],
                        Tag =
                            tmpint.Contains(m_CollectionOfPage2.Count)
                                ? ""
                                : GlobleParam.ResManagerText.GetString("Text00" + (24 + k)) + " " +
                                  GlobleParam.ResManagerText.GetString("Text00" + (57 + i)),
                        RefreshAction = o => RefreshImage((GDIRectText) o),
                    });
                    tempLoc1.Y += 24;
                }
                tempLoc1.Y = defeatY;
                tempLoc1.Offset(62, 0);
            }
        }

        private void InitPage1(Point tempLoc1, Point temp, int defeatY)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    m_CollectionOfPage1.Add(new GDIRectText()
                    {
                        BackColorVisible = true,
                        NeedDarwOutline = false,
                        Location = tempLoc1,
                        Size = m_BmpArr[1].Size,
                        BKImage = m_BmpArr[1],
                        Tag =
                            GlobleParam.ResManagerText.GetString("Text" + (j + 1).ToString("0000")) +
                            (i == 0 ? " Tc1 Status1" : " Tc2 Status1"),
                        RefreshAction = o => RefreshImage((GDIRectText) o),
                    });
                    if (i == 0)
                    {
                        m_CollectionOfPage1.Add(new GDIRectText()
                        {
                            Text = GlobleParam.ResManagerText.GetString("Text" + (j + 1).ToString("0000")),
                            TextBrush = GdiCommon.MediumGreyBrush,
                            NeedDarwOutline = false,
                            BackColorVisible = false,
                            DrawFont = GdiCommon.Txt10Font,
                            OutLineRectangle = new Rectangle(temp, new Size(200, 15))
                        });
                        temp.Y += 24;
                    }

                    tempLoc1.Y += 24;
                }
                tempLoc1.Y = defeatY;
                tempLoc1.Offset(246, 0);
            }
        }

        private void InitTitle()
        {
            var point = new Point(287, 119);
            var strSize = new Size(40, 40);
            for (int i = 0; i < 5; i++)
            {
                m_CollectionOfTitle.Add(new GDIRectText()
                {
                    Text = GlobleParam.ResManagerText.GetString("Text00" + (25 + i)),
                    DrawFont = GdiCommon.Txt10Font,
                    TextBrush = GdiCommon.MediumGreyBrush,
                    BackColorVisible = false,
                    NeedDarwOutline = false,
                    OutLineRectangle = new Rectangle(point, strSize),
                    TextFormat = GdiCommon.CenterFormat
                });
                point.Offset(62, 0);
            }
        }

        private void RefreshImage(GDIRectText item)
        {
            var name = item.Tag as string;
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            item.BKImage = BoolList[GlobleParam.Instance.FindInBoolIndex(name)]
                ? m_BmpArr[0]
                : m_BmpArr[1];
        }

        public override bool mouseDown(Point point)
        {
            if (m_MenuNum == 1 && m_Page2.IsVisible(point))
            {
                m_Page2.OnMouseDown(point);
            }
            else if (m_MenuNum == 2 && m_Page1.IsVisible(point))
            {
                m_Page1.OnMouseDown(point);
            }
            return true;
        }

        public override void paint(Graphics g)
        {
            m_CollectionOfTitle.ForEach(e => e.OnDraw(g));
            if (m_MenuNum == 2)
            {
                m_Page1.OnDraw(g);
                m_CollectionOfPage2.ForEach(e => e.OnPaint(g));
            }
            else if (m_MenuNum == 1)
            {
                m_Page2.OnDraw(g);
                m_CollectionOfPage1.ForEach(e => e.OnPaint(g));
            }
        }
    }
}