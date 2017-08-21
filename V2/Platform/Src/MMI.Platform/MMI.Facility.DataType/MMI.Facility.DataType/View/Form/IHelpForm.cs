using System.Windows.Forms;
using MMI.Facility.Interface.Data;

// ReSharper disable once CheckNamespace
namespace MMI.Facility.PublicUI.Interface
{
    /// <summary>
    /// 帮助类窗口
    /// </summary>
    public interface IHelpForm
    {
        event FormClosingEventHandler FormClosing;

        /// <summary>
        /// 数据包
        /// </summary>
        IDataPackage DataPackage { get; }

        /// <summary>
        ///         //
        /// 摘要:
        ///     获取或设置此窗体的当前多文档界面 (MDI) 父窗体。
        ///
        /// 返回结果:
        ///     System.Windows.Forms.Form，表示 MDI 父窗体。
        ///
        /// 异常:
        ///   System.Exception:
        ///     分配给此属性的 System.Windows.Forms.Form 没有被标记为 MDI 容器。- 或 -分配给此属性的 System.Windows.Forms.Form
        ///     同时作为子 MDI 窗体和 MDI 容器窗体。- 或 -分配给此属性的 System.Windows.Forms.Form 位于其他线程上。
        /// </summary>
        System.Windows.Forms.Form MdiParent { set; get; }

        /// <summary>
        ///         
        /// 摘要:
        ///     获取或设置窗体的窗口状态。
        ///
        /// 返回结果:
        ///     System.Windows.Forms.FormWindowState，表示窗体的窗口状态。默认值为 FormWindowState.Normal。
        ///
        /// 异常:
        ///   System.ComponentModel.InvalidEnumArgumentException:
        ///     指定值不在有效值范围内。
        /// </summary>
        FormWindowState WindowState { get; set; }

        /// <summary>
        ///         //
        /// // 摘要:
        /// //     获取或设置一个值，指示该窗体是否应显示为最顶层窗体。
        /// //
        /// // 返回结果:
        /// //     如果将窗体显示为最顶层窗体，则为 true；否则为 false。默认值为 false。
        /// </summary>
        bool TopMost { get; set; }

        /// <summary>
        /// 关闭窗体。
        /// </summary>
        void Close();

        /// <summary>
        ///         // 摘要:
        ///     获取或设置一个值，该值指示是否显示该控件及其所有子控件。
        ///
        /// 返回结果:
        ///     如果显示该控件及其所有子控件，则为 true；否则为 false。默认值为 true。
        /// </summary>
        bool Visible { set; get; }

        void Show();

        //
        // 摘要:
        //     将窗体显示为模式对话框，并将当前活动窗口设置为它的所有者。
        //
        // 返回结果:
        //     System.Windows.Forms.DialogResult 值之一。
        //
        // 异常:
        //   System.ArgumentException:
        //     owner 参数中指定的窗体就是显示的窗体。
        //
        //   System.InvalidOperationException:
        //     要显示的窗体已经可见。- 或 -所显示窗体被禁用。- 或 -显示的窗体不是顶级窗口。- 或 -显示为对话框的窗体已经是模式窗体。- 或 -当前进程不是以用户交互模式运行的（有关更多信息，请参见
        //     System.Windows.Forms.SystemInformation.UserInteractive）。
        DialogResult ShowDialog();

        /// <summary>
        /// 数据复位
        /// </summary>
        // todo delete
        void ReastData();


    }
}
