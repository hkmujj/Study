using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Model.UserAction
{
    public enum UserActionMeaning
    {
        Unkown,

        UpFreq = UserActionType.Max+1,

        DownFreq,

        Startup,

        Vigilant,
    }
}