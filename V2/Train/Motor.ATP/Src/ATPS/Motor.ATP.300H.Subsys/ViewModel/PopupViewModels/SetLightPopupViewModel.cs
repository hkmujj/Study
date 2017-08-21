using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SetLightPopupViewModel : DriverPopupViewModelBase
    {
        private int m_LightPercent;

        private const int Step = 5;

        public int LightPercent
        {
            set
            {
                if (value == m_LightPercent)
                {
                    return;
                }
                m_LightPercent = value;
                RaisePropertyChanged(() => LightPercent);
            }
            get { return m_LightPercent; }
        }

        public SetLightPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleSetLight;
            PopupViewName = PopupContentViewNames.SetLightView;
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            LightPercent = (int)driverInterface.ATP.Other.Light;

            base.ResponseAction(driverInterface);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            LightPercent = (int) DriverInterface.ATP.Other.Light;
        }
    }
}