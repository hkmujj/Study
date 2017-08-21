using System;

namespace MMI.Common.Msg.Interface.ACK
{
    public class ACKDetail : IACKDetail
    {
        public ACKDetail(int logicID, int infoLevel, string chinaInfo, string englishInfo, double chinaFontSize, double englishFontSize)
        {
            LogicID = logicID;
            InfoLevel = infoLevel;
            ChinaInfo = chinaInfo;
            EnglishInfo = englishInfo;
            ChinaFontSize = chinaFontSize;
            EnglishFontSize = englishFontSize;
        }

        public int LogicID { get; protected set; }
        public int InfoLevel { get; protected set; }
        public string ChinaInfo { get; protected set; }
        public string EnglishInfo { get; protected set; }
        public double ChinaFontSize { get; protected set; }
        public double EnglishFontSize { get; protected set; }
        public DateTime HappenTime { get; set; }
        public DateTime AffirmTime { get; set; }
        public DateTime RemoveTime { get; set; }
    }
}