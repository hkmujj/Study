using System;
using System.Windows.Forms;

namespace MMI.Facility.DataType.View.Form
{    
    /// <summary>
    /// �����ô��壬������岻��Ҫ�໥�������ƣ�����Ҫ�̳и���
    /// </summary>
    [Obsolete("replace with IHelpForm")]
    public interface IBaseConForm
    {
        /// <summary>
        /// �Ƿ���Mdi
        /// </summary>
        bool IsMdiMode { get; set;}

        void SetMdiParent(System.Windows.Forms.Form parentForm);

        /// <summary>
        /// �رմ���
        /// </summary>
        void CloseForm();

        /// <summary>
        /// ��ȡ����״̬
        /// </summary>
        /// <returns></returns>
        FormWindowState GetFormState();

        /// <summary>
        /// ���ô���״̬
        /// </summary>
        /// <param name="nFws"></param>
        void SetFormState(FormWindowState nFws);

        /// <summary>
        /// ���ݸ�λ
        /// </summary>
        void ReastData();

        void ShowForm();

        void ShowFormDialog();
    }
}
