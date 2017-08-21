namespace MMI.YDCommunicationWrapper
{
    public static class CommandConstant
    {
        /// <summary>
        /// 最大参数长度
        /// </summary>
        public const int MaxParamLength = 1000;

        /// <summary>
        /// 最大包长度
        /// </summary>
        public const int MaxCommandPackLength = MaxParamLength + CommandHeadLength;

        /// <summary>
        /// 命令头长度
        /// </summary>
        public const int CommandHeadLength = 24;
    }
}