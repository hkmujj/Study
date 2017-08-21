using System.Drawing;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppViewConfigUnit
    {
        /// <summary>
        /// 
        /// </summary>
        IAppViewConfig ParentConfig { get; }

        /// <summary>
        /// 视图编号
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        Color BgColor { get; }

        /// <summary>
        /// 背景图片
        /// </summary>
        Image BgImage { get; }

        /// <summary>
        /// 背景图片文件路径
        /// </summary>
        string BgImageFn { get; }

        /// <summary>
        /// 背景图片文件名称
        /// </summary>
        string BgImageFile { get; }

        /// <summary>
        /// 资源路径
        /// </summary>
        string RecPath { get; }
    }
}