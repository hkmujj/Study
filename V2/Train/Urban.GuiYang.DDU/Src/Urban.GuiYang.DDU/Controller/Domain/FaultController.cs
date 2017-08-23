using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain
{
    [Export]
    public class FaultController : ControllerBase<Lazy<FaultViewModel>>
    {
        [ImportingConstructor]
        public FaultController(Lazy<FaultViewModel> viewmodel) : base(viewmodel)
        {
            NextPage = new DelegateCommand(() => ViewModel.Value.Model.FaultManager.NextPage());
            LastPage = new DelegateCommand(() => ViewModel.Value.Model.FaultManager.LastPage());
            FaultLoad = new DelegateCommand(() => ViewModel.Value.Model.FaultManager.RestPage(ViewModel.Value.Model.IsCurrent));
        }

        public ICommand NextPage { get; private set; }
        public ICommand LastPage { get; private set; }
        public ICommand FaultLoad { get; private set; }

    }
}
