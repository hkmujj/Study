using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace TestSubsystem1.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TestSubMainViewModel : NotificationObject, IDataListener
    {
        private readonly IEventAggregator m_EventAggregator;
        private string m_Txt;

        public string Txt
        {
            set
            {
                if (value == m_Txt)
                {
                    return;
                }

                m_Txt = value;
                RaisePropertyChanged(() => Txt);
            }
            get { return m_Txt; }
        }

        [ImportingConstructor]
        public TestSubMainViewModel(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            Txt = "fdsafdsa";

            eventAggregator.GetEvent<NavigateViewEvent>().Subscribe(UpdateView, ThreadOption.UIThread, true);
        }

        private void UpdateView(string s)
        {
            var rm = ServiceLocator.Current.GetInstance<IRegionManager>();
            //if (rm.Regions.ContainsRegionWithName(RegionNames.MainContent) &&
            //    !rm.Regions[RegionNames.MainContent].Views.Contains(ServiceLocator.Current.GetInstance<SubView1>()))
            {
                //rm.RegisterViewWithRegion(RegionNames.MainContent, typeof(SubView1));
                //rm.RequestNavigate(RegionNames.MainContent, "TestSubsystem.View.SubView1");
            }
        }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {

        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {

        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            m_EventAggregator.GetEvent<NavigateViewEvent>().Publish("a");
        }
    }

    public class NavigateViewEvent : CompositePresentationEvent<string>
    {

    }
}