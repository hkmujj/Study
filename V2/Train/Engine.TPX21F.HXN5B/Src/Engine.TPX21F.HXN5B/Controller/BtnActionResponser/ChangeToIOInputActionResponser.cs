using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Constant;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.View.Contents.Other;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToIOInputActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            RegionManager.RequestNavigate(RegionNames.ContentMonitorContent,
                typeof(IOInputView).FullName);
            NavigateTo(StateKeys.Root_��������IO����);
        }
    }
}