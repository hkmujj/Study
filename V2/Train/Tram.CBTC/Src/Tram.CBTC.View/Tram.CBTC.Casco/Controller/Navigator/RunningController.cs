using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Tram.CBTC.Casco.Model.TempModel;
using Tram.CBTC.Casco.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.Casco.Controller.Navigator
{
    [Export]
    public class RunningController : ControllerBase<DomainViewModel>
    {
        public DelegateCommand AddLight { get; private set; }

        public DelegateCommand DecreaseLight { get; private set; }

        public DelegateCommand AddVolumn { get; private set; }

        public DelegateCommand DecreaseVolumn { get; private set; }

        public RunningModel RunningModel { get { return ViewModel.Model.RunningModel; } }

        public RunningController()
        {
            AddLight = new DelegateCommand(OnAddLight);
            DecreaseLight = new DelegateCommand(OnDecreaseLight);

            AddVolumn = new DelegateCommand(OnAddVolumn);
            DecreaseVolumn = new DelegateCommand(OnDecreaseVolumn);

        }

        public void Initalize(DomainViewModel domain)
        {
            ViewModel = domain;
        }

        private void OnDecreaseLight()
        {
            ViewModel.Domain.Other.Light = Math.Max(ViewModel.Domain.Other.Light - 10, 0);
            ViewModel.Domain.SendInterface.InputLight(new SendModel<double>(ViewModel.Domain.Other.Light));
        }
        
        private void OnAddVolumn()
        {
            ViewModel.Domain.Other.Volumne = Math.Min(ViewModel.Domain.Other.Volumne + 10, 100);
            ViewModel.Domain.SendInterface.InputLight(new SendModel<double>(ViewModel.Domain.Other.Volumne));
        }

        private void OnDecreaseVolumn()
        {
            ViewModel.Domain.Other.Volumne = Math.Max(ViewModel.Domain.Other.Volumne - 10, 0);
            ViewModel.Domain.SendInterface.InputLight(new SendModel<double>(ViewModel.Domain.Other.Volumne));
        }

        private void OnAddLight()
        {
            ViewModel.Domain.Other.Light = Math.Min(ViewModel.Domain.Other.Light + 10, 100);
            ViewModel.Domain.SendInterface.InputLight(new SendModel<double>(ViewModel.Domain.Other.Light));
        }
    }
}