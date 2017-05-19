﻿using Subway.ShiJiaZhuangLine1.Interface.Attibutes;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum FrsmHighSpeed
    {
        Normal,
        Select,
        Fault,
        [HelpDescription("高速断路器合上")]
        HighJoin,
        [HelpDescription("高速断路器断开")]
        HighDisconnect,
        [HelpDescription("高速断路器故障")]
        HighFalut,
        [HelpDescription("高速断路器通讯故障")]
        HighCommunicationFault,

        [HelpDescription("车间电源故障")]
        FramConect,
        [HelpDescription("车间电源连接，且供电")]
        FramCut,
        [HelpDescription("车间电源连接，未供电")]
        FramFault,

        [HelpDescription("受电弓升起")]
        PantographUp,
        [HelpDescription("受电弓降下")]
        PantographDown,
        [HelpDescription("受电弓故障（升起或降下）")]
        PantographFault,
        [HelpDescription("受电弓切除")]
        PantographCut,
        [HelpDescription("受电弓状态未知")]
        PantographStateUnkown,
    }
}