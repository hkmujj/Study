namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 
    /// </summary>
    public static class InfomationItemContentExtension
    {
        /// <summary>
        /// 是否只有文本显示
        /// </summary>
        /// <param name="itemContent"></param>
        /// <returns></returns>
        public static bool IsOnlyTextInfo(this IInfomationItemContent itemContent)
        {
            return itemContent.ResponseType == InfomationResponseType.None;
        }
    }
}