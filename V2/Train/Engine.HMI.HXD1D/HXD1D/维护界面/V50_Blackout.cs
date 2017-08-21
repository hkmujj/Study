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
    public class V50_Blackout : baseClass
    {
        public static bool IsMode1 = true;
        private List<Image> _imgs = new List<Image>();

        public override string GetInfo()
        {
            return "断电模式界面";
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
                "选择完成后，请按“确认”键结束！", FormatStyle.Font14, FormatStyle.GreenBrush, new Rectangle(230, 110, 320, 28), FormatStyle.MFormat);
            dcGs.DrawString(
                "模式选择", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(235, 131 + 70, 287, 28), FormatStyle.MFormat);
            dcGs.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(80, 175, 600, 125));

            dcGs.DrawImage(_imgs[IsMode1 ? 0 : 1], new Rectangle(230, 225, 41, 41));
            dcGs.DrawString(
                "切除", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(280, 240, 70, 28), FormatStyle.MFormat);
            dcGs.DrawImage(_imgs[!IsMode1 ? 0 : 1], new Rectangle(430, 225, 41, 41));
            dcGs.DrawString(
                "投入", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(480, 240, 70, 28), FormatStyle.MFormat);

            base.paint(dcGs);
        }
    }
}
