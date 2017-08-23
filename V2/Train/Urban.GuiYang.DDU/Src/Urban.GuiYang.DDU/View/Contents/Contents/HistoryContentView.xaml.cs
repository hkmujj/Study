using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.Model.Train;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.View.Contents.Contents
{
    /// <summary>
    /// HistoryContentView.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentContentAll)]
    public partial class HistoryContentView
    {
        public HistoryContentView()
        {
            InitializeComponent();
            IsVisibleChanged += (sender, args) =>
                {
                    var viewmol = ServiceLocator.Current.GetInstance<FaultViewModel>();
                    viewmol.Model.IsCurrent = (bool)args.NewValue == false;
                    viewmol.Controller.FaultLoad.Execute(null);
                };
        }
    }
}
