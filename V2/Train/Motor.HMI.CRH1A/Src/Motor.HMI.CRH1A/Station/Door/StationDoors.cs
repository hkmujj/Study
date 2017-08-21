using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Station.Door
{
    class StationDoors : CommonInnerControlBase
    {
        private Dictionary<StationDoorLocation, IStationDoor> m_StationDoors;
        private int[] m_BoolIndexs;
        public int[] BoolIndexs
        {
            set
            {
                m_BoolIndexs = value;
                if (BoolIndexs == null || BoolIndexs.Length != GlobalParam.CarCount * 2 * 5)
                {
                    var msg = string.Format("In bools of station doors is null or the count is not {0}", GlobalParam.CarCount * 2 * 5);
                    LogMgr.Fatal(msg);
                    throw new ArgumentException(msg);
                }
            }
            protected get { return m_BoolIndexs; }
        }

        public override void OnDraw(Graphics g)
        {
            foreach (var stationDoor in m_StationDoors.Values)
            {
                stationDoor.OnPaint(g);
            }
        }

        public StationDoors(GT_Station stationView, Point locationPoint, int[] inboolindexs)
        {
            BoolIndexs = inboolindexs;
            m_StationDoors = new Dictionary<StationDoorLocation, IStationDoor>();
            var createDoor = GetCreateStationDoorFunc();
            for (int i = 0; i < 2; i++)
            {
                var upordown = i;
                bool isup = i == 0;
                for (var j = 0; j < 8; j++)
                {
                    var carNo = j;
                    var door = createDoor();
                    door.RefreshAction = o =>
                    {
                        var d = (IStationDoor) o;
                        var bools = inboolindexs.Skip(upordown * 5 + carNo*10).Take(5).Select(s => stationView.BoolList[s]).ToArray();
                        d.State = GetState(bools);
                    };
                    var location = new StationDoorLocation { CarNo = (uint)j, IsUp = isup };
                    door.OutLineRectangle = new Rectangle(locationPoint.X + j * 84  + 95, locationPoint.Y + 1 + (isup ? 0 : 47), 30, 20);
                    door.DoorLocation = location;
                    m_StationDoors.Add(location, door);
                }
            }
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
                case StationDoorStyle.Solid :
                    return () => new SolidStationDoor() { Style = GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle };
                case StationDoorStyle.Hollow :
                    return () => new HollowStationDoor() { Style = GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle };
                default :
                    LogMgr.Fatal(string.Format("Can not find door style {0}, using default style {1} to create station door",
                        GlobalInfo.Instance.CRH1ADetailConfig.StationConfig.DoorStyle,
                        StationDoorStyle.Hollow));
                    return () => new SolidStationDoor();
            }
        }
    }
}
