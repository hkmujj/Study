using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Attibutes;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Subsystem.Constant;
using Subway.ShiJiaZhuangLine1.Subsystem.ViewModels;

namespace Subway.ShiJiaZhuangLine1.Subsystem.View.Contents
{
    /// <summary>
    /// EventView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContentRegion)]
    [TitleName("事件信息")]
    public partial class EventView
    {
        public EventView()
        {
            InitializeComponent();
            this.DataContextChanged += EventView_DataContextChanged;
        }

        void EventView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                Data = ((ShellViewModel)DataContext).Model;
            }
        }
        private void VisibleChangedMethod(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && !(bool)e.OldValue)
            {
                if (Data != null)
                {
                    Data.EventPageModel.GetCurrent.Execute(null);
                }
            }
            else if (!(bool)e.NewValue && (bool)e.OldValue)
            {
                if (Data != null)
                {
                    Data.EventPageModel.Reset.Execute(null);
                }
            }

        }
        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            if (Data != null && grid != null)
            {
                var eievent = grid.DataContext as IEventInfo;
                if (eievent != null)
                {
                    Data.EventPageModel.GetCurentEvent.Execute(eievent.LogicId.ToString());
                    //TODO add view change
                    ServiceLocator.Current.GetInstance<ShellViewModel>().Controller.ShellContentNavigateCommand.Execute(ViewNames.EventInfoView);
                    ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.ShellBottomRegion, ViewNames.EventInfoButtonView);
                }

            }
        }

        public IMMI Data { get; private set; }
    }
}
