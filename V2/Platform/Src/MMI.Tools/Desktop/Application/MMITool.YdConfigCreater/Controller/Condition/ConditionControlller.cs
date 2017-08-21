using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.YdConfigCreater.Events;
using MMITool.Addin.YdConfigCreater.ViewModel.Condition;

namespace MMITool.Addin.YdConfigCreater.Controller.Condition
{
    [Export]
    public class ConditionControlller : ControllerBase<ConditionViewModel>
    {
        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public ConditionControlller(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            ViewModel.Model.DBParam.IP = "10.2.2.68";

            ViewModel.Model.CreateResultCommand =
                new DelegateCommand(
                    () =>
                        m_EventAggregator.GetEvent<CreateResultEvent>()
                            .Publish(new CreateResultEvent.Args(ViewModel.Model,
                                ValidateCanCreate()
                                    ? CreateResultEvent.CreateState.ToStart
                                    : CreateResultEvent.CreateState.Fail)));
        }

        private bool ValidateCanCreate()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.Model.DBParam.DBName) )
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(ViewModel.Model.DBParam.IP))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(ViewModel.Model.DBParam.Port))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(ViewModel.Model.SystemParam.SystemID))
            {
                return false;
            }
            return true;
        }
    }
}