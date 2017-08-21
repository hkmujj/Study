using System.ComponentModel.Composition;
using System.Windows;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Motor.ATP._300T.Subsys.Constant;
using Motor.ATP._300T.Subsys.Control.UserAction.StateProvider;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Interface;
using System;
using Motor.ATP.Infrasturcture.Model.Events;

namespace Motor.ATP._300T.Subsys.WPFView.PopupViews.Contents
{
    /// <summary>
    /// SelecDirectionView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.RegionPopupViewContent)]
    public partial class SelecDirectionView 
    {
        [ImportingConstructor]
        public SelecDirectionView(SelectDirectionPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>().Subscribe(OnSelectedFreq);
        }

        private IEventAggregator m_EventAggregator;
        protected IEventAggregator EventAggregator
        {
            get
            {
                return m_EventAggregator ?? (m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>());
            }
        }

        private void OnSelectedFreq(DriverInputEventArgs<DriverInputFreq> driverInputEventArgs)
        {
            switch (driverInputEventArgs.SelectedContent.InputtedTrainFreq)
            {
                case TrainFreq.Unkown:
                    Rectangle1.Visibility = Visibility.Hidden;
                    Rectangle2.Visibility = Visibility.Hidden;
                    break;
                case TrainFreq.Up:
                    Rectangle1.Visibility = Visibility.Visible;
                    Rectangle2.Visibility = Visibility.Hidden;
                    break;
                case TrainFreq.Down:
                    Rectangle1.Visibility = Visibility.Hidden;
                    Rectangle2.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
