using Engine.LCDM.HDX2.Entity.Attributes;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public enum MachineType
    {
        [ResourceKey(ResourceKeys.LocalMachine)]
        Local,
        [ResourceKey(ResourceKeys.ReservMachine)]
        Reser,
        [ResourceKey(ResourceKeys.SigleMachine)]
        Sigle,
    }
}