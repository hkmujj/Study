using System;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    [Flags]
    public enum ProjectType
    {
        WuHan = 0,

        ChengDu = 1,

        GuangZhou = 2,

        All = WuHan | ChengDu | GuangZhou,
    }
}
