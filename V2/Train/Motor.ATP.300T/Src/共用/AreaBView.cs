using System;

namespace Motor.ATP._300T.共用
{
    [Flags]
    public enum AreaBView
    {
        SpeedValue = 0x1,

        ControlModel = 2,

        Cir = 4,

        All = SpeedValue | ControlModel | Cir,
    }
}