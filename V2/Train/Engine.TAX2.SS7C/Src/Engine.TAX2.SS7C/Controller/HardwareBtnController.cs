using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller
{
    [Export]
    public class HardwareBtnController : ControllerBase<HardwareBtnViewModel>
    {
        public void Initalize()
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

        protected SS7CViewModel ParentViewModel
        {
            get { return ViewModel.Parent.Value; }
        }

        private void OnB1Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseClick();
            }
        }

        private void OnB2Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseClick();
            }
        }

        private void OnB3Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseClick();
            }
        }

        private void OnB4Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseClick();
            }
        }

        private void OnB5Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseClick();
            }
        }

        private void OnB6Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseClick();
            }
        }

        private void OnB7Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseClick();
            }
        }

        private void OnB8Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseClick();
            }
        }

        private void OnB9Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseClick();
            }
        }

        private void OnB10Excute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseClick();
            }
        }

        private void OnOKExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnOk.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnOk.ActionResponser.ResponseClick();
            }
        }

        private void OnSettingExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnSetting.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnSetting.ActionResponser.ResponseClick();
            }
        }

        private void OnSaveAsExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnSaveAs.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnSaveAs.ActionResponser.ResponseClick();
            }
        }

        private void OnQueryExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnQuery.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnQuery.ActionResponser.ResponseClick();
            }
        }

        private void OnUnlockExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnUnlock.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnUnlock.ActionResponser.ResponseClick();
            }
        }

        private void OnRelaseExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnRelase.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnRelase.ActionResponser.ResponseClick();
            }
        }

        private void OnAlertExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnVigilant.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnVigilant.ActionResponser.ResponseClick();
            }
        }

        private void OnUpExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnUp.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnUp.ActionResponser.ResponseClick();
            }
        }

        private void OnRigthExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnRight.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnRight.ActionResponser.ResponseClick();
            }
        }

        private void OnLeftExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnLeft.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnLeft.ActionResponser.ResponseClick();
            }
        }

        private void OnDownExcute()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnDown.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnDown.ActionResponser.ResponseClick();
            }
        }

    }
}