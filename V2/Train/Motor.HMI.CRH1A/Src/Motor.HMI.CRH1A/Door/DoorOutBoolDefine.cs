using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Door
{
    public enum DoorOutBoolDefine
    {
        /// <summary>
        ///  MC1车左门切除/切入 1667
        /// </summary>
        Mc1LeftDoor = 0,

        /// <summary>
        /// TP1车左门切除/切入 
        /// </summary>
        Tp1LeftDoor,

        /// <summary>
        /// M1车左门切除/切入  
        /// </summary>
        M1LeftDoor,

        /// <summary>
        /// M3车左门切除/切入
        /// </summary>
        M3LeftDoor,

        /// <summary>
        /// TB车左门切除/切入
        /// </summary>
        TbLeftDoor,


        /// <summary>
        /// M2车左门切除/切入
        /// </summary>
        M2LeftDoor,

        /// <summary>
        /// TP2车左门切除/切入
        /// </summary>
        Tp2LeftDoor,

        Mc2LeftDoor,

        /// <summary>
        ///  MC1车右门切除/切入
        /// </summary>
        Mc1RigthDoor,

        /// <summary>
        /// TP1车右门切除/切入 
        /// </summary>
        Tp1RigthDoor,

        /// <summary>
        /// M1车右门切除/切入  
        /// </summary>
        M1RigthDoor,

        /// <summary>
        /// M3车右门切除/切入
        /// </summary>
        M3RigthDoor,


        /// <summary>
        /// TB车右门切除/切入
        /// </summary>
        TbRigthDoor,


        /// <summary>
        /// M2车右门切除/切入
        /// </summary>
        M2RigthDoor,

        /// <summary>
        /// TP2车右门切除/切入
        /// </summary>
        Tp2RigthDoor,

        Mc2RigthDoor,
    }

    public class DoorOutBoolDefineHelper
    {
        public static DoorOutBoolDefine GetOutBoolDefine(DoorInfo doorInfo)
        {
            if (doorInfo.Type == DoorType.UpDoor)
            {
                return (DoorOutBoolDefine)(doorInfo.TrainNumber + 8);
            }
            else
            {
                return (DoorOutBoolDefine) doorInfo.TrainNumber;
            }
        }
    }
}
