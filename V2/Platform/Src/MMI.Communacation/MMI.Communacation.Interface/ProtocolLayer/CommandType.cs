﻿namespace MMI.Communacation.Interface.ProtocolLayer
{
    public enum CommandType
    {
        HeartbeatPacketRequest = 1100,

        CourseStart = 1103,

        CourseOver = 1109,

        CirCmdRecv,

        UpdateStation = 4000,
        /// <summary>
        /// 时刻表
        /// </summary>
        TimeTable = 5000,
    }

}
