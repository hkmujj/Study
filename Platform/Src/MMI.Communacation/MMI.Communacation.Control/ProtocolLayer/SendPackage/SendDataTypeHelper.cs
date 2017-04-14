using System;
using System.Linq;

namespace MMI.Communacation.Control.ProtocolLayer.SendPackage
{
    public static class SendDataTypeHelper
    {
        private static SendDataTypeC[] m_AllSendDataType;

        public static SendDataTypeC[] AllSendDataType
        {
            get { return m_AllSendDataType ?? (m_AllSendDataType = Enum.GetValues(typeof(SendDataTypeC)).Cast<SendDataTypeC>().ToArray()); }
        }
    }
}