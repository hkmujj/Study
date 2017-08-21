using System.ComponentModel;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum ControlModel
    {
        [Description("")]
        Normal = 0,
        [Description("半列车")]
        HalfTrain = 1,
        [Description("紧急牵引")]
        EmergencyTraction,
        [Description("拖　拽")]
        Drag,
        [Description("慢　行")]
        Crawl,
        [Description("退　行")]
        Regression,
        [Description("ATB")]
        ATB,
        [Description("ATO")]
        ATO,
        [Description("RM")]
        RM,
        [Description("MSC")]
        MSC,
        [Description("RMF")]
        RMF,
        [Description("RMR")]
        RMR,
        [Description("CBTC")]
        CBTC,
        [Description("URM")]
        URM,
        [Description("SM")]
        SM,
        [Description("AR")]
        AR,
    }

    public enum WorkModel
    {
        [Description("")]
        Normal = 0,
        [Description("停放制动")]
        PB = 1,
        [Description("紧急制动")]
        EB,
        [Description("保持制动")]
        HB,
        [Description("快速制动")]
        QB,
        [Description("制动")]
        Brake,
        [Description("牵引")]
        Taction,
        [Description("惰行")]
        Coasting,
        [Description("零位")]
        Zero
    }
}