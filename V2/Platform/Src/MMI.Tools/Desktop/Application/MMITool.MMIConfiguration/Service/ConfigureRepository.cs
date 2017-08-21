using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.DataType.Config;
using MMITool.Addin.MMIConfiguration.Common;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.Service
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigureRepository
    {
        /// <summary>
        /// 所有的类型映射关系 
        /// </summary>
        public ReadOnlyCollection<ConfigTypeMapInfo> AllConfigTypeMapInfos { private set; get; }

        public void Initalize()
        {
            var types =
                typeof (SystemConfig).Assembly.GetTypes()
                    .Where(w => w.GetCustomAttributes(typeof (ConfigureDescriptionAttribute), false).Any())
                    .Select(
                        s =>
                            new Tuple<Type, ConfigureDescriptionAttribute>(s,
                                (ConfigureDescriptionAttribute)
                                    s.GetCustomAttributes(typeof (ConfigureDescriptionAttribute), false).First()));
        

            AllConfigTypeMapInfos = (from target in types
                                     let view =
                                         ViewMappedConfigTypeAttributeHelper.ViewMappedConfigTypeCollection.FirstOrDefault(
                                             f => f.ConfigType == target.Item1)
                                     where view != null
                                     select new ConfigTypeMapInfo(target.Item1, target.Item2, view)).ToList().AsReadOnly();
        }
    }
}