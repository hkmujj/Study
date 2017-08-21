using System;
using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    /// <summary>
    /// ����,����
    /// </summary>
    public enum ReserveCommon
    {
        /// <summary>
        /// ���� 
        /// </summary>
        [ResourceKey(ResourceKeys.Often)]
        Common,

        /// <summary>
        /// ����
        /// </summary>
        [ResourceKey(ResourceKeys.Reserve)]
        Reserve,
    }
}