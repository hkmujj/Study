using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Model.NetWorkModel
{
    [Export]
    public class NetWorkInfoModel : ModelBase
    {
        [ImportingConstructor]
        NetWorkInfoModel()
        {
            AllSatte = new ObservableCollection<NetWorkUnit>(GlobalParam.Instance.NetWorkUnits.OrderBy(o => o.Number));
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
