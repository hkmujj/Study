using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Model;
using CommonUtil.Util;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
using Urban.Iran.HMI.Controls;

namespace Urban.Iran.HMI.HVAC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HVAC2 : HMIBase
    {
        private FjButtonEx[] m_BtnArr;
        private readonly Color m_BkColor = Color.FromArgb(90, 87, 190);
        private List<CommonInnerControlBase> m_Collection;
        private Image[] m_Img;
        private void Hvac2MouseDown(FjButtonEx btnSender, Point pt)
        {
            switch (btnSender.BtnIndex)
            {
                case 1:
                    ChangedPage(IranViewIndex.HVACLegend);;
                    break;
                case 2:
                    ChangedPage(IranViewIndex.HVACPage1);;
                    break;
            }
        }

        public override string GetInfo()
        {
            return "HVAC2";
        }

        protected override bool Initalize()
        {
            m_Img = new Image[2];
            m_Img[0] = Image.FromFile(Path.Combine(RecPath, "frame\\AC_Page2_Off.png"));
            m_Img[1] = Image.FromFile(Path.Combine(RecPath, "frame\\AC_Page2_UnOff.png"));
            m_BtnArr = new[]
            {
                new FjButtonEx(1, GlobleParam.ResManager.GetString("String32"), GlobleRect.m_LegendbtnRect),
                new FjButtonEx(2, GlobleParam.ResManager.GetString("String244") + " ◀", GlobleRect.m_HlepRect)
            };
            m_BtnArr[0].MouseDown += Hvac2MouseDown;
            m_BtnArr[1].MouseDown += Hvac2MouseDown;

            const int interval = 7;
            var itemSize = new Size(18, 18);
            var itemSize1 = new Size(18, 36);
            var defelpoint = new Point(140, 130);
            var location = new Point(140, 130);
            var location2 = new Point(10, 155);
            const int strLength = 120;
            m_Collection = new List<CommonInnerControlBase>();
            int[] temps =
            {
                24, 25, 26, 27, 28, 37, 38, 44, 45, 46, 47, 48, 57, 58, 64, 65, 66, 67, 68, 77, 78, 84, 85,
                86, 87, 88, 97, 98, 104, 105, 106, 107, 108, 117, 118, 124, 125, 126, 127, 128, 134, 135, 136, 137, 138,
                147, 148, 154, 155, 156, 157, 158, 167, 168, 174, 175, 176, 177, 178, 187, 188, 194, 195, 196, 197, 198
            };
            for (int k = 1; k <= 5; k++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    CarOfTrain(k, j, location, itemSize, interval, location2, strLength, itemSize1, temps);
                    location.Y = defelpoint.Y;
                    location.Offset(itemSize.Width + interval, 0);
                }
                location.Y = defelpoint.Y;
                location.Offset(interval * 3, 0);
            }
            var point = new Point(180, 360);
            var size = new Size(40, 40);
            for (int i = 0; i < 5; i++)
            {
                m_Collection.Add(new GDIRectText
                {
                    Text = GlobleParam.ResManagerText.GetString("Text00" + (25 + i)),
                    TextBrush = GdiCommon.MediumGreyBrush,
                    BackColorVisible = false,
                    NeedDarwOutline = false,
                    DrawFont = GdiCommon.Txt10Font,
                    OutLineRectangle = new Rectangle(point, size),
                    TextFormat = GdiCommon.CenterFormat
                });
                point.Offset(115, 0);
            }

            return true;
        }

        private void CarOfTrain(int k, int j, Point location, Size itemSize, int interval, Point location2, int strLength,
            Size itemSize1, int[] temps)
        {
            for (int i = 0; i < 10; i++)
            {
                var tmp = GetCarOfTrinNum(k, j);
                if (i == 0) //上面的车厢号
                {
                    m_Collection.Add(new GDIRectText
                    {
                        Text = tmp,
                        NeedDarwOutline = false,
                        TextBrush = GdiCommon.MediumGreyBrush,
                        BackColorVisible = false,
                        DrawFont = GdiCommon.Txt9Font,
                        OutLineRectangle = new Rectangle(location, itemSize)
                    });
                    location.Offset(0, itemSize.Height + interval);
                    continue;
                }
                if (k == 1 && j == 1)//左边的文字
                {
                    m_Collection.Add(new GDIRectText
                    {
                        Text = GlobleParam.ResManagerText.GetString("Text003" + i),
                        NeedDarwOutline = true,
                        TextBrush = GdiCommon.MediumGreyBrush,
                        BackColorVisible = false,
                        DrawFont = GdiCommon.Txt9Font,
                        OutLinePen = GdiCommon.GrayWhitePen,
                        OutLineRectangle =
                            new Rectangle(location2.X, location2.Y, strLength, i == 4 ? itemSize1.Height : itemSize.Height)
                    });
                }

                location2.Offset(0, (i == 4 ? itemSize1.Height : itemSize.Height) + interval);
                if (i == 4)//中间的
                {
                    m_Collection.Add(new GDIProgress(Direction.Up)
                    { 
                        BackgroundColor = m_BkColor,
                        Location = location,
                        Size = itemSize1,
                        CurrentValue = 0,
                        MaxValue = 3,
                        OutLinePen = GdiCommon.MediumGreyPen,
                        NeedOutLine = true,
                        Tag = new[]
                        {
                            EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                            GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                            + tmp + " status 1/3",
                            EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                            GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                            + tmp + " status 2/3",
                            EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                            GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                            + tmp + " status 1",
                            EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                            GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                            + tmp + " faulty"
                        },
                        RefreshAction = o => RefreshProgress((GDIProgress)o)
                    });
                    location.Offset(0, itemSize1.Height + interval);
                    continue;
                }
                //方块
                m_Collection.Add(new GDIRectText
                {
                    NeedDarwOutline = true,
                    BackColorVisible = true,
                    OutLinePen = GdiCommon.MediumGreyPen,
                    BkColor = Color.Red,
                    OutLineRectangle = new Rectangle(location, itemSize),
                    RefreshAction = o => RefreshItem((GDIRectText)o),
                    Tag = GetItemTag(k, temps, i, tmp),
                    Visible = !temps.Contains(m_Collection.Count)
                });
                location.Offset(0, itemSize.Height + interval);
            }
        }
        /// <summary>
        /// 获取方块内的 tag
        /// </summary>
        /// <param name="k"></param>
        /// <param name="temps"></param>
        /// <param name="i"></param>
        /// <param name="tmp"></param>
        /// <returns></returns>
        private string[] GetItemTag(int k, int[] temps, int i, string tmp)
        {
            return temps.Contains(m_Collection.Count)
                ? new[] { "" }
                : new[]
                {
                    EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                    GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                    + tmp + " status 1",
                    EnumUtil.GetDescription((CarType) k).FirstOrDefault() + " " +
                    GlobleParam.ResManagerText.GetString("Text003" + i) + " "
                    + tmp + " status 2"
                };
        }
        /// <summary>
        /// 获取车厢号
        /// </summary>
        /// <param name="k">车辆</param>
        /// <param name="j">车厢</param>
        /// <returns></returns>
        private static string GetCarOfTrinNum(int k, int j)
        {
            var tmp = k <= 3
                ? (j == 1 ? "11" : j == 2 ? "12" : j == 3 ? "21" : "22")
                : (j == 1 ? "22" : j == 2 ? "21" : j == 3 ? "12" : "11");
            return tmp;
        }
        /// <summary>
        /// 刷新方块内的颜色
        /// </summary>
        /// <param name="item"></param>
        private void RefreshItem(GDIRectText item)
        {
            var names = item.Tag as string[];
            if (names.Length < 2)
            {
                return;
            }
            item.BackColorVisible = true;

            if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[0])])
            {
                item.BkColor = m_BkColor;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[1])])
            {
                item.BkColor = Color.Red;
            }
            else
            {
                item.BackColorVisible = false;
            }
        }
        /// <summary>
        /// 刷新矩形框 填充高度 以及颜色
        /// </summary>
        /// <param name="item"></param>
        private void RefreshProgress(GDIProgress item)
        {
            var names = item.Tag as string[];
            if (names.Length < 4)
            {
                return;
            }
            item.BackgroundColor = m_BkColor;
            if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[0])])
            {
                item.CurrentValue = 1;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[1])])
            {
                item.CurrentValue = 2;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[2])])
            {
                item.CurrentValue = 3;
            }
            else if (BoolList[GlobleParam.Instance.FindInBoolIndex(names[3])])
            {
                item.CurrentValue = 3;
                item.BackgroundColor = Color.Red;
            }
            else
            {
                item.CurrentValue = 0;
            }
        }

        public override void paint(Graphics g)
        {    
            //g.DrawString(str, GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, new Rectangle(218, 225, 470, 18), GdiCommon.CenterFormat);

            if (HVAC.isOff)
            {
                g.DrawImage(m_Img[0], new RectangleF(0, 0, 797.5f, 598));
            }
            else
            {
                g.DrawImage(m_Img[1], new RectangleF(0, 0, 797.5f, 598));
            }
            m_Collection.ForEach(e => e.OnPaint(g));
            GlobleParam.Instance.CurrentIranViewIndex = (IranViewIndex) 9;

            m_BtnArr[0].OnDraw(g);
            m_BtnArr[1].OnDraw(g);

        }
        public override bool mouseDown(Point point)
        {
            if (m_BtnArr[0].IsVisible(point))
                m_BtnArr[0].OnMouseDown(point);
            else if (m_BtnArr[1].IsVisible(point))
                m_BtnArr[1].OnMouseDown(point);
            return true;
        }
    }
}