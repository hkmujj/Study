using System;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model.Train;
using Motor.TCMS.CRH400BF.Resources.Keys;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    public class ButtonAdapter : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public ButtonAdapter(IEventAggregator eventAggregator, TrainModel trainModel) : base(eventAggregator, trainModel)
        {
        }
        [Import]
        public Lazy<ViewModelManager> ViewModelManager { get; private set; }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            App.Current.GetMainDispatcher().BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                MainScreent(args.ChangedBools);
                ReserveScreent(args.ChangedBools);
            }));

        }

        private void ReserveScreent(CommunicationDataChangedArgs<bool> argsChangedBools)
        {

            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮0, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B10Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮1, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B1Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮2, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B2Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮3, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B3Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮4, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B4Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮5, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B5Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮6, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B6Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮7, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B7Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮8, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B8Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb右屏按钮9, b => { if (b) { ViewModelManager.Value.ReserveViewModel.HardwareBtnViewModel.Model.B9Command.Execute(null); } });
        }

        private void MainScreent(CommunicationDataChangedArgs<bool> argsChangedBools)
        {
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮0, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B10Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮1, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B1Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮2, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B2Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮3, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B3Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮4, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B4Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮5, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B5Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮6, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B6Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮7, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B7Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮8, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B8Command.Execute(null); } });
            argsChangedBools.UpdateIfContains(InbKeys.Inb左屏按钮9, b => { if (b) { ViewModelManager.Value.MainViewModel.HardwareBtnViewModel.Model.B9Command.Execute(null); } });
        }
    }
}
