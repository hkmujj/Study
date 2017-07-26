using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T;
using Motor.ATP.DataAdapter.Util;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnCTCSActionResponser : F8OrdinaryActionResponser
    {
        public F8ReturnCTCSActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            InterfaceController.GotoRoot();
            switch (ATP.CTCS.CurrentCTCSType)
            {
                case CTCSType.CTCS2:
                    ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS2));
                    break;
                case CTCSType.CTCS3:
                    ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS3));
                    break;
            }
        }
    }
}
