using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;
using Subway.ShiJiaZhuangLine1.Subsystem.Resource.Keys;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{


    public class ResetModel : NotificationObject, IResetModel
    {
        public ResetModel()
        {
            Back = new DelegateCommand(BackMethod);
            Click = new DelegateCommand<string>(ClickMethod);
            Confirm = new DelegateCommand(ConfirmMethod);
            ConfirmVisibility = Visibility.Hidden;
        }
        private void ConfirmMethod()
        {
            ServiceLocator.Current.GetInstance<MMIModel>().Dataserver.WriteService.ChangeBool(OutBoolKeys.ResourceManager.GetString(CurrentKey), true, true);
            CurrentKey = string.Empty;
            this.SetPropertyValue(false);
        }

        public string CurrentKey
        {
            get { return m_CurrentKey; }
            set
            {
                if (value == m_CurrentKey)
                {
                    return;
                }
                m_CurrentKey = value;
                RaisePropertyChanged(() => CurrentKey);
            }
        }


        private bool m_IsCar1Smoke;
        private bool m_IsCar6Assist;
        private bool m_IsCar4Assist;
        private bool m_IsCar3Assist;
        private bool m_IsCar1Assist;
        private bool m_IsCar5Traction;
        private bool m_IsCar4Traction;
        private bool m_IsCar3Traction;
        private bool m_IsCar2Traction;
        private Visibility m_ConfirmVisibility;
        private string m_CurrentKey;

        private void ClickMethod(string obj)
        {
            CurrentKey = obj;
            IsCar2Traction = CurrentKey == ConfirmLogicDisplayString.车2牵引逆变器复位;
            IsCar3Traction = CurrentKey == ConfirmLogicDisplayString.车3牵引逆变器复位;
            IsCar4Traction = CurrentKey == ConfirmLogicDisplayString.车4牵引逆变器复位;
            IsCar5Traction = CurrentKey == ConfirmLogicDisplayString.车5牵引逆变器复位;
            IsCar1Assist = CurrentKey == ConfirmLogicDisplayString.车1辅助逆变器复位;
            IsCar3Assist = CurrentKey == ConfirmLogicDisplayString.车3辅助逆变器复位;
            IsCar4Assist = CurrentKey == ConfirmLogicDisplayString.车4辅助逆变器复位;
            IsCar6Assist = CurrentKey == ConfirmLogicDisplayString.车6辅助逆变器复位;
            IsCar1Smoke = CurrentKey == ConfirmLogicDisplayString.车1烟温探测系统复位;
        }

        private void BackMethod()
        {
            CurrentKey = string.Empty;
            this.SetPropertyValue(false);
        }

        public bool IsCar2Traction
        {
            get { return m_IsCar2Traction; }
            set
            {
                if (value == m_IsCar2Traction)
                {
                    return;
                }
                m_IsCar2Traction = value;
                RaisePropertyChanged(() => IsCar2Traction);
                VisibilityChanged();
            }
        }

        public bool IsCar3Traction
        {
            get { return m_IsCar3Traction; }
            set
            {
                if (value == m_IsCar3Traction)
                {
                    return;
                }
                m_IsCar3Traction = value;
                RaisePropertyChanged(() => IsCar3Traction);
                VisibilityChanged();
            }
        }

        public bool IsCar4Traction
        {
            get { return m_IsCar4Traction; }
            set
            {
                if (value == m_IsCar4Traction)
                {
                    return;
                }
                m_IsCar4Traction = value;
                RaisePropertyChanged(() => IsCar4Traction);
                VisibilityChanged();
            }
        }

        public bool IsCar5Traction
        {
            get { return m_IsCar5Traction; }
            set
            {
                if (value == m_IsCar5Traction)
                {
                    return;
                }
                m_IsCar5Traction = value;
                RaisePropertyChanged(() => IsCar5Traction);
                VisibilityChanged();
            }
        }

        public bool IsCar1Assist
        {
            get { return m_IsCar1Assist; }
            set
            {
                if (value == m_IsCar1Assist)
                {
                    return;
                }
                m_IsCar1Assist = value;
                RaisePropertyChanged(() => IsCar1Assist);
                VisibilityChanged();
            }
        }

        public bool IsCar3Assist
        {
            get { return m_IsCar3Assist; }
            set
            {
                if (value == m_IsCar3Assist)
                {
                    return;
                }
                m_IsCar3Assist = value;
                RaisePropertyChanged(() => IsCar3Assist);
                VisibilityChanged();
            }
        }

        public bool IsCar4Assist
        {
            get { return m_IsCar4Assist; }
            set
            {
                if (value == m_IsCar4Assist)
                {
                    return;
                }
                m_IsCar4Assist = value;
                RaisePropertyChanged(() => IsCar4Assist);
                VisibilityChanged();
            }
        }

        public bool IsCar6Assist
        {
            get { return m_IsCar6Assist; }
            set
            {
                if (value == m_IsCar6Assist)
                {
                    return;
                }
                m_IsCar6Assist = value;
                RaisePropertyChanged(() => IsCar6Assist);
                VisibilityChanged();
            }
        }

        public bool IsCar1Smoke
        {
            get { return m_IsCar1Smoke; }
            set
            {
                if (value == m_IsCar1Smoke)
                {
                    return;
                }
                m_IsCar1Smoke = value;
                RaisePropertyChanged(() => IsCar1Smoke);
                VisibilityChanged();
            }
        }

        public ICommand Click { get; private set; }
        public ICommand Confirm { get; private set; }
        public ICommand Back { get; private set; }

        public Visibility ConfirmVisibility
        {
            get { return m_ConfirmVisibility; }
            set
            {
                if (value == m_ConfirmVisibility)
                {
                    return;
                }
                m_ConfirmVisibility = value;
                RaisePropertyChanged(() => ConfirmVisibility);
            }
        }

        private void VisibilityChanged()
        {
            ConfirmVisibility = this.GetValueAny(true) ? Visibility.Visible : Visibility.Hidden;
        }
        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}