


using Excel.Interface;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var adpt = new ExcelAdapter();
            var ds = adpt.Adapter(CommonUtil.Util.DataSerialization.DeserializeFromXmlFile<Excel.Interface.ExcelReaderConfig>("x.xml"));
            //ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["Name"] };
            var d = ds.Tables[0].Rows.Find("2车电源电压/电池电压");
        }
    }
}
