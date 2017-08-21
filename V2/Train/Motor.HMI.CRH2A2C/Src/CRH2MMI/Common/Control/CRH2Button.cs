using System.Drawing;
using CommonUtil.Controls.Button;

namespace CRH2MMI.Common.Control
{
    /// <summary>
    /// CRH2的按键
    /// </summary>
    public partial class CRH2Button : GDIButton
    {
        public CRH2Button()
        {
            BtnBehavierStrategy = new CRH2BtnBehavierStrategy(this);
            DownImage = this.BackImage;
            UpImage = this.BackImage;
            SetTextStyle(12, FormatStyle.Center, true, "Arial");
            IsEnable = true;
            Visible = true;
        }

        /// <summary>
        /// 弹起时的图片
        /// </summary>
        public Image UpImage { set; get; }

        /// <summary>
        /// 按下时的图片
        /// </summary>
        public Image DownImage { set; get; }

    }
}
