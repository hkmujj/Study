using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum AirBrakeLocation
    {
        [ResourceKey(ResourceKeys.InGoods)]
        InGoods,
        [ResourceKey(ResourceKeys.InPassenger)]
        InPassenger,
    }
}