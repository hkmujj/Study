using System;
using System.Linq;
using MMI.Facility.Interface;


namespace Motor.HMI.CRH3C380B.Common
{
    public static class ObjectManagerExtenstion
    {
        public static baseClass FindObjcet(this ObjectManager om, baseClass neighbour, Type type)
        {
            return om.AllObject.FirstOrDefault(f => f.RecPath == neighbour.RecPath && f.GetType() == type);
        }

        public static T FindObjcet<T>(this ObjectManager om, baseClass neighbour) where T : CRH3C380BBase
        {
            return om.FindObjcet(neighbour, typeof (T)) as T;
        }

    }
}