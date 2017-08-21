using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Project
{
    /// <summary>
    /// 视图活动窗口，程序必须要show 的
    /// </summary>
    public interface IViewForm : IProjectNameProvider
    {

        /// <summary>
        /// 
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 
        /// </summary>
        bool TopMost { set; get; }

        /// <summary>
        ///         // 摘要:
        ///     获取一个值，该值指示控件是否已经被释放。
        ///
        /// 返回结果:
        ///     如果控件已经被释放，则为 true；否则为 false。
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        ///         // 摘要:
        ///     获取或设置一个值，该值指示是否显示该控件及其所有子控件。
        ///
        /// 返回结果:
        ///     如果显示该控件及其所有子控件，则为 true；否则为 false。默认值为 true。
        /// </summary>
        bool Visible { set; get; }


        /// <summary>
        /// 是否显示调试信息
        /// </summary>
        bool DebugInfoVisible { set; get; }

        /// <summary>
        /// 关闭窗体。
        /// </summary>
        void Close();

        /// <summary>
        /// 数据包
        /// </summary>
        IDataPackage DataPackage { get; }

        


        /// <summary>
        ///         // 摘要:
        ///     以桌面坐标设置窗体的位置。
        ///
        /// 参数:
        ///   x:
        ///     窗体位置的 x 坐标。
        ///
        ///   y:
        ///     窗体位置的 y 坐标。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetDesktopLocation(int x, int y);

        /// <summary>
        ///         //
        /// 摘要:
        ///     向用户显示具有指定所有者的窗体。
        ///
        /// 参数:
        ///   owner:
        ///     任何实现 System.Windows.Forms.IWin32Window 并表示将拥有此窗体的顶级窗口的对象。
        ///
        /// 异常:
        ///   System.ArgumentException:
        ///     owner 参数中指定的窗体就是显示的窗体。
        /// </summary>
        void Show();

        /// <summary>
        /// 激活窗口
        /// </summary>
        void Active();

        /// <summary>
        ///         // 摘要:
        ///     选择此窗体，并且可以选择下一个或上一个控件。
        ///
        /// 参数:
        ///   directed:
        ///     如果设置为 true，则更改活动控件
        ///
        ///   forward:
        ///     如果 directed 为 true，则它控制焦点移动的方向。如果此项为 true，则下一个控件被选定；否则，上一个控件被选定
        /// </summary>
        void Select();
    }
}
