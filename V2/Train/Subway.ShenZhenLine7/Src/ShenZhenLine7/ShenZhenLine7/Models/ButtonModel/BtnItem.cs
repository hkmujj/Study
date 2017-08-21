using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Subway.ShenZhenLine7.Interfaces;

namespace Subway.ShenZhenLine7.Models.ButtonModel
{
    /// <summary>
    /// 
    /// </summary>
    public class BtnItem : IBtnItem
    {
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
    }
}