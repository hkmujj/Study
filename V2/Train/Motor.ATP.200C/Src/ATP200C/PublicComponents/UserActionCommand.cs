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

        [Description("上")]
        Up,

        [Description("下")]
        Down,

        [Description("左")]
        Left,

        [Description("右")]
        Right,

        [Description("删除")]
        Delete,

        [Description("确定")]
        Ok,

        [Description("取消")]
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