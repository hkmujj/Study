using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonUtil.Util;

namespace Excel.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExcelParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Parser<T>(string fileDirectory) where T : ISetValueProvider
        {
            var type = typeof(T);

            var location =
                type.GetCustomAttributes(typeof(ExcelLocationAttribute), false).FirstOrDefault() as
                    ExcelLocationAttribute;
            if (location == null)
            {
                LogMgr.Warn("{0} has not {1} data, can not parser.", type, typeof(ExcelLocationAttribute));
                yield break;
            }


            var fields = type.GetFields();

            var props = type.GetProperties();

            var fs = (from info in fields
                      let f =
                          info.GetCustomAttributes(typeof(ExcelFieldAttribute), false).FirstOrDefault() as
                              ExcelFieldAttribute
                      where f != null
                      select new Tuple<FieldInfo, ExcelFieldAttribute>(info, f)).ToList();

            var ps = (from info in props
                      let f =
                          info.GetCustomAttributes(typeof(ExcelFieldAttribute), false).FirstOrDefault() as
                              ExcelFieldAttribute
                      where f != null
                      select new Tuple<PropertyInfo, ExcelFieldAttribute>(info, f)).ToList();

            var cls = fs.Select(s => new ColoumnConfig()
            {
                IsPrimaryKey = s.Item2.IsPrimaryKey,
                Name = s.Item2.Field,
            }).ToList();

            cls.AddRange(ps.Select(s => new ColoumnConfig()
            {
                IsPrimaryKey = s.Item2.IsPrimaryKey,
                Name = s.Item2.Field,
            }));

            var config = new ExcelReaderConfig()
            {
                File = Path.Combine(fileDirectory, location.File),
                SheetNames = new List<string>() { location.Sheet },
                Coloumns = cls,
            };

            var dt = config.Adapter();

            foreach (DataRow row in dt.Tables[location.Sheet].Rows)
            {
                var ins = Activator.CreateInstance<T>();

                foreach (var t in fs)
                {
                    var data = row[t.Item2.Field].ToString();

                    var values = DynamicInvokeHelper.DynamicInvoke(t.Item1.FieldType, data);
                    if (values == null)
                    {
                        ins.SetValue(t.Item1.Name, data);
                    }
                    else
                    {
                        t.Item1.SetValue(ins, values);
                    }
                }

                foreach (var t in ps)
                {
                    var data = row[t.Item2.Field].ToString();

                    var values = DynamicInvokeHelper.DynamicInvoke(t.Item1.PropertyType, data);
                    if (values == null)
                    {
                        ins.SetValue(t.Item1.Name, data);
                    }
                    else
                    {
                        t.Item1.SetValue(ins, values, null);
                    }


                    //var data = row[t.Item2.Field].ToString();
                    //if (t.Item1.PropertyType == typeof(string))
                    //{
                    //    t.Item1.SetValue(ins, data, null);
                    //}

                }

                yield return ins;
            }
        }

        private static void Test()
        {
            var d = Parser<MyClass>("D:\\").ToList();
        }



        [ExcelLocation("ATP200CΩ”ø⁄  ≈‰_InBool.xls", "Sheet1")]
        private class MyClass : ISetValueProvider
        {
            [ExcelField("Name", true)]
            public string Name1;

            [ExcelField("Index")]
            public int Index { set; get; }


            public void SetValue(string name, string value)
            {
                switch (name)
                {
                    case "Name1":
                        Name1 = value;
                        break;
                    case "Index":
                        Index = Convert.ToInt32(value);
                        break;
                }
            }
        }

    }
}