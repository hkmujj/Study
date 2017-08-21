using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.TCMS.CRH400BF.Extension;
using Motor.TCMS.CRH400BF.Model;
using Motor.TCMS.CRH400BF.Resources.Keys;
using Motor.TCMS.CRH400BF.ViewModel;

namespace Motor.TCMS.CRH400BF.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DomainController : ControllerBase<DomainViewModel>
    {
        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        public DomainController()
        {
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
        }

        private void OnLoaded(CommandParameter param)
        {
            ViewModel.Initalize();
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            if (GlobalParam.Instance.InitParam != null)
            {
                var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
                ws.ChangedInBoolOf(InbKeys.InbMMI亮屏, false);
            }
            ViewModel.Model.IsVisble = false;
        }
    }
}