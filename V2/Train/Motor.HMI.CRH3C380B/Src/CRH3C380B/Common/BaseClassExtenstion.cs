using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Facility.Interface;


namespace Motor.HMI.CRH3C380B.Common
{
    public static class BaseClassExtenstion
    {
        public static baseClass FindNeighbourObject(this baseClass obj, Type type)
        {
            return ObjectManager.Instance.FindObjcet(obj, type);
        }

        public static T FindNeighbourObject<T>(this baseClass obj) where T : CRH3C380BBase
        {
            return ObjectManager.Instance.FindObjcet<T>(obj);
        }

        public static void UpdateUiObject(this baseClass obj, CommunicationDataType dataType, IEnumerable<string> indexNames)
        {
            switch (dataType)
            {
                case CommunicationDataType.InBool :
                     obj.UIObj.InBoolList.AddRange(indexNames.Select(obj.GetInBoolIndex));
                    break;
                case CommunicationDataType.InFloat :
                    obj.UIObj.InFloatList.AddRange(indexNames.Select(obj.GetInFloatIndex));
                    break;
                case CommunicationDataType.OutBool :
                    obj.UIObj.OutBoolList.AddRange(indexNames.Select(obj.GetOutBoolIndex));
                    break;
                case CommunicationDataType.OutFloat :
                    obj.UIObj.OutFloatList.AddRange(indexNames.Select(obj.GetOutFloatIndex));
                    break;
                default :
                    throw new ArgumentOutOfRangeException("dataType");
            }
        }
    }
}