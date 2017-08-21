using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Excel.Interface;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.Interface.Data;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IVACViewModel))]
    public class VACViewModel : ViewModelBase, IVACViewModel
    {
        private double m_CurrentTemperature;

        public VACViewModel()
        {
            CurrentTemperature = 25;
            var tmp = ExcelParser.Parser<VACViewUnit>(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory);
            VAC = new ObservableCollection<VACViewUnit>(tmp);
            SetTemperrature = new DelegateCommand<string>(args =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }
                var result = 0;
                int.TryParse(args, out result);
                CurrentTemperature += result;
                EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
                {
                    Index = IndexConfigure.OutFloatIndex[OutFloatKeys.SetAirConditionTemperature],
                    SendData = (int)CurrentTemperature,
                    Type = CmdType.SetFloatValue
                });
            });
            SetModel = new DelegateCommand<string>(args =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }
                VACModel model;
                var resuult = Enum.TryParse(args, false, out model);
                if (resuult)
                {
                    int values = 0;

                    switch (model)
                    {
                        case VACModel.Unknow:
                            values = 0;
                            break;

                        case VACModel.AutoCool:
                            values = 1;
                            break;

                        case VACModel.ManualCool:
                            values = 2;
                            break;

                        case VACModel.Ventilation:
                            values = 3;
                            break;

                        case VACModel.Stop:
                            values = 4;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
                    {
                        Index = IndexConfigure.OutFloatIndex[OutFloatKeys.CoolModel],
                        SendData = values,
                        Type = CmdType.SetFloatValue
                    });
                }
            });
            ChangedVACPage = new DelegateCommand<string>(args =>
            {
                if (string.IsNullOrEmpty(args))
                {
                    return;
                }
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs
                {
                    Name = args,
                    Region = RegionNames.VACRegion
                });
            });
        }

        public override void Clear()
        {
            base.Clear();
            EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
            {
                Index = IndexConfigure.OutFloatIndex[OutFloatKeys.CoolModel],
                SendData = 0,
                Type = CmdType.SetFloatValue
            });
            EventAggregator.GetEvent<SendDataEvent>().Publish(new SendDataEventArgs()
            {
                Index = IndexConfigure.OutFloatIndex[OutFloatKeys.SetAirConditionTemperature],
                SendData = 0,
                Type = CmdType.SetFloatValue
            });
        }

        public ObservableCollection<VACViewUnit> VAC { get; set; }

        public double CurrentTemperature
        {
            get { return m_CurrentTemperature; }
            set
            {
                if (value.Equals(m_CurrentTemperature))
                {
                    return;
                }
                m_CurrentTemperature = value;
                RaisePropertyChanged(() => CurrentTemperature);
            }
        }

        public ICommand SetTemperrature { get; set; }
        public ICommand SetModel { get; set; }
        public ICommand ChangedVACPage { get; set; }
    }
}