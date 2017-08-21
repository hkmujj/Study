using System;
using System.Windows.Forms;

namespace MMI.Facility.DataType.View.Form
{    
    /// <summary>
    /// 连接用窗体，如果窗体不需要相互交互控制，则不需要继承该类
    /// </summary>
    [Obsolete("replace with IHelpForm")]
    public interface IBaseConForm
    {
        /// <summary>
        /// 是否是Mdi
        /// </summary>
        bool IsMdiMode { get; set;}

        void SetMdiParent(System.Windows.Forms.Form parentForm);

        /// <summary>
        /// 关闭窗体
        /// </summary>
        void CloseForm();

        /// <summary>
        /// 获取窗体状态
        /// </summary>
        /// <returns></returns>
        FormWindowState GetFormState();

        /// <summary>
        /// 设置窗体状态
        /// </summary>
        /// <param name="nFws"></param>
        void SetFormState(FormWindowState nFws);

        /// <summary>
        /// 数据复位
        /// </summary>
        void ReastData();

        void ShowForm();

        void ShowFormDialog();
    }
}
