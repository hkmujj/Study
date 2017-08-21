using System;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Adapter.Resouce;
using Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn;
using Engine._6A.ViewModel.Common;
using MMI.Facility.Interface.Data;

namespace Engine._6A.Adapter.Adapter.Common.DataMonitor
{
    public class ForTheColumnPageOneAdapter : ModelAdapterBase
    {
        private IForTheColumnOneViewModel OnePage = null;
        private IForTheColumnOneViewModel TwoPage = null;
        public ForTheColumnPageOneAdapter(IEngineAdapter adapter) : base(adapter)
        {
            var model = Adapter.Model.DataMonitor as Axle6DataMonitorViewModel;
            if (model != null)
            {
                OnePage = model.TrainUpOne;
                TwoPage = model.TrainDownOne;
            }
        }

        public override void Changed(CommunicationDataChangedArgs<float> args)
        {
            base.Changed(args);
            if (OnePage != null)
            {
                ChangedFloat(args, OnePage);
            }
            if (TwoPage != null)
            {
                ChangedFloat(args, TwoPage);
            }
        }

        private void ChangedFloat(CommunicationDataChangedArgs<float> args, IForTheColumnOneViewModel model)
        {
            var index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.漏电流1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.LeakageCurrentOneWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.漏电流2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.LeakageCurrentTwoWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.交流输入电压1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.InputVoltageOneWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.交流输入电压2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.InputVoltageTwoWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.交流输入电流1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.InputCurrentOneWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.交流输入电流2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.InputCurrentTwoWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.供电电压1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.SupplyVoltageOneWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.供电电压2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.SupplyVoltageTwoWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.供电电流1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.SupplyCurrentOneWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.供电电流2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.SupplyCurrentTwoWay = FormatCurrent(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.半电压1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.HalfVoltageOneWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.半电压2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.HalfVoltageTwoWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.使用电量1路];
            if (args.NewValue.ContainsKey(index))
            {
                model.UsePowerOneWay = FormatVoltage(args.NewValue[index]);
            }
            index = IndexConfigure.Instance.IndexFacade.InFloatDescriptionDictionary[InFloatKeys.使用电量2路];
            if (args.NewValue.ContainsKey(index))
            {
                model.UsePowerTwoWay = FormatVoltage(args.NewValue[index]);
            }
        }

        private string FormatVoltage(float fPara)
        {
            return fPara.ToString("F0") + "V";
        }

        private string FormatCurrent(float fPara)
        {
            return fPara.ToString("F0") + "A";
        }
    }
}