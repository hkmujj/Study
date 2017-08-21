using System;
using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Model
{
    [DebuggerDisplay("ActionMeaning={ActionMeaning}, Time={DateTime}")]
    public class RecoredAction
    {
        public RecoredAction(UserActionType actionType) : this((UserActionMeaning) (int) actionType, DateTime.Now)
        {
        }

        public RecoredAction(UserActionMeaning actionMeaning, DateTime dateTime)
        {
            ActionMeaning = actionMeaning;
            DateTime = dateTime;
        }

        public RecoredAction(UserActionMeaning actionMeaning) : this(actionMeaning, DateTime.Now)
        {
        }

        public UserActionMeaning ActionMeaning { get; private set; }

        public DateTime DateTime { get; private set; }

    }
}