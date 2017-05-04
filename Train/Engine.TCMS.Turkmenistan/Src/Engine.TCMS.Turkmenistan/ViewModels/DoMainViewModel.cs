using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Controllser;

namespace Engine.TCMS.Turkmenistan.ViewModels
{
    /// <summary>
    /// 根ViewModel
    /// </summary>
    [Export]
    public class DoMainViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public DoMainViewModel(DoMainController controller)
        {
            Controller = controller;
        }
        public DoMainController Controller { get; private set; }

    }
}