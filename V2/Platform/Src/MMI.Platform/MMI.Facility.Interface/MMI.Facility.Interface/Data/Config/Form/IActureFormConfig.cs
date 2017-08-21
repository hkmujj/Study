using System.Drawing;

namespace MMI.Facility.Interface.Data.Config.Form
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActureFormConfig
    {
        /// <summary>
        /// 界面的位置信息
        /// </summary>
        Rectangle Rectangle { get; }

        /// <summary>
        /// 是否一直前端显示
        /// </summary>
        bool TopMost { get; }

        /// <summary>
        /// 
        /// </summary>
        float DesignX { get; }

        /// <summary>
        /// 
        /// </summary>
        float DesignY { get; }

        /// <summary>
        /// 
        /// </summary>
        float DesignWidth { get; }

        /// <summary>
        /// 
        /// </summary>
        float DesignHeight { get; }

        /// <summary>
        /// 是否显示鼠标
        /// </summary>
        bool IsCourseVisible { get; }

        /// <summary>
        /// 外部按键是否显示。
        /// </summary>
        bool IsOutterFrameVisible { get; }

    }
}