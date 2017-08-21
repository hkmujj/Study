using System.Globalization;
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
    public class BrakeDataAdapter : ModelAdapterBase
    {
        private readonly IBrakeViewModel m_BrakeViewModelOne;
        private IBrakeViewModel m_BrakeViewModelTwo;
        public BrakeDataAdapter(IEngineAdapter adapter) : base(adapter)
        {
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    m_BrakeViewModelOne = Adapter.Model.DataMonitor.Brake;
                    break;
                case Engine6AType.Alex8:
                    var model = Adapter.Model.DataMonitor as Axle8DataMonitorViewModel;
                    if (model != null)
                    {
                        m_BrakeViewModelOne = model.BrakeOne;
                        m_BrakeViewModelTwo = model.BrakeTwo;
                    }
                    break;
            }
        }

        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    ChangedBoolPageOne(args);
                    break;
                case Engine6AType.Alex8:
                    ChangedBoolPageOne(args);
                    ChangedBoolPageTwo(args);
                    break;
            }
            base.Changed(args);
        }


        private void ChangedFloatPageOne(CommunicationDataChangedArgs<float> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车管压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.TrainPipe = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.停放缸压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.ParkingCylinderOne = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.停放缸2压力预留];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.ParkingCylinderTwo = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.均衡缸压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.BalancingCylinderOne = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.均衡缸2压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.BalancingCylinderTwo = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.制动缸压力预留];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.BrakeCylinder = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.当前流量];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.CurrentNetworkFlow = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车辆数];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.TrainNumber = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.参考速度];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.ReferSpeed = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.贯通辆数];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.TransNum = args.NewValue[index].ToString("F0");
            }

        }

        private void ChangedBoolPageOne(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.当前状态充排风中];
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.当前状态充排风结束];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.当前状态无充排风];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_BrakeViewModelOne.CurrentState = string.Empty;
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_BrakeViewModelOne.CurrentState = NameConst.充排风中;
                }

                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_BrakeViewModelOne.CurrentState = NameConst.充排风结束;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_BrakeViewModelOne.CurrentState = NameConst.无充排风;
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.压力变送器正常];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.PressureTransmitter = args.NewValue[index] ? NameConst.正常 : NameConst.异常;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.流量变送器正常];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelOne.FlowTransmitter = args.NewValue[index] ? NameConst.正常 : NameConst.异常;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.停放制动异常施加];
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.停放制动异常缓解];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1))
            {
                m_BrakeViewModelOne.ParkingBrake = "— —";
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_BrakeViewModelOne.ParkingBrake = NameConst.异常施加;
                }
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_BrakeViewModelOne.ParkingBrake = NameConst.异常缓解;
                }
            }

        }
        private void ChangedFloatPageTwo(CommunicationDataChangedArgs<float> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B列车管压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.TrainPipe = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B停放缸压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.ParkingCylinderOne = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B停放缸2压力预留];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.ParkingCylinderTwo = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B均衡缸压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.BalancingCylinderOne = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B均衡缸2压力];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.BalancingCylinderTwo = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B制动缸压力预留];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.BrakeCylinder = PressureConvert.PressureConverter(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B当前流量];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.CurrentNetworkFlow = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B列车辆数];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.TrainNumber = args.NewValue[index].ToString("f0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B参考速度];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.ReferSpeed = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.列车B贯通辆数];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.TransNum = args.NewValue[index].ToString("F0");
            }

        }

        private void ChangedBoolPageTwo(CommunicationDataChangedArgs<bool> args)
        {
            var index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B当前状态充排风中];
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B当前状态充排风结束];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B当前状态无充排风];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2))
            {
                m_BrakeViewModelTwo.CurrentState = string.Empty;
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_BrakeViewModelTwo.CurrentState = NameConst.充排风中;
                }

                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_BrakeViewModelTwo.CurrentState = NameConst.充排风结束;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    m_BrakeViewModelTwo.CurrentState = NameConst.无充排风;
                }
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B压力变送器正常];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.PressureTransmitter = args.NewValue[index] ? NameConst.正常 : NameConst.异常;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.列车B流量变送器正常];
            if (args.NewValue.ContainsKey(index))
            {
                m_BrakeViewModelTwo.FlowTransmitter = args.NewValue[index] ? NameConst.正常 : NameConst.异常;
            }
            index = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.停放制动异常施加];
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.停放制动异常缓解];
            if (args.NewValue.ContainsKey(index) || args.NewValue.ContainsKey(index1))
            {
                m_BrakeViewModelTwo.ParkingBrake = "— —";
                if (args.NewValue.ContainsKey(index) && args.NewValue[index])
                {
                    m_BrakeViewModelTwo.ParkingBrake = NameConst.异常施加;
                }
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    m_BrakeViewModelTwo.ParkingBrake = NameConst.异常缓解;
                }
            }
        }
        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    ChangedFloatPageOne(args);
                    break;
                case Engine6AType.Alex8:
                    ChangedFloatPageOne(args);
                    ChangedFloatPageTwo(args);
                    break;
            }
        }
    }
}