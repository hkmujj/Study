//using MMI.Facility.Interface.Data;
//using MMI.Facility.Interface.Service;
//using Motor.ATP.Domain.Interface;
//using Urban.ATC.Domain.Interface;
//using Urban.ATC.Domain.Interface.ViewStates;
//using Urban.ATC.Siemens.Resource.Internal;

//namespace Urban.ATC.Siemens.Model
//{
//    public class SiemensATC : ATC, IDataListener
//    {
//        public MessageMgr MessageMgr
//        {
//            get { return m_MessageMgr; }
//            set
//            {
//                if (m_MessageMgr != null && m_MessageMgr == value)
//                {
//                    return;
//                }
//                m_MessageMgr = value;
//            }
//        }

//        private ICourseService m_CourseService;
//        private MessageMgr m_MessageMgr;

//        public ICourseService CourseService
//        {
//            get { return m_CourseService; }
//            set
//            {
//                m_CourseService = value;
//                m_CourseService.CourseStateChanged += CourseStateChanged;
//            }
//        }

//        private void CourseStateChanged(object sender, CourseStateChangedArgs e)
//        {
//            CourseState = CourseService.CurrentCourseState;
//        }

//        public void DataActiveUpdate()
//        {
//        }

//        public void CourseChange()
//        {
//        }
//        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            UpdateEmergencyDetails(dataChangedArgs);
//            UpdateObcd(dataChangedArgs);
//            UpdateC3(dataChangedArgs);
//            UpdateC4(dataChangedArgs);
//            UpdateC1(dataChangedArgs);
//            UpdateC2(dataChangedArgs);
//            UpdateM1(dataChangedArgs);
//            UpdateM2(dataChangedArgs);
//            UpdateM3(dataChangedArgs);
//            UpdateM4(dataChangedArgs);
//            UpdateM5(dataChangedArgs);
//            UpdateM6(dataChangedArgs);
//            UpdateM7(dataChangedArgs);
//            UpdateM9(dataChangedArgs);
//            UpdateM10(dataChangedArgs);
//            UpdateBrake(dataChangedArgs);
//            UpdateMMIBack(dataChangedArgs);
//        }
//        private void UpdateMMIBack(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.MMI屏黑屏];
//            if (dataChangedArgs.NewValue.ContainsKey(idex0))
//            {
//                MMIBack = dataChangedArgs.NewValue[idex0];
//            }

//        }
//        private void UpdateBrake(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.请求制动];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.触发紧急制动];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1)
//               )
//            {
//                BrakeDetailsType = BrakeDetailsType.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    BrakeDetailsType = BrakeDetailsType.BrakingRequired;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    BrakeDetailsType = BrakeDetailsType.EnmergencyBrake;
//                }

//            }

//        }
//        private void UpdateM9(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ATP故障];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ATO故障];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.无线通信中断];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2)
//               )
//            {
//                FZoneStatus.DoorDetailModel = DoorDetailModel.None;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    FZoneStatus.DoorDetailModel = DoorDetailModel.ATP;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    FZoneStatus.DoorDetailModel = DoorDetailModel.ATO;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    FZoneStatus.DoorDetailModel = DoorDetailModel.RAD;
//                }
//            }

//        }
//        private void UpdateM10(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.进入车辆段];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.在车辆段内行驶];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.释放速度];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2)
//               )
//            {
//                FZoneStatus.SpecialModel = SpecialModel.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    FZoneStatus.SpecialModel = SpecialModel.DepotEntry;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    FZoneStatus.SpecialModel = SpecialModel.OnDepot;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    FZoneStatus.SpecialModel = SpecialModel.ReleaseSpeed;
//                }
//            }

//        }
//        private void UpdateM1(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.AM模式];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.SM模式];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.RM模式];
//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1))
//            {
//                MZoneSates.DriveModel = DriveModel.None;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.DriveModel = DriveModel.ATO;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.DriveModel = DriveModel.Supervised;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.DriveModel = DriveModel.Restricted;
//                }
//            }

