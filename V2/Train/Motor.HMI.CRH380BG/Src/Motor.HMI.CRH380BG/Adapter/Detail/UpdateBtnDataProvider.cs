using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Motor.HMI.CRH380BG.ViewModel;
using Motor.HMI.CRH380BG.Extension;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH380BG.Resources.Keys;

namespace Motor.HMI.CRH380BG.Adapter.Detail
{
    [Export(typeof (IUpdateDataProvider))]
    public class UpdateBtnDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateBtnDataProvider(IEventAggregator eventAggregator, CRH380BGViewModel viewModel, ViewModelManager manager)
            : base(eventAggregator, viewModel, manager)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var brake = Manager.MainViewModel.HardwareBtnViewModel.Model;
            var traction = Manager.ReserveViewModel.HardwareBtnViewModel.Model;

           

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第1个, b => ResponseCommand(b, brake.BReturnCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第2个, b => ResponseCommand(b, brake.LeftCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第3个, b => ResponseCommand(b, brake.RightCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第4个, b => ResponseCommand(b, brake.UpCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第5个, b => ResponseCommand(b, brake.DownCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏右侧按钮_由上往下第6个, b => ResponseCommand(b, brake.OKCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_1, b => ResponseCommand(b, brake.B1Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_2, b => ResponseCommand(b, brake.B2Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_3, b => ResponseCommand(b, brake.B3Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_4, b => ResponseCommand(b, brake.B4Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_5, b => ResponseCommand(b, brake.B5Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_6, b => ResponseCommand(b, brake.B6Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_7, b => ResponseCommand(b, brake.B7Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_8, b => ResponseCommand(b, brake.B8Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_9, b => ResponseCommand(b, brake.B9Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏下侧按钮_0, b => ResponseCommand(b, brake.B10Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第1个按钮, b => ResponseCommand(b, brake.BSwitchCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第2个按钮, b => ResponseCommand(b, brake.BLightCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第3个按钮, b => ResponseCommand(b, brake.BDayandnightSwitchCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第4个按钮, b => ResponseCommand(b, brake.BLanguageSelectCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第5个按钮, b => ResponseCommand(b, brake.BIofoBoxCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第6个按钮, b => ResponseCommand(b, brake.BFaultInfoCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第7个按钮, b => ResponseCommand(b, brake.BTrainRunningCheckCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第8个按钮, b => ResponseCommand(b, brake.BTrainStopCheckCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb1屏上侧第9个按钮, b => ResponseCommand(b, brake.BSwitchDisplayCommand));

            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第1个, b => ResponseCommand(b, traction.BReturnCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第2个, b => ResponseCommand(b, traction.LeftCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第3个, b => ResponseCommand(b, traction.RightCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第4个, b => ResponseCommand(b, traction.UpCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第5个, b => ResponseCommand(b, traction.DownCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏右侧按钮_由上往下第6个, b => ResponseCommand(b, traction.OKCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_1, b => ResponseCommand(b, traction.B1Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_2, b => ResponseCommand(b, traction.B2Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_3, b => ResponseCommand(b, traction.B3Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_4, b => ResponseCommand(b, traction.B4Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_5, b => ResponseCommand(b, traction.B5Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_6, b => ResponseCommand(b, traction.B6Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_7, b => ResponseCommand(b, traction.B7Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_8, b => ResponseCommand(b, traction.B8Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_9, b => ResponseCommand(b, traction.B9Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏下侧按钮_0, b => ResponseCommand(b, traction.B10Command));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第1个按钮, b => ResponseCommand(b, traction.BSwitchCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第2个按钮, b => ResponseCommand(b, traction.BLightCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第3个按钮, b => ResponseCommand(b, traction.BDayandnightSwitchCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第4个按钮, b => ResponseCommand(b, traction.BLanguageSelectCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第5个按钮, b => ResponseCommand(b, traction.BIofoBoxCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第6个按钮, b => ResponseCommand(b, traction.BFaultInfoCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第7个按钮, b => ResponseCommand(b, traction.BTrainRunningCheckCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第8个按钮, b => ResponseCommand(b, traction.BTrainStopCheckCommand));
            args.ChangedBools.UpdateIfContains(InBoolKeys.Inb3屏上侧第9个按钮, b => ResponseCommand(b, traction.BSwitchDisplayCommand));
        }

        private void ResponseCommand(bool need, ICommand command)
        {
            if (command != null && need && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }
    }
}