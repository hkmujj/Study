using System.ComponentModel;

namespace Urban.Iran.HMI.Common
{
    public enum PISType
    {
        [Description("站点")]
        Satation = 0,
        [Description("服务")]
        Service = 1,
        [Description("紧急")]
        Emergency = 2,
    }

    public enum SendOutboolType
    {
        AutoBroadcastFlg = 4801,
        ArriveBroadcast,
        DepartBroadcast,
        StopBroadcast,
        StopAnnouncement,
        EmergencyFlg
    }

    public enum SendFloatType
    {
        StartStation = 601,
        NextStation,
        EndStation,
        SkipStation,
        Announcement,

    }
}