namespace Urban.ATC.Domain.Interface.ViewStates
{
    public enum DoorRelease
    {
        Initial,
        OpenLeft,
        Openright,
        OpenBoth,
        OpenLeftfirst,
        OpenRightfirst,
        NoDoorSupervision,
        PermissiveReleaseDoor,
        ClosedAndLockedLevel1,
        ClosedAndLockedLevel2,
        ClosedAndLockedLevel3,
        ClosedAndLockedLevel4,
        ClosedAndLockedLevel5,
        ClosedAndLockedLevel6,
    }
}