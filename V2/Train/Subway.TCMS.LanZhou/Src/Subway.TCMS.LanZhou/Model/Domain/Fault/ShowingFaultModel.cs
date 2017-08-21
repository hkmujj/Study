using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Utils;

namespace Subway.TCMS.LanZhou.Model.Domain.Fault
{
    public class ShowingFaultModel : NotificationObject
    {
        private PageWrapper<FaultItem> m_ShowingPage;
        //public IEnumerable<FaultItem> AllShowingItems { get; set; }
        //public IEnumerable<FaultItem> Level1ShowingItems { get; set; }
        //public IEnumerable<FaultItem> Level2ShowingItems { get; set; }
        //public IEnumerable<FaultItem> Level3ShowingItems { get; set; }
        //public int  CurrentPageIndex { get; set; }

        public PageWrapper<FaultItem> AllShowingPage { set; get; }

        public PageWrapper<FaultItem> Level2ShowingPage { set; get; }

        public PageWrapper<FaultItem> Level3ShowingPage { set; get; }

        public PageWrapper<FaultItem> ShowingPage
        {
            set
            {
                if (Equals(value, m_ShowingPage))
                {
                    return;
                }
                m_ShowingPage = value;
                RaisePropertyChanged(() => ShowingPage);
            }
            get { return m_ShowingPage; }
        }
    }
}