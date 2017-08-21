using System;
using System.Data;
using CommonUtil.Util;
using MMI.Facility.Interface;
using Motor.HMI.CRH5G.Config.ConfigModel;

namespace Motor.HMI.CRH5G.底层共用
{
    public static class CommunicationDataIndexExtension
    {
        public static int FindIndex(this DataSet ds, string name)
        {
            try
            {
                var row = ds.Tables[0].Rows.Find(name);
                try
                {
                    return Convert.ToInt32(row[1]);
                }
                catch (Exception e)
                {
                    LogMgr.Error(string.Format("Can not convert index ({0}) to int where name = {1}. {2}", row[1], name, e));
                    return int.MaxValue;
                }

            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not find {0} in Communicatin data configs. {1}", name, e));
                return int.MaxValue;
            }
        }

        public static int FindIndex(this GlobalParam globalParam, CommunicationDataType dataType, string name)
        {
            return globalParam.CommunicationDataIndexDictionary[dataType].FindIndex(name);
        }

        public static int FindIndex(this GlobalParam globalParam, CommunicationDataModel dataModel)
        {
            return globalParam.FindIndex(dataModel.DataType, dataModel.Name);
        }

        public static int FindIndex(this CommunicationDataModel dataModel, baseClass baseClass)
        {
            int index = -1;
            switch (dataModel.DataType)
            {
                case CommunicationDataType.InBool:
                    index = baseClass.GetInBoolIndex(dataModel.Name);
                    break;
                case CommunicationDataType.InFloat:
                    index = baseClass.GetInFloatIndex(dataModel.Name);
                    break;
                case CommunicationDataType.OutBool:
                    index = baseClass.GetOutBoolIndex(dataModel.Name);
                    break;
                case CommunicationDataType.OutFloat:
                    index = baseClass.GetOutFloatIndex(dataModel.Name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return index;
        }

    }
}