namespace Motor.ATP.Infrasturcture.Interface.BrakeTest
{
    /// <summary>
    /// 
    /// </summary>
    public enum BrakeTestStatus
    {
        /// <summary>
        /// 正在执行制动测试
        /// </summary>
        BrakeTesting = 1,	// 
        /// <summary>
        /// 	//制动测试完成
        /// </summary>
        BrakeTestSucceed = 2,
        /// <summary>
        /// //制动测试测试失败
        /// </summary>
        BrakeTestFail = 3,
        /// <summary>
        /// //无法启动制动测试
        /// </summary>
        BrakeTestCannotStart = 4,	
    }
}
