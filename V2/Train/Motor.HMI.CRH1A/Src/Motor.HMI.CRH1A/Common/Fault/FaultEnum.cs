using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH1A.Common.Fault
{
    class FaultEnum
    {
        public enum FaultUnit
        {
            [Description("高 压")]
            HighVoltage=1,
            [Description("牵 引")]
            Traction,
            [Description("外门")]
            OutsideDoor,
            [Description("控制和通讯")]
            ControlSocket,
            [Description("辅助供电")]
            Power,
            [Description("制 动")]
            Brake,
            [Description("空调")]
            Aircondition,
            [Description("前 端")]
            Frontend,
            [Description("电池供电")]
            CellPower,
            [Description("供 风")]
            AirSupply,
            [Description("污 物 箱")]
            DirtBox,
            [Description("轴报")]
            Axial,
            [Description("烟雾警报器")]
            SmogSys,
            [Description("信息系统")]
            InfoSys
        }
    }
}
