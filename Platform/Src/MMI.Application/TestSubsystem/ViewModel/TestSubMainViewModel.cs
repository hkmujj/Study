using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace TestSubsystem.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TestSubMainViewModel : NotificationObject, IDataListener
    {
        private readonly IEventAggregator m_EventAggregator;
        private readonly IRegionManager m_RegionManager;
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
        public TestSubMainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            Txt = "fdsafdsa";

            eventAggregator.GetEvent<NavigateViewEvent>().Subscribe(UpdateView, ThreadOption.UIThread, true);
        }

        private void UpdateView(string s)
        {

            //if (Math.Abs(SubParam.Instance.SubsystemInitParam.CommunicationDataService.ReadService.GetFloatAt(5) - 1) < float.Epsilon)
            //{
            //    m_RegionManager.RequestNavigate(RegionNames.SubViewContent, typeof(SubSubView1).FullName);
            //}
            //if (Math.Abs(SubParam.Instance.SubsystemInitParam.CommunicationDataService.ReadService.GetFloatAt(5) - 2) < float.Epsilon)
            //{
            //    m_RegionManager.RequestNavigate(RegionNames.SubViewContent, typeof(SubSubView2).FullName);
            //}
            //if (Math.Abs(SubParam.Instance.SubsystemInitParam.CommunicationDataService.ReadService.GetFloatAt(5) - 3) < float.Epsilon)
            //{
            //    m_RegionManager.RequestNavigate(RegionNames.SubViewContent, typeof(TableTest).FullName);
            //}
            //if (Math.Abs(SubParam.Instance.SubsystemInitParam.CommunicationDataService.ReadService.GetFloatAt(5) - 4) < float.Epsilon)
            //{
            //    m_RegionManager.RequestNavigate(RegionNames.SubViewContent, typeof(TavleTest2).FullName);
            //}
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