using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum MakeupAirState
    {
        [ResourceKey(ResourceKeys.MakeupAir)]
        Makeup,

        [ResourceKey(ResourceKeys.NotMakeupAir)]
        Not,

        [ResourceKey(ResourceKeys.Neutral)]
        Neutral,
    }
}