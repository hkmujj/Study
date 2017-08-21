using System.Drawing;

namespace Motor.ATP._300T.共用.功能键与菜单
{
    public struct MenuValue
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="isDrawBkRect">是否是填表框</param>
        /// <param name="isChecked">选中状态</param>
        /// <param name="contentStr">文字内容</param>
        /// <param name="contentImg">图片内容</param>
        /// <param name="contentRect">所在区域</param>
        /// <param name="alignment"></param>
        public MenuValue(bool isDrawBkRect, bool isChecked, string contentStr, Image contentImg, RectangleF contentRect, FontRelated alignment = FontRelated.靠左)
            : this()
        {
            Alignment = alignment;
            IsDrawBKRect = isDrawBkRect;
            IsChecked = isChecked;
            ContentStr = contentStr;
            ContentImg = contentImg;
            ContentRect = contentRect;
        }

        public void OnDraw(Graphics g)
        {
            if (IsDrawBKRect)
            {
                g.FillRectangle(IsChecked ? SolidBrushsItems.WhiteBrush : SolidBrushsItems.DarkGrayBrush, ContentRect);
                g.DrawString(ContentStr, FontsItems.Font16YouB,
                    IsChecked ? SolidBrushsItems.BlackBrush : SolidBrushsItems.WhiteBrush,
                    ContentRect, FontsItems.TheAlignment(Alignment));
            }
            else
            {
                g.DrawString(" " + ContentStr, FontsItems.Font16YouB,
                    SolidBrushsItems.WhiteBrush, ContentRect, FontsItems.TheAlignment(Alignment));

                if (IsChecked)
                {
                    g.DrawRectangle(PenItems.WhitePen2, Rectangle.Round(ContentRect));
                }
            }

            if (ContentImg != null)
            {
                g.DrawImage(ContentImg, ContentRect);
            }
        }

        public bool IsDrawBKRect { get; private set; }

        public bool IsChecked { get; set; }

        public string ContentStr { get; set; }

        public Image ContentImg { get; set; }

        public RectangleF ContentRect { get; set; }

        public FontRelated Alignment { private set; get; }
    }
}