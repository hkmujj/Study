using System;

namespace MMI.Common.Msg.Interface.ACK
{
    public interface IACKDetail
    {
        int LogicID { get; }
        int InfoLevel { get; }
        string ChinaInfo { get; }
        string EnglishInfo { get; }
        double ChinaFontSize { get; }
        double EnglishFontSize { get; }
        DateTime HappenTime { get; set; }
        DateTime AffirmTime { get; set; }
        DateTime RemoveTime { get; set; }
    }
}