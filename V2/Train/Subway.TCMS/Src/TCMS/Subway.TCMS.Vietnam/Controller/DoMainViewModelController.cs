using System;
using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.Vietnam.ViewModel;

namespace Subway.TCMS.Vietnam.Controller
{
    [Export]
    public class DoMainViewModelController : ControllerBase<Lazy<DoMainViewModel>>
    {
        [ImportingConstructor]
        public DoMainViewModelController(Lazy<DoMainViewModel> viewModel) : base(viewModel) { }

        public DoMainViewModel DoMainViewModel { get { return ViewModel.Value; } }
    }
}
