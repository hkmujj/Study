using System;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Config;
using Engine._6A.Enums;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.DataMonitor
{
    public class RunLineOneAdapter : ModelAdapterBase
    {
        private IRunLineOneViewModelBase m_PageOne;
        private IRunLineOneViewModelBase m_PageTwo;
        public RunLineOneAdapter(IEngineAdapter adapter) : base(adapter)
        {
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    m_PageOne = Adapter.Model.DataMonitor.RunLineOne;
                    break;
                case Engine6AType.Alex8:
                    var model = Adapter.Model.DataMonitor as Axle8DataMonitorViewModel;
                    if (model != null)
                    {
                        m_PageOne = model.RunLineOnePageOne;
                        m_PageTwo = model.RunLineOnePageTwo;
                    }
                    break;
            }
        }

        public override void DataChnged(object sender, CommunicationDataChangedArgs args)
        {
            base.DataChnged(sender, args);
            switch (GlobalParam.Engine6AConfig.Type)
            {
                case Engine6AType.Alex6:
                    DataChangedPageOne(sender, args);
                    break;
                case Engine6AType.Alex8:
                    DataChangedPageOne(sender, args);
                    DataChangedPageTwo(sender, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void DataChangedPageTwo(object sender, CommunicationDataChangedArgs args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位1正常];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位1非正常];
            var index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit1 = Format(m_PageTwo.Axle1Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit2 = Format(m_PageTwo.Axle1Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit3 = Format(m_PageTwo.Axle1Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit4 = Format(m_PageTwo.Axle1Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit5 = Format(m_PageTwo.Axle1Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle1Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle1Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle1Bit6 = Format(m_PageTwo.Axle1Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit1 = Format(m_PageTwo.Axle2Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit2 = Format(m_PageTwo.Axle2Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit3 = Format(m_PageTwo.Axle2Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit4 = Format(m_PageTwo.Axle2Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit5 = Format(m_PageTwo.Axle2Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle2Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle2Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle2Bit6 = Format(m_PageTwo.Axle2Bit6, args.ChangedFloats.NewValue[index3]);

            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit1 = Format(m_PageTwo.Axle3Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit2 = Format(m_PageTwo.Axle3Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit3 = Format(m_PageTwo.Axle3Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit4 = Format(m_PageTwo.Axle3Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit5 = Format(m_PageTwo.Axle3Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴7位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴7位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle3Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle3Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle3Bit6 = Format(m_PageTwo.Axle3Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit1 = Format(m_PageTwo.Axle4Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit2 = Format(m_PageTwo.Axle4Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit3 = Format(m_PageTwo.Axle4Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit4 = Format(m_PageTwo.Axle4Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit5 = Format(m_PageTwo.Axle4Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴8位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴8位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageTwo.Axle4Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageTwo.Axle4Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageTwo.Axle4Bit6 = Format(m_PageTwo.Axle4Bit6, args.ChangedFloats.NewValue[index3]);
            }

        }

        private string Format(bool bPara, float fPara)
        {
            return string.Format("{0}#{1}", fPara.ToString("f0"), bPara ? "正常" : "非正常");
        }

        private string Format(string sPara, float fPara)
        {
            return fPara.ToString("F0") + "#" + (!string.IsNullOrEmpty(sPara) ? sPara.Split('#')[1] : "正常");
        }
        private void DataChangedPageOne(object sender, CommunicationDataChangedArgs args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位1正常];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位1非正常];
            var index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit1 = Format(m_PageOne.Axle1Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit2 = Format(m_PageOne.Axle1Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit3 = Format(m_PageOne.Axle1Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit4 = Format(m_PageOne.Axle1Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit5 = Format(m_PageOne.Axle1Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴1位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴1位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle1Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle1Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle1Bit6 = Format(m_PageOne.Axle1Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit1 = Format(m_PageOne.Axle2Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit2 = Format(m_PageOne.Axle2Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit3 = Format(m_PageOne.Axle2Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit4 = Format(m_PageOne.Axle2Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit5 = Format(m_PageOne.Axle2Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴2位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴2位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle2Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle2Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle2Bit6 = Format(m_PageOne.Axle2Bit6, args.ChangedFloats.NewValue[index3]);

            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit1 = Format(m_PageOne.Axle3Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit2 = Format(m_PageOne.Axle3Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit3 = Format(m_PageOne.Axle3Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit4 = Format(m_PageOne.Axle3Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit5 = Format(m_PageOne.Axle3Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴3位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴3位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle3Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle3Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle3Bit6 = Format(m_PageOne.Axle3Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit1 = Format(m_PageOne.Axle4Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit2 = Format(m_PageOne.Axle4Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit3 = Format(m_PageOne.Axle4Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit4 = Format(m_PageOne.Axle4Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit5 = Format(m_PageOne.Axle4Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴4位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴4位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle4Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle4Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle4Bit6 = Format(m_PageOne.Axle4Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit1 = Format(m_PageOne.Axle5Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit2 = Format(m_PageOne.Axle5Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit3 = Format(m_PageOne.Axle5Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit4 = Format(m_PageOne.Axle5Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit5 = Format(m_PageOne.Axle5Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴5位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴5位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle5Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle5Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle5Bit6 = Format(m_PageOne.Axle5Bit6, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位1正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位1非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位1温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit1 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit1 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit1 = Format(m_PageOne.Axle6Bit1, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位2正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位2非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位2温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit2 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit2 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit2 = Format(m_PageOne.Axle6Bit2, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位3正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位3非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位3温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit3 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit3 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit3 = Format(m_PageOne.Axle6Bit3, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位4正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位4非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位4温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit4 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit4 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit4 = Format(m_PageOne.Axle6Bit4, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位5正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位5非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位5温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit5 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit5 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit5 = Format(m_PageOne.Axle6Bit5, args.ChangedFloats.NewValue[index3]);
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位6正常];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDescriptionDictionary[InBoolKeys.轴6位6非正常];
            index3 = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.轴6位6温度];
            if (args.ChangedBools.NewValue.ContainsKey(index1) && args.ChangedBools.NewValue[index1])
            {
                m_PageOne.Axle6Bit6 = Format(true, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedBools.NewValue.ContainsKey(index2) && args.ChangedBools.NewValue[index2])
            {
                m_PageOne.Axle6Bit6 = Format(false, GlobalParam.InitParam.CommunicationDataService.ReadService.GetFloatAt(index3));
            }
            if (args.ChangedFloats.NewValue.ContainsKey(index3))
            {
                m_PageOne.Axle6Bit6 = Format(m_PageOne.Axle6Bit6, args.ChangedFloats.NewValue[index3]);
            }
        }
    }
}