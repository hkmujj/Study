using System;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Converter;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Config;
using Engine._6A.Enums;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.DataMonitor
{
    public class InsulationAdapter : ModelAdapterBase
    {
        private readonly IInsulationViewModel m_InsulationOne;
        private readonly IInsulationViewModel m_InsulationTwo;
        public InsulationAdapter(IEngineAdapter adapter) : base(adapter)
        {
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    m_InsulationOne = Adapter.Model.DataMonitor.Insulation;
                    break;
                case Engine6AType.Alex8:
                    var tmp = Adapter.Model.DataMonitor as Axle8DataMonitorViewModel;
                    if (tmp != null)
                    {
                        m_InsulationOne = tmp.InsulationOne;
                        m_InsulationTwo = tmp.InsulationTwo;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    ChangedOne(args);
                    break;
                case Engine6AType.Alex8:
                    ChangedOne(args);
                    ChangedTwo(args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangedOne(CommunicationDataChangedArgs<float> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.检测电压];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationOne.TestVoltage = args.NewValue[index1];
            }
            index1 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.报警门限];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationOne.AlarmThreshold = args.NewValue[index1].ToString("F0");
            }
        }
        private void ChangedTwo(CommunicationDataChangedArgs<float> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B检测电压];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationTwo.TestVoltage = args.NewValue[index1];
            }
            index1 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B报警门限];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationTwo.AlarmThreshold = args.NewValue[index1].ToString("F0");
            }
        }

        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(args);
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    ChangedOne(args);
                    break;
                case Engine6AType.Alex8:
                    ChangedOne(args);
                    ChangedTwo(args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void ChangedOne(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.检测类型出库检测];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.检测类型运行检测];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_InsulationOne.TestType = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_InsulationOne.TestType = NameConst.出库检测;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_InsulationOne.TestType = NameConst.运行检测;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.绝缘状态正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.绝缘状态报警];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_InsulationOne.TestResult = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_InsulationOne.TestResult = NameConst.正常;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_InsulationOne.TestResult = NameConst.报警;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.功率模块故障];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationOne.PowerModule = args.NewValue[index1] ? NameConst.故障 : string.Empty;
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.外网有电];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationOne.ElectricNetwork = args.NewValue[index1] ? NameConst.有电 : NameConst.无电;
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.电钥匙开];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationOne.ElectricKey = args.NewValue[index1] ? NameConst.开 : string.Empty;
            }
        }
        private void ChangedTwo(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B检测类型出库检测];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B检测类型运行检测];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_InsulationTwo.TestType = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_InsulationTwo.TestType = NameConst.出库检测;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_InsulationTwo.TestType = NameConst.运行检测;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B绝缘状态正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B绝缘状态报警];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_InsulationTwo.TestResult = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_InsulationTwo.TestResult = NameConst.正常;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_InsulationTwo.TestResult = NameConst.报警;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B功率模块故障];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationTwo.PowerModule = args.NewValue[index1] ? NameConst.故障 : NameConst.正常;
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B外网有电];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationTwo.ElectricNetwork = args.NewValue[index1] ? NameConst.有电 : NameConst.无电;
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B电钥匙开];
            if (args.NewValue.ContainsKey(index1))
            {
                m_InsulationTwo.ElectricKey = args.NewValue[index1] ? NameConst.开 : string.Empty;
            }
        }
    }
}