using System.Runtime.InteropServices.ComTypes;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Converter;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.SystemSetting
{
    public class MonitorAdapter : ModelAdapterBase
    {
        private readonly IMonitorDataViewModel TrainInfo;
        public MonitorAdapter(IEngineAdapter adapter) : base(adapter)
        {
            TrainInfo = Adapter.Model.SystemSetting.TrainInfo.MonitorData;
        }

        public override void Changed(CommunicationDataChangedArgs<bool> args)
        {
            base.Changed(args);
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.装置状态监控调车];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.装置状态监控非调车];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.装置状态降级调车];
            var index4 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.装置状态降级非调车];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                TrainInfo.InstallationStatus = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    TrainInfo.InstallationStatus = NameConst.监控调车;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    TrainInfo.InstallationStatus = NameConst.监控非调车;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    TrainInfo.InstallationStatus = NameConst.降级调车;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    TrainInfo.InstallationStatus = NameConst.降级非调车;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.客货本补客本];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.客货本补客补];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.客货本补货本];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.客货本补货补];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                TrainInfo.PassengerComplement = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    TrainInfo.PassengerComplement = NameConst.客本;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    TrainInfo.PassengerComplement = NameConst.客补;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    TrainInfo.PassengerComplement = NameConst.货本;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    TrainInfo.PassengerComplement = NameConst.货补;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况牵引];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况制动];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况惰行];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况向前];
            var index5 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况向后];
            var index6 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.工况零位];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4) || args.NewValue.ContainsKey(index5) || args.NewValue.ContainsKey(index6))
            {
                TrainInfo.WorkingCondition = string.Empty;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    TrainInfo.WorkingCondition = NameConst.牵引;
                }
                if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    TrainInfo.WorkingCondition = NameConst.制动;
                }
                if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    TrainInfo.WorkingCondition = NameConst.惰行;
                }
                if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    TrainInfo.WorkingCondition = NameConst.向前;
                }
                if (args.NewValue.ContainsKey(index5) && args.NewValue[index5])
                {
                    TrainInfo.WorkingCondition = NameConst.向后;
                }
                if (args.NewValue.ContainsKey(index6) && args.NewValue[index6])
                {
                    TrainInfo.WorkingCondition = NameConst.零位;
                }
            }

        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.公里标LKJ];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.KilometerPost = "K+" + args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.机车车号LKJ];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.TrainID = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.运用车次LKJ];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.UseCarID = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.正司机号LKJ];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.DriverID = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.副司机号LKJ];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.FitDriverID = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.速度];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.Speed = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.辆数];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.TotallNum = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.总重];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.TotalWeight = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.计长];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.RememberLong = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.循环计数第1页];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.CirculationNum = args.NewValue[index].ToString("F0");
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.错误标识第1页];
            if (args.NewValue.ContainsKey(index))
            {
                TrainInfo.ErrorFlag = args.NewValue[index].ToString("F0");
            }
        }
    }
}