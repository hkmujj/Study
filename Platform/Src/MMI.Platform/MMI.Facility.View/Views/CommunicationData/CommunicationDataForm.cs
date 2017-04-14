using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using MMI.Facility.View.ViewModel;

namespace MMI.Facility.View.Views.CommunicationData
{
    public partial class CommunicationDataForm : System.Windows.Forms.Form, IShareForm
    {
        private CommunicationDataFacadeView m_CommunicationDataFacadeView;

        private CommunicationDataFacadeServiceViewModel m_CommunicationDataFacadeServiceViewModel;

        public CommunicationDataForm() : this(null)
        {
            
        }

        public CommunicationDataForm(IDataPackage dataPackage)
        {
            DataPackage = dataPackage;

            InitializeComponent();
            
            m_CommunicationDataFacadeView = new CommunicationDataFacadeView();
            
            elementHost1.Child = m_CommunicationDataFacadeView;
            
            if (!DesignMode)
            {
                var dataService = (CommunicationDataFacadeServiceBase) dataPackage.CommunicationDataFacadeService;
                m_CommunicationDataFacadeServiceViewModel = new CommunicationDataFacadeServiceViewModel(dataPackage, dataService);
                m_CommunicationDataFacadeView.ViewViewModel = m_CommunicationDataFacadeServiceViewModel;
            }
        }

        public IDataPackage DataPackage { get; private set; }

        public void ReastData()
        {
            
        }

        public void SelectBool(int index, ProjectType projectType = ProjectType.Unkown, string appName = null)
        {
            m_CommunicationDataFacadeServiceViewModel.SetSelectedType(projectType, appName);
            m_CommunicationDataFacadeServiceViewModel.SetSelectedBoolIndex(index);
        }

        public void SelectFloat(int index, ProjectType projectType = ProjectType.Unkown, string appName = null)
        {
            m_CommunicationDataFacadeServiceViewModel.SetSelectedType(projectType, appName);
            m_CommunicationDataFacadeServiceViewModel.SetSelectedFloatIndex(index);
        }
    }
}
