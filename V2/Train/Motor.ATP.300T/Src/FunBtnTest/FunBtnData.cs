namespace Motor.ATP._300T.FunBtnTest
{
    /// <summary>
    /// 列车数据
    /// </summary>
    public class FunBtnData
    {
        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainId { get; set; }

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverId { get; set; }
        
        /// <summary>
        /// 列车长度
        /// </summary>
        public string TrainLength { get; set; }

        public bool TrainDataSetReady()
        {
            return string.IsNullOrEmpty(TrainId) && TrainId.Trim() != "0" &&
                   !string.IsNullOrEmpty(DriverId) && DriverId.Trim() != "0" &&
                   string.IsNullOrEmpty(TrainLength) && TrainLength.Trim() != "0";
        }
    }
}
