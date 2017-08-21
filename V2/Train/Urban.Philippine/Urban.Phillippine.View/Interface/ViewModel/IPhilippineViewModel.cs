using CommonUtil.Annotations;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IPhilippineViewModel : IViewModelBase
    {
        IMainViewModel MainViewModel { get; }
        ILoginViewModel Login { get; }
        ITitleViewModel Title { get; }
        IButtonViewModel Button { get; }
        IVVVFViewModel VVVF { get; set; }
        IBrakeViewModel Brake { get; set; }
        ITMSViewModel TMS { get; set; }
        IAPSViewModel APS { get; set; }
        IIOStateViewModel IOState { get; set; }
        IVACViewModel VAC { get; set; }
        IDataServiceViewModel DataService { get; }
        IChangedPasswordViewModel ChangedPassword { get; }
        IInstrusctionViewModel Instrusction { get; }
        IVACTestViewModel VACTest { get; }
        IFaultRecordVIewModel FaultRecord { get; }
        IReLoginViewModel ReLoginViewModel { get; }
    }
}