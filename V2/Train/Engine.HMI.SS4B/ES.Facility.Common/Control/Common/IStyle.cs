using System.Drawing;

namespace ES.JCTMS.Common.Control.Common
{
    /// <summary>
    ///     功能描述：控件外观style
    ///     创建人：lih
    ///     创建时间：2014-07-14
    /// </summary>
    public interface IStyle
    {
        /// <summary>
        ///     读取或设置控件背景图片属性
        /// </summary>
        Image Background { get; set; }
    }
}