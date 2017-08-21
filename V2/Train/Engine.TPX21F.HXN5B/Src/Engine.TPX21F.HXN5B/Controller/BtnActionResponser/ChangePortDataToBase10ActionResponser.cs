using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.ViewSource.DesignData;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    class ChangePortDataToBase10ActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            DesignPortData.Instance.DataStyle = PortDataStyle.Base10;
        }
    }
}