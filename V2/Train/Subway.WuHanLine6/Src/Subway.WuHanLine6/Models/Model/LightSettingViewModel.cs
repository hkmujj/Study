using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class LightSettingViewModel : ModelBase
    {
        private byte m_Light;
        private bool m_IsAuto;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        [ImportingConstructor]
        public LightSettingViewModel(LightSettingController controller)
        {
            Controller = controller;
            Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        public sealed override void Clear()
        {
            Light = 50;
            IsAuto = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public LightSettingController Controller { get; private set; }

        /// <summary>
        /// 亮度
        /// </summary>
        public byte Light
        {
            get { return m_Light; }
            set
            {
                if (value.Equals(m_Light))
                    return;
                m_Light = value;
                RaisePropertyChanged(() => Light);
            }
        }

        /// <summary>
        /// 是否是自动模式
        /// </summary>
        public bool IsAuto
        {
            get { return m_IsAuto; }
            set
            {
                if (value == m_IsAuto)
                    return;
                m_IsAuto = value;
                RaisePropertyChanged(() => IsAuto);
            }
        }
    }
}
