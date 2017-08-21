using System.ComponentModel.Composition;
using Subway.WuHanLine6.Models.BtnModel;
using Subway.WuHanLine6.Resource.Keys;
using Subway.WuHanLine6.ViewModels;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// </summary>
    public abstract class BtnResponseBase : IBtnActionResponse
    {
        /// <summary>
        ///     ViewModel
        /// </summary>
        [Import]
        protected WuHanViewModel ViewModel { get; private set; }

        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public abstract void ButtonClick();

        /// <summary>
        ///     导航到对应的Key
        /// </summary>
        /// <param name="key"></param>
        protected void Navigator(string key)
        {
            ViewModel.Controller.Navigator(key);
        }
    }

    /// <summary>
    ///     空调界面帮助按钮响应
    /// </summary>
    [Export]
    public class NetWorkBtnResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.网络拓扑);
        }
    }

    /// <summary>
    ///     空调界面帮助按钮响应
    /// </summary>
    [Export]
    public class NetWorkHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.网络拓扑帮助);
        }
    }

    /// <summary>
    ///     空调界面帮助按钮响应
    /// </summary>
    [Export]
    public class AirConditionHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.空调帮助);
        }
    }

    /// <summary>
    ///     空调界面帮助按钮响应
    /// </summary>
    [Export]
    public class AirConditioBtnResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.空调状态);
        }
    }
}
