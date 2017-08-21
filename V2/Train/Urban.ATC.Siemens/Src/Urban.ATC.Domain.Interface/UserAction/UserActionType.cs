namespace Motor.ATP.Domain.Interface.UserAction
{
    public enum UserActionType
    {
        None,

        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        B7,
        B8,
        B9,
        B10,
        B11,
    }

    public static class UserActionTypeExtension
    {
        public static bool IsInputData(this UserActionType actionType)
        {
            return actionType >= UserActionType.B1 && actionType <= UserActionType.B11;
        }

        public static bool IsInputControl(this UserActionType actionType)
        {
            return actionType >= UserActionType.F1 && actionType <= UserActionType.F8;
        }
    }
}