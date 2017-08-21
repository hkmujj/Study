using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using DevExpress.Mvvm;
using LightRail.HMI.SZLHLF.Event;
using LightRail.HMI.SZLHLF.ViewModel;
using LightRail.HMI.SZLHLF.Model.MaintainModel;
using MMI.Facility.WPFInfrastructure.Interfaces;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class InputKeyController : ControllerBase<Lazy<InputKeyInfoModel>>
    {
        [ImportingConstructor]
        public InputKeyController(Lazy<InputKeyInfoModel> viewModel)
            : base(viewModel)
        {
            InputKeyCommand = new DelegateCommand<string>(OnInputKey);

            InputKeyEnterCommand = new DelegateCommand<NavigatorEventArgs>(str =>
            {
                SZLHLFViewModel.DoMainViewModel.Controller.Navigator.Execute(str);
            });

            InputKeyCancelCommand = new DelegateCommand(() =>
            {
                viewModel.Value.InputNumbers = string.Empty;
            });
        }

        private void OnInputKey(string obj)
        {
            if (obj.Contains("Del") && ViewModel.Value.InputNumbers.Length > 0)
            {
                ViewModel.Value.InputNumbers = ViewModel.Value.InputNumbers.Substring(0, ViewModel.Value.InputNumbers.Length - 1);
            }
            else
            {
                ViewModel.Value.InputNumbers += obj;
            }
        }
        

        public ICommand InputKeyCommand { private set; get; }

        public ICommand InputKeyEnterCommand { private set; get; }

        public ICommand InputKeyCancelCommand { private set; get; }
    }
}