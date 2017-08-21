using System;
using System.Reflection;


namespace Motor.ATP.DataAdapter
{
    public enum SelfCheckStatus
    {
        CheckStatus_Checking = 1,	//正在自检 
        CheckStatus_Success = 2,	//自检成功
        CheckStatus_Fail = 3	//自检失败
    }
}
