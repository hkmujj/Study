using System.Collections.Generic;
using System.Linq;
using ES.Facility.PublicModule.Util;
using MMI.Facility.Control.Data;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.DataType.Config
{
    class XmlUIObjectParser : IUIObjectParser
    {
        public List<IUIObject> Parser(string file)
        {
            return DataSerialization.DeserializeFromXmlFile<List<UIObject>>(file, "root").Cast<IUIObject>().ToList();
        }
    }
}
