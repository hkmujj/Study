using System.Drawing;

namespace LightRail.Ethiopia.TMS
{
    /// <summary>
    /// 功能描述：公共字体样式
    /// 创建人：唐林
    /// 创建时间：2014-07-15
    /// </summary>
    public class FontStyle_ES
    {
        /// <summary>
        /// 读取或设置文本字体属性
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 读取或设置文本画刷属性
        /// </summary>
        public SolidBrush TextBrush { get; set; }

        /// <summary>
        /// 读取或设置文本在控件上的对齐方式属性
        /// </summary>
        public StringFormat StringFormat { get; set; }
    }
}
