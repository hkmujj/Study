using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace Motor.ATP._200C.Subsys.Resources.String
{
    internal sealed class ATP200CStringKeysCreater
    {

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void Create()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\Src\ATPS\Motor.ATP.200C.Subsys\Resources\String");

            var sb = new StringBuilder();

            sb.AppendFormat(
                "namespace {0}\r\n{{\r\n    /// <summary>\r\n    /// 200C String 键值\r\n    /// </summary>\r\n    " +
                "public static class ATP200CStringKeys{{",
                typeof(ATP200CStringKeysCreater).Namespace);

            var format = "public const string String{0} = \"{1}\";";

            using (var fs = new FileStream(Path.Combine(path, "StringResource.xaml"), FileMode.Open, FileAccess.Read)
            )
            {
                var rd = (ResourceDictionary) XamlReader.Load(fs);

                foreach (string k in rd.Keys.OfType<string>().OrderBy(o => o))
                {
                    sb.AppendFormat(format, k.Split('.').Last(), k);
                }
            }

            sb.Append("}}");

            var tar = Path.Combine(path, "ATP200CStringKeys.cs");

            File.WriteAllText(tar, sb.ToString());
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void OrderKeys()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\Src\ATPS\Motor.ATP.200C.Subsys\Resources\String");

            var tarf = Path.Combine(path, "StringResource.xaml");

            var sb = new StringBuilder();
            sb.Append(
                "<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns:system=\"clr-namespace:System;assembly=mscorlib\"> \r\n\r\n");

            var format = "<system:String x:Key=\"{0}\">{1}</system:String>";

            using (
                var fs = new FileStream(tarf, FileMode.Open, FileAccess.Read)
            )
            {
                var rd = (ResourceDictionary) XamlReader.Load(fs);

                var last = string.Empty;
                var preLen = "Motor.ATP.200C.String.".Length;
                foreach (string k in rd.Keys.OfType<string>().OrderBy(o => o))
                {
                    if (last != string.Empty && last.Length >= preLen && k.Length >= preLen && last[preLen] != k[preLen])
                    {
                        sb.Append("\r\n\r\n");
                    }

                    sb.AppendFormat(format, k, rd[k]);

                    last = k;
                }

                sb.Append("\r\n\r\n</ResourceDictionary>");

            }

            File.WriteAllText(tarf, sb.ToString());

        }
    }
}