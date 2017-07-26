using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace Motor.ATP.Infrasturcture.Resources.Strings
{
    internal sealed class PopupViewStringKeysCreater
    {

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void Create()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\Src\Infrastructure\Motor.ATP.Infrasturcture\Resources\Strings");

            var sb = new StringBuilder();

            sb.AppendFormat(
                "namespace {0}\r\n{{\r\n    /// <summary>\r\n    /// 弹出框内容的 键值\r\n    /// </summary>\r\n    " +
                "public static class PopupViewStringKeys{{",
                typeof(PopupViewStringKeysCreater).Namespace);

            var format = "public const string String{0} = \"{1}\";";

            using (var fs = new FileStream(Path.Combine(path, "StringResource_CH.xaml"), FileMode.Open, FileAccess.Read)
            )
            {
                var rd = (ResourceDictionary) XamlReader.Load(fs);

                foreach (string k in rd.Keys)
                {
                    sb.AppendFormat(format, k.Split('.').Last(), k);
                }
            }

            sb.Append("}}");

            var tar = Path.Combine(path, "PopupViewStringKeys.cs");

            File.WriteAllText(tar, sb.ToString());
        }
    }
}