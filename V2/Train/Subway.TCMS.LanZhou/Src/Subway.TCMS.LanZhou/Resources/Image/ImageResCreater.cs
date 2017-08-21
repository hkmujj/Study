using System;
using System.IO;
using System.Text;

namespace Subway.TCMS.LanZhou.Resources.Image
{
    internal class ImageResCreater
    {
        // ReSharper disable once UnusedMember.Local
        private static void FormatNames()
        {
            var dir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Src\Subway.TCMS.LanZhou\Resources\Image\Details\"));

            var files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fl = file.Replace("-", "");

                File.Move(file, fl);
            }
        }

        // ReSharper disable once UnusedMember.Local
        private static void Create()
        {
            var f = "    <ImageSource x:Key=\"Subway.TCMS.LanZhou.{0}\">pack://application:,,,/Subway.TCMS.LanZhou;component/Resources/Image/Details/{1}</ImageSource>\r\n";

            var dir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Src\Subway.TCMS.LanZhou\Resources\Image\Details\"));

            var files = Directory.GetFiles(dir, "*.png", SearchOption.AllDirectories);

            var sb = new StringBuilder();

            sb.AppendLine(
                "<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");

            string lastRelativePath = null;

            foreach (var file in files)
            {
                
                var relativePath = Path.GetDirectoryName(file).Replace(dir, "");
                if (lastRelativePath != relativePath)
                {
                    lastRelativePath = relativePath;
                    sb.AppendLine();
                }

                var key = relativePath.Replace("\\", "") + Path.GetFileNameWithoutExtension(file);
                var value = relativePath.Replace("\\", "/") + "/" + Path.GetFileName(file);


                sb.AppendFormat(f, "Img" + key, value);
            }

            sb.AppendLine();
            sb.AppendLine("</ResourceDictionary>");

            var s = sb.ToString();

            var target = Path.Combine(dir, "../TCMSLanZhouImageResource.xaml");

            File.WriteAllText(target, s);
        }
    }
}