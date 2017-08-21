using System.Collections.Generic;
using Excel.Interface;


namespace Demo
{
    class CommunicationPortReaderConfigTest
    {
        public static void Test()
        {
            var data = new ExcelReaderConfig
            {
                Coloumns = new List<Excel.Interface.ColoumnConfig>
                {
                    new ColoumnConfig() {IsPrimaryKey = true, Name = "Name"},
                    new ColoumnConfig() {IsPrimaryKey = false, Name = "CRH2A"}
                },
                File = "CRH2型车接口适配.xls",
                SheetNames = new List<string> { "InBool" },
            };

           CommonUtil.Util. DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }
}
