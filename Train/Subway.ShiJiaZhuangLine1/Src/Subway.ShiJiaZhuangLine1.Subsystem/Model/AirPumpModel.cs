using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class AirPumpModel : NotificationObject,  IAirPunp
    {
        private AirPumpStatus m_Car4AirPumpStatus;
        private AirPumpStatus m_Car3AirPumpStatus;
        private double m_Car5CompressValue;
        private double m_Car2CompressValue;

        public AirPumpModel()
        {
            Car3AirPumpStatus = AirPumpStatus.AirClosed;
            Car4AirPumpStatus = AirPumpStatus.AirClosed;
        }
        public double Car2CompressValue
        {
            get { return m_Car2CompressValue; }
            set
            {
                if (value.Equals(m_Car2CompressValue))
                {
                    return;
                }
                m_Car2CompressValue = value;
                RaisePropertyChanged(() => Car2CompressValue);
            }
        }

        public double Car5CompressValue
        {
            get { return m_Car5CompressValue; }
            set
            {
                if (value.Equals(m_Car5CompressValue))
                {
                    return;
                }
                m_Car5CompressValue = value;
                RaisePropertyChanged(() => Car5CompressValue);
            }
        }

        public AirPumpStatus Car3AirPumpStatus
        {
            get { return m_Car3AirPumpStatus; }
            set
            {
                if (value == m_Car3AirPumpStatus)
                {
                    return;
                }
                m_Car3AirPumpStatus = value;
                RaisePropertyChanged(() => Car3AirPumpStatus);
            }
        }

        public AirPumpStatus Car4AirPumpStatus
        {
            get { return m_Car4AirPumpStatus; }
            set
            {
                if (value == m_Car4AirPumpStatus)
                {
                    return;
                }
                m_Car4AirPumpStatus = value;
                RaisePropertyChanged(() => Car4AirPumpStatus);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空气压缩机严重故障];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空气压缩机运行];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空气压缩机断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) ||
                args.NewValue.ContainsKey(index3))
            {
                Car3AirPumpStatus = AirPumpStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car3AirPumpStatus = AirPumpStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car3AirPumpStatus = AirPumpStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car3AirPumpStatus = AirPumpStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空气压缩机严重故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空气压缩机运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空气压缩机断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) ||
                args.NewValue.ContainsKey(index3))
            {
                Car4AirPumpStatus = AirPumpStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car4AirPumpStatus = AirPumpStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car4AirPumpStatus = AirPumpStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car4AirPumpStatus = AirPumpStatus.AirClosed;
                }
            }
        }
       
        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {
            args.UpdateIfContains(InFloatKeys.TC1_主风管压力, f => Car2CompressValue = f);
            args.UpdateIfContains(InFloatKeys.TC2_主风管压力, f => Car5CompressValue = f);

        }
    }
}