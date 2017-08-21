using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine9.Envets;
using Subway.ShenZhenLine9.Extention;
using Subway.ShenZhenLine9.Resource.Keys;
using Subway.ShenZhenLine9.ViewModels;

namespace Subway.ShenZhenLine9.Controller.ViewModelController
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class BrakeViewController : ControllerBase<BrakeViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public BrakeViewController()
        {
            var events = ServiceLocator.Current.GetInstance<IEventAggregator>()
               ;
            events.GetEvent<BoolDataChangedEvent>().Subscribe(BooChanged);
            events.GetEvent<FloatDataChangedEvent>().Subscribe(FloatChanged);

        }

        private void FloatChanged(DataChangedEventArgs<float> obj)
        {
            obj.Data.UpdateIfContain(InFloatKeys.车1制动缸压力, f => ViewModel.Car1BrakeCylinder = f);
            obj.Data.UpdateIfContain(InFloatKeys.车2制动缸压力, f => ViewModel.Car2BrakeCylinder = f);
            obj.Data.UpdateIfContain(InFloatKeys.车3制动缸压力, f => ViewModel.Car3BrakeCylinder = f);
            obj.Data.UpdateIfContain(InFloatKeys.车4制动缸压力, f => ViewModel.Car4BrakeCylinder = f);
            obj.Data.UpdateIfContain(InFloatKeys.车5制动缸压力, f => ViewModel.Car5BrakeCylinder = f);
            obj.Data.UpdateIfContain(InFloatKeys.车6制动缸压力, f => ViewModel.Car6BrakeCylinder = f);
        }

        private void BooChanged(DataChangedEventArgs<bool> args)
        {
            args.Data.UpdateIfContain(InBoolKeys.车辆1转向架1切除, b => ViewModel.IsCar1Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆1转向架2切除, b => ViewModel.IsCar1Numbe2Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆2转向架1切除, b => ViewModel.IsCar2Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆2转向架2切除, b => ViewModel.IsCar2Numbe2Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆3转向架1切除, b => ViewModel.IsCar3Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆3转向架2切除, b => ViewModel.IsCar3Numbe2Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆4转向架1切除, b => ViewModel.IsCar4Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆4转向架2切除, b => ViewModel.IsCar4Numbe2Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆5转向架1切除, b => ViewModel.IsCar5Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆5转向架2切除, b => ViewModel.IsCar5Numbe2Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆6转向架1切除, b => ViewModel.IsCar6Numbe1Cut = b);
            args.Data.UpdateIfContain(InBoolKeys.车辆6转向架2切除, b => ViewModel.IsCar6Numbe2Cut = b);


        }
    }
}