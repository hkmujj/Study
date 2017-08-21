namespace CRH2MMI.Common.Config
{
    /// <summary>
    /// 车的配置
    /// </summary>
    internal class TrainConfig
    {
        /// <summary>
        /// 列车长度 8
        /// </summary>
        public const int TrainLength = 8;

        /// <summary>
        /// 列车的名字列表
        /// </summary>
        //public static string[] TrainNames;

        public static readonly string[] DefaultTrainNames;

        static TrainConfig()
        {
            DefaultTrainNames = new string[TrainLength];
            for (int i = 0; i < TrainLength; i++)
            {
                DefaultTrainNames[i] = (i + 1).ToString();
            }
        }
    }
}
