using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Subway.WuHanLine6.Configs;
using Subway.WuHanLine6.Controller;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 牵引辅助切除
    /// </summary>
    [Export]
    public class TractionAssistCutModel : ModelBase
    {
        private bool m_IsChecked;
        private ObservableCollection<TactionAssistCutUnit> m_All;
        private string m_DisplayStr;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public TractionAssistCutModel(TractionAssistCutController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            Intance = this;
            All = new ObservableCollection<TactionAssistCutUnit>(TractionAssistCutFactory.Instance.GetAllUnit().OrderBy(o => o.Index));
            controller.Initalize();
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<TactionAssistCutUnit> All
        {
            get { return m_All; }
            set
            {
                if (Equals(value, m_All))
                {
                    return;
                }
                m_All = value;
                RaisePropertyChanged(() => All);
                RaisePropertyChanged(() => Brake);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayStr
        {
            get { return m_DisplayStr; }
            set
            {
                if (value == m_DisplayStr)
                {
                    return;
                }
                m_DisplayStr = value;
                RaisePropertyChanged(() => DisplayStr);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<TactionAssistCutUnit> Brake { get { return All.Where(w => w.Index >= 14 && w.Index <= 17).ToList(); } }
        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                if (value == m_IsChecked)
                {
                    return;
                }
                m_IsChecked = value;
                Controller.IsCheckedChanged();
                RaisePropertyChanged(() => IsChecked);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public static TractionAssistCutModel Intance { get; private set; }

        /// <summary>
        /// 控制类
        /// </summary>
        public TractionAssistCutController Controller { get; private set; }

    }
}