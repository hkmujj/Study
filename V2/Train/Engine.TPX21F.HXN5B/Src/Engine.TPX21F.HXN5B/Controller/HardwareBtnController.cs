using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Engine.TPX21F.HXN5B.Controller
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
            ViewModel.Model.B1MouseUpCommand = new DelegateCommand<CommandParameter>(OnB1MouseUpExcute);
            ViewModel.Model.B2MouseUpCommand = new DelegateCommand<CommandParameter>(OnB2MouseUpExcute);
            ViewModel.Model.B3MouseUpCommand = new DelegateCommand<CommandParameter>(OnB3MouseUpExcute);
            ViewModel.Model.B4MouseUpCommand = new DelegateCommand<CommandParameter>(OnB4MouseUpExcute);
            ViewModel.Model.B5MouseUpCommand = new DelegateCommand<CommandParameter>(OnB5MouseUpExcute);
            ViewModel.Model.B6MouseUpCommand = new DelegateCommand<CommandParameter>(OnB6MouseUpExcute);
            ViewModel.Model.B7MouseUpCommand = new DelegateCommand<CommandParameter>(OnB7MouseUpExcute);
            ViewModel.Model.B8MouseUpCommand = new DelegateCommand<CommandParameter>(OnB8MouseUpExcute);
            ViewModel.Model.B9MouseUpCommand = new DelegateCommand<CommandParameter>(OnB9MouseUpExcute);
            ViewModel.Model.B10MouseUpCommand = new DelegateCommand<CommandParameter>(OnB10MouseUpExcute);
            ViewModel.Model.BReturnMouseUpCommand = new DelegateCommand<CommandParameter>(OnBReturnMouseUpCommand);

            ViewModel.Model.B1MouseDownCommand = new DelegateCommand<CommandParameter>(OnB1MouseDownExcute);
            ViewModel.Model.B2MouseDownCommand = new DelegateCommand<CommandParameter>(OnB2MouseDownExcute);
            ViewModel.Model.B3MouseDownCommand = new DelegateCommand<CommandParameter>(OnB3MouseDownExcute);
            ViewModel.Model.B4MouseDownCommand = new DelegateCommand<CommandParameter>(OnB4MouseDownExcute);
            ViewModel.Model.B5MouseDownCommand = new DelegateCommand<CommandParameter>(OnB5MouseDownExcute);
            ViewModel.Model.B6MouseDownCommand = new DelegateCommand<CommandParameter>(OnB6MouseDownExcute);
            ViewModel.Model.B7MouseDownCommand = new DelegateCommand<CommandParameter>(OnB7MouseDownExcute);
            ViewModel.Model.B8MouseDownCommand = new DelegateCommand<CommandParameter>(OnB8MouseDownExcute);
            ViewModel.Model.B9MouseDownCommand = new DelegateCommand<CommandParameter>(OnB9MouseDownExcute);
            ViewModel.Model.B10MouseDownCommand = new DelegateCommand<CommandParameter>(OnB10MouseDownExcute);
            ViewModel.Model.BReturnMouseDownCommand = new DelegateCommand<CommandParameter>(OnBReturnMouseDownCommand);

            ViewModel.Model.LeftCommand = new DelegateCommand(OnLeftExcute);
            ViewModel.Model.RightCommand = new DelegateCommand(OnRigthExcute);
            ViewModel.Model.UpCommand = new DelegateCommand(OnUpExcute);
            ViewModel.Model.DownCommand = new DelegateCommand(OnDownExcute);
            ViewModel.Model.QueryCommand = new DelegateCommand(OnQueryExcute);
            ViewModel.Model.SaveAsCommand = new DelegateCommand(OnSaveAsExcute);
            ViewModel.Model.SettingCommand = new DelegateCommand(OnSettingExcute);
            ViewModel.Model.OKCommand = new DelegateCommand(OnOKExcute);
            ViewModel.Model.BTemperatureCommand = new DelegateCommand(OnTemperature);
            ViewModel.Model.BLightDownCommand = new DelegateCommand(OnLightDown);
            ViewModel.Model.BLightUpCommand = new DelegateCommand(OnLightUp);
            ViewModel.Model.BContrastCommand = new DelegateCommand(OnContrast);
            ViewModel.Model.BSoundDownCommand = new DelegateCommand(OnSoundDown);
            ViewModel.Model.BSoundUpCommand= new DelegateCommand(OnSoundUp);
            ViewModel.Model.BContextCommand= new DelegateCommand(OnContext);
            ViewModel.Model.BExclamationCommand= new DelegateCommand(OnExclamation);
            ViewModel.Model.BQuestionMarkCommand= new DelegateCommand(OnQuestionMark);
            ViewModel.Model.BReturnCommand= new DelegateCommand(OnReturn);
        }

        private void OnReturn()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser.ResponseClick();
            }
        }

        private void OnQuestionMark()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBQuestionMark.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBQuestionMark.ActionResponser.ResponseClick();
            }
        }

        private void OnExclamation()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBExclamation.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBExclamation.ActionResponser.ResponseClick();
            }
        }

        private void OnContext()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBContext.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBContext.ActionResponser.ResponseClick();
            }
        }

        private void OnSoundUp()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBSoundUp.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBSoundUp.ActionResponser.ResponseClick();
            }
        }

        private void OnSoundDown()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBSoundDown.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBSoundDown.ActionResponser.ResponseClick();
            }
        }

        private void OnContrast()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBContrast.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBContrast.ActionResponser.ResponseClick();
            }
        }

        private void OnLightUp()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBLightUp.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBLightUp.ActionResponser.ResponseClick();
            }
        }

        private void OnLightDown()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBLightDown.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBLightDown.ActionResponser.ResponseClick();
            }
        }

        private void OnTemperature()
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
               ParentViewModel.Model.CurrentStateInterface.BtnBTemperature.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBTemperature.ActionResponser.ResponseClick();
            }
        }

        protected HXN5BViewModel ParentViewModel
        {
            get { return ViewModel.Parent.Value; }
        }

        private void OnB1MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB2MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB3MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB4MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB5MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB6MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB7MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB8MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB9MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB10MouseDownExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnBReturnMouseDownCommand(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB1MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB2MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB3MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB4MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB5MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB6MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB7MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB8MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB9MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB10MouseUpExcute(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnBReturnMouseUpCommand(CommandParameter para)
        {
            if (ParentViewModel.Model.CurrentStateInterface != null &&
                ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser.ResponseMouseUp(para);
            }
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
                ParentViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser != null)
            {
                ParentViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser.ResponseClick();
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