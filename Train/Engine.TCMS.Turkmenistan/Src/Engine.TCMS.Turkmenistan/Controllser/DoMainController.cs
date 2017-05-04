using System;
using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.ViewModels;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.Turkmenistan.Controllser
{
    [Export]
    public class DoMainController : ControllerBase<Lazy<DoMainViewModel>>
    {
        [ImportingConstructor]
        public DoMainController(Lazy<DoMainViewModel> viewModel) : base(viewModel)
        {

        }
    }
}