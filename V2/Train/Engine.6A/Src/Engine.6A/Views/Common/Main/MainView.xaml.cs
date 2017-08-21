using System;
using System.Windows;
using System.Windows.Controls;
using Engine._6A.Config;
using Engine._6A.Constance;
using Engine._6A.Enums;
using Engine._6A.Event;
using Engine._6A.EventArgs;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace Engine._6A.Views.Common.Main
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainContentRegion)]
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            IsVisibleChanged += MainView_IsVisibleChanged;
        }

        private void MainView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.OldValue && (bool)e.NewValue)
            {
                var args = new NavigateEventArgs { Region = RegionNames.ButtonRegion };
                switch (GlobalParam.Engine6AConfig.Type)
                {
                    case Engine6AType.Alex6:
                        args.Name = Axle6ControlName.ButtonView;
                        break;
                    case Engine6AType.Alex8:
                        args.Name = Axle8ControlName.Axle8ButtonView;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigateEvent>().Publish(args);
            }
        }
    }
}
