using System;
using CommonUtil.Model;

namespace Mmi.Communication.Index.Adapter
{
    public static class CommunicationIndexFacadeExtension
    {
        public static int FindIndex(this CommunicationIndexFacade facade, CommunicationIndexType type, string name)
        {
            IReadOnlyDictionary<string, int> dt;
            switch (type)
            {
                case CommunicationIndexType.InBool :
                    dt = facade.InBoolDictionary;
                    break;
                case CommunicationIndexType.InFloat :
                    dt = facade.InFloatDictionary;
                    break;
                case CommunicationIndexType.OutBool :
                    dt = facade.OutBoolDictionary;
                    break;
                case CommunicationIndexType.OutFloat :
                    dt = facade.OutFloatDictionary;
                    break;
                default :
                    throw new ArgumentOutOfRangeException("type");
            }

            return dt[name];

        }
    }
}