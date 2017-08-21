using System;
using System.Windows;
using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Entity.Extension;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    /// <summary>
    /// ³µÁ¾¸öÊý
    /// </summary>
    public enum VehicleCount
    {
        [ResourceKey(ResourceKeys.Double)]
        Multi,

        [ResourceKey(ResourceKeys.Single)]
        Single,
    }
}