
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface;
using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;
using Urban.Domain.TrainState.Model.Common;
using Urban.NanJing.NingTian.DDU.Flash.Adapter.Flash;
using Urban.NanJing.NingTian.DDU.Flash.Interface;
using Urban.NanJing.NingTian.DDU.Flash.Model;
using Urban.NanJing.NingTian.DDU.Flash.Model.Train;
using Urban.NanJing.NingTian.DDU.Flash.Model.Train.Car;
using Urban.NanJing.NingTian.DDU.Index;
using Urban.NanJing.NingTian.DDU.Resource.Ch;
using Urban.NanJing.NingTian.DDU.Resource.Internal;

namespace Urban.NanJing.NingTian.DDU.Flash.Adapter
{
    public class FlashDataAdapter : IFlashDataProvider, IDataListener, IDisposable
    {
        private readonly Dictionary<string, Dictionary<string, string>> m_CommandDataDictionary;
        private readonly Timer m_UpdateFlashTimer;

        private readonly UpdateParam m_UpdateParam;

        private NingTianConfig m_NingTianConfig;

        private List<string> m_BypassNames = new List<string>()
        {
            "A{0}车总风缸欠压旁路开关激活端操作",
            "A{0}车总风缸欠压旁路开关激活端未操作",
            "A{0}车总风缸欠压旁路开关未激活端操作",
            "A{0}车紧急制动旁路按钮激活端操作",
            "A{0}车紧急制动旁路按钮激活端未操作",
            "A{0}车紧急制动旁路按钮未激活端操作",
            "A{0}车紧急制动旁路开关激活端操作",
            "A{0}车紧急制动旁路开关激活端未操作",
            "A{0}车紧急制动旁路开关未激活端操作",
            "A{0}车警惕按钮旁路开关激活端操作",
            "A{0}车警惕按钮旁路开关激活端未操作",
            "A{0}车警惕按钮旁路开关未激活端操作",
            "A{0}车所有制动缓解旁路开关激活端操作",
            "A{0}车所有制动缓解旁路开关激活端未操作",
            "A{0}车所有制动缓解旁路开关未激活端操作",
            "A{0}车所有停放制动缓解旁路激活端操作",
            "A{0}车所有停放制动缓解旁路激活端未操作",
            "A{0}车所有停放制动缓解旁路未激活端操作",
            "A{0}车联锁旁路激活端操作",
            "A{0}车联锁旁路激活端未操作",
            "A{0}车联锁旁路未激活端操作",
            "A{0}车钥匙旁路开关激活端操作",
            "A{0}车钥匙旁路开关激活端未操作",
            "A{0}车钥匙旁路开关未激活端操作",
            "A{0}车左侧门使能旁路激活端操作",
            "A{0}车左侧门使能旁路激活端未操作",
            "A{0}车左侧门使能旁路未激活端操作",
            "A{0}车右侧门使能旁路激活端操作",
            "A{0}车右侧门使能旁路激活端未操作",
            "A{0}车右侧门使能旁路未激活端操作",
            "A{0}车紧急疏散门旁路开关激活端操作",
            "A{0}车紧急疏散门旁路开关激活端未操作",
            "A{0}车紧急疏散门旁路开关未激活端操作",
            "A{0}车司机室左侧门旁路开关激活端操作",
            "A{0}车司机室左侧门旁路开关激活端未操作",
            "A{0}车司机室左侧门旁路开关未激活端操作",
            "A{0}车司机室右侧门旁路开关激活端操作",
            "A{0}车司机室右侧门旁路开关激活端未操作",
            "A{0}车司机室右侧门旁路开关未激活端操作",
            "A{0}车蓄电池隔离开关激活端操作",
            "A{0}车蓄电池隔离开关激活端未操作",
            "A{0}车蓄电池隔离开关未激活端操作",
            "A{0}车半车运行开关激活端操作",
            "A{0}车半车运行开关激活端未操作",
            "A{0}车半车运行开关未激活端操作",
            "A{0}车ATC故障隔离开关激活端操作",
            "A{0}车ATC故障隔离开关激活端未操作",
            "A{0}车ATC故障隔离开关未激活端操作",
        };

        public IReadOnlyDictionary<string, Dictionary<string, string>> CommandDataDictionary { get; private set; }

        public ITrain Domain { private set; get; }

        public IFlashInteractive FlashInteractive { private set; get; }

        private CommunicationIndexFacade IndexFacade
        {
            get { return IndexConfigure.Instance.CommunicationIndexFacade; }
        }

