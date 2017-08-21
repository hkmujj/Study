using System;
using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OkActionResponser : BtnActionResponserBase
    {
        public StateInterfaceKey ChangToAfterOk { set; get; }

        public event Action OkAction;

        public override void ResponseMouseUp()
        {
            OnOkAction();

            if (ChangToAfterOk != null)
            {
                ChangeStateTo(ChangToAfterOk);
            }
        }

        protected virtual void OnOkAction()
        {
            var handler = OkAction;
            if (handler != null)
            {
                handler();
            }
        }
    }
}