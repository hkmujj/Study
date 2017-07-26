using System.Diagnostics.CodeAnalysis;

namespace Motor.ATP.Infrasturcture.Interface
{
#pragma warning disable 1591
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum LevelChangeType

    {
        YG_CTCS0TOCTCS2 = 1,
        YG_CTCS2TOCTCS3 = 2,
        YG_CTCS3TOCTCS4 = 3,
        YG_CTCS4TOCTCS3 = 4,
        YG_CTCS3TOCTCS2 = 5,
        YG_CTCS2TOCTCS0 = 6,
        //级间转换确认点
        QR_CTCS0TOCTCS2 = 7,
        QR_CTCS2TOCTCS3 = 8,
        QR_CTCS3TOCTCS4 = 9,
        QR_CTCS4TOCTCS3 = 10,
        QR_CTCS3TOCTCS2 = 11,
        QR_CTCS2TOCTCS0 = 12,
    }
#pragma warning restore 1591
}
