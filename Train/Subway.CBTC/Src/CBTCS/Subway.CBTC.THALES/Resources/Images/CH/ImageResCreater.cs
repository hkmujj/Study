using System;
using System.IO;
using System.Text;

namespace Subway.CBTC.THALES.Resources.Images.CH
{
    internal static class ImageResCreater
    {
        public static void Create()
        {
            var dir =
                Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Src\CBTCS\Subway.CBTC.QuanLuTong\Resources\Images\CH"));

            var format = "<ImageSource x:Key=\"Subway.CBTC.THALES.Images{0}\">CH{1}</ImageSource>\r\n";

            var imgs = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);

            var sb = new StringBuilder();

            sb.Append(
                "<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"><!--ReSharper disable InconsistentNaming--> ");

            foreach (var i in imgs)
            {
                sb.AppendFormat(format, i.Replace(dir, "").Replace(".png","").Replace("\\","."), i.Replace(dir,"").Replace("\\","/"));
            }
            sb.Append("<!--  ReSharper restore InconsistentNaming  --></ResourceDictionary> ");
            // ReSharper disable once UnusedVariable
            var s = sb.ToString();

            var target = Path.Combine(dir, @"..\ImageResource_CH.xaml");

            File.WriteAllText(target, s, Encoding.Unicode);
        }
    }
}