using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Door
{
    /// <summary>
    /// 门的状态判断
    /// </summary>
    public class DoorStateAdpt
    {
        /// <summary>
        /// 判断门的状态
        /// </summary>
        /// <returns></returns>
        public DoorState GetDoorState(DoorInfo doorInfo, bool[] value)
        {
            var i = doorInfo.TrainNumber;

            var bIsOpen = value[i * 10 + 1];
            var bIsCut = value[i * 10 + 2];
            var bIsFault = value[i * 10 + 3];
            var bIsRelase = value[i * 10 + 4];

            if (doorInfo.Type == DoorType.DownDoor)
            {
                bIsOpen = value[i * 10 + 1 + 5];
                bIsCut = value[i * 10 + 2 + 5];
                bIsFault = value[i * 10 + 3 + 5];
                bIsRelase = value[i * 10 + 4 + 5];

            }
            if (bIsOpen || bIsRelase)
            {
                if (bIsFault && !bIsCut)
                {
                    return DoorState.OpenAndFault;
                }
                if (!bIsFault && bIsCut)
                {
                    return DoorState.OpenAndOff;
                }
                if (!bIsFault && !bIsCut)
                {
                    return DoorState.OpenOrRelease;
                }
            }

            if (!bIsOpen && !bIsRelase)
            {
                if (bIsFault && !bIsCut)
                {
                    return DoorState.CloseAndFault;
                }
                if (!bIsFault && bIsCut)
                {
                    return DoorState.CloseAndOff;
                }
                if (!bIsFault && !bIsCut)
                {
                    return DoorState.Close;
                }
            }

            return DoorState.Unknown;
        }

    }

}
