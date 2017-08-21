using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.LCDM.HDX2.DataAdapter.Extension;
using Engine.LCDM.HDX2.DataAdapter.Resource;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.DataAdapter
{
    [Export(typeof(ISendInterface))]
    class HXD2SendInterface : ISendInterface
    {
        private List<bool> m_EmptyBools;
        private List<float> m_EmptyFloats;

        private List<bool> EmptyBools
        {
            get { return m_EmptyBools ?? (m_EmptyBools=Enumerable.Repeat(false, WriteService.ReadOnlyBoolDictionary.Count).ToList()); }
        }

        private List<float> EmptyFloats
        {
            get { return m_EmptyFloats ?? (m_EmptyFloats=Enumerable.Repeat(0f, WriteService.ReadOnlyFloatDictionary.Count).ToList()); }
        }

        private ICommunicationDataWriteService WriteService
        {
            get
            {
                return HXD2Param.Instance.InitParam.CommunicationDataService.WriteService;
            }
        }

        public void SendTrainId(string[] trainId)
        {

        }

        public void SendReserveCommon(ReserveCommon reserveCommon)
        {
            WriteService.ChangeBool(OutBoolKeys.设置备用常用, reserveCommon != ReserveCommon.Common);
        }

        public void SendVehicleCount(VehicleCount vehicleCount)
        {
            WriteService.ChangeBool(OutBoolKeys.设置单双节模式设置单节双节, vehicleCount != VehicleCount.Multi);
        }

        public void SendAirBrakePressure(AirBrakePressure pressure)
        {
            WriteService.ChangeBool(OutBoolKeys.空气制动定压设置500kpa600kpa, pressure != AirBrakePressure.KP600);
        }

        public void SendAirBrakeUseState(UseState state)
        {
            WriteService.ChangeBool(OutBoolKeys.空气制动切除投入, state != UseState.Using);
        }

        public void SendAirBrakeLocation(AirBrakeLocation location)
        {
            WriteService.ChangeBool(OutBoolKeys.空气制动客车位货车位, location != AirBrakeLocation.InGoods);
        }

        public void SendAirBrakeMakeupAirState(MakeupAirState state)
        {
            switch (state)
            {
                case MakeupAirState.Makeup:
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置中立, false);
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置不补风, false);
                    break;
                case MakeupAirState.Not:
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置中立, false);
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置不补风, true);
                    break;
                case MakeupAirState.Neutral:
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置中立, true);
                    WriteService.ChangeBool(OutBoolKeys.空气制动列车管补风设置不补风, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }

        public void SendEmergenceTimeUp(EmergenceTimerState state)
        {
            

            var flag = false;
            switch (state)
            {
                case EmergenceTimerState.Counting:
                    flag = false;
                    break;
                case EmergenceTimerState.End:
                    flag = true;
                    break;
                case EmergenceTimerState.Stopped:
                    flag = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }

            WriteService.ChangeBool(OutBoolKeys.紧急制动60秒结束, flag);
        }

        public void Reset()
        {
            WriteService.ChangeBools(EmptyBools, WriteService.ReadOnlyBoolDictionary.Min(m => m.Key));
            WriteService.ChangeFloats(EmptyFloats, WriteService.ReadOnlyFloatDictionary.Min(m => m.Key));
        }
    }
}