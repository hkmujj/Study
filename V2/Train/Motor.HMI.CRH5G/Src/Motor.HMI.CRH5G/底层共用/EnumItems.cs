namespace Motor.HMI.CRH5G.底层共用
{
    /// <summary>
    /// 视图名
    /// </summary>
    public enum ViewIDName : int
    {
        ClassOverScreen,
        BlackScreen,
        FaultReceive,
        MenuScreen,
        TitleScreen,
        InstrumentScreen1,
        InstrumentScreen2,
        ButtonsScreen,
        TestOneScreen,
        TestTwoScreen,
        TestThreeScreen,
        HomeScreen,
        BrakeOneTest,
        BrakeTwoTest,
        BrakeThreeTest,
        SystemOneSettings,
        SystemTwoSettings,
        DownloadDataFault,
        Command ,
        MaintainScreen,
        FaultEnsure,
        PassWord,
        FaultHistory,
        LanguageTimeSettings,
    }

    public enum FontName : int
    {
        宋体,
        黑体,
    }

    public enum FontRelated : int
    {
        居中,
        靠左,
        靠右,
        靠左上,
    }

    public enum RectRiseDirection : int
    {
        上,
        下,
        左,
        右,
    }
}
