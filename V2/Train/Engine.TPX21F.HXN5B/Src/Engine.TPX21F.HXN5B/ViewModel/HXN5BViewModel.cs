using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel
{
    [Export]
    public class HXN5BViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HXN5BViewModel(HXN5BController controller, HXN5BModel model, HardwareBtnViewModel hardwareBtnViewModel, OtherViewModel otherViewModel, DomainViewModel domain, HXN5BDebugger debugger)
        {
            Controller = controller;
            Model = model;
            HardwareBtnViewModel = hardwareBtnViewModel;
            OtherViewModel = otherViewModel;
            Domain = domain;
            m_Debugger = debugger;
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

        private readonly HXN5BDebugger m_Debugger;

        public HXN5BController Controller { private set; get; }

        public HXN5BModel Model { private set; get; }

        public HardwareBtnViewModel HardwareBtnViewModel { private set; get; }

        public OtherViewModel OtherViewModel { private set; get; }

        public DomainViewModel Domain { get; private set; }
    }
}