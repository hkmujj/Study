using Engine.Angola.TCMS.Resource.Keys;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Engine.Angola.TCMS.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToRootActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root);
        }
    }
}
