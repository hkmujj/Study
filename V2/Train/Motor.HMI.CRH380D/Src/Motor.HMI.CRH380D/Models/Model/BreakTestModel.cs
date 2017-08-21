using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using LightRail.HMI.SZLHLF.Model;
using Motor.HMI.CRH380D.Controller;
using Motor.HMI.CRH380D.Models.ConfigModel;

namespace Motor.HMI.CRH380D.Models.Model
{
    [Export]
    public class BreakTestModel : ModelBase
    {
        private ObservableCollection<BreakTestInfo> m_AllBreakTestInfo;
        private int m_BreakTestInfoNumDisplay;
        private string m_BreakTestInfoDisplay;

        [ImportingConstructor]
        public BreakTestModel(BreakTestController breakTestController)
        {
            TrainModel = new TrainModel();

            m_AllBreakTestInfo = new ObservableCollection<BreakTestInfo>(GlobalParam.Instance.BreakTestInfos.OrderBy(o => o.Index));

            BreakTestController = breakTestController;
            BreakTestController.ViewModel = this;
            BreakTestController.Initalize();
        }

        /// <summary>
        /// 控制
        /// </summary>
        public BreakTestController BreakTestController { get; set; }

        /// <summary>
        /// 车辆集合
        /// </summary>
        public TrainModel TrainModel { get; private set; }

        /// <summary>
        /// 所有制动试验提示文本
        /// </summary>
        public ObservableCollection<BreakTestInfo> AllBreakTestInfo
        {
            get { return m_AllBreakTestInfo; }
            set
            {
                if (value == m_AllBreakTestInfo)
                {
                    return;
                }
                m_AllBreakTestInfo = value;
                RaisePropertyChanged(() => AllBreakTestInfo);
            }
        }

        /// <summary>
        /// 需要显示的制动试验提示文本的编号
        /// </summary>
        public int BreakTestInfoNumDisplay
        {
            get { return m_BreakTestInfoNumDisplay; }
            set
            {
                if (value == m_BreakTestInfoNumDisplay)
                {
                    return;
                }
                m_BreakTestInfoNumDisplay = value;
                BreakTestController.UpdateBreakTestInfoDisplay(value);
                RaisePropertyChanged(() => BreakTestInfoNumDisplay);
            }
        }

        /// <summary>
        /// 需要显示的制动试验提示文本
        /// </summary>
        public string BreakTestInfoDisplay
        {
            get { return m_BreakTestInfoDisplay; }
            set
            {
                if (value == m_BreakTestInfoDisplay)
                {
                    return;
                }
                m_BreakTestInfoDisplay = value;
                RaisePropertyChanged(() => BreakTestInfoDisplay);
            }
        }
    }
}