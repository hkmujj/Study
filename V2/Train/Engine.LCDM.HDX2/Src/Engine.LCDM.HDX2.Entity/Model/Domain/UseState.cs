using System;
using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum UseState
    {
        [ResourceKey(ResourceKeys.CutIn)]
        Using,

        [ResourceKey(ResourceKeys.CutOff)]
        Cutoff,
    }
}