using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class RunningController : ControllerBase<Lazy<RunningViewModel>>
    {
       
        [ImportingConstructor]
        public RunningController(Lazy<RunningViewModel> viewModel) : base(viewModel)
        {
           
        }


        public override void Initalize()
        {
           
        }
    }
}