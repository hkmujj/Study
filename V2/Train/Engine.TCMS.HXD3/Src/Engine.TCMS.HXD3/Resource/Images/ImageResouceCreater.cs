using System;
using System.IO;
using System.Text;

namespace Engine.TCMS.HXD3.Resource.Images
{
    internal static class ImageResouceCreater
    {
        private static void Format()
        {
            var path = @"E:\Git\mmi\trunk\V2\Train\Engine.TCMS.HXD3\Src\Engine.TCMS.HXD3\Resource\Images\HXD3TCMS";
            var fs = Directory.GetFiles(path);
            foreach (var f in fs)
            {
                var p = new FileInfo(f);
                var des = p.FullName.Replace("-", "");
                des = des.Replace("_", "");
                p.MoveTo(des);
            }
        }

        private static void Create()
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var path = @"E:\Git\mmi\trunk\V2\Train\Engine.TCMS.HXD3\Src\Engine.TCMS.HXD3\Resource\Images\HXD3TCMS";

            var sb = new StringBuilder();

            //< ImageSource x: Key = "BtnImage" > HXD3TCMS / Button.png </ ImageSource >
            var fs = Directory.GetFiles(path);
            foreach (var f in fs)
            {
                var p = new FileInfo(f);
                sb.AppendFormat("<ImageSource x:Key=\"{0}\">HXD3TCMS/{1}</ImageSource>\r\n",
                    p.Name.Replace(p.Extension, ""),
                    p.Name);

            }

            var s = sb.ToString();
        }
    }
}