using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.WuHanLine6.GIS.Config;
using Subway.WuHanLine6.GIS.Interfaces;

namespace Subway.WuHanLine6.GIS.Models
{
    internal class GlobalParams
    {
        static GlobalParams()
        {
            Instance = new GlobalParams();
        }
        public static GlobalParams Instance { get; private set; }
        public SubsystemInitParam InitParam { get; private set; }
        public IProjectIndexDescriptionConfig IndexDescription { get; private set; }
        public IList<StationName> StationNames { get; private set; }
        public IShell Right { get; set; }
        public IShell Left { get; set; }
        public void Initlization(SubsystemInitParam initParam)
        {
            if (initParam == null)
            {
                AppLog.Error($"方法：{nameof(Initlization)}  参数：{nameof(initParam)} 不能为Null");
                return;
            }
            InitParam = initParam;
            IndexDescription = initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(new CommunicationDataKey(initParam.AppConfig));
            Initlization(initParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory, initParam.AppConfig.AppPaths.ConfigDirectory);
        }
        public void Initlization(string root, string app)
        {
            if (string.IsNullOrEmpty(root) || string.IsNullOrEmpty(app))
            {
                AppLog.Error($"方法：{nameof(Initlization)} 参数:{nameof(root)} 不能为空 或者 参数：{nameof(app)} 参数不能为空！");
                return;
            }
            StationNames = ExcelParser.Parser<StationName>(app).ToList();

        }
    }
}