using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Control.UserAction.InputDataInterpreter;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectTrainDataViewModel : DriverPopupViewModelBase
    {
        public SelectTrainDataViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleSelectTrainMarshallingNumber;
            PopupViewName = PopupContentViewNames.SelectTrainDataView;
        }
    }
}