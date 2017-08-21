using System;
using System.ComponentModel.Composition;
using Motor.HMI.CRH380BG.Model.Interface;
using Motor.HMI.CRH380BG.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Motor.HMI.CRH380BG.Controller
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class MainCRH380BGController : ControllerBase<Lazy<CRH380BGViewModel>>, IResetSupport
    {
        public void Reset()
        {

        }
    }
}