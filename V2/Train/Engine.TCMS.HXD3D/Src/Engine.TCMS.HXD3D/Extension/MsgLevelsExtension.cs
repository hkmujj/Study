using MsgReceiveMod;

namespace Engine.TCMS.HXD3D.Extension
{
    public static class MsgLevelsExtension
    {
        public static string ToFaultName(this MsgLevels levels)
        {
            switch (levels)
            {
                case MsgLevels.Level0:
                    return "A1";

                case MsgLevels.Level1:
                    return "A2";

                case MsgLevels.Level2:
                    return "A3";

                case MsgLevels.Level3:
                    return "C";

                case MsgLevels.Level4:
                    return "L";
            }

            return "";
        }
    }
}