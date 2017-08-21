using System;
using System.ComponentModel.Composition;
using MMI.Facility.Interface.Data;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IDataServiceViewModel))]
    public class DataServiceViewModel : ViewModelBase, IDataServiceViewModel
    {
        public DataServiceViewModel()
        {
            EventAggregator.GetEvent<SendDataEvent>().Subscribe((args) =>
            {
                switch (args.Type)
                {
                    case CmdType.Exit:
                        break;

                    case CmdType.ChangePage:
                        break;

                    case CmdType.SetBoolValue:
                        GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeBool(args.Index,
                            args.Index == 1);
                        break;

                    case CmdType.SetInBoolValue:
                        break;

                    case CmdType.SetFloatValue:
                        GlobalParam.InitParam.CommunicationDataService.WriteService.ChangeFloat(args.Index,
                            args.SendData);
                        break;

                    case CmdType.SetInFloatValue:
                        break;

                    case CmdType.SetCirPrintString:
                        break;

                    case CmdType.SetExValue:
                        break;

                    case CmdType.SetSoundId:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}