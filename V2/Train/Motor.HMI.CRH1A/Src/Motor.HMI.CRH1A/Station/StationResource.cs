using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Station.Car;
using Motor.HMI.CRH1A.Station.Door;
using Motor.HMI.CRH1A.Station.Overload;

namespace Motor.HMI.CRH1A.Station
{
    class StationResource
    {
        private readonly int[] m_DoorStateIndex;

        private readonly GT_Station m_StationView;

        public StationResource(GT_Station view)
        {
            if (view == null)
            {
                var em = "Can not construct StationResource where input argument view = null";
                LogMgr.Error(em);
                throw new ArgumentNullException("view", em);
            }

            m_StationView = view;
            m_DoorStateIndex = view.UIObj.InBoolList.Take(GlobalParam.CarCount * 5 * 2).ToArray();
        }

        public bool[] GetDoorStateValues(IStationDoor stationDoor)
        {
            return m_DoorStateIndex.Skip((int) ( (stationDoor.DoorLocation.IsUp ? 0 : 1 )* 5 + stationDoor.DoorLocation.CarNo* 10 )).Take(5).Select(s => m_StationView.BoolList[s]).ToArray();
        }

        public OverloadState GetOverloadState(IDoorViewCar doorViewCar)
        {
            const int START_INDEX = 10 * GlobalParam.CarCount;

            var idx = START_INDEX + 16 + doorViewCar.CarNo;
            if (m_StationView.BoolList[m_StationView.UIObj.InBoolList[idx]])
            {
                return OverloadState.SeriousOverloading;
            }
            idx = START_INDEX + doorViewCar.CarNo;
            if (m_StationView.BoolList[m_StationView.UIObj.InBoolList[idx]])
            {
                return OverloadState.Overloading;
            }
            return OverloadState.Default;
        }
    }
}
