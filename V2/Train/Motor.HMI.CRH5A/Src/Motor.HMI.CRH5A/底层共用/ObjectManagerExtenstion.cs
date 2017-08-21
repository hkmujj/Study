using System;
using System.Linq;
using MMI.Facility.Interface;

namespace Motor.HMI.CRH5A.底层共用
{
    public static class ObjectManagerExtenstion
    {
        public static baseClass FindObjcet(this ObjectManager om, baseClass neighbour, Type type)
        {
            return om.AllObject.FirstOrDefault(f => f.RecPath == neighbour.RecPath && f.GetType() == type);
        }

        public static T FindObjcet<T>(this ObjectManager om, baseClass neighbour) where T : CRH5ABase
        {
            return om.FindObjcet(neighbour, typeof (T)) as T;
        }

    }
}