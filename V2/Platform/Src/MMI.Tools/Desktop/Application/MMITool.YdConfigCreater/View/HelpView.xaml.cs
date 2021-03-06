﻿using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMITool.Addin.YdConfigCreater.ViewModel;

namespace MMITool.Addin.YdConfigCreater.View
{
    /// <summary>
    /// HelpView.xaml 的交互逻辑
    /// </summary>
    [ViewExport("HelpView")]
    [Export]
    public partial class HelpView
    {
        [ImportingConstructor]
        public HelpView(HelpViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public string Title
        {
            get { return "版本更新说明"; }
        }
    }
}
