using System;
using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    /// <summary>
    /// 备用,常用
    /// </summary>
    public enum ReserveCommon
    {
        /// <summary>
        /// 常用 
        /// </summary>
        [ResourceKey(ResourceKeys.Often)]
        Common,

        /// <summary>
        /// 备用
        /// </summary>
        [ResourceKey(ResourceKeys.Reserve)]
        Reserve,
    }
}