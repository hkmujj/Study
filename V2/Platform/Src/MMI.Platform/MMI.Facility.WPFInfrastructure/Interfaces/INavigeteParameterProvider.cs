using Microsoft.Practices.Prism;

namespace MMI.Facility.WPFInfrastructure.Interfaces
{
    /// <summary>
    /// 导航查询参数 
    /// </summary>
    public interface INavigeteParameterProvider
    {
        /// <summary>
        /// 将此id 转换成导航查询参数
        /// </summary>
        /// <returns></returns>
        UriQuery ConvertToNavigateParameterUriQuery();
    }
}