        private List<string> m_Param1Names = new List<string>()
        {
            InFloatKeys.A1车空簧压力1kPa,
            InFloatKeys.A1车空簧压力2kPa,
            InFloatKeys.A1车空簧压力3kPa,
            InFloatKeys.A1车空簧压力4kPa,
            InFloatKeys.B1车空簧压力1kPa,
            InFloatKeys.B1车空簧压力2kPa,
            InFloatKeys.B1车空簧压力3kPa,
            InFloatKeys.B1车空簧压力4kPa,
            InFloatKeys.B2车空簧压力1kPa,
            InFloatKeys.B2车空簧压力2kPa,
            InFloatKeys.B2车空簧压力3kPa,
            InFloatKeys.B2车空簧压力4kPa,
            InFloatKeys.A2车空簧压力1kPa,
            InFloatKeys.A2车空簧压力2kPa,
            InFloatKeys.A2车空簧压力3kPa,
            InFloatKeys.A2车空簧压力4kPa,
            InFloatKeys.A1车中间电压1500V,
            InFloatKeys.B1车中间电压11500V,
            InFloatKeys.B1车中间电压21500V,
            InFloatKeys.B2车中间电压11500V,
            InFloatKeys.B2车中间电压21500V,
            InFloatKeys.A2车中间电压1500V,
            InFloatKeys.A1车中间电流,
            InFloatKeys.B1车中间电流1,
            InFloatKeys.B1车中间电流2,
            InFloatKeys.B2车中间电流1,
            InFloatKeys.B2车中间电流2,
            InFloatKeys.A2车中间电流,
            InFloatKeys.A1车辅助输出相电压230V,
            InFloatKeys.A2车辅助输出相电压230V,
            InFloatKeys.A1车110V输出110V,
            InFloatKeys.A2车110V输出110V,
            InFloatKeys.A1车充电电流2A,
            InFloatKeys.A2车充电电流2A,
            InFloatKeys.A1车总风缸压力,
            InFloatKeys.A2车总风缸压力,
            InFloatKeys.A1车室内温度,
            InFloatKeys.B1车室内温度,
            InFloatKeys.B2车室内温度,
            InFloatKeys.A2车室内温度,
            InFloatKeys.A1车室外温度,
            InFloatKeys.B1车室外温度,
            InFloatKeys.B2车室外温度,
            InFloatKeys.A2车室外温度,
            InFloatKeys.A1车目标温度,
            InFloatKeys.B1车目标温度,
            InFloatKeys.B2车目标温度,
            InFloatKeys.A2车目标温度,
            InFloatKeys.A1功率消耗,
            InFloatKeys.B1功率消耗,
            InFloatKeys.B2功率消耗,
            InFloatKeys.A2功率消耗,

        };

