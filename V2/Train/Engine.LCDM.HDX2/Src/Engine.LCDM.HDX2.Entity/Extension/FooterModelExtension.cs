using System;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.LCDM.HDX2.Entity.Model;
using Engine.LCDM.HDX2.Entity.Model.Domain;

namespace Engine.LCDM.HDX2.Entity.Extension
{
    public static class FooterModelExtension
    {
        public static void ChangeNewSettingByBuffer(this FooterModel footerModel, int index)
        {
            footerModel.SettingBuffer[index] = ((Enum)footerModel.SettingBuffer[index]).GetNextEnum();
            footerModel.NewSettings[index] = ((Enum)footerModel.SettingBuffer[index]).GetResourceKey();
        }

        public static void CreateSettingByBuffer(this FooterModel footerModel)
        {
            var keys = footerModel.SettingBuffer.Select(s => s != null ? s is Enum ? ((Enum)s).GetResourceKey() : s.ToString() : null).ToArray();

            footerModel.NewSettings = null;

            footerModel.CurrentSettings = new ObservableCollection<string>(keys);
        }
    }
}