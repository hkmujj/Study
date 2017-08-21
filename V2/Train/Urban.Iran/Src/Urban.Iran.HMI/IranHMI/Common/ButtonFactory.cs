using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Urban.Iran.HMI.Common
{
    public static class ButtonFactory
    {
        private static readonly StringFormat CenterFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        public static GDIButton CreateButton(Rectangle region, string content = "", Action<GDIButton> initAction = null)
        {
            var btn = new GDIButton() { BackImage = GdiCommon.BtnBkBitmap, Text = content, OutLineRectangle = region, Padding = Padding.Empty };
            btn.BtnBehavierStrategy = new IranHmiDefaultButtonBehavier(btn);
            btn.ContentTextControl.BkColor = Color.Empty;
            btn.ContentTextControl.DrawFont = GdiCommon.Txt12Font;
            btn.ContentTextControl.TextColor = GdiCommon.MediumGreyPen.Color;
            btn.ContentTextControl.TextFormat = CenterFormat;
            btn.ContentTextControl.NeedDarwOutline = false;

            if (initAction != null)
            {
                initAction(btn);
            }

            return btn;
        }

        public static GDIButton CreateButton(Rectangle region, Action<GDIButton> initAction = null)
        {
            return CreateButton(region, "", initAction);
        }

        public static GDIButton CreateShadowButton(Rectangle region, string content = "", Action<GDIButton> initAction = null)
        {
            var btn = new IranUnenableShadowButton { BackImage = GdiCommon.BtnBkBitmap, Text = content, OutLineRectangle = region, Padding = Padding.Empty };
            btn.ContentTextControl.BkColor = Color.Empty;
            btn.ContentTextControl.DrawFont = GdiCommon.Txt12Font;
            btn.ContentTextControl.TextColor = GdiCommon.MediumGreyPen.Color;
            btn.ContentTextControl.TextFormat = CenterFormat;
            btn.ContentTextControl.NeedDarwOutline = false;

            if (initAction != null)
            {
                initAction(btn);
            }

            return btn;
        }

        public static GDIButton CreateShadowButton(Rectangle region, Action<GDIButton> initAction = null)
        {
            return CreateShadowButton(region, "", initAction);
        }
    }
}