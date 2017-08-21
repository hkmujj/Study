using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using System;
using System.Windows;

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// InputTrainDataView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class SelectTrainDataView 
    {
        [ImportingConstructor]
        public SelectTrainDataView(SelectTrainDataViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>().Subscribe(OnSelectedTrainData);
        }

        private IEventAggregator m_EventAggregator;
        protected IEventAggregator EventAggregator
        {
            get
            {
                return m_EventAggregator ?? (m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());
            }
        }

        private void OnSelectedTrainData(DriverInputEventArgs<DriverInputTrainData> driverInputEventArgs)
        {
            switch (driverInputEventArgs.SelectedContent.InputtedTrainData[0])
            {
                case "":
                    Rectangle1.Visibility = Visibility.Hidden;
                    Rectangle2.Visibility = Visibility.Hidden;
                    break;
                case "8辆":
                    Rectangle1.Visibility = Visibility.Visible;
                    Rectangle2.Visibility = Visibility.Hidden;
                    break;
                case "16辆":
                    Rectangle1.Visibility = Visibility.Hidden;
                    Rectangle2.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
