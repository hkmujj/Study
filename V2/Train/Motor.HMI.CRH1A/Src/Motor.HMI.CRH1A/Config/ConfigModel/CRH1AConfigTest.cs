using Motor.HMI.CRH1A.Util;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    static class CRH1AConfigTest
    {
        public static void Test()
        {
            var data = new CRH1AConfig() {DebugConfig = new CRH1ADebug()
            {
                AutoLightUpScreen = true,
                Name = "22"
            }};

            //DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");

            var d1 = DataSerialization.DeserializeFromXmlFile<CRH1AConfig>("D:\\a.xml");
        }
    }
}
