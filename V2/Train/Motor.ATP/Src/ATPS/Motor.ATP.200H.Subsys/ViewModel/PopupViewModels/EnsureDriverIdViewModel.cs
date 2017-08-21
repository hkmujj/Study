using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
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

            PopViewTitleContent = PopupViewStringKeys.StringTitleEnsureDriverIdView;
            TitleContent = PopupViewStringKeys.StringTitleEnsureDriverIdView;
            PopupViewName = PopupContentViewNames.EnsureDriverIdView;
            EventAggregator.GetEvent<DriverInputEvent<DriverInputDriverId>>()
                   .Subscribe(OnInputted, ThreadOption.PublisherThread, true, args => args.ATPType == ATPType.ATP200H);
        }

        private void OnInputted(DriverInputEventArgs<DriverInputDriverId> obj)
        {
            DriverId = obj.SelectedContent.DriverId;
        }
    }
}