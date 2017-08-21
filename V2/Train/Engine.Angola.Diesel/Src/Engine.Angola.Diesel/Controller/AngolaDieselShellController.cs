using System.ComponentModel.Composition;
using Engine.Angola.Diesel.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.Angola.Diesel.Controller
{
    [Export]
    public class AngolaDieselShellController : ControllerBase<AngolaDieselShellViewModel>
    {
    }
}