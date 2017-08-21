using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum AirBrakePressure
    {
        [ResourceKey(ResourceKeys.KP500)]
        KP500,
        [ResourceKey(ResourceKeys.KP600)]
        KP600,

    }
}