namespace Subway.XiaMenLine1.Interface
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

        BoradcastType Type { get; }
    }

    public enum BoradcastType
    {
        /// <summary>
        /// 爱心广播
        /// </summary>
        Love = 1,
        /// <summary>
        /// 紧急广播
        /// </summary>
        Emergent = 2,
        /// <summary>
        /// 定制广播
        /// </summary>
        Customized = 3,
    }
}
