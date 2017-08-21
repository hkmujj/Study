using DevExpress.Mvvm;
using LightRail.HMI.SZLHLF.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace LightRail.HMI.SZLHLF.Controller
{
    [Export]
    public class InputTrainNumController : ControllerBase<InputPasswordViewModel>
    {
        public InputTrainNumController()
        {
            ConfirmTrainNum = new DelegateCommand((ConfirmTrainNumMethod));
        }

        private void ConfirmTrainNumMethod()
        {
            var Num = ViewModel.Model.Text;
            if (Num != null)
            {
                ViewModel.Model.TrainNum = Num;
            }
        }

        public ICommand ConfirmTrainNum { get; private set; }
    }
}
