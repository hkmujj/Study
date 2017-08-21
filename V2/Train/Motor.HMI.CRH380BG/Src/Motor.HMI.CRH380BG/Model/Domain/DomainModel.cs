using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Brake;
using Motor.HMI.CRH380BG.Model.Domain.Door;
using Motor.HMI.CRH380BG.Model.Domain.Emergency;
using Motor.HMI.CRH380BG.Model.Domain.MainData;
using Motor.HMI.CRH380BG.Model.Domain.Maintain;
using Motor.HMI.CRH380BG.Model.Domain.Switch;
using Motor.HMI.CRH380BG.Model.Domain.System;
using Motor.HMI.CRH380BG.Model.Domain.Title;
using Motor.HMI.CRH380BG.Model.Domain.Fault;


namespace Motor.HMI.CRH380BG.Model.Domain
{
    [Export]
    public class DomainModel : NotificationObject
    {
        [ImportingConstructor]

        public DomainModel(MainDataModel mainData,DoorModel door,SwitchModel switchModel,TitleModel titleModel, BrakeModel brakeModel,EmergencyModel emergencyModel,MaintainModel maintainModel,SystemModel systemModel, FaultModel faultModel)
        {
            MainData = mainData;
            Door = door;
            Switch = switchModel;
            TitleModel = titleModel;
            BrakeModel = brakeModel;
            EmergencyModel = emergencyModel;
            MaintainModel = maintainModel;
            SystemModel = systemModel;
            FaultModel = faultModel;
        }

        public MainDataModel MainData { private set; get; }

        public DoorModel Door { private set; get; }

        public SwitchModel Switch { private set; get; }

        public TitleModel TitleModel { private set; get; }

        public BrakeModel BrakeModel { get; private set; }

        public EmergencyModel EmergencyModel { get; private set; }

        public MaintainModel MaintainModel { get; private set; }

        public SystemModel SystemModel { get; private set; }

        public FaultModel FaultModel { get; private set; }
    }
}
