using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum EngineControlModel
    {
        /// <summary>
        /// 主控
        /// </summary>
        [ResourceKey(ResourceKeys.MainControl)]
        Main,

        /// <summary>
        /// 从控
        /// </summary>
        [ResourceKey(ResourceKeys.AssistControl)]
        Assist,
    }
}