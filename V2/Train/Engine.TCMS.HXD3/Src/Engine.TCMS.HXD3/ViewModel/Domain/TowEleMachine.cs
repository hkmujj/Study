using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    public class TowEleMachineItem : NotificationObject
    {
        private ObservableCollection<TochMahineState> m_States;
        private ObservableCollection<float> m_Values;

        [DebuggerStepThrough]
        public TowEleMachineItem(TowEleMachineConfig config)
        {
            Config = config;
            Values = new ObservableCollection<float>(Enumerable.Repeat(0f, 6));
            States = new ObservableCollection<TochMahineState>(Enumerable.Repeat(TochMahineState.NotWork, 6));

        }

        public TowEleMachineConfig Config { private set; get; }

        public ObservableCollection<float> Values
        {
            set
            {
                if (Equals(value, m_Values))
                {
                    return;
                }

                m_Values = value;
                RaisePropertyChanged(() => Values);
            }
            get { return m_Values; }
        }

        /// <summary>
        /// 后面两种需要
        /// </summary>
        public ObservableCollection<TochMahineState> States
        {
            set
            {
                if (Equals(value, m_States))
                {
                    return;
                }

                m_States = value;
                RaisePropertyChanged(() => States);
            }
            get { return m_States; }
        }
    }
}