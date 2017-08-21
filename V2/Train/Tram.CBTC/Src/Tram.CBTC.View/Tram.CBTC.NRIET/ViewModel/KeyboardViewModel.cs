using System.Windows.Input;
using DevExpress.XtraPrinting.Native;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.NRIET.ViewModel
{
    //[Export]
    public class KeyboardViewModel : NotificationObject
    {
        //[ImportingConstructor]
        public KeyboardViewModel(DomainViewModel viewModel)
        {
            ViewModel = viewModel;

            InputCommand = new DelegateCommand<string>(Input);
            BackspaceCommand = new DelegateCommand(Backspace);
            ReturnCommand = new DelegateCommand(Return);
        }


        private void Input(string content)
        {
            ViewModel.StartViewModel.InputCommand.Execute(content);
            ViewModel.SettingViewModel.InputCommand.Execute(content);
        }


        private void Backspace()
        {
            if (!ViewModel.StartViewModel.InputContent.IsEmpty())
            {
                ViewModel.StartViewModel.InputContent = ViewModel.StartViewModel.InputContent.Substring(0, ViewModel.StartViewModel.InputContent.Length - 1);
            }
        }


        private void Return()
        {
            ViewModel.Controller.IsOpenKeyboard = false;
        }


        public DomainViewModel ViewModel { private set; get; }

        public ICommand InputCommand { private set; get; }

        public ICommand BackspaceCommand { private set; get; }

        public ICommand ReturnCommand { private set; get; }
    }
}