//        }
//        private void UpdateM2(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.IXL等级];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ITC等级];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.CTC等级];
//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1))
//            {
//                MZoneSates.ActualLevels = ActualLevels.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.ActualLevels = ActualLevels.Interlocking;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.ActualLevels = ActualLevels.Intermittent;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.ActualLevels = ActualLevels.Continuous;
//                }
//            }

//        }
//        private void UpdateM3(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ARoffered];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.ARactive];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.DTROisoffered];
//            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.DTROactive];
//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex3))
//            {
//                MZoneSates.ReverseModel = ReverseModel.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.ReverseModel = ReverseModel.AROffered;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.ReverseModel = ReverseModel.ARActive;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.ReverseModel = ReverseModel.DTRO;
//                }
//                if (DataService.ReadService.GetBoolAt(idex3))
//                {
//                    MZoneSates.ReverseModel = ReverseModel.DTROactive;
//                }
//            }

//        }
//        private void UpdateM4(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.超出停车范围];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.在停车范围];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1))
//            {
//                MZoneSates.StopModel = StopModel.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.StopModel = StopModel.Outside;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.StopModel = StopModel.Inside;
//                }
//            }

//        }

//        private void UpdateM5(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.左侧车门打开];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.右侧车门打开];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.两侧车门同时打开];
//            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.先开左门];
//            var idex4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.先开右门];
//            var idex5 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.不再监控车门];
//            var idex6 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.允许释放车门];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex3) || dataChangedArgs.NewValue.ContainsKey(idex4) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex5) || dataChangedArgs.NewValue.ContainsKey(idex6))
//            {
//                MZoneSates.DoorRelease = DoorRelease.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.OpenLeft;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.Openright;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.OpenBoth;
//                }
//                if (DataService.ReadService.GetBoolAt(idex3))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.OpenLeftfirst;
//                }
//                if (DataService.ReadService.GetBoolAt(idex4))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.OpenRightfirst;
//                }
//                if (DataService.ReadService.GetBoolAt(idex5))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.NoDoorSupervision;
//                }
//                if (DataService.ReadService.GetBoolAt(idex6))
//                {
//                    MZoneSates.DoorRelease = DoorRelease.PermissiveReleaseDoor;
//                }
//            }

//        }
//        private void UpdateM6(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.关闭门命令];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.要求离站];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.扣车];
//            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.跳停];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex3))
//            {
//                MZoneSates.DepartureType = DepartureType.None;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.DepartureType = DepartureType.DoorCloseOrder;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.DepartureType = DepartureType.DepartureRequest;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.DepartureType = DepartureType.Hold;
//                }
//                if (DataService.ReadService.GetBoolAt(idex3))
//                {
//                    MZoneSates.DepartureType = DepartureType.Skip;
//                }
//            }

//        }

//        private void UpdateM7(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门手动打开手动关闭];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门自动打开手动关闭];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车门自动打开自动关闭];

//            if (dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex2))
//            {
//                MZoneSates.DoorModel = DoorModel.None;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    MZoneSates.DoorModel = DoorModel.MM;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    MZoneSates.DoorModel = DoorModel.AM;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    MZoneSates.DoorModel = DoorModel.AA;
//                }

//            }

//        }

//        private void UpdateC3(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.列车失去完整性];
//            if (dataChangedArgs.NewValue.ContainsKey(idex0))
//            {
//                CZoneStatus.TrainInteGrityC3 = TrainInteGrity.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    CZoneStatus.TrainInteGrityC3 = TrainInteGrity.TrainIntegrity;
//                }
//            }
//        }

//        private void UpdateC4(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.制动缸压力不正常];
//            if (dataChangedArgs.NewValue.ContainsKey(idex0))
//            {
//                CZoneStatus.TrainInteGrityC4 = TrainInteGrity.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    CZoneStatus.TrainInteGrityC4 = TrainInteGrity.BrakingPressure;
//                }

//            }

