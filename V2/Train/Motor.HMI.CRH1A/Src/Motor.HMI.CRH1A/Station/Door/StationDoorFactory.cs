using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Station.Door
{
    class StationDoorFactory
    {
        private readonly StationResource m_StationResource;

        public StationDoorFactory(StationResource stationResource)
        {
            m_StationResource = stationResource;
        }

        public IStationDoor CreateStationDoor(Point location, int carNo, bool isUp)
        {
            var createDoor = GetCreateStationDoorFunc();

            var door = createDoor();
            door.RefreshAction = o =>
            {
                var d = (IStationDoor)o;
                var bools = m_StationResource.GetDoorStateValues(d);
                d.State = GetState(bools);
            };
            var stationDoorLocation = new StationDoorLocation { CarNo = (uint)carNo, IsUp = isUp };
            door.OutLineRectangle = new Rectangle(location, StationDoorBase.DefaultSize);
            //door.OutLineRectangle = new Rectangle(locationPoint.X + j * 84 + 95, locationPoint.Y + 1 + (isup ? 0 : 47), 30, 20);
            door.DoorLocation = stationDoorLocation;

            return door;
        }

        private StationDoorState GetState(bool[] bools)
        {
            if (bools[2])
            {
                return StationDoorState.Cut;
            }
            if (bools[3])
            {
                return StationDoorState.Fault;
            }
            if (bools[1])
            {
                return StationDoorState.Open;
            }
            if (bools[4])
            {
                return StationDoorState.Release;
            }
            if (bools[0])
            {
                return StationDoorState.Close;
            }
            //默认关闭
            return StationDoorState.Close;
        }

        private Func<StationDoorBase> GetCreateStationDoorFunc()
        {
            switch (GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle)
            {
                case StationDoorStyle.Solid:
                    return () => new SolidStationDoor() { Style = GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle };
                case StationDoorStyle.Hollow:
                    return () => new HollowStationDoor() { Style = GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle };
                default:
                    LogMgr.Fatal(string.Format("Can not find door style {0}, using default style {1} to create station door",
                        GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle,
                        StationDoorStyle.Hollow));
                    return () => new SolidStationDoor();
            }
        }
    }
}
