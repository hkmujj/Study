using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    public class ArTestView
    {
        private HMIBase m_BaseClass;
        public ArTestView(List<CommonInnerControlBase> lstList, HMIBase baseClas)
        {
            m_BaseClass = baseClas;
            var txtSize1 = new Size(135, 30);
            var vluewSize = new Size(95, 30);
            var unitSize = new Size(60, 30);
            var titleSize = new Size(286, 25);
            var outLinewPen = new Pen(Color.FromArgb(198, 196, 194), 2f);

            var textFont = GdiCommon.Txt12Font;
            var textColor = Color.Black;

            var titleColor = Color.FromArgb(150, 150, 150);

            const int startX = 64;
            const int startY = 180;

            var tmpX = startX;
            var tmpY = startY;

            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String146"),
                DrawFont = textFont,
                TextColor = titleColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, titleSize.Width, titleSize.Height),
                TextFormat = GdiCommon.CenterFormat,
                NeedDarwOutline = false,
                BackColorVisible = false,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 1,
                OutLinePen = outLinewPen
            });
            tmpX = tmpX + titleSize.Width + 114;

            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String150"),
                DrawFont = textFont,
                TextColor = titleColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, titleSize.Width, titleSize.Height),
                TextFormat = GdiCommon.CenterFormat,
                NeedDarwOutline = false,
                BackColorVisible = false,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 2,
                OutLinePen = outLinewPen
            });
            tmpX = startX;
            tmpY = tmpY + titleSize.Height + 10;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String147"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 3,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String148"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 4,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String146"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 5,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 6,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String149"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 7,
                OutLinePen = outLinewPen
            });
            var tmpText = (GDIRectText)lstList.Find(f => f is GDIRectText && (int)f.Tag == 2);
            tmpX = tmpText.OutLineRectangle.X;
            tmpY = startY + titleSize.Height + 10;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String147"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 8,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String148"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 9,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String146"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 10,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String151"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 11,
                OutLinePen = outLinewPen
            });
            tmpY += txtSize1.Height;
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManager.GetString("String152"),
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, txtSize1.Width, txtSize1.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 12,
                OutLinePen = outLinewPen
            });
            tmpX = lstList.Find(f => f is GDIRectText && (int)f.Tag == 3).OutLineRectangle.X;
            tmpY = lstList.Find(f => f is GDIRectText && (int)f.Tag == 3).OutLineRectangle.Y;
            tmpX += txtSize1.Width;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Acceleration Inital speed", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Acceleration Final speed", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Acceleration Acceleration", "F2" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Acceleration Inital speed", "F0" },
                // RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Acceleration Traction level", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpX = lstList.Find(f => f is GDIRectText && (int)f.Tag == 8).OutLineRectangle.X;
            tmpY = lstList.Find(f => f is GDIRectText && (int)f.Tag == 8).OutLineRectangle.Y;
            tmpX += txtSize1.Width;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Retardation Inital speed", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Retardation Final speed", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Retardation Acceleration", "F2" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Retardation Distance", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpY += vluewSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, vluewSize.Width, vluewSize.Height),
                TextFormat = GdiCommon.RightFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = new string[] { "Retardation Brake level", "F0" },
                RefreshAction = o => RefreshValue((GDIRectText)o),
                OutLinePen = outLinewPen
            });
            tmpX = lstList.Find(f => f is GDIRectText && (int)f.Tag == 3).OutLineRectangle.X;
            tmpY = lstList.Find(f => f is GDIRectText && (int)f.Tag == 3).OutLineRectangle.Y;
            tmpX = tmpX + vluewSize.Width + txtSize1.Width;
            lstList.Add(new GDIRectText()
            {
                Text = "km/h",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 23,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "km/h",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 24,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "m/s²",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 25,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 26,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "%",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 27,
                OutLinePen = outLinewPen
            });
            tmpX = lstList.Find(f => f is GDIRectText && (int)f.Tag == 8).OutLineRectangle.X;
            tmpY = lstList.Find(f => f is GDIRectText && (int)f.Tag == 8).OutLineRectangle.Y;
            tmpX = tmpX + vluewSize.Width + txtSize1.Width;
            lstList.Add(new GDIRectText()
            {
                Text = "km/h",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 28,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "km/h",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 29,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "m/s²",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 30,
                OutLinePen = outLinewPen
            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "m",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 31,
                OutLinePen = outLinewPen

            });
            tmpY += unitSize.Height;
            lstList.Add(new GDIRectText()
            {
                Text = "%",
                DrawFont = textFont,
                TextColor = textColor,
                OutLineRectangle = new Rectangle(tmpX, tmpY, unitSize.Width, unitSize.Height),
                TextFormat = GdiCommon.LeftFormat,
                NeedDarwOutline = true,
                BackColorVisible = true,
                BkColor = GdiCommon.MediumGreyBrush.Color,
                Tag = 32,
                OutLinePen = outLinewPen

            });
            lstList.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 392, 700, 78),
                NeedDarwOutline = true,
                BackColorVisible = false,
                Tag = 33,
                OutLinePen = GdiCommon.DarkGreyPen

            });
            lstList.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(2, 394, 696, 74),
                NeedDarwOutline = true,
                BackColorVisible = false,
                Tag = 34,
                OutLinePen = GdiCommon.DarkGreyPen

            });
            lstList.Add(new GDIRectText()
            {
                Text = GlobleParam.ResManagerText.GetString("Text0021"),
                OutLineRectangle = new Rectangle(120, 394, 574, 74),
                NeedDarwOutline = false,
                BackColorVisible = false,
                Tag = 35,
                TextColor = Color.FromArgb(150, 150, 150),
                OutLinePen = GdiCommon.DarkGreyPen,
                TextFormat = GdiCommon.LeftFormat

            });
            lstList.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(0, 170, 410, 221),
                NeedDarwOutline = true,
                BackColorVisible = false,
                Tag = 36,
                TextColor = Color.FromArgb(150, 150, 150),
                OutLinePen = new Pen(Color.FromArgb(40, 40, 40), 3f),
                TextFormat = GdiCommon.LeftFormat

            });
            lstList.Add(new GDIRectText()
            {
                OutLineRectangle = new Rectangle(412, 170, 390, 221),
                NeedDarwOutline = true,
                BackColorVisible = false,
                Tag = 37,
                TextColor = Color.FromArgb(150, 150, 150),
                OutLinePen = new Pen(Color.FromArgb(40, 40, 40), 3f),
                TextFormat = GdiCommon.LeftFormat

            });
        }

        private void RefreshValue(GDIRectText item)
        {
            var names = item.Tag as string[];
            if (names.Length < 2)
            {
                return;
            }
            try
            {
                item.Text = m_BaseClass.FloatList[GlobleParam.Instance.FindInBoolIndex(names[0])].ToString(names[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
