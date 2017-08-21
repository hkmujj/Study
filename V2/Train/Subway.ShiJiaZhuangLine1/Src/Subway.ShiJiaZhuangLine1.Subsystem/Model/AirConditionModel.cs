using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class AirConditionModel : NotificationObject, IAirCondition
    {
        private AirSystemStatus m_Car6Air2;
        private AirSystemStatus m_Car5Air2;
        private AirSystemStatus m_Car4Air2;
        private AirSystemStatus m_Car3Air2;
        private AirSystemStatus m_Car2Air2;
        private AirSystemStatus m_Car1Air2;
        private AirSystemStatus m_Car6Air1;
        private AirSystemStatus m_Car5Air1;
        private AirSystemStatus m_Car4Air1;
        private AirSystemStatus m_Car3Air1;
        private AirSystemStatus m_Car2Air1;
        private AirSystemStatus m_Car1Air1;

        public AirSystemStatus Car1Air1
        {
            get { return m_Car1Air1; }
            set
            {
                if (value == m_Car1Air1)
                {
                    return;
                }
                m_Car1Air1 = value;
                RaisePropertyChanged(() => Car1Air1);
            }
        }

        public AirSystemStatus Car2Air1
        {
            get { return m_Car2Air1; }
            set
            {
                if (value == m_Car2Air1)
                {
                    return;
                }
                m_Car2Air1 = value;
                RaisePropertyChanged(() => Car2Air1);
            }
        }

        public AirSystemStatus Car3Air1
        {
            get { return m_Car3Air1; }
            set
            {
                if (value == m_Car3Air1)
                {
                    return;
                }
                m_Car3Air1 = value;
                RaisePropertyChanged(() => Car3Air1);
            }
        }

        public AirSystemStatus Car4Air1
        {
            get { return m_Car4Air1; }
            set
            {
                if (value == m_Car4Air1)
                {
                    return;
                }
                m_Car4Air1 = value;
                RaisePropertyChanged(() => Car4Air1);
            }
        }

        public AirSystemStatus Car5Air1
        {
            get { return m_Car5Air1; }
            set
            {
                if (value == m_Car5Air1)
                {
                    return;
                }
                m_Car5Air1 = value;
                RaisePropertyChanged(() => Car5Air1);
            }
        }

        public AirSystemStatus Car6Air1
        {
            get { return m_Car6Air1; }
            set
            {
                if (value == m_Car6Air1)
                {
                    return;
                }
                m_Car6Air1 = value;
                RaisePropertyChanged(() => Car6Air1);
            }
        }

        public AirSystemStatus Car1Air2
        {
            get { return m_Car1Air2; }
            set
            {
                if (value == m_Car1Air2)
                {
                    return;
                }
                m_Car1Air2 = value;
                RaisePropertyChanged(() => Car1Air2);
            }
        }

        public AirSystemStatus Car2Air2
        {
            get { return m_Car2Air2; }
            set
            {
                if (value == m_Car2Air2)
                {
                    return;
                }
                m_Car2Air2 = value;
                RaisePropertyChanged(() => Car2Air2);
            }
        }

        public AirSystemStatus Car3Air2
        {
            get { return m_Car3Air2; }
            set
            {
                if (value == m_Car3Air2)
                {
                    return;
                }
                m_Car3Air2 = value;
                RaisePropertyChanged(() => Car3Air2);
            }
        }

        public AirSystemStatus Car4Air2
        {
            get { return m_Car4Air2; }
            set
            {
                if (value == m_Car4Air2)
                {
                    return;
                }
                m_Car4Air2 = value;
                RaisePropertyChanged(() => Car4Air2);
            }
        }

        public AirSystemStatus Car5Air2
        {
            get { return m_Car5Air2; }
            set
            {
                if (value == m_Car5Air2)
                {
                    return;
                }
                m_Car5Air2 = value;
                RaisePropertyChanged(() => Car5Air2);
            }
        }

        public AirSystemStatus Car6Air2
        {
            get { return m_Car6Air2; }
            set
            {
                if (value == m_Car6Air2)
                {
                    return;
                }
                m_Car6Air2 = value;
                RaisePropertyChanged(() => Car6Air2);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调1故障];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调1运行];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car1Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car1Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car1Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car1Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car1Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car1Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car1Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car1Air2 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调1故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调1运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car2Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car2Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car2Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car2Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车2号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car2Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car2Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car2Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car2Air2 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调1故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调1运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car3Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car3Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car3Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car3Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car3Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car3Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car3Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car3Air2 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调1故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调1运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car4Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car4Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car4Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car4Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car4Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car4Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car4Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car4Air2 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调1故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调1运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car5Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car5Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car5Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car5Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车5号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car5Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car5Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car5Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car5Air2 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调1故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调1运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调1断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car6Air1 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car6Air1 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car6Air1 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car6Air1 = AirSystemStatus.AirClosed;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调2故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调2运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号空调2断开];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3))
            {
                Car6Air2 = AirSystemStatus.AirClosed;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car6Air2 = AirSystemStatus.AirFault;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car6Air2 = AirSystemStatus.AirRunning;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car6Air2 = AirSystemStatus.AirClosed;
                }
            }
        }


        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}