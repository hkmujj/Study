using System.ComponentModel.Composition;
using LightRail.HMI.SZLHLF.Model;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export]
    public class SecondLevelViewModel : ModelBase
    {
        [ImportingConstructor]
        public SecondLevelViewModel(InputPasswordViewModel inputPasswordViewModel,SecondViewModel secondViewModel)
        {
            InputPasswordViewModel = inputPasswordViewModel;
            SecondViewModel = secondViewModel;
        }

        public SecondViewModel SecondViewModel { get; private set; }
        public InputPasswordViewModel InputPasswordViewModel { get; private set; }
    }
}
