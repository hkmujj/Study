using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200H
{
    [Serializable]
    public class SignalDataOut200H : SignalDataOut
    {



      

        public override void ClearInfo()
        {
            ScreenSwitchFlag = false;
            base.ClearInfo();
        }

    }
}
