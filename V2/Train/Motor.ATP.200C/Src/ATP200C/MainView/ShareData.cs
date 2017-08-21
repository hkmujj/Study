namespace ATP200C.MainView
{
    /// <summary>
    /// GT_ShareData 类 共享数据类
    /// 保存相关配置信息的类
    /// </summary>
    public class ShareData
    {
        /// <summary>
        /// 司机号
        /// </summary>
        public static string DirverId = "5506474";

        /// <summary>
        /// 车次号
        /// </summary>
        public static string TrainId = "D5104";
    }


    public class TrainInfo
    {       
        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverId { set; get; }

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainId { set; get; }
    }

}
