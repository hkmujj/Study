using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using HXD1D.Common;
using HXD1D.Titlte;
using HXD1D.控制设置;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V48_PressModeSet : baseClass
    {
        public static bool IsCutOff = true;
        public static String PointOut = "";
        private List<Image> _imgs = new List<Image>();

        public override string GetInfo()
        {
            return "空压机选择界面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(
                "选择完成后，请按“确认”键结束", FormatStyle.Font14, FormatStyle.RedBrush, new Rectangle(230, 110, 300, 28), FormatStyle.MFormat);
            dcGs.DrawString(
                "模式选择", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(235, 131+70, 287, 28), FormatStyle.MFormat);
            dcGs.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(80,175,600,250));

            dcGs.DrawImage(_imgs[IsCutOff ? 0 : 1], new Rectangle(230,225,41,41));
            dcGs.DrawString(
                "模式一", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(280, 240, 70, 28), FormatStyle.MFormat);
            dcGs.DrawImage(_imgs[!IsCutOff ? 0 : 1], new Rectangle(430, 225, 41, 41));
            dcGs.DrawString(
                "模式二", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(480, 240, 70, 28), FormatStyle.MFormat);
            
            dcGs.DrawString(
                "模式一：总风压力低于750kPa，启单泵；\n      低于680kPa，启双泵。", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(210, 295, 360, 40),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near });
            dcGs.DrawString(
                "模式二：总风压力低于750kPa，启双泵。", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(210, 345, 360, 28),
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Near });

            dcGs.DrawString(
               PointOut,
               FormatStyle.Font12,
               FormatStyle.GreenBrush,
               new RectangleF(270, 482, 180, 30),
               new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
               );

            base.paint(dcGs);
        }
    }
}
