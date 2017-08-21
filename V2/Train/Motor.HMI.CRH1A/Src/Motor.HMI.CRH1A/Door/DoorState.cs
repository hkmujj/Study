using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.Door
{
    public enum DoorState
    {
        /// <summary>
        /// 车门已开或被释放 显示为全绿
        /// </summary>
        OpenOrRelease,
        /// <summary>
        /// 车门已开或被释放，并有故障 显示为全红
        /// </summary>
        OpenAndFault ,
        /// <summary>
        /// /车门已开或被释放,并切断 显示为全蓝
        /// </summary>
        OpenAndOff,
        /// <summary>
        /// 车门关闭显示为绿边框
        /// </summary>
        Close,
        /// <summary>
        /// 车门关闭，并有故障 显示为红边框
        /// </summary>
        CloseAndFault,
        /// <summary>
        /// 车门关闭，并被切断 显示为蓝边框
        /// </summary>
        CloseAndOff,
        /// <summary>
        /// 状态不明 显示为灰边框
        /// </summary>
        Unknown,
    }

    public enum DoorType
    {
        /// <summary>
        /// 上面的门
        /// </summary>
        UpDoor,
        /// <summary>
        /// 下面的门
        /// </summary>
        DownDoor,
    }
}
