using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Model.BtnStragy;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.Maintenance
{
    [Export]
    public class MaintenanceController : ControllerBase<MaintenanceViewModel>
    {
        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public MaintenanceController(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            m_EventAggregator.GetEvent<InputCharEvent>().Subscribe(ParserInputChar);
            m_EventAggregator.GetEvent<InputControlWordEvent>().Subscribe(VerfyPassword);
        }

        private void VerfyPassword(InputControlWordEvent.Args obj)
        {
            ViewModel.Model.InputingPassword = string.Empty;

            if (obj.Word == InputControlWordEvent.OkOrCancel.Ok)
            {
                var id = ViewModel.Parent.Value.Parent.Value.Model.CurrentStateInterface.Id;
                if (id == StateInterfaceKey.Parser(StateKeys.Root_维护模式))
                {
                    ViewModel.Parent.Value.Parent.Value.Controller.NavigateTo(StateKeys.Root_维护模式_主页面);
                }
                else if (id == StateInterfaceKey.Parser(StateKeys.Root_制动系统_维护))
                {
                    ViewModel.Parent.Value.Parent.Value.Controller.NavigateTo(StateKeys.Root_制动系统_维护主界面);
                }
            }
        }

        private void ParserInputChar(InputCharEvent.Args args)
        {
            var id = ViewModel.Parent.Value.Parent.Value.Model.CurrentStateInterface.Id;
            if (id == StateInterfaceKey.Parser(StateKeys.Root_维护模式)
                || id == StateInterfaceKey.Parser(StateKeys.Root_制动系统_维护))
            {
                ViewModel.Model.InputingPassword += args.C;
            }
        }
    }
}