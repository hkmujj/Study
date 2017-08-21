using System.ComponentModel.Composition;
using Engine.HMI.SS3B.View.Event;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{
    public class ViewModelBase : NotificationObject
    {
        [Import]
        protected IRegionManager RegionManager { private set; get; }


        protected IEventAggregator EventAggregator { private set; get; }

        protected CompositePresentationEvent<ChangeViewContentEventArg> ChangeViewContentEvent
        {
            get
            {
                return EventAggregator.GetEvent<CompositePresentationEvent<ChangeViewContentEventArg>>();
            }
        }

        public ViewModelBase()
        {
            EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            ChangeViewContentEvent.Subscribe(OnChangeViewContent, ThreadOption.UIThread);
        }

        private void OnChangeViewContent(ChangeViewContentEventArg changeViewContentEventArg)
        {
            if (RegionManager == null)
            {
                RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            }
            RegionManager.RequestNavigate(changeViewContentEventArg.RegionName, changeViewContentEventArg.ViewName);
        }
    }
}