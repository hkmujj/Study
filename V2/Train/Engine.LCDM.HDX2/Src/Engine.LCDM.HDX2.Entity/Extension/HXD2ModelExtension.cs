using Engine.LCDM.HDX2.Entity.Model;

namespace Engine.LCDM.HDX2.Entity.Extension
{
    public static class HXD2ModelExtension
    {
        /// <summary>
        /// 重新触发 状态变化事件。
        /// </summary>
        /// <param name="model"></param>
        public static void ReRaiseStateInterfaceChanged(this HXD2Model model)
        {
            var state = model.StateInterface;
            model.StateInterface = null;
            model.StateInterface = state;
        }
    }
}