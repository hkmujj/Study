using System.Drawing;

namespace CommonUtil.Controls
{
    /// <summary>
    /// RectangleImage, 矩形图片
    /// </summary>
    public class RectangleImage : CommonInnerControlBase
    {
        /// <summary>
        /// 图片
        /// </summary>
        public Image Image { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        public override void OnDraw(Graphics g)
        {
            if (!Visible)
            {
                return;
            }

            if (Image != null)
            {
                if (!Size.IsEmpty)
                {
                    g.DrawImage(Image, OutLineRectangle);
                }
                else
                {
                    g.DrawImage(Image, Location);
                }
            }
        }
    }
}