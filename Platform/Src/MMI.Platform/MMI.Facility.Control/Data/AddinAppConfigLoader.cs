using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.DataType;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;

namespace MMI.Facility.Control.Data
{
    internal class AddinAppConfigLoader : SubsystemAppConfigLoader
    {
        public override IAppConfig LoadAppConfig(IConfig rootConfig, string baseDirectory,
            ISubsystemConfig subsystemConfig)
        {

            var config = (AppConfig) base.LoadAppConfig(rootConfig, baseDirectory, subsystemConfig);

            LoadAppRootConfig(baseDirectory, config, subsystemConfig.Name);

            LoadOtherFiles(config);

            return config;
        }

        private void LoadOtherFiles(AppConfig config)
        {
            LoadViewConfig(config);
            LoadObjectConfig(config);
            LoadLogicConfig(config);
        }


        private void LoadAppRootConfig(string baseDirectory, AppConfig appConfig, string appName)
        {
            var configDicrectory = Path.Combine(baseDirectory, string.Format("{0}\\Config", appName));

            var config =
                DataSerialization.DeserializeFromXmlFile<AppConfig>(Path.Combine(configDicrectory, AppConfig.FileName));

            if (config != null)
            {
                appConfig.ActureFormViewConfig = config.ActureFormViewConfig;
                appConfig.AppFileConfig = config.AppFileConfig;
                appConfig.AppCommunicationInterfaceConfig = config.AppCommunicationInterfaceConfig;
            }
            appConfig.AppName = appName;
            appConfig.AppPaths = new AppPaths(baseDirectory, appConfig);

        }

        private void LoadLogicConfig(AppConfig config)
        {
            var logicConfig = new AppLogicConfigCollection() {ParentConfig = config};
            config.AppLogicConfig = logicConfig;
            // TODO 
            if (!File.Exists(config.AppPaths.LogicConfigFile))
            {
                SysLog.Error(
                    string.Format(
                        "Can not found logic config file ({0}), ignore read file, logic caculate can not use...",
                        config.AppPaths.LogicConfigFile));
                return;
            }
            var allLines = File.ReadAllLines(config.AppPaths.LogicConfigFile, Encoding.Default);
            foreach (var line in allLines)
            {
                var logic = new LogicObject() {ParentConfig = config};
                if (logic.InitParaFromString(line))
                {
                    logicConfig.AppLogicConfigDic.Add(logic.Index, logic);
                }
                else
                {
                    SysLog.Warn(string.Format("Can not parser string 【{0}】 of file 【{1}】 to logic config.", line,
                        config.AppPaths.LogicConfigFile));
                }
            }
        }

        private void LoadViewConfig(AppConfig config)
        {
            if (!File.Exists(config.AppPaths.ViewConfigFile))
            {
                throw new FileNotFoundException(string.Format("{0} not found. .", config.AppPaths.ViewConfigFile));
            }
            var tmpStrArray = File.ReadAllLines(config.AppPaths.ViewConfigFile, Encoding.Default);

            var allLines = tmpStrArray.Select(t => t.Replace('\t', ' '))
                .Select(s => s.Trim())
                .Where(s => s.Length > 0 && !s[0].Equals(';'))
                .ToList();

            var viewConfig = new AppViewConfig() {ParentConfig = config};
            config.AppViewConfig = viewConfig;
            foreach (var s in allLines)
            {
                var appViewUnit = new AppViewConfigUnit() {ParentConfig = viewConfig};
                var error = string.Empty;
                if (InitViewConfigUnitFromString(appViewUnit, s, ref error))
                {

                    if (viewConfig.AppViewConfigDic.ContainsKey(appViewUnit.Index))
                    {
                        SysLog.Error(
                            string.Format("you have regist the ViewConfig of index ={0}, we'll ignore this regist",
                                appViewUnit.Index));
                    }
                    else
                    {
                        viewConfig.AppViewConfigDic.Add(appViewUnit.Index, appViewUnit);
                    }
                }
                else
                {
                    SysLog.Error(error);
                    SysLog.Error(string.Format("occuse some error where parser file {0}, line {1}",
                        config.AppPaths.ViewConfigFile, s));
                }
            }

        }

        private bool InitViewConfigUnitFromString(AppViewConfigUnit viewConfig, string content, ref string errorInfo)
        {
            var tmpStr = string.Empty;
            var tmpSourceStr = content.Replace('\t', ' ').Trim();

            //视图编号
            if (StringHelper.findStringByKey(tmpSourceStr, string.Empty, " ", ref tmpStr))
            {
                tmpStr = tmpStr.Trim();

                var tmpInt = -1;
                if (int.TryParse(tmpStr, out tmpInt))
                {
                    viewConfig.Index = tmpInt;
                }
                else
                {
                    errorInfo = string.Format("Can not parse string 【{0}】, view index should be int", tmpStr);
                    return false;
                }
            }

            //背景图片
            viewConfig.BgImage = null;
            if (StringHelper.findStringByKey(tmpSourceStr, "<", ">", ref tmpStr))
            {
                var file = viewConfig.ParentConfig.ParentConfig.AppPaths.ResourceDirectory + tmpStr.Trim();

                if (!File.Exists(file))
                {
                    //背景文件不存在
                    errorInfo = string.Format("Can not found backgroud image {0}", file);
                    return false;
                }

                viewConfig.BgImage = Image.FromFile(file);
            }

            if (StringHelper.findStringByKey(tmpSourceStr, "[#", "#]", ref tmpStr))
            {
                tmpStr = tmpStr.Trim();

                var tmpStringList = StringHelper.getValueBySpace(tmpStr, " ");

                if (tmpStringList.Count != 3) //字符信息必须达到预计个数
                {
                    errorInfo = string.Format("Can not parse string 【{0}】", tmpStr);
                    return false;
                }

                //开始按颜色GBA进行字符判断
                var tmpIntG = 0;
                var tmpIntB = 0;
                var tmpIntR = 0;

                if (int.TryParse(tmpStringList[0], out tmpIntG) &&
                    int.TryParse(tmpStringList[1], out tmpIntB) &&
                    int.TryParse(tmpStringList[2], out tmpIntR))
                {
                    viewConfig.BgColor = Color.FromArgb(tmpIntR, tmpIntG, tmpIntB);
                }
                else
                {
                    errorInfo = string.Format("Can not parse string 【{0}】, view backgroud color should be int", tmpStr);
                    return false;
                }
            }

            return true;
        }

        private void LoadObjectConfig(AppConfig config)
        {
            if (!File.Exists(config.AppPaths.ObjectConfigFile))
            {
                throw new FileNotFoundException(string.Format("{0} not found. .", config.AppPaths.ObjectConfigFile));
            }
            var ext = Path.GetExtension(config.AppPaths.ObjectConfigFile);
            if (string.IsNullOrEmpty(ext))
            {
                throw new FileLoadException(string.Format("{0} is not a file .", config.AppPaths.ObjectConfigFile));
            }
            IUIObjectParser objParser = null;
            switch (ext.ToLower())
            {
                case ".xml":
                    objParser = new XmlUIObjectParser();
                    break;

                case ".txt":
                    objParser = new TextUIObjectParser();
                    break;
                default:
                    throw new ArgumentException(string.Format("There is not UIObjectParser for {0} file.", ext));
            }

            var appobjConfg = new UIObjectConfig()
            {
                ParentConfig = config,
                UIObjects = objParser.Parser(config.AppPaths.ObjectConfigFile)
            };
            config.AppObjcetConfig = appobjConfg;

        }
    }
}