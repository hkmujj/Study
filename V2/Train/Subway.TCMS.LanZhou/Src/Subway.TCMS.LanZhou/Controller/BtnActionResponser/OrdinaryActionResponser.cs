using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Subway.TCMS.LanZhou.Event;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.BtnStragy;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.View.Contents.TrainStatus;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Controller.BtnActionResponser
{
    public class OrdinaryActionResponser : NotificationObject, IBtnActionResponser
    {
        protected Type m_TrainStatusPageType;
        public OrdinaryActionResponser()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            m_Domain = new Lazy<DomainViewModel>(() => ServiceLocator.Current.GetInstance<DomainViewModel>());
        }

        public IEventAggregator EventAggregator { get; private set; }

        public IRegionManager RegionManager { get; private set; }

        public BtnItem Parent { get; set; }

        public BtnStateProvider StateProvider { get { return Parent.StateProvider; } }

        private readonly Lazy<DomainViewModel> m_Domain;

        public DomainViewModel Domain
        {
            get { return m_Domain.Value; }
        }

        public virtual void UpdateState()
        {

        }

        public void TowStatusButtonSelected(bool b)
        {     
            Domain.TrainViewModel.Model.IsSelectedTowPage = b;
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public virtual void ResponseClick()
        {
        }

        /// <summary>
        /// 导航内容
        /// </summary>
        /// <param name="contentViewType"></param>
        protected void RequestNavigateToContent(Type contentViewType)
        {
            EventAggregator.GetEvent<ViewChangedEvent>().Publish(new ViewChangedEvent.Args(contentViewType)); 

            RequestNavigateToContent(StateKeys.Root, contentViewType);

        }
        /// <summary>
        /// 导航内容
        /// </summary>
        /// <param name="stateKey"></param>
        /// <param name="contentViewType"></param>
        protected void RequestNavigateToContent(string stateKey, Type contentViewType)
        {
            Domain.Controller.NavigateTo(stateKey);

            RegionManager.RequestNavigateToContent(contentViewType);
        }

    }
}
