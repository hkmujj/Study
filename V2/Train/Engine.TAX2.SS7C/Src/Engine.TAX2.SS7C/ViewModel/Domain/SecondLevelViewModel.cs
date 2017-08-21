using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain
{
    [Export]
    public class SecondLevelViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SecondLevelViewModel(InputPasswordViewModel inputPasswordViewModel, SetWhellRViewModel setWhellRViewModel, SetMonitorViewModel setMonitorViewModel, SetAccDataViewModel setAccDataViewModel)
        {
            InputPasswordViewModel = inputPasswordViewModel;
            SetWhellRViewModel = setWhellRViewModel;
            SetMonitorViewModel = setMonitorViewModel;
            SetAccDataViewModel = setAccDataViewModel;
        }

        public InputPasswordViewModel InputPasswordViewModel { get; private set; }

        public SetWhellRViewModel SetWhellRViewModel { get; private set; }

        public SetMonitorViewModel SetMonitorViewModel { get; private set; }

        public SetAccDataViewModel SetAccDataViewModel { get; private set; }

    }
}