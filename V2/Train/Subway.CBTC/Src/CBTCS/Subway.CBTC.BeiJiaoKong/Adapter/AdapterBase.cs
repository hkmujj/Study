using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.CBTC.BeiJiaoKong.Interfaces;
using Subway.CBTC.BeiJiaoKong.ViewModel;

namespace Subway.CBTC.BeiJiaoKong.Adapter
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public abstract class AdapterBase : IClear, IInitialize, IDataListener
    {
        static AdapterBase()
        {
            ViewModel = ServiceLocator.Current.GetInstance<BeiJiaoKongViewModel>();
        }
        /// <summary>
        /// 课程结束清除数据
        /// </summary>
        public virtual void Clear()
        {

        }

        /// <summary>
        /// 课程初始化
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 北交控ViewModel根节点
        /// </summary>
        protected static BeiJiaoKongViewModel ViewModel { get; private set; }

        /// <summary>
        /// bool 值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public  virtual void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>
        /// float值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public virtual void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>
        /// data值变化
        /// </summary>
        /// <param name="sender"/><param name="dataChangedArgs"/>
        public virtual void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
        }
    }
}