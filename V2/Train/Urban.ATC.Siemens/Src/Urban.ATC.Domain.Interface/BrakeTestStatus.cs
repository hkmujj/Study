namespace Motor.ATP.Domain.Interface
{
    public enum BrakeTestStatus
    {
        BrakeTesting = 1,	//正在执行制动测试 
        BrakeTestSucceed = 2,	//制动测试完成
        BrakeTestFail = 3,	//制动测试测试失败
    }
}
