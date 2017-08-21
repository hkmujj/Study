using System.Collections.Generic;
using MMI.Facility.Interface.Project;
using Urban.Phillippine.View.Config.KeyResouce;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config
{
    public class GlobalParam
    {
        public static SubsystemInitParam InitParam;
        public static IDictionary<string, UserInfo> AllUserConfig;
        public const string UserFileName = "UserConfig.xml";
        public static UserType UserType;
        public static UserInfo CurrentUserInfo;
        public static string CurrentUserName;

        public static int Train
        {
            get
            {
                return
                    (int)
                        InitParam.CommunicationDataService.ReadService.GetFloatAt(
                            IndexConfigure.IntFloatIndex[InFloatKeys.TrainNum]);
            }
        }
    }
}