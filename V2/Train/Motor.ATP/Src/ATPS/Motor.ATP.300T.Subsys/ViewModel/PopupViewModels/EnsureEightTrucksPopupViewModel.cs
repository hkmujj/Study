using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureEightTrucksPopupViewModel : EnsureEventPopupViewModelBase
    {

        public EnsureEightTrucksPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainMarshallingNumber;
            EnsurceContent = PopupViewStringKeys.StringEnsureEightTrucksContent;
        }

    }
}