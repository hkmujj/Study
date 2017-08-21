using Motor.ATP.Domain.Interface.Infomation;

namespace Motor.ATP.Domain.Model.Infomation
{
    public interface IEnsureInfomationCache
    {
        /// <summary>
        /// 清除所有
        /// </summary>
        void Clear();

        /// <summary>
        /// 增加一个
        /// </summary>
        /// <param name="infomationItem"></param>
        void Add(IInfomationItem infomationItem);

        /// <summary>
        /// 删除一个
        /// </summary>
        /// <param name="infomationItem"></param>
        void Remove(IInfomationItem infomationItem);

        /// <summary>
        /// 获得当前正在确认的
        /// </summary>
        /// <returns></returns>
        IInfomationItem GetCurrentEnsuringItemContent();
    }
}