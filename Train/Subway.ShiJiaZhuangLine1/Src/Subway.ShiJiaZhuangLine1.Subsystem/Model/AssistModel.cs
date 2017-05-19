using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class AssistModel : NotificationObject, IAssistModel
    {
        private AssistPowerStatus m_Car6Charge;
        private AssistPowerStatus m_Car1Charge;
        private AssistPowerStatus m_Car4Organ;
        private AssistPowerStatus m_Car3Organ;
        private AssistPowerStatus m_Car6Organ;
        private AssistPowerStatus m_Car1Organ;

        public AssistModel()
        {
            Car1Charge = AssistPowerStatus.AssistChargeDisconnect;
            Car6Charge = AssistPowerStatus.AssistChargeDisconnect;
        }

        public AssistPowerStatus Car1Organ
        {
            get { return m_Car1Organ; }
            set
            {
                if (value == m_Car1Organ)
                {
                    return;
                }
                m_Car1Organ = value;
                RaisePropertyChanged(() => Car1Organ);
            }
        }

        public AssistPowerStatus Car6Organ
        {
            get { return m_Car6Organ; }
            set
            {
                if (value == m_Car6Organ)
                {
                    return;
                }
                m_Car6Organ = value;
                RaisePropertyChanged(() => Car6Organ);
            }
        }

        public AssistPowerStatus Car3Organ
        {
            get { return m_Car3Organ; }
            set
            {
                if (value == m_Car3Organ)
                {
                    return;
                }
                m_Car3Organ = value;
                RaisePropertyChanged(() => Car3Organ);
            }
        }

        public AssistPowerStatus Car4Organ
        {
            get { return m_Car4Organ; }
            set
            {
                if (value == m_Car4Organ)
                {
                    return;
                }
                m_Car4Organ = value;
                RaisePropertyChanged(() => Car4Organ);
            }
        }

        public AssistPowerStatus Car1Charge
        {
            get { return m_Car1Charge; }
            set
            {
                if (value == m_Car1Charge)
                {
                    return;
                }
                m_Car1Charge = value;
                RaisePropertyChanged(() => Car1Charge);
            }
        }

        public AssistPowerStatus Car6Charge
        {
            get { return m_Car6Charge; }
            set
            {
                if (value == m_Car6Charge)
                {
                    return;
                }
                m_Car6Charge = value;
                RaisePropertyChanged(() => Car6Charge);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            var index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号辅助逆变器故障];
            var index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号辅助逆变器运行];
            var index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号辅助逆变器断开];
            var index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb1号车辅助逆变器通讯故障];

            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car1Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car1Organ = AssistPowerStatus.AssistOrganFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car1Organ = AssistPowerStatus.AssistOrganRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car1Organ = AssistPowerStatus.AssistOrganDisconnect;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car1Organ = AssistPowerStatus.AssistOrganCommunicationFault;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号辅助逆变器故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号辅助逆变器运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车3号辅助逆变器断开];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb3号车辅助逆变器通讯故障];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car3Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car3Organ = AssistPowerStatus.AssistOrganFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car3Organ = AssistPowerStatus.AssistOrganRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car3Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car3Organ = AssistPowerStatus.AssistOrganCommunicationFault;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号辅助逆变器故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号辅助逆变器运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车4号辅助逆变器断开];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb4号车辅助逆变器通讯故障];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car4Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car4Organ = AssistPowerStatus.AssistOrganFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car4Organ = AssistPowerStatus.AssistOrganRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car4Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car4Organ = AssistPowerStatus.AssistOrganCommunicationFault;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号辅助逆变器故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号辅助逆变器运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号辅助逆变器断开];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb6号车辅助逆变器通讯故障];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car6Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car6Organ = AssistPowerStatus.AssistOrganFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car6Organ = AssistPowerStatus.AssistOrganRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car6Organ = AssistPowerStatus.AssistOrganDisconnect; ;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car6Organ = AssistPowerStatus.AssistOrganCommunicationFault;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号充电机故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号充电机运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车6号充电机断开];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb6号车充电机通讯故障];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car6Charge = AssistPowerStatus.AssistChargeDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car6Charge = AssistPowerStatus.AssistChargeFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car6Charge = AssistPowerStatus.AssistChargeRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car6Charge = AssistPowerStatus.AssistChargeDisconnect; ;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car6Charge = AssistPowerStatus.AssistChargeCommunicationFault;
                }
            }
            index1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号充电机故障];
            index2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号充电机运行];
            index3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb车1号充电机断开];
            index4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.Inb1号车充电机通讯故障];
            if (args.NewValue.ContainsKey(index1) || args.NewValue.ContainsKey(index2) || args.NewValue.ContainsKey(index3) || args.NewValue.ContainsKey(index4))
            {
                Car1Charge = AssistPowerStatus.AssistChargeDisconnect; ;
                if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
                {
                    Car1Charge = AssistPowerStatus.AssistChargeFault; ;
                }
                else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
                {
                    Car1Charge = AssistPowerStatus.AssistChargeRunning; ;
                }
                else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
                {
                    Car1Charge = AssistPowerStatus.AssistChargeDisconnect; ;
                }
                else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
                {
                    Car1Charge = AssistPowerStatus.AssistChargeCommunicationFault;
                }
            }
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }
    }
}