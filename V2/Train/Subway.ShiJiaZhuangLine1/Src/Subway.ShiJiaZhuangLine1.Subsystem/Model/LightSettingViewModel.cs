using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface.Model;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class LightSettingViewModel : NotificationObject, ILightSettingViewModel
    {
        public LightSettingViewModel()
        {
            Light = 20;
            Plus = new DelegateCommand(PlusMethod);
            Subtract = new DelegateCommand(SubtractMethod);
        }

        private void SubtractMethod()
        {
            if (Light > 0)
            {
                Light -= 10;
            }
        }

        private void PlusMethod()
        {
            if (Light < 100)
            {
                Light += 10;
            }
        }

        private double m_Light;

        public double Light
        {
            get { return m_Light; }
            set
            {
                if (value.Equals(m_Light))
                {
                    return;
                }
                m_Light = value;
                RaisePropertyChanged(() => Light);
            }
        }

        public ICommand Plus { get; private set; }
        public ICommand Subtract { get; private set; }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}