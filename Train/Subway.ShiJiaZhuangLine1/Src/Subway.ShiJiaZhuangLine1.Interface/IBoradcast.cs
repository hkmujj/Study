namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IBoradcast
    {
        /// <summary>
        /// 广播逻辑号
        /// </summary>
        int LogicNum { get; }
        /// <summary>
        /// 广播编号
        /// </summary>
        int Number { get; }
        /// <summary>
        /// 广播内容
        /// </summary>
        string Content { get; }

    }
}
