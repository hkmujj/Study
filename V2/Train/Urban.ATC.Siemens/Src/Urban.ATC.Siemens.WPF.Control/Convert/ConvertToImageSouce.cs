using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Urban.ATC.Siemens.WPF.Control.Convert
{
    public static class ConvertToImageSouce
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        private static readonly Dictionary<Image, ImageSource> Image = new Dictionary<Image, ImageSource>();

        public static ImageSource ChangeImageToImageSouce(Image image)
        {
            if (Image.ContainsKey(image))
            {
                return Image[image];
            }
            var hBit = (new Bitmap(image)).GetHbitmap();
            ImageSource wpfImage = Imaging.CreateBitmapSourceFromHBitmap(
                hBit, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()
                );
            if (!DeleteObject(hBit))
            {
                throw new Win32Exception();
            }
            Image.Add(image, wpfImage);
            return wpfImage;
        }
    }
}