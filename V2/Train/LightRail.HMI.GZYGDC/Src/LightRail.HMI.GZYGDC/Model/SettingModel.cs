using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace LightRail.HMI.GZYGDC.Model
{
    [Export]
    public class SettingModel : NotificationObject
    {
        private int m_SettingBrightness;


        public SettingModel()
        {
            

            //默认值
            SettingBrightness = 100;

            
        }


        /// <summary>
        /// 设定亮度百分比
        /// </summary>
        public int SettingBrightness
        {
            get { return m_SettingBrightness; }
            set
            {
                if (Equals(value, m_SettingBrightness))
                {
                    return;
                }

                m_SettingBrightness = value;

                RaisePropertyChanged(() => SettingBrightness);
            }
        }


    }
}