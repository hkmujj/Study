using System.ComponentModel.Composition;
using Motor.TCMS.CRH400BF.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.TCMS.CRH400BF.Model;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Motor.TCMS.CRH400BF.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HardwareBtnController : ControllerBase<HardwareBtnViewModel>
    {
        public ViewLocation ViewLocation { get; set; }

        public override void Initalize()
        {
            ViewModel.Model.B1Command = new DelegateCommand(OnB1Excute);
            ViewModel.Model.B2Command = new DelegateCommand(OnB2Excute);
            ViewModel.Model.B3Command = new DelegateCommand(OnB3Excute);
            ViewModel.Model.B4Command = new DelegateCommand(OnB4Excute);
            ViewModel.Model.B5Command = new DelegateCommand(OnB5Excute);
            ViewModel.Model.B6Command = new DelegateCommand(OnB6Excute);
            ViewModel.Model.B7Command = new DelegateCommand(OnB7Excute);
            ViewModel.Model.B8Command = new DelegateCommand(OnB8Excute);
            ViewModel.Model.B9Command = new DelegateCommand(OnB9Excute);
            ViewModel.Model.B10Command = new DelegateCommand(OnB10Excute);
            ViewModel.Model.LeftCommand = new DelegateCommand(OnLeftExcute);
            ViewModel.Model.RightCommand = new DelegateCommand(OnRigthExcute);
            ViewModel.Model.UpCommand = new DelegateCommand(OnUpExcute);
            ViewModel.Model.DownCommand = new DelegateCommand(OnDownExcute);
            ViewModel.Model.AlertCommand = new DelegateCommand(OnAlertExcute);
            ViewModel.Model.RelaseCommand = new DelegateCommand(OnRelaseExcute);
            ViewModel.Model.UnlockCommand = new DelegateCommand(OnUnlockExcute);
            ViewModel.Model.QueryCommand = new DelegateCommand(OnQueryExcute);
            ViewModel.Model.SaveAsCommand = new DelegateCommand(OnSaveAsExcute);
            ViewModel.Model.SettingCommand = new DelegateCommand(OnSettingExcute);
            ViewModel.Model.OKCommand = new DelegateCommand(OnOKExcute);
     
        }

        protected StateViewModel StateViewModel { get { return ViewModel.DomainViewModel.StateViewModel; } }

        private void OnB1Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB2Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB3Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB4Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB5Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB6Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB7Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB8Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB9Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnB10Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseClick(StateViewModel);
            }
        }

        private void OnOKExcute()
        {

        }

        private void OnSettingExcute()
        {

        }

        private void OnSaveAsExcute()
        {

        }

        private void OnQueryExcute()
        {

        }

        private void OnUnlockExcute()
        {

        }

        private void OnRelaseExcute()
        {

        }

        private void OnAlertExcute()
        {

        }

        private void OnUpExcute()
        {

        }

        private void OnRigthExcute()
        {

        }

        private void OnLeftExcute()
        {

        }

        private void OnDownExcute()
        {

        }

    }
}