        public FlashDataAdapter(SubsystemInitParam initParam, ITrain domain, IFlashInteractive flashInteractive, CommunicationIndexFacade indexFacade, ICommunicationDataService dataService)
        {
            m_UpdateParam = new UpdateParam
            {
                IndexFacade = indexFacade,
                DataService = dataService
            };
            Domain = domain;
            FlashInteractive = flashInteractive;
            FlashInteractive.OnReceiveFromFlash += FlashInteractiveOnOnReceiveFromFlash;

            m_NingTianConfig = initParam.DataPackage.ServiceManager.GetService<NingTianConfig>();

            m_UpdateFlashTimer = new Timer(OnUpdateFlash, domain, 0, 200);
            m_CommandDataDictionary = new Dictionary<string, Dictionary<string, string>>()
            {
                {
                    FlashFuncs.ATC,
                    Enum.GetNames(typeof (ATCParam)).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Door,
                    Enumerable.Range(1, 76).Cast<DoorParam>().ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Communication,
                    Enumerable.Range(1, 51).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Hvac,
                    Enumerable.Range(1, 36).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Hvac1,
                    Enumerable.Range(1, 64).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Bypass,
                    Enumerable.Range(1, 32).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Param,
                    Enumerable.Range(1, 28).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
                {
                    FlashFuncs.Param1,
                    Enumerable.Range(1, 34).ToDictionary(kvp => kvp.ToString(), kvp => string.Empty)
                },
            };

            CommandDataDictionary = m_CommandDataDictionary.AsReadOnlyDictionary();
        }

        private void FlashInteractiveOnOnReceiveFromFlash(string cmd, string value)
        {
            if (cmd == FlashFuncs.PISSure)
            {
                ParserPISSure(value);
            }
        }

        private void ParserPISSure(string value)
        {
            var datas = value.Split('#');
            var model = (PISModel) Enum.Parse(typeof (PISModel), datas[0]);
            var region = int.Parse(datas[1]);
            switch (model)
            {
                case PISModel.Manaul:
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.手动模式], true);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.半自动模式], false);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.自动模式], false);

                    m_UpdateParam.DataService.WriteService.ChangeFloat(
                        m_UpdateParam.IndexFacade.OutFloatDictionary[OutFloatKeys.起始站], ConvertToStationIndex(datas[2]));

                    m_UpdateParam.DataService.WriteService.ChangeFloat(
                        m_UpdateParam.IndexFacade.OutFloatDictionary[OutFloatKeys.终点站], ConvertToStationIndex(datas[3]));
                    m_UpdateParam.DataService.WriteService.ChangeFloat(
                        m_UpdateParam.IndexFacade.OutFloatDictionary[OutFloatKeys.下一站], ConvertToStationIndex(datas[4]));
                    break;
                case PISModel.SemiAutomatic:
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.手动模式], false);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.半自动模式], true);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.自动模式], false);

                    m_UpdateParam.DataService.WriteService.ChangeFloat(
                        m_UpdateParam.IndexFacade.OutFloatDictionary[OutFloatKeys.起始站], ConvertToStationIndex(datas[2]));

                    m_UpdateParam.DataService.WriteService.ChangeFloat(
                        m_UpdateParam.IndexFacade.OutFloatDictionary[OutFloatKeys.终点站], ConvertToStationIndex(datas[3]));
                    break;
                case PISModel.Auto:
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.手动模式], false);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.半自动模式], false);
                    m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.自动模式], true);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (region == 1)
            {
                m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.上行], true);
                m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.下行], false);
            }
            else
            {
                m_UpdateParam.DataService.WriteService.ChangeBool(
                       m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.上行], false);
                m_UpdateParam.DataService.WriteService.ChangeBool(
                        m_UpdateParam.IndexFacade.OutBoolDictionary[OutBoolKeys.下行], true);
            }
        }

        private float ConvertToStationIndex(string station)
        {
            var kvp = m_NingTianConfig.StationDictionary.FirstOrDefault(f => f.Value == station);
            return kvp.Key;
        }

        private string GetShowingStationName(int index)
        {
            if (m_NingTianConfig.StationDictionary.ContainsKey(index))
            {
                return m_NingTianConfig.StationDictionary[index];
            }
            return "----";
        }

        private void OnUpdateFlash(object state)
        {
            UpdateMainView();

            // TODO  更新 flash 界面信息
            UpdateAtcFuncParam();

            UpdateMenFuncParam();

            SendDatas();

        }

        private void UpdateMenFuncParam()
        {
            UpdateDoorStates();
            var a1Car = Domain.Cars.First(f => (CarType) f.Type == CarType.A1);
            var a2Car = Domain.Cars.First(f => (CarType) f.Type == CarType.A2);
            //var b1Car = Domain.Cars.First(f => (CarType)f.Type == CarType.B1);
            //var b2Car = Domain.Cars.First(f => (CarType)f.Type == CarType.B2);

            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A1AC.ToString()] =
                ConvertToDoorParam(a1Car.Power.PowerItems.First(f => f.Type == CarPowerType.AC).State);
            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A1DC.ToString()] =
                ConvertToDoorParam(a1Car.Power.PowerItems.First(f => f.Type == CarPowerType.DC).State);
            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A1Driver.ToString()] = "1";
            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A2Driver.ToString()] = "0";
            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A2AC.ToString()] =
                ConvertToDoorParam(a2Car.Power.PowerItems.First(f => f.Type == CarPowerType.AC).State);
            m_CommandDataDictionary[FlashFuncs.Door][DoorParam.A2DC.ToString()] =
                ConvertToDoorParam(a2Car.Power.PowerItems.First(f => f.Type == CarPowerType.DC).State);



            for (int i = (int) DoorParam.A1PassengerState; i < (int) DoorParam.B1PassengerState; i++)
            {
                m_CommandDataDictionary[FlashFuncs.Door][((DoorParam) i).ToString()] =
                    ((int) Domain.Cars[i - (int) DoorParam.A1PassengerState].PassengerCollection[0].PassengerState)
                        .ToString();
            }
        }

        private string ConvertToDoorParam(CarPowerState state)
        {
            switch (state)
            {
                case CarPowerState.Normal:
                    return "1";
                case CarPowerState.Fault:
                    return "3";
                case CarPowerState.Unkown:
                    return "2";
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }

        private void UpdateDoorStates()
        {
            foreach (var car in Domain.Cars)
            {
                foreach (var dr in car.Doors)
                {
                    var paraIndex = ((DoorParam) (car.CarNumber*car.Doors.Count + (int) dr.Identity)).ToString();
                    var stateValue = "1";
                    switch (dr.DoorState)
                    {
                        case DoorState.Unkown:
                            stateValue = "7";
                            break;
                        case DoorState.Open:
                            stateValue = "3";
                            break;
                        case DoorState.Close:
                            stateValue = "1";
                            break;
                        case DoorState.Fault:
                            stateValue = "4";
                            break;
                        case DoorState.Lock:
                            break;
                        case DoorState.Unlock:
                            stateValue = "5";
                            break;
                        case DoorState.EmergencyUnlock:
                            break;
                        case DoorState.Obstacle:
                            stateValue = "6";
                            break;
                        case DoorState.CutOut:
                            stateValue = "2";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    m_CommandDataDictionary[FlashFuncs.Door][paraIndex] = stateValue;
                }
            }
        }

        private void UpdateMainView()
        {
            FlashInteractive.SetVisible(Domain.Visible);
        }

        // ReSharper disable once UnusedMember.Local
        private void SendDatas()
        {
            foreach (var kvp in CommandDataDictionary.Where(w => w.Key != FlashFuncs.Bypass))
            {
                FlashInteractive.SetValue(kvp.Key, kvp.Value.Values.ToList());
            }
        }

        private void UpdateAtcFuncParam()
        {
            var datas = m_CommandDataDictionary[FlashFuncs.ATC];
            datas[ATCParam.ATC.ToString()] = ConvertToParam((ATPState) Domain.ATP.ATPState);
            datas[ATCParam.Model.ToString()] = ConvertToParam((ATPModel) Domain.ATP.WorkModel);
            datas[ATCParam.Speed.ToString()] = Domain.Speed.CurrentSpeed.Value.ToString("0.0");
            //datas[ATCParam.HandShank.ToString()] = "1";
            datas[ATCParam.EB.ToString()] = ConvertToParam(Domain.BrakingCollection[0].BrakingState);
            datas[ATCParam.FaultLevel.ToString()] = "1";
            datas[ATCParam.FaultCount.ToString()] = "1";
            datas[ATCParam.LimitedSpeedBackGroud.ToString()] = "1";
            datas[ATCParam.Info.ToString()] = float.IsNaN(Domain.Speed.LimitedSpeed.Value)
                ? ViewResource.NoLimitedSpeed
                : string.Format("<{0:0.0}{1}", Domain.Speed.LimitedSpeed.Value, ViewResource.KmPHour);
            datas[ATCParam.Temperature.ToString()] = "19";
            datas[ATCParam.ContactVoltage.ToString()] = Domain.Power.ContactLinePower.ContactLineVoltage.ToString("0");
            datas[ATCParam.NextStation.ToString()] = GetShowingStationName((int) m_UpdateParam.GetInFloatAt(InFloatKeys.下一站));
            datas[ATCParam.EndStation.ToString()] = GetShowingStationName((int)m_UpdateParam.GetInFloatAt(InFloatKeys.目的地站));
        }

        private string ConvertToParam(BrakingState state)
        {
            if (state == BrakingState.Relase)
            {
                return "2";
            }
            if (state == BrakingState.Apply)
            {
                return "3";
            }
            //if (state == BrakingState.Unkown)
            {
                return "1";
            }
        }

        private string ConvertToParam(ATPState state)
        {
            return ((int) state).ToString();
        }

        private string ConvertToParam(ATPModel model)
        {
            return ((int) model).ToString();
        }

        public void VoluntaryResponse(ICommunicationDataService dataService)
        {
            if (GetInBoolValue(dataService, InBoolKeys.激活紧急手柄状态))
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "1";
            }
            else if (GetInBoolValue(dataService, InBoolKeys.紧急手柄未激活状态))
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "2";
            }
            else
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "3";
            }
        }

        private void UpdateCommunicationView()
        {
            var com = m_CommandDataDictionary[FlashFuncs.Communication];

            com[1.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车REP状态正常,
                InBoolKeys.A1车REP状态中断,
                InBoolKeys.A1车REP状态待机,
            });
            com[2.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车VCMe状态正常,
                InBoolKeys.A1车VCMe状态中断,
                InBoolKeys.A1车VCMe状态待机,
            });
            com[3.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车AXMe状态正常,
                InBoolKeys.A1车AXMe状态中断,
                InBoolKeys.A1车AXMe状态待机,
            });

            com[4.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车ERMe状态正常,
                InBoolKeys.A1车ERMe状态中断,
                InBoolKeys.A1车ERMe状态待机,
            });

            com[5.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车PIS状态正常,
                InBoolKeys.A1车PIS状态中断,
                InBoolKeys.A1车PIS状态待机,
            });

            com[6.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DDU状态正常,
                InBoolKeys.A1车DDU状态中断,
                InBoolKeys.A1车DDU状态待机,
            });


            com[7.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车HVAC状态正常,
                InBoolKeys.A1车HVAC状态中断,
                InBoolKeys.A1车HVAC状态待机,
            });


            com[8.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DXMe1状态正常,
                InBoolKeys.A1车DXMe1状态中断,
                InBoolKeys.A1车DXMe1状态待机,
            });

            com[9.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DCU状态正常,
                InBoolKeys.A1车DCU状态中断,
                InBoolKeys.A1车DCU状态待机,
            });

            com[10.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DXMe2状态正常,
                InBoolKeys.A1车DXMe2状态中断,
                InBoolKeys.A1车DXMe2状态待机,
            });

            com[11.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车SIV状态正常,
                InBoolKeys.A1车SIV状态中断,
                InBoolKeys.A1车SIV状态待机,
            });
            com[12.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DIMe1状态正常,
                InBoolKeys.A1车DIMe1状态中断,
                InBoolKeys.A1车DIMe1状态待机,
            });
            com[13.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车BCU状态正常,
                InBoolKeys.A1车BCU状态中断,
                InBoolKeys.A1车BCU状态待机,
            });

            com[14.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车DIMe2状态正常,
                InBoolKeys.A1车DIMe2状态中断,
                InBoolKeys.A1车DIMe2状态待机,
            });

            com[15.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车ATC状态正常,
                InBoolKeys.A1车ATC状态中断,
                InBoolKeys.A1车ATC状态待机,
            });

            com[16.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车EDCU1状态正常,
                InBoolKeys.A1车EDCU1状态中断,
                InBoolKeys.A1车EDCU1状态待机,
            });

            com[17.ToString()] = GetState(new[]
            {
                InBoolKeys.A1车EDCU2状态正常,
                InBoolKeys.A1车EDCU2状态中断,
                InBoolKeys.A1车EDCU2状态待机,
            });

            com[18.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车REP状态正常,
                InBoolKeys.B1车REP状态中断,
                InBoolKeys.B1车REP状态待机,
            });

            com[19.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车DXMe状态正常,
                InBoolKeys.B1车DXMe状态中断,
                InBoolKeys.B1车DXMe状态待机,
            });

            com[20.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车DIMe状态正常,
                InBoolKeys.B1车DIMe状态中断,
                InBoolKeys.B1车DIMe状态待机,
            });
            com[21.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车HVAC状态正常,
                InBoolKeys.B1车HVAC状态中断,
                InBoolKeys.B1车HVAC状态待机,
            });

            com[22.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车DCU1状态正常,
                InBoolKeys.B1车DCU1状态中断,
                InBoolKeys.B1车DCU1状态待机,
            });

            com[23.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车DCU2状态正常,
                InBoolKeys.B1车DCU2状态中断,
                InBoolKeys.B1车DCU2状态待机,
            });

            com[24.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车BCU状态正常,
                InBoolKeys.B1车BCU状态中断,
                InBoolKeys.B1车BCU状态待机,
            });
            com[25.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车EDCU1状态正常,
                InBoolKeys.B1车EDCU1状态中断,
                InBoolKeys.B1车EDCU1状态待机,
            });
            com[26.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车EDCU1状态正常,
                InBoolKeys.B1车EDCU1状态中断,
                InBoolKeys.B1车EDCU1状态待机,
            });
            // B2 车
            com[27.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车REP状态正常,
                InBoolKeys.B2车REP状态中断,
                InBoolKeys.B2车REP状态待机,
            });
            com[28.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车DXMe状态正常,
                InBoolKeys.B2车DXMe状态中断,
                InBoolKeys.B2车DXMe状态待机,
            });
            com[29.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车DIMe状态正常,
                InBoolKeys.B2车DIMe状态中断,
                InBoolKeys.B2车DIMe状态待机,
            });
            com[30.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车HVAC状态正常,
                InBoolKeys.B2车HVAC状态中断,
                InBoolKeys.B2车HVAC状态待机,
            });
            com[31.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车DCU1状态正常,
                InBoolKeys.B2车DCU1状态中断,
                InBoolKeys.B2车DCU1状态待机,
            });
            com[32.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车DCU2状态正常,
                InBoolKeys.B2车DCU2状态中断,
                InBoolKeys.B2车DCU2状态待机,
            });
            com[33.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车BCU状态正常,
                InBoolKeys.B2车BCU状态中断,
                InBoolKeys.B2车BCU状态待机,
            });
            com[34.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车EDCU1状态正常,
                InBoolKeys.B2车EDCU1状态中断,
                InBoolKeys.B2车EDCU1状态待机,
            });
            com[35.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车EDCU2状态正常,
                InBoolKeys.B2车EDCU2状态中断,
                InBoolKeys.B2车EDCU2状态待机,
            });

            com[36.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车REP状态正常,
                InBoolKeys.A2车REP状态中断,
                InBoolKeys.A2车REP状态待机,
            });
            com[37.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车VCMe状态正常,
                InBoolKeys.A2车VCMe状态中断,
                InBoolKeys.A2车VCMe状态待机,
            });
            com[38.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车AXMe状态正常,
                InBoolKeys.A2车AXMe状态中断,
                InBoolKeys.A2车AXMe状态待机,
            });

            com[39.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车ERMe状态正常,
                InBoolKeys.A2车ERMe状态中断,
                InBoolKeys.A2车ERMe状态待机,
            });

            com[40.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车PIS状态正常,
                InBoolKeys.A2车PIS状态中断,
                InBoolKeys.A2车PIS状态待机,
            });

            com[41.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DDU状态正常,
                InBoolKeys.A2车DDU状态中断,
                InBoolKeys.A2车DDU状态待机,
            });


            com[42.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车HVAC状态正常,
                InBoolKeys.A2车HVAC状态中断,
                InBoolKeys.A2车HVAC状态待机,
            });


            com[43.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DXMe1状态正常,
                InBoolKeys.A2车DXMe1状态中断,
                InBoolKeys.A2车DXMe1状态待机,
            });

            com[44.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DCU状态正常,
                InBoolKeys.A2车DCU状态中断,
                InBoolKeys.A2车DCU状态待机,
            });

            com[45.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DXMe2状态正常,
                InBoolKeys.A2车DXMe2状态中断,
                InBoolKeys.A2车DXMe2状态待机,
            });

            com[46.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车SIV状态正常,
                InBoolKeys.A2车SIV状态中断,
                InBoolKeys.A2车SIV状态待机,
            });
            com[47.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DIMe1状态正常,
                InBoolKeys.A2车DIMe1状态中断,
                InBoolKeys.A2车DIMe1状态待机,
            });
            com[48.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车BCU状态正常,
                InBoolKeys.A2车BCU状态中断,
                InBoolKeys.A2车BCU状态待机,
            });

            com[49.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车DIMe2状态正常,
                InBoolKeys.A2车DIMe2状态中断,
                InBoolKeys.A2车DIMe2状态待机,
            });

            com[50.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车EDCU1状态正常,
                InBoolKeys.A2车EDCU1状态中断,
                InBoolKeys.A2车EDCU1状态待机,
            });

            com[51.ToString()] = GetState(new[]
            {
                InBoolKeys.A2车EDCU2状态正常,
                InBoolKeys.A2车EDCU2状态中断,
                InBoolKeys.A2车EDCU2状态待机,
            });
        }

        private void UpdateDoorParams()
        {
            var dr = m_CommandDataDictionary[FlashFuncs.Door];


            dr[DoorParam.A1MState.ToString()] =
                GetMState(new[]
                {
                    InBoolKeys.A1车空压机未知灰色,
                    InBoolKeys.A1车空压机工作绿色,
                    InBoolKeys.A1车空压机未工作黑色
                });

            dr[DoorParam.A2MState.ToString()] =
                GetMState(new[]
                {
                    InBoolKeys.A2车空压机未知灰色,
                    InBoolKeys.A2车空压机工作绿色,
                    InBoolKeys.A2车空压机未工作黑色
                });

            dr[DoorParam.B1Pantograph.ToString()] = GetState(new[]
            {
                InBoolKeys.B1车受电弓升弓,
                InBoolKeys.B1车受电弓降弓,
                InBoolKeys.B1车受电弓降弓中,
                InBoolKeys.B1车受电弓升弓中,
                InBoolKeys.B1车受电弓降弓故障,
                InBoolKeys.B1车受电弓升弓故障,
                InBoolKeys.B1车受电弓未知,
            });

            dr[DoorParam.B2Pantograph.ToString()] = GetState(new[]
            {
                InBoolKeys.B2车受电弓升弓,
                InBoolKeys.B2车受电弓降弓,
                InBoolKeys.B2车受电弓降弓中,
                InBoolKeys.B2车受电弓升弓中,
                InBoolKeys.B2车受电弓降弓故障,
                InBoolKeys.B2车受电弓升弓故障,
                InBoolKeys.B2车受电弓未知,
            });

            for (int i = 0; i < 4; i++)
            {
                var name = ConvertToCarName(i);
                dr[((DoorParam) ((int) DoorParam.A1PassengerState + i)).ToString()] = GetState(new[]
                {
                    InBoolKeys.ResourceManager.GetString(name + "车乘客信息1正常"),
                    InBoolKeys.ResourceManager.GetString(name + "车乘客信息1故障"),
                    InBoolKeys.ResourceManager.GetString(name + "车乘客信息1未知"),
                });
            }

            for (int i = 0; i < 4; i++)
            {
                //InBoolKeys.A1车制动1无故障缓解
                //InBoolKeys.A1车制动1故障
                //InBoolKeys.A1车制动1施加
                //InBoolKeys.A1车制动1隔离
                //InBoolKeys.A1车制动1车隔离
                //InBoolKeys.A1车制动1未知
                var name = ((CarType) i).ToString();
                dr[(45 + i*2).ToString()] = GetState(new[]
                {
                    InBoolKeys.ResourceManager.GetString(name + "车制动1无故障缓解"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动1故障"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动1施加"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动1隔离"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动1车隔离"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动1未知"),
                });
                dr[(45 + i*2 + 1).ToString()] = GetState(new[]
                {
                    InBoolKeys.ResourceManager.GetString(name + "车制动2无故障缓解"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动2故障"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动2施加"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动2隔离"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动2车隔离"),
                    InBoolKeys.ResourceManager.GetString(name + "车制动2未知"),
                });
            }

            //牵引
            for (int i = 0; i < 6; i++)
            {
                //InBoolKeys.A1车制动1无故障缓解
                //InBoolKeys.A1车制动1故障
                //InBoolKeys.A1车制动1施加
                //InBoolKeys.A1车制动1隔离
                //InBoolKeys.A1车制动1车隔离
                //InBoolKeys.A1车制动1未知

                var idxs = Enumerable.Range(0, 4).Select(s => 647 + s + i*4).ToArray();
                dr[(53 + i).ToString()] = GetState(idxs);
            }

            // 电制动
            for (int i = 0; i < 6; i++)
            {
                //InBoolKeys.A1车制动1无故障缓解
                //InBoolKeys.A1车制动1故障
                //InBoolKeys.A1车制动1施加
                //InBoolKeys.A1车制动1隔离
                //InBoolKeys.A1车制动1车隔离
                //InBoolKeys.A1车制动1未知

                var idxs = Enumerable.Range(0, 4).Select(s => 671 + s + i*4).ToArray();
                dr[(59 + i).ToString()] = GetState(idxs);
            }

            // 空调
            for (int i = 0; i < 4; i++)
            {
                var name = ((CarType) i).ToString();
                dr[(65 + i).ToString()] = GetState(new[]
                {
                    InBoolKeys.ResourceManager.GetString(name + "车空调无故障"),
                    InBoolKeys.ResourceManager.GetString(name + "车空调故障"),
                    InBoolKeys.ResourceManager.GetString(name + "车空调本地模式"),
                    InBoolKeys.ResourceManager.GetString(name + "车空调未知"),
                });
            }


            // 停放制动
            dr[DoorParam.A1ParkingBrake.ToString()] =
                GetPakingBrake(new[]
                {
                    InBoolKeys.A1车停放制动无故障和缓解,
                    InBoolKeys.A1车停放制动故障,
                    InBoolKeys.A1车停放制动施加,
                    InBoolKeys.A1车停放制动隔离,
                    InBoolKeys.A1车停放制动未知
                });
            dr[DoorParam.A2ParkingBrake.ToString()] =
                GetPakingBrake(new[]
                {
                    InBoolKeys.A2车停放制动无故障和缓解,
                    InBoolKeys.A2车停放制动故障,
                    InBoolKeys.A2车停放制动施加,
                    InBoolKeys.A2车停放制动隔离,
                    InBoolKeys.A2车停放制动未知
                });
            dr[DoorParam.B1ParkingBrake.ToString()] =
                GetPakingBrake(new[]
                {
                    InBoolKeys.B1车停放制动无故障和缓解,
                    InBoolKeys.B1车停放制动故障,
                    InBoolKeys.B1车停放制动施加,
                    InBoolKeys.B1车停放制动隔离,
                    InBoolKeys.B1车停放制动未知
                });
            dr[DoorParam.B2ParkingBrake.ToString()] =
                GetPakingBrake(new[]
                {
                    InBoolKeys.B2车停放制动无故障和缓解,
                    InBoolKeys.B2车停放制动故障,
                    InBoolKeys.B2车停放制动施加,
                    InBoolKeys.B2车停放制动隔离,
                    InBoolKeys.B2车停放制动未知
                });
        }

        private static string ConvertToCarName(int carNo)
        {
            var name = ((CarType) carNo).ToString();
            return name;
        }

        private string GetState(int[] indexs)
        {
            var index = indexs.Length;
            foreach (var name in indexs.Reverse())
            {

                if (m_UpdateParam.GetInBoolAt(name))
                {
                    break;
                }
                index--;
            }
            return index.ToString();
        }


        private string GetState(string[] names)
        {
            var index = names.Length;
            foreach (var name in names.Reverse())
            {

                if (m_UpdateParam.GetInBoolAt(name))
                {
                    break;
                }
                index--;
            }
            return index.ToString();
        }

        private string GetMState(IEnumerable<string> names)
        {
            var index = 3;
            foreach (var name in names.Reverse())
            {

                if (m_UpdateParam.GetInBoolAt(name))
                {
                    break;
                }
                index--;
            }

            return index.ToString();
        }

        private string GetPakingBrake(IEnumerable<string> names)
        {
            var index = 5;
            foreach (var name in names.Reverse())
            {

                if (m_UpdateParam.GetInBoolAt(name))
                {
                    break;
                }
                index --;
            }

            return index.ToString();
        }

        public void OnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            if (GetInBoolValue(communicationDataChangedArgs.ChangedBools, InBoolKeys.激活紧急手柄状态) == true)
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "1";
            }
            if (GetInBoolValue(communicationDataChangedArgs.ChangedBools, InBoolKeys.紧急手柄未激活状态) == true)
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "2";
            }
            if (GetInBoolValue(communicationDataChangedArgs.ChangedBools, InBoolKeys.紧急手柄状态未知) == true)
            {
                m_CommandDataDictionary[FlashFuncs.ATC][ATCParam.HandShank.ToString()] = "3";
            }

            var datas = m_CommandDataDictionary[FlashFuncs.ATC];
            datas[ATCParam.Brake1.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.A1车制动缸压力1kPa).ToString("0");
            datas[ATCParam.Brake2.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.A1车制动缸压力2kPa).ToString("0");
            datas[ATCParam.Brake3.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.A2车制动缸压力1kPa).ToString("0");
            datas[ATCParam.Brake4.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.A2车制动缸压力2kPa).ToString("0");
            datas[ATCParam.Brake5.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.B1车制动缸压力1kPa).ToString("0");
            datas[ATCParam.Brake6.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.B1车制动缸压力2kPa).ToString("0");
            datas[ATCParam.Brake7.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.B2车制动缸压力1kPa).ToString("0");
            datas[ATCParam.Brake8.ToString()] = m_UpdateParam.GetInFloatAt(InFloatKeys.B2车制动缸压力2kPa).ToString("0");

            UpdateDoorParams();

            UpdateCommunicationView();

            UpdateHVACView();

            UpdateHVAC1View();

            UpdateBypassView();

            UpdateParamView();

            UpdateParam1View();
        }

        private void UpdateParam1View()
        {
            var pm = CommandDataDictionary[FlashFuncs.Param1];
            for (int i = 0; i < m_Param1Names.Count; i++)
            {
                var param1Name = m_Param1Names[i];
                pm[(i + 1).ToString()] = m_UpdateParam.GetInFloatAt(param1Name).ToString("0");
            }
        }

        private void UpdateParamView()
        {
        }

        private void UpdateBypassView()
        {
            var bp = CommandDataDictionary[FlashFuncs.Bypass];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    bp[(i*16 + j + 1).ToString()] =
                        GetState(
                            m_BypassNames.Skip(j*3)
                                .Take(3)
                                .Select(s => InBoolKeys.ResourceManager.GetString(string.Format(s, i + 1)))
                                .ToArray());
                }
            }
        }


        private void UpdateHVAC1View()
        {
            var hv = CommandDataDictionary[FlashFuncs.Hvac1];
            for (int i = 0; i < 4; i++)
            {
                var car = ConvertToCarName(i);
                for (int j = 1; j < 9; j++)
                {
                    hv[(i*8 + j).ToString()] =
                        GetState(new[]
                        {
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态开13", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态开23", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态全开", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态关", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态故障", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车新风阀{1}状态未知", car, j)),
                        });

                    hv[(4*8 + i*8 + j).ToString()] =
                        GetState(new[]
                        {
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车回风阀{1}状态开", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车回风阀{1}状态关", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车回风阀{1}状态故障", car, j)),
                            InBoolKeys.ResourceManager.GetString(string.Format("{0}车回风阀{1}状态未知", car, j)),
                        });
                }
            }
        }

        private void UpdateHVACView()
        {
            var hv = CommandDataDictionary[FlashFuncs.Hvac];
            for (int i = 1; i < 5; i++)
            {
                var car = ConvertToCarName(i - 1);
                hv[i.ToString()] =
                    m_UpdateParam.GetInFloatAt(InFloatKeys.ResourceManager.GetString(car + "车室内温度")).ToString("0");

                hv[(i + 4).ToString()] =
                    m_UpdateParam.GetInFloatAt(InFloatKeys.ResourceManager.GetString(car + "车室外温度")).ToString("0");

                hv[(i + 8).ToString()] =
                    m_UpdateParam.GetInFloatAt(InFloatKeys.ResourceManager.GetString(car + "车目标温度")).ToString("0");

                hv[(i + 12).ToString()] =
                    m_UpdateParam.GetInFloatAt(InFloatKeys.ResourceManager.GetString(car + "功率消耗")).ToString("0");

                hv[(i + 16).ToString()] = GetState(new[]
                {
                    InBoolKeys.ResourceManager.GetString(car + "空调模式自动"),
                    InBoolKeys.ResourceManager.GetString(car + "空调模式手动"),
                    InBoolKeys.ResourceManager.GetString(car + "空调模式通风"),
                    InBoolKeys.ResourceManager.GetString(car + "空调模式停止"),
                });

                for (int j = 1; j < 5; j++)
                {
                    hv[(20 + (i - 1)*4 + j).ToString()] = GetState(new[]
                    {
                        InBoolKeys.ResourceManager.GetString(string.Format("{0}车空调压缩机{1}状态未知", car, j)),
                        InBoolKeys.ResourceManager.GetString(string.Format("{0}车空调压缩机{1}状态故障", car, j)),
                        InBoolKeys.ResourceManager.GetString(string.Format("{0}车空调压缩机{1}状态工作", car, j)),
                        InBoolKeys.ResourceManager.GetString(string.Format("{0}车空调压缩机{1}状态待机", car, j)),
                    });
                }
            }
        }



        public bool GetInBoolValue(ICommunicationDataService dataService, string indexName)
        {
            return dataService.ReadService.GetBoolAt(IndexFacade.InBoolDictionary[indexName]);
        }

        public float GetInFloatValue(ICommunicationDataService dataService, string indexName)
        {
            return dataService.ReadService.GetFloatAt(IndexFacade.InFloatDictionary[indexName]);
        }


        public bool? GetInBoolValue(CommunicationDataChangedArgs<bool> communicationDataChangedArgs, string indexName)
        {
            if (communicationDataChangedArgs.NewValue.ContainsKey(
                IndexFacade.InBoolDictionary[indexName]))
            {
                return
                    communicationDataChangedArgs.NewValue[
                        IndexFacade.InBoolDictionary[indexName]];
            }
            return null;
        }

        public float GetInFloatValue(CommunicationDataChangedArgs<float> communicationDataChangedArgs, string indexName)
        {
            if (
                communicationDataChangedArgs.NewValue.ContainsKey(
                    IndexConfigure.Instance.CommunicationIndexFacade.InFloatDictionary[indexName]))
            {
                return communicationDataChangedArgs.NewValue[IndexFacade.InFloatDictionary[indexName]];
            }
            return float.NaN;
        }

        public void Dispose()
        {
            m_UpdateFlashTimer.Dispose();
        }
    }
}