using MMI.Facility.Interface.Data;
using MMI.Facility.View.ViewModel;

namespace MMI.Facility.View.Views.Course
{
    public partial class CourseForm : System.Windows.Forms.Form
    {
        public CourseForm() : this(null)
        {

        }

        public CourseForm(IDataPackage dataPackage)
        {
            //DataPackage = dataPackage;

            InitializeComponent();

            //m_CommunicationDataFacadeView = new CommunicationDataFacadeView();

            elementHost1.Child = new CourseView() {DataContext = new CourseViewModel() {DataPackage = dataPackage}};

            //if (!DesignMode)
            //{
            //    var dataService = (CommunicationDataFacadeServiceBase)dataPackage.CommunicationDataFacadeService;
            //    m_CommunicationDataFacadeServiceViewModel = new CommunicationDataFacadeServiceViewModel(dataPackage, dataService);
            //    m_CommunicationDataFacadeView.ViewViewModel = m_CommunicationDataFacadeServiceViewModel;
            //}
        }
    }
}
