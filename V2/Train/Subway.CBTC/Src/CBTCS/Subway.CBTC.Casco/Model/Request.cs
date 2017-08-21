using System;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Request;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Subway.CBTC.Casco.Constant;
using Subway.CBTC.Casco.View.Contents.RegionF;

namespace Subway.CBTC.Casco.Model
{
    public class Request : IRequest
    {
        public Request()
        {
            m_RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }
        private readonly IRegionManager m_RegionManager;
        /// <summary>
        /// 请求界面
        /// </summary>
        /// <param name="view">Name枚举</param>
        public void RequestView(ViewNames view)
        {
            switch (view)
            {
                case ViewNames.BM:
                    m_RegionManager.RequestNavigate(RegionNames.ContentF, typeof(RegionFLayout).FullName);
                    break;
                case ViewNames.Menu:
                    m_RegionManager.RequestNavigate(RegionNames.ContentF, typeof(RegionFNormal).FullName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("view", view, null);
            }
        }
    }
}
