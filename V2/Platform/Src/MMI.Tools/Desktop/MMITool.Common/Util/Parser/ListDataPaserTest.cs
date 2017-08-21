using System.Collections.Generic;
using System.Linq;

namespace MMITool.Common.Util.Parser
{
    class ListDataPaserTest
    {
        class Model
        {
            [DataMember(IsRequire = true, Order = 0)]
            public int ID { set; get; }

            [DataMember(IsRequire = true, Order = 1)]
            public string Name { set; get; }

            [DataMember(IsRequire = true, Order = 2)]
            public string Description { set; get; }
        }

        public static void Test()
        {
            var data = new List<List<string>>()
            {
                new List<string>(){ "1", "n1", "na1"},
                new List<string>(){ "2", "n2", "na2"},
            };
            var mod = new Model();
            foreach (var d in data)
            {
                mod.ID = d[0].AsInt();
                mod.Name = d[1];
            }

            var mods = ListDataPaser.Parse<Model>(data);

            data = new List<List<string>>()
            {
                new List<string>(){ "n1","1",  "na1"},
                new List<string>(){ "n2","2",  "na2"},

            };

            var mod2 = ListDataPaser.Parse<Model>(data, new[] {"Name", "ID"});

            var mod3 = ListDataPaser.Parse<Model>(data.FirstOrDefault());
        }
    }
}
