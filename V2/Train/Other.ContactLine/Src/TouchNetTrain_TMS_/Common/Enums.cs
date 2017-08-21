namespace NC_TMS.Common
{
    /// <summary>
    /// 功能描述：视图状态枚举
    /// </summary>
    public enum ViewState
    {
        黑屏 = 0,

        Main = 1,

        Net = 2,

        TCU = 3,

        ACU = 4,

        BCU = 5,

        PIS = 6,
        PIS_StartStation = 601,
        PIS_TerminalStation = 602,

        HVAC = 7,
        HVAC_State = 701,
        HVAC_Paremset = 702,
        HVAC_Test = 703,

        MianTain = 8,
        MianTain_ParamSet = 801,
        MianTain_InstructionTest = 802,
        MianTain_CurrentFaultList = 803,
        MianTain_HistoryFaultList = 804,
        MianTain_Doors = 804,
        MianTain_Fault_DeviceSelect=8031,
        MianTain_Fault_TimeSet = 8032,

        时间设置 = 1002,
        密码设置 = 1003,
        轮径设置 = 1004,
        车号设置 = 1005,
        加速度测试 = 1006,
        测试 = 1007,
        数据记录 = 1008,
        USB = 1009,
        维护指南 = 1010,
        端口数据 = 1011,
        版本 = 1012,
        IO = 1013,

        HelpMain = 901
    }
}
