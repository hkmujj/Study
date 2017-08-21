using System.ComponentModel;

namespace Engine.TCMS.HXD3D.过程数据.NetControl.Child.TransInfo
{
    public enum TransType
    {
        [Description("CI1")] CI1,
        [Description("CI2")] CI2,
        [Description("CI3")] CI3,
        [Description("CI4")] CI4,
        [Description("CI5")] CI5,
        [Description("CI6")] CI6,
        [Description("APU1-1")] APU11,
        [Description("APU1-2")] APU12,
        [Description("APU2-1")] APU21,
        [Description("APU2-2")] APU22,
        [Description("BCU")] BCU,
        [Description("LG1")] LG1,
        [Description("LG2")] LG2,
        [Description("PSU1")] PSU1,
        [Description("PSU2")] PSU2,
        [Description("6A")] A6A
    }
}