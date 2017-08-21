using MMI.Facility.Interface.Project;
using Motor.LKJ._2000.DataAdapter;
using Motor.LKJ._2000.Entity.View;
using Motor.LKJ._2000.Entity.ViewModel;

namespace Motor.LKJ._2000.Subsystem
{
    public partial class LKJ2000Form : ProjectFormBase
    {
        protected LKJ2000Form()
        {
            InitializeComponent();
        }

        public LKJ2000Form(SubsystemInitParam initParam)
            : base(initParam.AppConfig.AppName, initParam.DataPackage)
        {
            InitializeComponent();

            var data = new LKJ2000DataAdapter(initParam, new LKJ2000MainViewModel(initParam));

            elementHost1.Child = new LKJ2000MainView()
            {
                DataContext = data.ViewModel
            };
            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);
        }
    }
}
