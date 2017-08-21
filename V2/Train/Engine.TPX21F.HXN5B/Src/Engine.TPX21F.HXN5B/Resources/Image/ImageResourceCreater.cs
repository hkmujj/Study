using System.IO;
using System.Text;

namespace Engine.TPX21F.HXN5B.Resources.Image
{
    public class ImageResourceCreater
    {

        // ReSharper disable once UnusedMember.Local
        private static void Create()
        {
            var f = "<ImageSource x:Key=\"{0}\">Details/{1}</ImageSource>\r\n";

            var dir = @"E:\Git\mmi\trunk\V2\Train\Engine.TPX21F.HXN5B\Src\Engine.TPX21F.HXN5B\Resources\Image\Details";

            var files = Directory.GetFiles(dir, "*.png");

            var sb = new StringBuilder();

            foreach (var file in files)
            {
                sb.AppendFormat(f, "Img" + Path.GetFileNameWithoutExtension(file), Path.GetFileName(file));
            }

            var s = sb.ToString();
        }
    }
}