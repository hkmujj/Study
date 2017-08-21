using CommonUtil.Util;

namespace Motor.ATP._200H.ConfigModel
{
    public class ATP200HDetailConfig
    {
        public const string FileName = "ATP200HDetailConfig.xml";

        public UsedFor UsedFor { set; get; }

        public AdapterConfig AdapterConfig { get; set; }


        // ReSharper disable once UnusedMember.Local
        private void Test()
        {
            var d = new ATP200HDetailConfig() {AdapterConfig = new AdapterConfig()};
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }

    public enum UsedFor
    {
        CRH1A,
        CRH2A,
    }
}