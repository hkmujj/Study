using Engine.LCDM.HDX2.Entity.Events;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public interface ISendInterface
    {
        void SendTrainId(string[] trainId);

        void SendReserveCommon(ReserveCommon reserveCommon);

        /// <summary>
        /// 单双节
        /// </summary>
        /// <param name="vehicleCount"></param>
        void SendVehicleCount(VehicleCount vehicleCount);

        void SendAirBrakePressure(AirBrakePressure pressure);

        void SendAirBrakeUseState(UseState state);

        void SendAirBrakeLocation(AirBrakeLocation location);

        void SendAirBrakeMakeupAirState(MakeupAirState state);

        void SendEmergenceTimeUp(EmergenceTimerState state);

        /// <summary>
        /// 课程结束清除数据
        /// </summary>
        void Reset();
    }
}