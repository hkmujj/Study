using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(IResetSupport))]
    [Export]
    public class AssistPowerTestViewModel : TestViewModelBase
    {

        [ImportingConstructor]
        public AssistPowerTestViewModel(TestController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            APU1 = new APUTestModel();
            APU2 = new APUTestModel();
            StateKeyName = StateKeys.Root_检修状态_试验_辅助电源;
        }

        public APUTestModel APU1 { private set; get; }

        public APUTestModel APU2 { private set; get; }

        public override void RestartTest()
        {
            APU1.Visible = false;
            APU2.Visible = false;
        }
    }
}