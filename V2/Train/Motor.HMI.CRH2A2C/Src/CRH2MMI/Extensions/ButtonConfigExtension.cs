using System;
using System.Drawing;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.Screen;

namespace CRH2MMI.Extensions
{
    public static class ButtonConfigExtension
    {
        public static CRH2Button ReflectButton(this ButtonConfig config)
        {
            var btn = new CRH2Button()
            {
                OutLineRectangle = config.Rectangle,
                DownImage = GlobalResource.Instance.BtnDownImage,
                UpImage = GlobalResource.Instance.BtnUpImage,
                TextColor = Color.White,
                Text = config.Content,
                Tag = config,
            };

            return btn;
        }
    }
}