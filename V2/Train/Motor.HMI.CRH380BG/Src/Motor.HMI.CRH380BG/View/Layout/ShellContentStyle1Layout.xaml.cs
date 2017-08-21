using System;
using System.ComponentModel.Composition;
using System.Windows;
using Motor.HMI.CRH380BG.Constant;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.View.Layout
{
    /// <summary>
    /// ShellContentLayout.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ShellContent, IsDefaultView = true)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ShellContentStyle1Layout
    {
        public ShellContentStyle1Layout()
        {
            InitializeComponent();
            //this.Loaded += OnLoaded;
        }

        // TODO 检测用Command为什么会出错
        //private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    var vm = DataContext as CRH380BGViewModel;
        //    if (vm != null)
        //    {
        //        if (vm.StateViewModel.Controller != null && ( vm.StateViewModel != null))
        //        {
        //            vm.StateViewModel.Controller.OnLoaded(new CommandParameter() { Sender = this });
        //        }
        //    }
               
        //}
    }
}
