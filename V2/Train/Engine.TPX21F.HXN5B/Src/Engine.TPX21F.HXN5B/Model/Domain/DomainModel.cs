using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.MainData;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain
{
    [Export]
    public class DomainModel : NotificationObject
    {
        private bool m_VigilantCountDownFlag;
        private int m_CurrentVigilantCountDown;

        [ImportingConstructor]
        public DomainModel(MainDataModel mainData)
        {
            MainData = mainData;
        }

        public MainDataModel MainData { private set; get; }

        public bool VigilantCountDownFlag
        {
            get { return m_VigilantCountDownFlag; }
            set
            {
                if (value == m_VigilantCountDownFlag)
                {
                    return;
                }

                m_VigilantCountDownFlag = value;
                RaisePropertyChanged(() => VigilantCountDownFlag);
            }
        }

        public int CurrentVigilantCountDown
        {
            get { return m_CurrentVigilantCountDown; }
            set
            {
                if (value == m_CurrentVigilantCountDown)
                {
                    return;
                }

                m_CurrentVigilantCountDown = value;
                RaisePropertyChanged(() => CurrentVigilantCountDown);
            }
        }
    }
}