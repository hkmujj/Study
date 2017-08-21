namespace MMI.Common.Msg.Interface.ButtonReactivation
{
    public class ButtonInfo : IButtonInfo
    {
        public ButtonInfo(int logic, int outLogic, string chinaInfo, string englishInfo)
        {
            Logic = logic;
            OutLogic = outLogic;
            ChinaInfo = chinaInfo;
            EnglishInfo = englishInfo;
        }

        public int Logic { get; private set; }
        public int OutLogic { get; private set; }
        public string ChinaInfo { get; private set; }
        public string EnglishInfo { get; private set; }
        public ButtonStatus Status { get; set; }
    }
}