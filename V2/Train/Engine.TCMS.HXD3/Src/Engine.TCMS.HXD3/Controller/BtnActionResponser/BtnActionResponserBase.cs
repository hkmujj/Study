using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.BtnStragy;
using Engine.TCMS.HXD3.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    public abstract class BtnActionResponserBase :IBtnActionResponser
    {
        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        [Import]
        protected HXD3TCMSViewModel ViewModel { private set; get; }

        [Import]
        protected IRegionManager RegionManager { private set; get; }

        public BtnItem Parent { get; set; }

        public virtual void UpdateState()
        {
            
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public abstract void ResponseClick();

        protected void NavigateTo(string stateKey)
        {
            ViewModel.Controller.NavigateTo(stateKey);
        }

        protected void NavigateBack()
        {
            ViewModel.Controller.NavigateBack();
        }

        protected void NavigateToRoot()
        {
            ViewModel.Controller.NavigateToRoot();
        }
    }
}