using System.Diagnostics;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.UserController;

namespace Motor.HMI.CRH1A.Common.View
{
    class CRH1Button : CRH1AButton
    {
        [DebuggerStepThrough]
        public CRH1Button()
        {
            BtnBehavierStrategy = new CRH1ButtonStrategy(this);
        }
    }
}
