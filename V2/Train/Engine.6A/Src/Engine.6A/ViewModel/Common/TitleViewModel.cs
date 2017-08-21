using System;
using System.ComponentModel.Composition;
using Engine._6A.Event;
using Engine._6A.Interface.ViewModel;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(ITitleViewModel))]
    public class TitleViewModel : ViewModelBase, ITitleViewModel
    {
        private string m_Category;
        private DateTime m_DateTime;

        public TitleViewModel()
        {
            EventAggregator.GetEvent<TrainChangedEvent>().Subscribe((args) =>
            {
                Category = args.Name.Contains("A") ? "A节数据" : "B节数据";
            });
        }
        public DateTime DateTime
        {
            get { return m_DateTime; }
            set
            {
                if (value.Equals(m_DateTime))
                {
                    return;
                }
                m_DateTime = value;
                RaisePropertyChanged(() => DateTime);
            }
        }

        public string Category
        {
            get { return m_Category; }
            set
            {
                if (value == m_Category)
                {
                    return;
                }
                m_Category = value;
                RaisePropertyChanged(() => Category);
            }
        }
    }
}
