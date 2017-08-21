using System.Threading;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Converter;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.SystemSetting
{
    public class MicrocomputerInfoAdapter : ModelAdapterBase
    {
        private readonly IMicrocomputerInfoViewModel m_MicrocomputerInfo;
        public MicrocomputerInfoAdapter(IEngineAdapter adapter) : base(adapter)
        {
            m_MicrocomputerInfo = Adapter.Model.SystemSetting.TrainInfo.MicrocomputerInfo;
        }

        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.受电弓升起];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.Pantograph = args.NewValue[index] ? NameConst.升 : NameConst.降;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.司机室占用状态1端占用];
            if (args.NewValue.ContainsKey(index) && args.NewValue[index])
            {
                m_MicrocomputerInfo.OccupiedEnd = NameConst.一端占用;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.司机室占用状态2端占用];
            if (args.NewValue.ContainsKey(index) && args.NewValue[index])
            {
                m_MicrocomputerInfo.OccupiedEnd = NameConst.二端占用;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.主断状态闭合];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.MainFaultStatus = args.NewValue[index] ? NameConst.闭 : NameConst.合;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸运转];
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸全制];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸初制];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸常制];
            var index4 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸重联];
            var index5 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸抑制];
            var index6 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.大闸状态大闸紧急];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4) || args.NewValue.ContainsKey(index5) || args.NewValue.ContainsKey(index6))
            {
                m_MicrocomputerInfo.BigFloodgate = string.Empty;
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.运转;
                }
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.全制;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.初制;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.常制;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.重联;
                }
                if (args.NewValue.ContainsKey(index5) && args.NewValue[index5])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.抑制;
                }
                if (args.NewValue.ContainsKey(index6) && args.NewValue[index6])
                {
                    m_MicrocomputerInfo.BigFloodgate = NameConst.紧急;
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.小闸状态小闸单缓];
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.小闸状态小闸全制];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.小闸状态小闸常制];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.小闸状态小闸运转];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                m_MicrocomputerInfo.SmallFloodgate = string.Empty;
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_MicrocomputerInfo.SmallFloodgate = NameConst.单缓;
                }
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_MicrocomputerInfo.SmallFloodgate = NameConst.全制;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_MicrocomputerInfo.SmallFloodgate = NameConst.常制;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    m_MicrocomputerInfo.SmallFloodgate = NameConst.运转;
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.停放切除];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.ParkingCut = args.NewValue[index] ? NameConst.切除 : string.Empty;
            }
        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.循环计数第2页];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.CirculationNum = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.错误标识第2页];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.ErrorFlag = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.手柄级位];
            if (args.NewValue.ContainsKey(index))
            {
                m_MicrocomputerInfo.HandleLevel = args.NewValue[index].ToString("F0");
            }
        }
    }
}