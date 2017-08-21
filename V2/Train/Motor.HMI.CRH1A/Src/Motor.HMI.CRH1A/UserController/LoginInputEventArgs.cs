using System;

namespace Motor.HMI.CRH1A.UserController
{
    class LoginInputEventArgs : EventArgs
    {
        public LoginInputEventArgs(string inputstring)
        {
            Inputstring = inputstring;
        }

        public string Inputstring { private set; get; }
    }
}