//        }
//        private void UpdateC2(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式RM];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式SM_I];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式SM_C];
//            var idex3 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式AM_I];
//            var idex4 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.最高模式AM_C];
//            var idex5 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.预选模式闪烁];
//            if (dataChangedArgs.NewValue.ContainsKey(idex5))
//            {
//                CZoneStatus.ModelFlicker = dataChangedArgs.NewValue[idex5];
//            }
//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1) || dataChangedArgs.NewValue.ContainsKey(idex3) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex4))
//            {
//                CZoneStatus.MaximumMode = MaximumMode.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    CZoneStatus.MaximumMode = MaximumMode.RestrictedMode;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    CZoneStatus.MaximumMode = MaximumMode.SMIntermittent;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    CZoneStatus.MaximumMode = MaximumMode.SMContinuous;
//                }
//                if (DataService.ReadService.GetBoolAt(idex3))
//                {
//                    CZoneStatus.MaximumMode = MaximumMode.AMIntermittent;
//                }
//                if (DataService.ReadService.GetBoolAt(idex4))
//                {
//                    CZoneStatus.MaximumMode = MaximumMode.AMContinuous;
//                }
//            }

//        }
//        private void UpdateC1(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆正在牵引];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆为惰行];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆正在制动];

//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1))
//            {
//                CZoneStatus.DriveingBrakeType = DriveingBrakeType.Initial;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    CZoneStatus.DriveingBrakeType = DriveingBrakeType.Motoring;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    CZoneStatus.DriveingBrakeType = DriveingBrakeType.Coasting;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    CZoneStatus.DriveingBrakeType = DriveingBrakeType.Braking;

//                }
//            }

//        }
//        private void UpdateObcd(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idex0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU激活绿色];
//            var idex1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU待机白色];
//            var idex2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车首OBCU关闭红色];

//            if (dataChangedArgs.NewValue.ContainsKey(idex2) || dataChangedArgs.NewValue.ContainsKey(idex0) ||
//                 dataChangedArgs.NewValue.ContainsKey(idex1))
//            {
//                CZoneStatus.ObcuModel = OBCUModel.None;
//                if (DataService.ReadService.GetBoolAt(idex0))
//                {
//                    CZoneStatus.ObcuModel = OBCUModel.Level3;
//                }
//                if (DataService.ReadService.GetBoolAt(idex1))
//                {
//                    CZoneStatus.ObcuModel = OBCUModel.Level1;
//                }
//                if (DataService.ReadService.GetBoolAt(idex2))
//                {
//                    CZoneStatus.ObcuModel = OBCUModel.Level2;

//                }
//            }

//        }

//        private void UpdateEmergencyDetails(CommunicationDataChangedArgs<bool> dataChangedArgs)
//        {
//            var idx0 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆空转打滑];
//            var idx1 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.车辆施加EB];
//            var idx2 = IndexConfigure.Instance.IndexFacade.InBoolDictionary[InBoolKeys.PSD未关闭];

//            if (dataChangedArgs.NewValue.ContainsKey(idx0) || dataChangedArgs.NewValue.ContainsKey(idx1) ||
//                dataChangedArgs.NewValue.ContainsKey(idx2))
//            {
//                MZoneSates.EmergencyModel = EmergencyModel.None;
//                if (DataService.ReadService.GetBoolAt(idx0))
//                {
//                    MZoneSates.EmergencyModel = EmergencyModel.Slip;
//                }
//                if (DataService.ReadService.GetBoolAt(idx1))
//                {
//                    MZoneSates.EmergencyModel = EmergencyModel.EmergencyBrake;
//                }
//                if (DataService.ReadService.GetBoolAt(idx2))
//                {
//                    MZoneSates.EmergencyModel = EmergencyModel.PSDNotCLose;
//                }
//            }

//        }

//        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
//        {
//            UpdateA2Recgion(dataChangedArgs);
//            // ((SpeedModel)(Speed.TargetSpeed)).Value = GetIfContains(dataChangedArgs, InFloatKeys.到目标点的距离, (float)Speed.TargetSpeed.Value);
//            UpdateMessage(dataChangedArgs);
//            UpdateTargetBar();

//            UpdateSpeed(dataChangedArgs);

//            UpdateTReion(dataChangedArgs);
//        }

//        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
//        {
//        }

