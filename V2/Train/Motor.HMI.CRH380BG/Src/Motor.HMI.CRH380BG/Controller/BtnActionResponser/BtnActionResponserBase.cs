using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.Model.BtnStragy;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller.BtnActionResponser
{
    public abstract class BtnActionResponserBase :IBtnActionResponser
    {
        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        [Import]
        protected Lazy<CRH380BGViewModel> ViewModel { private set; get; }

        [Import]
        protected Lazy<ViewModelManager> ViewManager { private set; get; } 

        public BtnItem Parent { get; set; }

        public virtual void UpdateState()
        {
            
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public virtual void ResponseClick(StateViewModel stateViewModel)
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

        protected void NavigateTo(StateViewModel stateViewModel,string stateKey)
        {
            stateViewModel.Controller.NavigateTo(stateKey);
        }

        protected void NavigateBack(StateViewModel stateViewModel)
        {
            stateViewModel.Controller.NavigateBack();
        }

        protected void NavigateToRoot(StateViewModel stateViewModel)
        {
            stateViewModel.Controller.NavigateToRoot();
        }
    }
}