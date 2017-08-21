using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;

namespace MMI.Facility.Control.Data
{
    class TextUIObjectParser : IUIObjectParser
    {
        public List<IUIObject> Parser(string file)
        {
            // 从文件中读取对象配置信息


            var tmpStrArray = File.ReadAllLines(file, Encoding.Default);
            var objectFileContent =
                tmpStrArray.Select(str => str.Replace('\t', ' '))
                           .Select(tmpString => tmpString.Trim())
                           .Where(tmpString => tmpString.Length > 0 && !tmpString[0].Equals(';'))
                           .ToList();


            return ( from obj in objectFileContent
                     let uio = new UIObject()
                     where uio.InitParaFromString(obj)
                     select uio ).Cast<IUIObject>().ToList();
        }
    }
}
