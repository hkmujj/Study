using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.WuHanLine6.Models.BtnModel
{
    /// <summary>
    ///
    /// </summary>
    public class BtnItem : NotificationObject
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="content"></param>
        /// <param name="btnActionResponse"></param>
        public BtnItem(object content, IBtnActionResponse btnActionResponse)
        {
            Content = content;
            BtnActionResponse = btnActionResponse;
            ButtonClick = new DelegateCommand(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if (BtnActionResponse != null)
            {
                BtnActionResponse.ButtonClick();
            }
        }

        /// <summary>
        /// 按钮响应命令
        /// </summary>
        public ICommand ButtonClick { get; private set; }

        /// <summary>
        /// 按钮响应事件
        /// </summary>
        public IBtnActionResponse BtnActionResponse { get; private set; }

        /// <summary>
        /// 按钮类容
        /// </summary>
        public object Content { get; private set; }
    }
}