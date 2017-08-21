using System.Diagnostics;
using Engine.TAX2.SS7C.Model.ConfigModel;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.Details
{
    public class TrainState2Item : NotificationObject
    {
        private AbnormalState m_AbnormalState;

        [DebuggerStepThrough]
        public TrainState2Item(TrainInfoPage2Config item)
        {
            Item = item;
        }

        public TrainInfoPage2Config Item { private set; get; }

        public AbnormalState AbnormalState
        {
            get { return m_AbnormalState; }
            set
            {
                if (value == m_AbnormalState)
                {
                    return;
                }

                m_AbnormalState = value;
                RaisePropertyChanged(() => AbnormalState);
            }
        }
    }
}