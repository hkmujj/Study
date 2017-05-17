using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class SmokeModel : NotificationObject, ISmoke
    {
        private SmokeStatus m_Car2SmokeStatus;
        private SmokeStatus m_Car6SmokeStatus;
        private SmokeStatus m_Car5SmokeStatus;
        private SmokeStatus m_Car4SmokeStatus;
        private SmokeStatus m_Car3SmokeStatus;
        private SmokeStatus m_Car1SmokeStatus;

        public SmokeModel()
        {
            Car1SmokeStatus = SmokeStatus.SmokeNormal;
            Car2SmokeStatus = SmokeStatus.SmokeNormal;
            Car3SmokeStatus = SmokeStatus.SmokeNormal;
            Car4SmokeStatus = SmokeStatus.SmokeNormal;
            Car5SmokeStatus = SmokeStatus.SmokeNormal;
            Car6SmokeStatus = SmokeStatus.SmokeNormal;
        }
        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号烟温探测探测到];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号烟温探测失效];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car1SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car1SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car1SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car1SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号烟温探测探测到];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号烟温探测失效];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car2SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car2SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car2SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car2SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号烟温探测探测到];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号烟温探测失效];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car3SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car3SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car3SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car3SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号烟温探测探测到];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号烟温探测失效];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car4SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car4SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car4SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car4SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号烟温探测探测到];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号烟温探测失效];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car5SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car5SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car5SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car5SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号烟温探测探测到];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号烟温探测失效];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号烟温探测正常];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car6SmokeStatus = SmokeStatus.SmokeNormal;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car6SmokeStatus = SmokeStatus.SmokeTemperature;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car6SmokeStatus = SmokeStatus.SmokeFault;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car6SmokeStatus = SmokeStatus.SmokeNormal;
                }
            }
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public SmokeStatus Car1SmokeStatus
        {
            get { return m_Car1SmokeStatus; }
            set
            {
                if (value == m_Car1SmokeStatus)
                {
                    return;
                }
                m_Car1SmokeStatus = value;
                RaisePropertyChanged(() => Car1SmokeStatus);
            }
        }

        public SmokeStatus Car2SmokeStatus
        {
            get { return m_Car2SmokeStatus; }
            set
            {
                if (value == m_Car2SmokeStatus)
                {
                    return;
                }
                m_Car2SmokeStatus = value;
                RaisePropertyChanged(() => Car2SmokeStatus);
            }
        }

        public SmokeStatus Car3SmokeStatus
        {
            get { return m_Car3SmokeStatus; }
            set
            {
                if (value == m_Car3SmokeStatus)
                {
                    return;
                }
                m_Car3SmokeStatus = value;
                RaisePropertyChanged(() => Car3SmokeStatus);
            }
        }

        public SmokeStatus Car4SmokeStatus
        {
            get { return m_Car4SmokeStatus; }
            set
            {
                if (value == m_Car4SmokeStatus)
                {
                    return;
                }
                m_Car4SmokeStatus = value;
                RaisePropertyChanged(() => Car4SmokeStatus);
            }
        }

        public SmokeStatus Car5SmokeStatus
        {
            get { return m_Car5SmokeStatus; }
            set
            {
                if (value == m_Car5SmokeStatus)
                {
                    return;
                }
                m_Car5SmokeStatus = value;
                RaisePropertyChanged(() => Car5SmokeStatus);
            }
        }

        public SmokeStatus Car6SmokeStatus
        {
            get { return m_Car6SmokeStatus; }
            set
            {
                if (value == m_Car6SmokeStatus)
                {
                    return;
                }
                m_Car6SmokeStatus = value;
                RaisePropertyChanged(() => Car6SmokeStatus);
            }
        }
    }
}