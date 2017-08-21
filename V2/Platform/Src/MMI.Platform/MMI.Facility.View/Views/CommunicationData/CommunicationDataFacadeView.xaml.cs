using MMI.Facility.View.ViewModel;

namespace MMI.Facility.View.Views.CommunicationData
{
    /// <summary>
    /// CommunicationDataFacadeView.xaml 的交互逻辑
    /// </summary>
    public partial class CommunicationDataFacadeView
    {
        private CommunicationDataFacadeServiceViewModel m_ViewViewModel;

        public CommunicationDataFacadeView()
        {
            InitializeComponent();
        }

        public CommunicationDataFacadeServiceViewModel ViewViewModel
        {
            set
            {
                m_ViewViewModel = value;
                DataContext = value;
            }
            get { return m_ViewViewModel; }
        }
    }
}
