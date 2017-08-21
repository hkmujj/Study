using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LightRail.HMI.SZLHLF.Extension
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
