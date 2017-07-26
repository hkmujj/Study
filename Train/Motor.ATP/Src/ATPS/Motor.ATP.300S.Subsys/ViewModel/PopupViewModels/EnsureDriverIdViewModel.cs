using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300S.Subsys.Constant;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureDriverIdViewModel : DriverPopupViewModelBase
    {
        private string m_DriverId;

        public string DriverId
        {
            set
            {
                if (value == m_DriverId)
                {
                    return;
                }

                m_DriverId = value;
                RaisePropertyChanged(() => DriverId);
            }
            get { return m_DriverId; }
        }

        public EnsureDriverIdViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureDriverIdView;
            PopupViewName = PopupContentViewNames.EnsureDriverIdView;
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            DriverId = driverInterface.ATP.TrainInfo.Driver.DriverId;

            base.ResponseAction(driverInterface);
        }
    }
}