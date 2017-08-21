using System;
using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CancelActionResponser : BtnActionResponserBase
    {
        public StateInterfaceKey ChangToAfterCancel { set; get; }

        public event Action CancelAction;

        public override void ResponseMouseUp()
        {
            OnCancelAction();

            if (ChangToAfterCancel != null)
            {
                ChangeStateTo(ChangToAfterCancel);
            }
        }

        protected virtual void OnCancelAction()
        {
            var handler = CancelAction;
            if (handler != null)
            {
                handler();
            }
        }
    }
}