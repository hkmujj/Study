using System;
using System.IO;

namespace Mmi.Common.StationReader
{
    internal class StationReaderTest
    {
        public static void Test()
        {
            var file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\车站信息模板.xls"));

            var reader = new StationReader();

            reader.Load(file);
        }
    }
}