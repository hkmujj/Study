using System;
using System.Linq;
using System.Windows.Controls;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Extension
{
    public static class UserControlExtension
    {
        public static void ResourceManagerOnResourceChanged(this UserControl control, object sender, EventArgs eventArgs)
        {
            if (control.Resources.MergedDictionaries.Any())
            {
                control.Resources.MergedDictionaries.Clear();
            }
            control.Resources.MergedDictionaries.Add(HXD2ResourceManager.Instance);
        }
    }
}