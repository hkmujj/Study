using CBTC.Infrasturcture.Model.Constant;

namespace CBTC.Infrasturcture.Model.Request
{
    /// <summary>
    /// 适配层请求
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// 请求界面
        /// </summary>
        /// <param name="view">Name枚举</param>
        void RequestView(ViewNames view);
    }
}
