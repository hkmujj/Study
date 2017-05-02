using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 网络拓扑Model
    /// </summary>
    [Export]
    public class NetWorkModel : ModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NetWorkModel()
        {
            AllSatte = new ObservableCollection<NetWorkUnit>(GlobalParams.Instance.NetWorkUnits);
        }

        private ObservableCollection<NetWorkUnit> m_AllSatte;

        /// <summary>
        /// 所有状态
        /// </summary>
        public ObservableCollection<NetWorkUnit> AllSatte
        {
            get { return m_AllSatte; }
            set
            {
                if (Equals(value, m_AllSatte))
                {
                    return;
                }
                m_AllSatte = value;
                RaisePropertyChanged(() => AllSatte);
            }
        }
    }
}