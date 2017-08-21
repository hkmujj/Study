using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class CancelFreqActionResponser : DriverActionResponserBase
    {
         public CancelFreqActionResponser(IDriverSelectableItem item, UserActionType userActionType = UserActionType.F8)
            : base(item, userActionType)
        {
        }

        public override void ResponseMouseDown()
        {

        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频));
        }
    }
}
