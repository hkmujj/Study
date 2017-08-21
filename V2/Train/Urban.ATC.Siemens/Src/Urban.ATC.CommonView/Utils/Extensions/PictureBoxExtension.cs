using System;
using System.Drawing;
using System.Windows.Forms;

namespace Motor.ATP.CommonView.Utils.Extensions
{
    public static class PictureBoxExtension
    {
        private static readonly Action<PictureBox, Image> UpdateImageAction = (pbox, img) => pbox.Image = img;

        public static void InvokeUpdateImageIfNeed(this PictureBox pbox, Image image)
        {
            pbox.InvokeIfNeed(UpdateImageAction, pbox, image);
        }
    }
}