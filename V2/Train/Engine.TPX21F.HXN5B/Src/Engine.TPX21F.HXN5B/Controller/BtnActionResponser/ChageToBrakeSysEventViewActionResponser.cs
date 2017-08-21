using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Constant;
using Engine.TPX21F.HXN5B.View.Contents.Brake.Detail;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    class ChageToBrakeSysEventViewActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            RegionManager.RequestNavigate(RegionNames.ContentBrakeSystemBooter, typeof(BrakeSysEventView).FullName);
        }
    }
}