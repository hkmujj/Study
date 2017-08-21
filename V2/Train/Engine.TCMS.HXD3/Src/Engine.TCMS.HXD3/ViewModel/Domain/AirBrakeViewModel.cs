using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class AirBrakeViewModel : NotificationObject
    {
        private AirBrakeItemState m_ValveCrock;
        private AirBrakeItemState m_Sand;
        private AirBrakeItemState m_PackingValve;
        private AirBrakeItemState m_Compressor2;
        private AirBrakeItemState m_Compressor1;
        private AirBrakeItemState m_Pantograph;

        public AirBrakeItemState Compressor1
        {
            set
            {
                if (value == m_Compressor1)
                {
                    return;
                }

                m_Compressor1 = value;
                RaisePropertyChanged(() => Compressor1);
            }
            get { return m_Compressor1; }
        }

        public AirBrakeItemState Pantograph
        {
            set
            {
                if (value == m_Pantograph)
                {
                    return;
                }

                m_Pantograph = value;
                RaisePropertyChanged(() => Pantograph);
            }
            get { return m_Pantograph; }
        }

        public AirBrakeItemState Compressor2
        {
            set
            {
                if (value == m_Compressor2)
                {
                    return;
                }

                m_Compressor2 = value;
                RaisePropertyChanged(() => Compressor2);
            }
            get { return m_Compressor2; }
        }

        public AirBrakeItemState PackingValve
        {
            set
            {
                if (value == m_PackingValve)
                {
                    return;
                }

                m_PackingValve = value;
                RaisePropertyChanged(() => PackingValve);
            }
            get { return m_PackingValve; }
        }

        public AirBrakeItemState Sand
        {
            set
            {
                if (value == m_Sand)
                {
                    return;
                }

                m_Sand = value;
                RaisePropertyChanged(() => Sand);
            }
            get { return m_Sand; }
        }

        public AirBrakeItemState ValveCrock
        {
            set
            {
                if (value == m_ValveCrock)
                {
                    return;
                }

                m_ValveCrock = value;
                RaisePropertyChanged(() => ValveCrock);
            }
            get { return m_ValveCrock; }
        }
    }
}