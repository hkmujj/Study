using System;
using System.Drawing;
using System.Windows.Forms;

namespace Motor.ATP.CommonView.Utils.Extensions
{
    public static class LabelExtension
    {
        private static readonly Action<Label, string> UpdateTextAction = (label, s) => label.Text = s;

        public static void InvokeUpdateTextIfNeed(this Label label, string text)
        {
            label.InvokeIfNeed(UpdateTextAction, label, text);
        }

        public static Font GetLableAutoFont(this Label label, string txt)
        {
            var g = label.CreateGraphics();

            var font = new Font(label.Font.FontFamily, 1, label.Font.Style, label.Font.Unit, label.Font.GdiCharSet,
                label.Font.GdiVerticalFont);
            var region = label.GetActureRectangleF();
            for (int i = 2; i < label.Height * 2 / 3; i++)
            {
                var old = font;
                font = new Font(label.Font.FontFamily, i, label.Font.Style, label.Font.Unit, label.Font.GdiCharSet,
                label.Font.GdiVerticalFont);
                var size = g.MeasureString(txt, font);
                if (size.Width > region.Width || size.Height > region.Height)
                {
                    font.Dispose();
                    return old;
                }
                old.Dispose();
            }
            return font;
        }

        /// <summary>
        /// 使 lable 具有自动字体大小功能。
        /// </summary>
        /// <param name="label"></param>
        /// <param name="maxText">最大可能的内容</param>
        /// <returns></returns>
        public static Label MakeAutoFont(this Label label, string maxText)
        {
            label.SizeChanged += (sender, args) =>
            {
                label.Font = label.GetLableAutoFont(maxText);
                label.Invalidate();
            };
            return label;
        }
    }
}