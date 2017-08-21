using System;
using System.Data;
using CommonUtil.Util;
using Communication.LogicInterface.Adapter;
using Urban.Domain.Interface;

namespace Urban.Iran.HMI.Common
{
    public static class CommunicationIndexFacadeExtension
    {
        public static int FindIndex(this CommunicationIndexFacade facade, CommunicationIndexType type, string name)
        {
            DataTable dt;
            switch (type)
            {
                case CommunicationIndexType.InBool :
                    dt = facade.InBoolTable;
                    break;
                case CommunicationIndexType.InFloat :
                    dt = facade.InFloatTable;
                    break;
                case CommunicationIndexType.OutBool :
                    dt = facade.OutBoolTable;
                    break;
                case CommunicationIndexType.OutFloat :
                    dt = facade.OutFloatTable;
                    break;
                default :
                    throw new ArgumentOutOfRangeException("type");
            }

            try
            {
                return Convert.ToInt32(dt.Rows.Find(name)[1]);
            }
            catch (Exception e)
            {
                var msg = string.Format("Can not get {0} index , where name = {1}, it's not exist or it's not a number.", type, name);
                LogMgr.Error(msg);
                throw new Exception(msg, e);
            }

        }
    }
}