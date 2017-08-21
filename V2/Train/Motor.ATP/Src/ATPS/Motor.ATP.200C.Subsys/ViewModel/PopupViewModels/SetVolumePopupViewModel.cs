using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SetVolumePopupViewModel : DriverPopupViewModelBase
    {
        private int m_VolumePercent;

        public int VolumePercent
        {
            set
            {
                if (value == m_VolumePercent)
                {
                    return;
                }
                m_VolumePercent = value;
                RaisePropertyChanged(() => VolumePercent);
            }
            get { return m_VolumePercent; }
        }

        public SetVolumePopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringVolumeAdjust;
            PopupViewName = PopupContentViewNames.SetVolumeView;
            PopViewTitleContent = PopupViewStringKeys.StringVolume;
            VolumePercent = 50;
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            VolumePercent = (int) driverInterface.ATP.Other.Volumne;

            base.ResponseAction(driverInterface);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            VolumePercent = (int)DriverInterface.ATP.Other.Volumne;
        }
    }
}