using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.SystemSetting
{
    public class OlLevelMeterAdapter : ModelAdapterBase
    {
        private readonly IOlLevelMeterTestViewModel m_OlLevelMeter;
        public OlLevelMeterAdapter(IEngineAdapter adapter) : base(adapter)
        {
            m_OlLevelMeter = Adapter.Model.SystemSetting.TrainInfo.OlLevelMeterTest;
        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.循环计数第4页];
            if (args.NewValue.ContainsKey(index))
            {
                m_OlLevelMeter.CirculationNum = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.错误标识第4页];
            if (args.NewValue.ContainsKey(index))
            {
                m_OlLevelMeter.ErrorFlag = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.油量];
            if (args.NewValue.ContainsKey(index))
            {
                m_OlLevelMeter.OilMass = args.NewValue[index].ToString("F0");
            }
        }
    }
}