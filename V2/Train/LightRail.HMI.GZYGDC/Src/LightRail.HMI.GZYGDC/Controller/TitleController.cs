using System;
using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class TitleController : ControllerBase<Lazy<TitleViewModel>>
    {
       
        [ImportingConstructor]
        public TitleController(Lazy<TitleViewModel> viewModel) : base(viewModel)
        {
           
        }


        public override void Initalize()
        {
           
        }
    }
}