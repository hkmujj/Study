using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Subway.XiaMenLine1.Interface.Attibutes;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Subsystem.ViewModels;

namespace Subway.XiaMenLine1.Subsystem.View.Contents
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
            if (Data != null && grid != null && grid.Tag != null && !grid.Tag.ToString().Equals("0"))
            {
                Data.EventPageModel.GetCurentEvent.Execute(grid.Tag.ToString());
            }
        }

        public IMMI Data { get; private set; }
    }
}
