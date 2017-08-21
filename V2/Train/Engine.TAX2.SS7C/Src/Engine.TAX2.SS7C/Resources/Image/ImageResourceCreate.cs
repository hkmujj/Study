using System.IO;
using System.Text;

namespace Engine.TAX2.SS7C.Resources.Image
{
    public class ImageResourceCreater
    {

        // ReSharper disable once UnusedMember.Local
        private static void Create()
        {
            var f = "<ImageSource x:Key=\"{0}\">Detail/{1}</ImageSource>\r\n";

            var dir = @"E:\Git\mmi\trunk\V2\Train\Engine.TAX2.SS7C\Src\Engine.TAX2.SS7C\Resources\Image\Detail";

            var files = Directory.GetFiles(dir, "*.png");

            var sb = new StringBuilder();

            foreach (var file in files)
            {
                sb.AppendFormat(f, Path.GetFileNameWithoutExtension(file), Path.GetFileName(file));
            }

            var s = sb.ToString();
        }
    }
}