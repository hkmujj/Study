using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Subway.CBTC.Casco.Model.BtnStragy;
using Subway.CBTC.Casco.Model.TempModel;

namespace Subway.CBTC.Casco.Model
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private IStateInterface m_CurrentStateInterface;

        [DebuggerStepThrough]
        [ImportingConstructor]
        public DomainModel(RunningModel runningModel, MaintenceModel maintenceModel)
        {
            RunningModel = runningModel;
            MaintenceModel = maintenceModel;
        }

        public RunningModel RunningModel { get; private set; }

        public MaintenceModel MaintenceModel { get; private set; }
        [Import]
        public MessageModel MessageModel { get; private set; }

        public IStateInterface CurrentStateInterface
        {
            private set
            {
                if (Equals(value, m_CurrentStateInterface))
                {
                    return;
                }

                m_CurrentStateInterface = value;
                m_CurrentStateInterface.UpdateState();
                RaisePropertyChanged(() => CurrentStateInterface);
            }
            get { return m_CurrentStateInterface; }
        }

        public void UpdateCurrentState(IStateInterface current)
        {
            CurrentStateInterface = current;
        }
    }
}