//        private void UpdateMessage(CommunicationDataChangedArgs<float> dataChangedArgs)
//        {
//            var idex = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.信息];
//            if (!dataChangedArgs.NewValue.ContainsKey(idex))
//            {
//                return;
//            }
//            var tmp = (int)dataChangedArgs.NewValue[idex];
//            if (m_MessageMgr.CurrentMsgList.Contains(tmp))
//            {
//                return;
//            }
//            m_MessageMgr.AddMsg(tmp);
//            UpdateDisplayMsg();
//        }

//        private void UpdateDisplayMsg()
//        {
//            var tmp = m_MessageMgr.GetCurrentFirstMessge();
//            if (tmp == null)
//            {
//                return;
//            }
//            FZoneStatus.InfoLevl = tmp.Level;
//            FZoneStatus.MessgeInfo = tmp;
//        }
//        private void UpdateA2Recgion(CommunicationDataChangedArgs<float> dataChangedArgs)
//        {
//            TargetDistance = GetIfContains(dataChangedArgs, InFloatKeys.到目标点的距离, (float)TargetDistance);
//            TargetSpeed = GetIfContains(dataChangedArgs, InFloatKeys.目标点速度, (float)TargetSpeed);
//        }

//        private void UpdateTReion(CommunicationDataChangedArgs<float> dataChangedArgs)
//        {
//            MZoneSates.NumberT1 = GetIfContains(dataChangedArgs, InFloatKeys.服务号, MZoneSates.NumberT1);
//            MZoneSates.NumberT2 = GetIfContains(dataChangedArgs, InFloatKeys.目的地号, MZoneSates.NumberT2);
//            MZoneSates.NumberT3 = GetIfContains(dataChangedArgs, InFloatKeys.工号, MZoneSates.NumberT3);
//        }
//        private void UpdateSpeed(CommunicationDataChangedArgs<float> dataChangedArgs)
//        {
//            Speed.CurrentSpeed.Value = GetIfContains(dataChangedArgs, InFloatKeys.列车当前速度, Speed.CurrentSpeed.Value);
//            Speed.TargetSpeed.Value = GetIfContains(dataChangedArgs, InFloatKeys.ATP推荐速度, Speed.TargetSpeed.Value);
//            Speed.EmergencyBrakeInterventionSpeed.Value = GetIfContains(dataChangedArgs, InFloatKeys.紧急制动触发速度, Speed.EmergencyBrakeInterventionSpeed.Value);
//        }

//        private void UpdateTargetBar()
//        {
//            if (Speed.TargetSpeed.Value >= 60)
//            {
//                if (TargetDistance > 300)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else if (TargetDistance <= 150)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//            }
//            else if (Speed.TargetSpeed.Value >= 25)
//            {
//                if (TargetDistance > 300)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else if (TargetDistance <= 150)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else
//                {
//                    TargetDistanceBarColor = TargetBarType.Yellow;
//                }
//            }
//            else if (Speed.TargetSpeed.Value > 0)
//            {
//                if (TargetDistance > 300)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else if (TargetDistance <= 150)
//                {
//                    TargetDistanceBarColor = TargetBarType.Yellow;
//                }
//                else
//                {
//                    TargetDistanceBarColor = TargetBarType.Yellow;
//                }
//            }
//            else
//            {
//                if (TargetDistance > 300)
//                {
//                    TargetDistanceBarColor = TargetBarType.LightGreen;
//                }
//                else if (TargetDistance <= 150)
//                {
//                    TargetDistanceBarColor = TargetBarType.Yellow;
//                }
//                else
//                {
//                    TargetDistanceBarColor = TargetBarType.Red;
//                }
//            }
//        }

//        private float GetIfContains(CommunicationDataChangedArgs<float> dataChangedArgs, string key, float defaultValue)
//        {
//            if (dataChangedArgs.NewValue.ContainsKey(IndexConfigure.Instance.IndexFacade.InFloatDictionary[key]))
//            {
//                return dataChangedArgs.NewValue[IndexConfigure.Instance.IndexFacade.InFloatDictionary[key]];
//            }
//            return defaultValue;
//        }
//    }
//}