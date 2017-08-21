using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Converter;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.SystemSetting
{
    public class ElectropsychometerTestAdapter : ModelAdapterBase
    {
        private IElectropsychometerTestViewModel Electropsychometer;
        public ElectropsychometerTestAdapter(IEngineAdapter adapter) : base(adapter)
        {
            Electropsychometer = Adapter.Model.SystemSetting.TrainInfo.ElectropsychometerTest;
        }

        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.正向有功];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.PositiveActivePower = args.NewValue[index] ? NameConst.有功 : string.Empty;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.反向有功];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.ReverseActivePower = args.NewValue[index] ? NameConst.有功 : string.Empty;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.正向无功];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.PositiveReactivePower = args.NewValue[index] ? NameConst.无功 : string.Empty;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.反向无功];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.ReverseReactivePower = args.NewValue[index] ? NameConst.无功 : string.Empty;
            }
        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.循环计数第3页];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.CirculationNum = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.错误标识第3页];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.ErrorFlag = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.电流];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.Current = args.NewValue[index].ToString("F0") + "A";
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.电压];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.Voltage = args.NewValue[index].ToString("F0") + "V";
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.电压频率];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.VoltageFrequency = args.NewValue[index].ToString("F2");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.功率因素];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.PowerFactor = (args.NewValue[index]*0.1).ToString("F2");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.有功功率];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.ActivePower = args.NewValue[index].ToString("F2");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.无功功率];
            if (args.NewValue.ContainsKey(index))
            {
                Electropsychometer.ReactivePower = args.NewValue[index].ToString("F2");
            }
        }
    }
}