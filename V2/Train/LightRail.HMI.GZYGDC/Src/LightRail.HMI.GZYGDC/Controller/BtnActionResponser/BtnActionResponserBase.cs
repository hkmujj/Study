using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using LightRail.HMI.GZYGDC.Model.BtnStragy;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace LightRail.HMI.GZYGDC.Controller.BtnActionResponser
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
            ViewModel.Value.Controller.NavigateCenterTo(stateKey);
        }

        protected void NavigateBack()
        {
            ViewModel.Value.Controller.NavigateCenterBack();
        }

        protected void NavigateToRoot()
        {
            ViewModel.Value.Controller.NavigateCenterToRoot();
            ViewModel.Value.Controller.NavigateBottomToRoot();
        }
    }
}
