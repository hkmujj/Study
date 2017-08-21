namespace Urban.ATC.Siemens.WPF.Interface.ViewStates
{
    public enum DoorRelease
    {
        Initial,
        OpenLeft,
        Openright,
        OpenBoth,
        OpenLeftfirst,
        OpenRightfirst,

        PermitLeft,
        PermitRight,
        PermitDouble,

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