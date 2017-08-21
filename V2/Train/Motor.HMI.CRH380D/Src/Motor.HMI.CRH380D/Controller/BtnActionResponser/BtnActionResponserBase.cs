using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Motor.HMI.CRH380D.Models.BtnStragy;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller.BtnActionResponser
{
    public abstract class BtnActionResponserBase : IBtnActionResponser
    {
        [Import]
        protected IEventAggregator EventAggregator { private set; get; }

        [Import]
        protected Lazy<DomainViewModel> ViewModel { private set; get; }

        [Import]
        protected IRegionManager RegionManager { private set; get; }

        public BtnItem Parent { get; set; }

        public abstract void ResponseClick();

        public void UpdateState()
        {
            //throw new NotImplementedException();
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
