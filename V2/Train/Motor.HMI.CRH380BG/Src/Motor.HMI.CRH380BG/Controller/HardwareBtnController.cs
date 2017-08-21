using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Motor.HMI.CRH380BG.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HardwareBtnController : ControllerBase<HardwareBtnViewModel>
    {
        protected StateViewModel StateViewModel
        {
            get { return ViewModel.DomainViewModel.StateViewModel; }
        }

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
            ViewModel.Model.QueryCommand = new DelegateCommand(OnQueryExcute);
            ViewModel.Model.SaveAsCommand = new DelegateCommand(OnSaveAsExcute);
            ViewModel.Model.SettingCommand = new DelegateCommand(OnSettingExcute);
            ViewModel.Model.OKCommand = new DelegateCommand(OnOKExcute);
            ViewModel.Model.BSwitchCommand = new DelegateCommand(OnSwitch);
            ViewModel.Model.BLightCommand = new DelegateCommand(OnLight);
            ViewModel.Model.BDayandnightSwitchCommand = new DelegateCommand(OnDayandnightSwitch);
            ViewModel.Model.BLanguageSelectCommand = new DelegateCommand(OnLanguageSelect);
            ViewModel.Model.BIofoBoxCommand = new DelegateCommand(OnIofoBox);
            ViewModel.Model.BFaultInfoCommand = new DelegateCommand(OnFaultInfo);
            ViewModel.Model.BTrainRunningCheckCommand = new DelegateCommand(OnTrainRunningCheck);
            ViewModel.Model.BTrainStopCheckCommand = new DelegateCommand(OnTrainStopCheck);
            ViewModel.Model.BSwitchDisplayCommand = new DelegateCommand(OnSwitchDisplay);
            ViewModel.Model.BReturnCommand = new DelegateCommand(OnReturn);

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

            ViewModel.Model.BOkMouseUpCommand = new DelegateCommand<CommandParameter>(OnOkMouseUpExcute);
            ViewModel.Model.BOkMouseDownCommand = new DelegateCommand<CommandParameter>(OnOkMouseDownExcute);
        }



        private void OnReturn()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBReturn.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnSwitchDisplay()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBSwitchDisplay.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBSwitchDisplay.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnTrainStopCheck()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBTrainStopCheck.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBTrainStopCheck.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnTrainRunningCheck()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBTrainRunningCheck.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBTrainRunningCheck.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnFaultInfo()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBFaultInfo.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBFaultInfo.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnIofoBox()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBIofoBox.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBIofoBox.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnLanguageSelect()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBLanguageSelect.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBLanguageSelect.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnDayandnightSwitch()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBDayandnightSwitch.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBDayandnightSwitch.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnSwitch()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBSwitch.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBSwitch.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnLight()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
               StateViewModel.Model.CurrentStateInterface.BtnBLight.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnBLight.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }



        private void OnB1Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB2Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB3Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB4Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB5Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB6Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB7Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB8Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB9Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB10Excute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnOKExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnSettingExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnSetting.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnSetting.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnSaveAsExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnSaveAs.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnSaveAs.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnQueryExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnQuery.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnQuery.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnUpExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnUp.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnUp.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnRigthExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnRight.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnRight.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnLeftExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnLeft.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnLeft.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnDownExcute()
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnDown.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnDown.ActionResponser.ResponseClick(this.StateViewModel);
            }
        }

        private void OnB1MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB2MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB3MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB4MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB5MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB6MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB7MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB8MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB9MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnB10MouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseMouseDown(para);
            }
        }


        private void OnB1MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB2MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB2.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB3MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB3.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB4MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB4.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB5MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB5.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB6MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB6.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB7MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB7.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB8MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB8.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB9MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB9.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnB10MouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnB10.ActionResponser.ResponseMouseUp(para);
            }
        }

        private void OnOkMouseDownExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser.ResponseMouseDown(para);
            }
        }

        private void OnOkMouseUpExcute(CommandParameter para)
        {
            if (StateViewModel.Model.CurrentStateInterface != null &&
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser != null)
            {
                StateViewModel.Model.CurrentStateInterface.BtnOK.ActionResponser.ResponseMouseUp(para);
            }
        } 
    }
}