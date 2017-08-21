using System.IO;
using System.Text;

namespace Motor.HMI.CRH380BG.Resources.Image
{
    public class ImageResourceCreater
    {

        // ReSharper disable once UnusedMember.Local
        private static void Create()
        {
            var f = "<ImageSource x:Key=\"{0}\">Details/{1}</ImageSource>\r\n";

            var dir = @"E:\Git\mmi\trunk\V2\Train\Motor.HMI.CRH380BG\Src\Motor.HMI.CRH380BG\Resources\Image\Details";

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