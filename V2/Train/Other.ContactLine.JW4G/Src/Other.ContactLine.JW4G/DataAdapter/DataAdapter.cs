using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;
using Other.ContactLine.JW4G.Model;
using Other.ContactLine.JW4G.Resources;

namespace Other.ContactLine.JW4G.DataAdapter
{
    [Export]
    public class DataAdapter : IDataListener
    {
        [ImportingConstructor]
        public DataAdapter(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            m_AllFault = GlobalParam.Instance.FalutInUnits.ToDictionary(t => t.Number, t => t);
        }

        [Import]
        public ContactLineModel Model { get; private set; }
        public ICommunicationDataReadService DataServer
        {
            get
            {
                return GlobalParam.Instance.InitParam.CommunicationDataService.ReadService;
            }
        }
        protected IProjectIndexDescriptionConfig IndexDescription { get; private set; }
        public void Initalize()
        {
            IndexDescription = GlobalParam.Instance.IndexConfig;
            GlobalParam.Instance.InitParam.RegistDataListener(this);
        }

        private Dictionary<int, FalutInUnit> m_AllFault;
        private List<FalutInUnit> HistoryFault = new List<FalutInUnit>();
        protected IEventAggregator EventAggregator { get; private set; }
        /// <summary>
        /// 数据变化函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadServiceDataChanged(object sender, MMI.Facility.Interface.Data.CommunicationDataChangedArgs e)
        {

            var indexblack = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.黑屏];
            if (e.ChangedBools.NewValue.ContainsKey(indexblack))
            {
                Model.MMIBlack = e.ChangedBools.NewValue[indexblack] ? Visibility.Visible : Visibility.Hidden;
            }

            //e.ChangedFloats.NewValue
            //EventAggregator.GetEvent<DataChangedEvent>().Publish(e);
            var floatIndex0 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机转速];
            var floatIndex1 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发送机冷却水温];
            var floatIndex2 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机机油压力];
            var floatIndex3 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机增压压力];
            var floatIndex4 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机燃油消耗];
            var floatIndex5 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.里程];
            var floatIndex6 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.车速];
            var floatIndex7 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.蓄电池电压];
            var floatIndex8 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.蓄电池电流];
            var floatIndex9 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.燃油油位];
            var floatIndex10 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.变速箱温度];
            var floatIndex11 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.变速箱转速];
            var floatIndex12 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机进气温度];
            var floatIndex13 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机机油温度];
            var floatIndex14 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机电池电压];
            var floatIndex15 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机工作小时];
            var floatIndex16 = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.发动机发电电流];
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex0))
            {
                Model.Speed = e.ChangedFloats.NewValue[floatIndex0];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex1))
            {
                Model.WaterTemputer = e.ChangedFloats.NewValue[floatIndex1];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex2))
            {
                Model.FuelPressure = e.ChangedFloats.NewValue[floatIndex2];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex3))
            {
                Model.IncreasePressure = e.ChangedFloats.NewValue[floatIndex3];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex4))
            {
                Model.FuelUse = e.ChangedFloats.NewValue[floatIndex4];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex5))
            {
                Model.Course = e.ChangedFloats.NewValue[floatIndex5];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex6))
            {
                Model.TrainSpeed = e.ChangedFloats.NewValue[floatIndex6];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex7))
            {
                Model.Voltage = e.ChangedFloats.NewValue[floatIndex7];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex8))
            {
                Model.Electric = e.ChangedFloats.NewValue[floatIndex8];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex9))
            {
                Model.FuelPosition = e.ChangedFloats.NewValue[floatIndex9] * 0.2f;
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex10))
            {
                Model.TempretureOfTransmission = e.ChangedFloats.NewValue[floatIndex10];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex11))
            {
                Model.SpeedOfTransmission = e.ChangedFloats.NewValue[floatIndex11];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex12))
            {
                Model.FlowTempreture = e.ChangedFloats.NewValue[floatIndex12];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex13))
            {
                Model.FuelTempreture = e.ChangedFloats.NewValue[floatIndex13];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex14))
            {
                Model.BatteryPressure = e.ChangedFloats.NewValue[floatIndex14];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex15))
            {
                Model.WorkHours = e.ChangedFloats.NewValue[floatIndex15];
            }
            if (e.ChangedFloats.NewValue.ContainsKey(floatIndex16))
            {
                Model.MakeElectric = e.ChangedFloats.NewValue[floatIndex16];
            }
            var boolIndex0 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.空挡];
            var boolIndex1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.前进];
            var boolIndex2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.后退];
            if (e.ChangedBools.NewValue.ContainsKey(boolIndex0))
            {
                if (e.ChangedBools.NewValue[boolIndex0])
                    Model.WorkStates = WorkStates.Empty;
            }
            if (e.ChangedBools.NewValue.ContainsKey(boolIndex1))
            {
                if (e.ChangedBools.NewValue[boolIndex1])
                    Model.WorkStates = WorkStates.Up;
            }
            if (e.ChangedBools.NewValue.ContainsKey(boolIndex2))
            {
                if (e.ChangedBools.NewValue[boolIndex2])
                    Model.WorkStates = WorkStates.Down;
            }
            if (e.ChangedBools.NewValue.Any(a => m_AllFault.ContainsKey(a.Key)))
            {
                if (m_AllFault.Any(w => e.ChangedBools.NewValue.ContainsKey(w.Key)))
                {
                    foreach (var temp in m_AllFault)
                    {

                        if (e.ChangedBools.NewValue.ContainsKey(temp.Key))
                        {
                            if (e.ChangedBools.NewValue[temp.Key])
                            {
                                if (Model.HistoryFault.Contains(temp.Value))
                                    break;
                                else
                                {
                                    Model.HistoryFault.Add(temp.Value);
                                }
                            }
                            else
                            {
                                if (Model.HistoryFault.Contains(temp.Value))
                                {
                                    //  HistoryFault.Remove(temp.Value);
                                    Model.HistoryFault.Remove(temp.Value);
                                }

                            }
                        }
                    }
                }

                Model.InfoUnit = Model.HistoryFault.Count == 0 ? "" : Model.HistoryFault[0].ContentAlgorithmBase;
            }
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceDataChanged(sender, dataChangedArgs);
        }
    }
}