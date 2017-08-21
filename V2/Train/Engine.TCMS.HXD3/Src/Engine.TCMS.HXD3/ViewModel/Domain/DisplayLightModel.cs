using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(IResetSupport))]
    [Export]
    public class DisplayLightViewModel : TestViewModelBase
    {
        [ImportingConstructor]
        public DisplayLightViewModel(TestController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            StateKeyName = StateKeys.Root_ºÏ–ﬁ◊¥Ã¨_ ‘—È_œ‘ æµ∆;
        }
    }
}