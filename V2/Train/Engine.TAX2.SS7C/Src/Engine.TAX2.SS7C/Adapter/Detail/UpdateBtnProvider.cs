using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine.TAX2.SS7C.Extension;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;

namespace Engine.TAX2.SS7C.Adapter.Detail
{
    [Export(typeof(IUpdateDataProvider))]
    public class UpdateBtnProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public UpdateBtnProvider(IEventAggregator eventAggregator, SS7CViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var btn = ViewModel.HardwareBtnViewModel;

            args.ChangedBools.UpdateIfContains(InB.MMI1键按下状态, b => ResponseCommand(b, btn.Model.B1Command));
            args.ChangedBools.UpdateIfContains(InB.MMI2键按下状态, b => ResponseCommand(b, btn.Model.B2Command));
            args.ChangedBools.UpdateIfContains(InB.MMI3键按下状态, b => ResponseCommand(b, btn.Model.B3Command));
            args.ChangedBools.UpdateIfContains(InB.MMI4键按下状态, b => ResponseCommand(b, btn.Model.B4Command));
            args.ChangedBools.UpdateIfContains(InB.MMI5键按下状态, b => ResponseCommand(b, btn.Model.B5Command));
            args.ChangedBools.UpdateIfContains(InB.MMI6键按下状态, b => ResponseCommand(b, btn.Model.B6Command));
            args.ChangedBools.UpdateIfContains(InB.MMI7键按下状态, b => ResponseCommand(b, btn.Model.B7Command));
            args.ChangedBools.UpdateIfContains(InB.MMI8键按下状态, b => ResponseCommand(b, btn.Model.B8Command));
            args.ChangedBools.UpdateIfContains(InB.MMI9键按下状态, b => ResponseCommand(b, btn.Model.B9Command));
            args.ChangedBools.UpdateIfContains(InB.MMI0键按下状态, b => ResponseCommand(b, btn.Model.B10Command));

            args.ChangedBools.UpdateIfContains(InB.MMI解锁键按下状态, b => ResponseCommand(b, btn.Model.UnlockCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI警惕键按下状态, b => ResponseCommand(b, btn.Model.AlertCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI缓解键按下状态, b => ResponseCommand(b, btn.Model.RelaseCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI查询键按下状态, b => ResponseCommand(b, btn.Model.QueryCommand));

            args.ChangedBools.UpdateIfContains(InB.MMI上键按下状态, b => ResponseCommand(b, btn.Model.UpCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI下键按下状态, b => ResponseCommand(b, btn.Model.DownCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI左键按下状态, b => ResponseCommand(b, btn.Model.LeftCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI右键按下状态, b => ResponseCommand(b, btn.Model.RightCommand));

            args.ChangedBools.UpdateIfContains(InB.MMI转存键按下状态, b => ResponseCommand(b, btn.Model.SaveAsCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI确认键按下状态, b => ResponseCommand(b, btn.Model.OKCommand));
            args.ChangedBools.UpdateIfContains(InB.MMI设定键按下状态, b => ResponseCommand(b, btn.Model.SettingCommand));
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