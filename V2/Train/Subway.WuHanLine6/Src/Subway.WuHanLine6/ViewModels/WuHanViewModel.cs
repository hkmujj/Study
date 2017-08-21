using System.ComponentModel.Composition;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Regions;
using Subway.WuHanLine6.Constance;
using Subway.WuHanLine6.Controller;
using Subway.WuHanLine6.Interfaces;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.ViewModels
{
    /// <summary>
    /// 武汉  ViewModel
    /// </summary>
    [Export]
    public class WuHanViewModel : IViewModel
    {
        /// <summary>
        /// 初始化WuHanViewModel
        /// </summary>
        /// <param name="model">实现IModels接口的</param>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public WuHanViewModel([Import(ClassExportName.WuHanModel)]IModels model, [Import(ClassExportName.WuHanControler)] WuHanController controller)
        {
            AppLog.Info("Get WuHan Model");
            Model = model as WuHanModel;
            Controller = controller;
            controller.ViewModel = this;
            Instacnce = this;
        }

        /// <summary>
        /// 单例 WuHanViewModel
        /// </summary>
        public static WuHanViewModel Instacnce { get; private set; }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// <see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.
        /// </returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// Model
        /// </summary>
        public WuHanModel Model { get; private set; }

        /// <summary>
        /// 视图控制类
        /// </summary>
        public WuHanController Controller { get; private set; }
    }
}