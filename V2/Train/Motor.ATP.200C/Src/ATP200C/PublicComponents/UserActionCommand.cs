using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CommonUtil.Util;

namespace ATP200C.PublicComponents
{
    public enum UserActionCommand
    {
        [Description]
        Unknown,

        [Description("��")]
        Up,

        [Description("��")]
        Down,

        [Description("��")]
        Left,

        [Description("��")]
        Right,

        [Description("ɾ��")]
        Delete,

        [Description("ȷ��")]
        Ok,

        [Description("ȡ��")]
        Cancel,

    }

    public static class UserActionCommandHelper
    {
        private static Dictionary<UserActionCommand, List<string>> m_CmdDescriptionDictionary;

        static UserActionCommandHelper()
        {
            m_CmdDescriptionDictionary = Enum.GetValues(typeof (UserActionCommand))
                                             .Cast<UserActionCommand>()
                                             .ToDictionary(kvp => kvp, kvp => EnumUtil.GetDescription(kvp));
        }

        public static UserActionCommand GetActionCommand(string description)
        {
            return ( from kvp in m_CmdDescriptionDictionary where kvp.Value.Contains(description) select kvp.Key ).FirstOrDefault();
        }
    }
}