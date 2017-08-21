using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine._6A.Adapter.ConfigInfo;
using Engine._6A.Config;
using Engine._6A.Interface.ViewModel.SystemSeting;
using Excel.Interface;

namespace Engine._6A.Adapter.Adapter.Common.SystemSetting
{
    class PlateformInfoAdapter : ModelAdapterBase
    {
        public PlateformInfoAdapter(IEngineAdapter adapter) : base(adapter)
        {
            Init();
        }

        void Init()
        {
            var tmp = ExcelParser.Parser<PlateFormInfo>(GlobalParam.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory);

            var type1 = typeof(IPlateformInfoViewModel);

            foreach (var info in type1.GetProperties())
            {
                foreach (var formInfo in tmp.Where(formInfo => formInfo.Name == info.Name))
                {
                    info.SetValue(Adapter.Model.SystemSetting.PlateformInfo, formInfo.Content, null);
                }
            }

            var tmp2 =
                ExcelParser.Parser<VersionInfo>(
                    GlobalParam.InitParam.DataPackage.Config.SystemDicrectory.SystemConfigDirectory).ToList();
            var type2 = typeof(IVersionInfoViewModel);
            foreach (var info in type2.GetProperties())
            {
                foreach (var versionInfo in tmp2.Where(versionInfo => versionInfo.Name == info.Name))
                {
                    info.SetValue(Adapter.Model.SystemSetting.VersionInfo, versionInfo.Version, null);
                }
            }
        }
    }
}
