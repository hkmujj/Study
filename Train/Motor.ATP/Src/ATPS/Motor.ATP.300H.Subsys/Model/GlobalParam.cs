using System;
using System.IO;
using CommonUtil.Util;
using MMI.Facility.Interface.Project;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Config;

namespace Motor.ATP._300H.Subsys.Model
{
    public class GlobalParam : GlobalParamBase
    {
        public static readonly GlobalParam Instance = new GlobalParam();
    }
}