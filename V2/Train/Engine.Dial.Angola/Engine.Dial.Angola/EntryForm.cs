using MMI.Facility.Interface.Project;

namespace Engine.Dial.Angola
{
    public partial class EntryForm : ProjectFormBase
    {
        public EntryForm()
        {
            InitializeComponent();
        }

        public EntryForm(SubsystemInitParam initParam) : this()
        {
            AppConfig = initParam.AppConfig;
            AppName = AppConfig.AppName;
            DataPackage = initParam.DataPackage;

            //elementHost1.Child = ServiceLocator.Current.GetInstance<DialShell>();
            elementHost1.Child = new MainShell();
        }
    }
}