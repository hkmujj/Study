using CommonUtil.Controls;

namespace Motor.HMI.CRH1A.Station.Door
{
    interface IStationDoor : ICommonInnerControl
    {
        StationDoorState State { set; get; }

        StationDoorStyle Style { get; }

        StationDoorLocation DoorLocation { get; }

        //int[] BoolIndexs { set; }
    }

}
