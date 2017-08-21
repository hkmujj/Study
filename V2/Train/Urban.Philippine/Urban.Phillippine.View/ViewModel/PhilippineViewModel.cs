using System.ComponentModel.Composition;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IPhilippineViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class PhilippineViewModel : ViewModelBase, IPhilippineViewModel
    {
        [Import]
        public IMainViewModel MainViewModel { get; private set; }

        [Import]
        public ILoginViewModel Login { get; private set; }

        [Import]
        public ITitleViewModel Title { get; private set; }

        [Import]
        public IButtonViewModel Button { get; private set; }

        [Import]
        public IVVVFViewModel VVVF { get; set; }

        [Import]
        public IBrakeViewModel Brake { get; set; }

        [Import]
        public ITMSViewModel TMS { get; set; }

        [Import]
        public IAPSViewModel APS { get; set; }

        [Import]
        public IIOStateViewModel IOState { get; set; }

        [Import]
        public IVACViewModel VAC { get; set; }

        [Import]
        public IDataServiceViewModel DataService { get; private set; }

        [Import]
        public IChangedPasswordViewModel ChangedPassword { get; private set; }
        [Import]
        public IInstrusctionViewModel Instrusction { get; private set; }
        [Import]
        public IVACTestViewModel VACTest { get; private set; }
        [Import]
        public IFaultRecordVIewModel FaultRecord { get; private set; }
        [Import]
        public IReLoginViewModel ReLoginViewModel { get; private set; }
    }
}