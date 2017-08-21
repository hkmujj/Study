using System.Drawing;

namespace ES.JCTMS.Common.Control.Common
{
    /// <summary>
    ///     功能描述：控件接口
    ///     创建人：lih
    ///     创建时间：2014-07-14
    /// </summary>
    public interface IControl
    {
        /// <summary>
        ///     读取或设置控件文本属性
        /// </summary>
        string Text { get; set; }

        /// <summary>
        ///     读取控件编号属性
        /// </summary>
        int ID { get; }

        /// <summary>
        ///     获取样式属性
        /// </summary>
        IStyle Style { get; }

        /// <summary>
        ///     读取或设置控件位置属性
        /// </summary>
        RectangleF Rect { get; set; }

        /// <summary>
        ///     读取或设置控件是否获得焦点属性
        /// </summary>
        bool IsFocus { get; set; }

        /// <summary>
        ///     控件绘制
        /// </summary>
        /// <param name="dcGs"></param>
        void Paint(Graphics dcGs);
    }
}