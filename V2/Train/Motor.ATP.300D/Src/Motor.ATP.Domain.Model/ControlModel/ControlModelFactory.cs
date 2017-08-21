using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using CommonUtil.Util;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model.ControlModel
{
    public class ControlModelFactory
    {
        public static ControlModelBase Create(ATPDomainBase atpDomain, ControlType controlType)
        {
            var typeName = string.Format("Motor.ATP.Domain.Model.ControlModel.{0}ControlModel", controlType);

            try
            {
                var type = Type.GetType(typeName);

                if (type != null)
                {
                    var obj = (ControlModelBase)Activator.CreateInstance(type, atpDomain);
                    return obj;
                }
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Can not get type of {0} , {1}", typeName, e));
            }

            return new UnkownControlModel(atpDomain);

        }
    }
}