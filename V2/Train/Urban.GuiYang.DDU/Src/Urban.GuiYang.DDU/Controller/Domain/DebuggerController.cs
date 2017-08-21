using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Extension;
using Urban.GuiYang.DDU.Model;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain
{
    [Export]
    public class DebuggerController : ControllerBase<DebuggerViewModel>
    {

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            if (GlobalParam.Instance.InitParam == null)
            {
                SetValueWhenDebug(false);
            }
            else if (GlobalParam.Instance.InitParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                SetValueWhenDebug(true);
            }
        }

        private void SetValueWhenDebug(bool isMMIStart)
        {
            if (isMMIStart)
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService.ChangeInBoolOf(
                    InbKeys.Inb车辆屏亮屏, true, true);
            }

            ViewModel.DomainViewModel.Model.Visible = true;
        }
    }
}