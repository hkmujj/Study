using System.ComponentModel;

namespace CRH2MMI.TelentControl
{
    public enum TeleControlBtnType
    {
        [Description("受电弓\n降下")]
        AcceptEleDown = 0,

        [Description("受电弓1\n降下")]
        AcceptEle1Down,

        [Description("受电弓2\n降下")]
        AcceptEle2Down,

        [Description("V C B\n断")]
        VCBOff,

        [Description("压缩机\n切 除")]
        // ReSharper disable once InconsistentNaming
        TCarCompressorCutOff,

        [Description("压缩机\n切 除")]
        MCarCompressorCutOff,

        [Description("电源切换\n(ACK2合)")]
        ACK2On,

        [Description("无变压器\nM车切除")]
        MCarWithoutTransformerCutOff,

        [Description("有变压器\nM车切除")]
        MCarWithTransformerCutOff,

        [Description("受电弓\n升起")]
        AcceptEleUp,

        [Description("受电弓1\n升起")]
        AcceptEle1Up,

        [Description("受电弓2\n升起")]
        AcceptEle2Up,

        [Description("V C B\n合")]
        VCBOn,

        [Description("压缩机\n切除复位")]
        // ReSharper disable once InconsistentNaming
        TCarCompressorReset,

        [Description("压缩机\n切除复位")]
        MCarCompressorReset,

        [Description("切除复位\n(ACK2断)")]
        ACK2Off,

        [Description("M车切除复位")]
        MCarReset,

    }
}
