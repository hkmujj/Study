using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Engine.LCDM.HXD3.Extentions
{
    public static class UsercontrolExtention
    {
        public static void ResourceChanged(this UserControl control, object sender, EventArgs args)
        {
            if (control.Resources.MergedDictionaries.Any())
            {
                control.Resources.MergedDictionaries.Clear();
            }
            control.Resources.MergedDictionaries.Add(sender as ResourceDictionary);
        }
    }
}