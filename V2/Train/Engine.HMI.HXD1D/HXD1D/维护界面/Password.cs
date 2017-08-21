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
    public class Password : baseClass
    {
        public static int Index = 0;
        public static string PointOut = "";

        public override string GetInfo()
        {
            return "密码界面";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString(
                "进入维护界面必需通过身份验证", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(235, 131, 287, 28), FormatStyle.MFormat);
            dcGs.DrawString(
                "请输入密码：", FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(290, 215, 150, 32), FormatStyle.MFormat);
            dcGs.DrawRectangle(FormatStyle.WhitePen, new Rectangle(217, 173, 318, 256));
            for (int i = 0; i < 6; i++)
            {
                dcGs.DrawRectangle(FormatStyle.WhitePen1, new Rectangle(309 + i * 25, 268, 22, 40));
            }

            dcGs.DrawString(
                PointOut, FormatStyle.Font14, FormatStyle.WhiteBrush, new Rectangle(329, 348, 150, 32), FormatStyle.MFormat);

            if (ControlSeting._rowid >= 0 && ControlSeting._rowid < 6)
            {
                dcGs.FillRectangle(FormatStyle.BlueBrush, new Rectangle(309 + ControlSeting._rowid * 25, 268, 22, 40));
            }

            foreach (var i in Title.PassWordDictionary.Keys)
            {
                dcGs.DrawString("*", FormatStyle.Font10, FormatStyle.WhiteBrush,
                    new Rectangle(309 + i * 25, 268, 22, 40), FormatStyle.MFormat);
            }

            base.paint(dcGs);
        }
    }
}
