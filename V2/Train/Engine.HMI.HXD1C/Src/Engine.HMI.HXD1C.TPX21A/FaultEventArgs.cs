using System;

namespace Engine.HMI.HXD1C.TPX21A
{
    public class FaultEventArgs : EventArgs
    {
        public Fault m_Fault=new Fault();
        public FaultEventArgs(Fault fa)
        {
            m_Fault = fa;
        }
    }
}