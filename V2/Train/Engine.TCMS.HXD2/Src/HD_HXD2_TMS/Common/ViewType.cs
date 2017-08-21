namespace HD_HXD2_TMS.Common
{
    public enum ViewType
    {
        Black = 0,
        Main = 1,
        Info = 2,
        /// <summary>
        /// 控制-隔离-A
        /// </summary>
        ControlInsulateA = 301,
        /// <summary>
        /// 控制-隔离-B
        /// </summary>
        ControlInsulateB = 302,
        /// <summary>
        /// 控制-受电弓-A
        /// </summary>
        ControlBowA = 303,
        /// <summary>
        /// 控制-受电弓-B
        /// </summary>
        ControlBowB = 304,
        ControlRunTest = 305,
        ControlDistance = 306,
        Breaking = 4,
        DataDriveA = 501,
        DataDriveB = 502,
        DataBreakerA = 503,
        DataBreakerB = 504,
        DataAuxiliaryA = 505,
        DataAuxiliaryB = 506,
        DataBreaking = 507,
        DataVersion = 508,
        DataOnlineState = 509,
        DataConverter = 510,
        DataTrafficState = 511,
        DataTrafficStateRIOM21 = 512,
        DataTrafficStateRIOM22 = 513,
        InputWheel = 601,
        InputTime = 602,
        InputOther = 603,
        InputScreen = 604,
        InputOil = 605,
        TestMilulation = 701,
        TestWheel = 702,
        TestAuxiliary = 703,
        Test380V = 704,
        TestPhase = 705,
        FalutHistroyA = 801,
        FalutHistroyB = 802,
        FalutConveterA = 803,
        FalutConveterB = 804,
        FalutDownload = 805,
        CurrentFalut = 9,
        CurrentFalutA = 901,
        CurrentFalutB = 902
    }
}
