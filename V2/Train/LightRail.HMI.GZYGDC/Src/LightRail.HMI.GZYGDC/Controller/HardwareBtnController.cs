using System.ComponentModel.Composition;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class HardwareBtnController : ControllerBase<HardwareBtnViewModel>
    {
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

        protected DomainViewModel ParentViewModel
        {
            get { return ViewModel.Parent.Value; }
        }

        private void OnB1Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB1.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB1.ActionResponser.ResponseClick();
            }
        }

        private void OnB2Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB2.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB2.ActionResponser.ResponseClick();
            }
        }

        private void OnB3Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB3.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB3.ActionResponser.ResponseClick();
            }
        }

        private void OnB4Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB4.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB4.ActionResponser.ResponseClick();
            }
        }

        private void OnB5Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB5.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB5.ActionResponser.ResponseClick();
            }
        }

        private void OnB6Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB6.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB6.ActionResponser.ResponseClick();
            }
        }

        private void OnB7Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB7.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB7.ActionResponser.ResponseClick();
            }
        }

        private void OnB8Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB8.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB8.ActionResponser.ResponseClick();
            }
        }

        private void OnB9Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB9.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB9.ActionResponser.ResponseClick();
            }
        }

        private void OnB10Excute()
        {
            if (ParentViewModel.Model.CenterStateInterface != null &&
                ParentViewModel.Model.CenterStateInterface.BtnB10.ActionResponser != null)
            {
                ParentViewModel.Model.CenterStateInterface.BtnB10.ActionResponser.ResponseClick();
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