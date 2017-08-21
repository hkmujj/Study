using System.IO;
using System.Text;

namespace Engine.Angola.Diesel.Resource.Image
{
    class ImageKeyCreater
    {
        private static void Te()
        {
            //< ImageSource x: Key = "IndicatorLogo" > DetailImages / IndicatorLogo.png </ ImageSource >

            var sb = new StringBuilder();

            var files =
                Directory.GetFiles(
                    @"E:\Git\mmi\trunk\V2\Train\Subway.Angola.Diesel\Src\Subway.Angola.Diesel\Resource\Image\DetailImages",
                    "*.png");

            foreach (var f in files)
            {
                sb.AppendFormat(
                    "<ImageSource x:Key=\"{0}\" >DetailImages/{1}</ImageSource>",
                    Path.GetFileNameWithoutExtension(f), Path.GetFileName(f));
            }

            // ReSharper disable once UnusedVariable
            var s = sb.ToString();
        }
    }
}
