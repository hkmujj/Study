using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.Model.Domain.Other;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdataDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarCommunicationStatusUpdataDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarCommunicationStatusUpdataDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        private OtherModel Model
        {
            get { return ViewModel.OtherViewModel.Model; }
        }
        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            Model.CarCommunicationStatus.CommunicationAtc = GetCommunicationAtcState();
            Model.CarCommunicationStatus.CommunicationBCU1 = GetCommunicationBCU1State();
            Model.CarCommunicationStatus.CommunicationBCU2 = GetCommunicationBCU2State();
            Model.CarCommunicationStatus.CommunicationBCU3 = GetCommunicationBCU3State();
            Model.CarCommunicationStatus.CommunicationBCU4 = GetCommunicationBCU4State();
            Model.CarCommunicationStatus.CommunicationCcu1 = GetCommunicationCcu1State();
            Model.CarCommunicationStatus.CommunicationCcu2 = GetCommunicationCcu2State();
            Model.CarCommunicationStatus.CommunicationCcud1 = GetCommunicationCcud1State();
            Model.CarCommunicationStatus.CommunicationCcud2 = GetCommunicationCcud2State();
            Model.CarCommunicationStatus.CommunicationDcua1 = GetCommunicationDcua1State();
            Model.CarCommunicationStatus.CommunicationDcua2 = GetCommunicationDcua2State();
            Model.CarCommunicationStatus.CommunicationDcua3 = GetCommunicationDcua3State();
            Model.CarCommunicationStatus.CommunicationDcua4 = GetCommunicationDcua4State();
            Model.CarCommunicationStatus.CommunicationDcum1 = GetCommunicationDcum1State();
            Model.CarCommunicationStatus.CommunicationDcum2 = GetCommunicationDcum2State();
            Model.CarCommunicationStatus.CommunicationDcum3 = GetCommunicationDcum3State();
            Model.CarCommunicationStatus.CommunicationDcum4 = GetCommunicationDcum4State();
            Model.CarCommunicationStatus.CommunicationEcr1 = GetCommunicationEcr1State();
            Model.CarCommunicationStatus.CommunicationEcr2 = GetCommunicationEcr2State();
            Model.CarCommunicationStatus.CommunicationEdcu1 = GetCommunicationEdcu1State();
            Model.CarCommunicationStatus.CommunicationEdcu2 = GetCommunicationEdcu2State();
            Model.CarCommunicationStatus.CommunicationEdcu3 = GetCommunicationEdcu3State();
            Model.CarCommunicationStatus.CommunicationEdcu4 = GetCommunicationEdcu4State();
            Model.CarCommunicationStatus.CommunicationEdcu5 = GetCommunicationEdcu5State();
            Model.CarCommunicationStatus.CommunicationEdcu6 = GetCommunicationEdcu6State();
            Model.CarCommunicationStatus.CommunicationEdcu7 = GetCommunicationEdcu7State();
            Model.CarCommunicationStatus.CommunicationEdcu8 = GetCommunicationEdcu8State();
            Model.CarCommunicationStatus.CommunicationEdcu9 = GetCommunicationEdcu9State();
            Model.CarCommunicationStatus.CommunicationEdcu10 = GetCommunicationEdcu10State();
            Model.CarCommunicationStatus.CommunicationEdcu11 = GetCommunicationEdcu11State();
            Model.CarCommunicationStatus.CommunicationEdcu12 = GetCommunicationEdcu12State();
            Model.CarCommunicationStatus.CommunicationErm1 = GetCommunicationErm1State();
            Model.CarCommunicationStatus.CommunicationErm2 = GetCommunicationErm2State();
            Model.CarCommunicationStatus.CommunicationFas1 = GetCommunicationFas1State();
            Model.CarCommunicationStatus.CommunicationFas2 = GetCommunicationFas2State();
            Model.CarCommunicationStatus.CommunicationHcac1 = GetCommunicationHcac1State();
            Model.CarCommunicationStatus.CommunicationHcac2 = GetCommunicationHcac2State();
            Model.CarCommunicationStatus.CommunicationHcac3 = GetCommunicationHcac3State();
            Model.CarCommunicationStatus.CommunicationHcac4 = GetCommunicationHcac4State();
            Model.CarCommunicationStatus.CommunicationHcac5 = GetCommunicationHcac5State();
            Model.CarCommunicationStatus.CommunicationHcac6 = GetCommunicationHcac6State();
            Model.CarCommunicationStatus.CommunicationHmi1 = GetCommunicationHmi1State();
            Model.CarCommunicationStatus.CommunicationHmi2 = GetCommunicationHmi2State();
            Model.CarCommunicationStatus.CommunicationPIS1 = GetCommunicationPIS1State();
            Model.CarCommunicationStatus.CommunicationPIS2 = GetCommunicationPIS2State();
            Model.CarCommunicationStatus.CommunicationPds1 = GetCommunicationPds1State();
            Model.CarCommunicationStatus.CommunicationPds2 = GetCommunicationPds2State();
            Model.CarCommunicationStatus.CommunicationRiom1 = GetCommunicationRiom1State();
            Model.CarCommunicationStatus.CommunicationRiom2 = GetCommunicationRiom2State();
            Model.CarCommunicationStatus.CommunicationRiom3 = GetCommunicationRiom3State();
            Model.CarCommunicationStatus.CommunicationRiom4 = GetCommunicationRiom4State();
            Model.CarCommunicationStatus.CommunicationRiom5 = GetCommunicationRiom5State();
            Model.CarCommunicationStatus.CommunicationRiom6 = GetCommunicationRiom6State();
            Model.CarCommunicationStatus.CommunicationRiom7 = GetCommunicationRiom7State();
            Model.CarCommunicationStatus.CommunicationRiom8 = GetCommunicationRiom8State();
            Model.CarCommunicationStatus.CommunicationTds = GetCommunicationTdsState();


        }
        private CarCommunicationStatus GetCommunicationAtcState()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ATC绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ATC红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ATC黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationBCU1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationBCU2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationBCU3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationBCU4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态BCU4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationCcu1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationCcu2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCU2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationCcud1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationCcud2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态CCUD2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcua1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcua2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcua3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcua4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUA4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcum1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcum2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcum3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationDcum4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态DCUM4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEcr1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEcr2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ECR2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu5State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU5绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU5红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU5黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu6State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU6绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU6红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU6黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu7State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU7绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU7红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU7黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu8State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU8绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU8红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU8黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu9State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU9绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU9红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU9黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu10State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU10绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU10红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU10黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu11State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU11绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU11红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU11黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationEdcu12State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU12绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU12红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态EDCU12黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationErm1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationErm2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态ERM2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationFas1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationFas2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态FAS2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac5State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC5绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC5红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC5黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHcac6State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC6绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC6红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HCAC6黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHmi1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationHmi2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态HMI2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationPIS1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationPIS2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PIS2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationPds1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationPds2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态PDS2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom1State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM1绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM1红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM1黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom2State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM2绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM2红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM2黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom3State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM3绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM3红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM3黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom4State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM4绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM4红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM4黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom5State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM5绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM5红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM5黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom6State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM6绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM6红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM6黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom7State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM7绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM7红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM7黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationRiom8State()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM8绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM8红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态RIOM8黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
        private CarCommunicationStatus GetCommunicationTdsState()
        {
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态TDS绿))
            {
                return CarCommunicationStatus.Normal;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态TDS红))
            {
                return CarCommunicationStatus.Fault;
            }
            if (DataService.ReadService.GetInBoolOf(InbKeys.Inb通讯状态TDS黄))
            {
                return CarCommunicationStatus.SlaveDeviceNormal;
            }
            return CarCommunicationStatus.Unknow;
        }
    }
}
