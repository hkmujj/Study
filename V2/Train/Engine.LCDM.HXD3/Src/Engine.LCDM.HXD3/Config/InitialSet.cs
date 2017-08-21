using Excel.Interface;

namespace Engine.LCDM.HXD3.Config
{
     [ExcelLocation("配置表.xls", "Sheet1")]
     public class InitialSet:ISetValueProvider
     {
         [ExcelField("Name")]
         public string Name { get; set; }
         [ExcelField("Content")]
         public string Content { get; set; }

         public void SetValue(string propertyOrFieldName, string value)
         {
         }
    }
}
