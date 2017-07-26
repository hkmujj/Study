namespace Motor.ATP.DataAdapter.Util
{
    public enum SelfCheckStatus
    {
        CheckStatus_Checking = 1,	//正在自检 
        CheckStatus_Success = 2,	//自检成功
        CheckStatus_Fail = 3	//自检失败
    }
    public enum BrakeStatus
    {
        NoneBreakSign = 0,	//无制动施加
        PowerLoseSign = 1,	//切除牵引
        CommonBreak_1 = 2,	//1级常用制动
        CommonBreak_4 = 3,	//4级常用制动
        CommonBreak_7 = 4,	//7级常用制动
        CommonBreak_E = 5,	//紧急制动
        CommonBreak_R = 6	//允许缓解
    }
    //紧急制动类型
    public enum EBType
    {
        EBType_OVERSPEED = 1,//超速类型，停车后允许缓解
        EBType_ALERT = 2, //警惕类型，停车后且按压警惕允许缓解
        EBType_LIMIT0 = 3,//限速为0情况
        EBTYPE_SlideProtect= 4,//溜逸防护触发
        EBTYPE_StopProtect= 5,//停车防护触发
	    EBTYPE_BackProtect=6//退行防护触发
    }

    //消息状态
    public enum MessageConfirmType
    {
        MessageConfirmType_None = 0,
        MessageConfirmType_Confirm = 1,
        MessageConfirmType_Cancel = 2
    }
}
