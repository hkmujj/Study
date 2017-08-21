using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Engine.LCDM.HDX2.Entity.Controls
{
    public class HXD2TextBox : TextBox
    {
        static HXD2TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HXD2TextBox), new FrameworkPropertyMetadata(typeof(HXD2TextBox)));
        }
    }
}
