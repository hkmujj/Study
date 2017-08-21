using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Motor.HMI.CRH380BG.Model;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.ViewModel.Domain;
using Motor.HMI.CRH380BG.Controller;

namespace Motor.HMI.CRH380BG.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CRH380BGViewModel : NotificationObject
    {
        [ImportingConstructor]
        public CRH380BGViewModel(CRH380BGModel model, HardwareBtnViewModel mainHardwareBtnViewModel,
            StateViewModel mainStateViewModel,DomainViewModel domain,OtherViewModel other,CRH380BGDebugger debugger)
        {
            Model = model;
            HardwareBtnViewModel = mainHardwareBtnViewModel;
            StateViewModel = mainStateViewModel;
            Domain = domain;
            domain.Parent = this;
            Other = other;
            m_Debugger = debugger;

            mainHardwareBtnViewModel.DomainViewModel = this;
            mainStateViewModel.DomainViewModel = this;
            m_Debugger.ViewModel = this;

            SetValueWhenDebug();
        }

        private void SetValueWhenDebug()
        {
            // 直接EXE打开，或者 调试模式。
            if (GlobalParam.Instance.InitParam == null ||
                GlobalParam.Instance.InitParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                m_Debugger.Initalize();
            }
        }

        private readonly CRH380BGDebugger m_Debugger;

        public ViewLocation ViewLocation { get; set; }

        public IRegionManager RegionManager { get; set; }

        public CRH380BGModel Model { private set; get; }

        public StateViewModel StateViewModel { get; private set; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }

        public DomainViewModel Domain { get; private set; }

        public OtherViewModel Other { get; private set; } 
    }
}