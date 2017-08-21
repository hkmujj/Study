using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.BtnStragy;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    public abstract class BtnActionResponserBase :IBtnActionResponser
    {
        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        [Import]
        protected Lazy<HXN5BViewModel> ViewModel { private set; get; }

        [Import]
        protected IRegionManager RegionManager { private set; get; }

        public BtnItem Parent { get; set; }

        public virtual void UpdateState()
        {
            
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public virtual void ResponseClick()
        {
            
        }

        /// <summary>
        /// 响应按下
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void ResponseMouseDown(CommandParameter parameter)
        {
            
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void ResponseMouseUp(CommandParameter parameter)
        {
            
        }

        protected void NavigateTo(string stateKey)
        {
            ViewModel.Value.Controller.NavigateTo(stateKey);
        }

        protected void NavigateBack()
        {
            ViewModel.Value.Controller.NavigateBack();
        }

        protected void NavigateToRoot()
        {
            ViewModel.Value.Controller.NavigateToRoot();
        }
    }
}