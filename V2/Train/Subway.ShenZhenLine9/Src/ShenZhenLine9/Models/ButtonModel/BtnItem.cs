using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Subway.ShenZhenLine9.Interfaces;

namespace Subway.ShenZhenLine9.Models.ButtonModel
{
    /// <summary>
    /// 
    /// </summary>
    public class BtnItem :NotificationObject, IBtnItem
    {
        private bool m_IsFault;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public BtnItem(IBtnResponse response)
        {
            Response = response;
            Command = new DelegateCommand((ExecuteMethod));
        }

        private void ExecuteMethod()
        {
            Response?.MouseUp();
        }

        /// <summary>
        /// 按钮文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 响应类
        /// </summary>
        public IBtnResponse Response { get; }

        /// <summary>
        /// 命令
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// 故障
        /// </summary>
        public bool IsFault
        {
            get { return m_IsFault; }
            set
            {
                if (value == m_IsFault)
                    return;

                m_IsFault = value;
                RaisePropertyChanged(() => IsFault);
            }
        }
    }
}