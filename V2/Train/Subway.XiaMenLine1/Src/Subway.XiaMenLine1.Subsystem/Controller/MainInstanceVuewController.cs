using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.XiaMenLine1.Subsystem.Model;

namespace Subway.XiaMenLine1.Subsystem.Controller
{
    [Export]
    public class MainInstanceVuewController : ControllerBase<Lazy<MainInstanceViewModel>>
    {
        [ImportingConstructor]
        public MainInstanceVuewController(Lazy<MainInstanceViewModel> viewModel) : base(viewModel)
        {
            InputCahr = new DelegateCommand<string>(InputCahrMethod);
            ClearChars = new DelegateCommand(ClearCharsMethod);
        }

        private void ClearCharsMethod()
        {
            ViewModel.Value.Password = string.Empty;
        }

        private void InputCahrMethod(string obj)
        {
            if (string.IsNullOrEmpty(obj) || obj.Length > 1)
            {
                return;
            }
            if (!string.IsNullOrEmpty(ViewModel.Value.Password) && ViewModel.Value.Password.Length >= 4)
            {
                return;
            }

            ViewModel.Value.Password += obj;

        }

        public ICommand InputCahr { get; private set; }
        public ICommand ClearChars { get; private set; }
    }

}