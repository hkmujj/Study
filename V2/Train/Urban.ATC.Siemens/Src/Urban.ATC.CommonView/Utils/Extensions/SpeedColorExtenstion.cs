using System.Drawing;
using CommonUtil.Util;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP.CommonView.Utils.Extensions
{
    public static class SpeedColorExtenstion
    {
        public static Color GetColor(this ATPColor atpColor)
        {
            switch (atpColor)
            {
                case ATPColor.None:
                    return Color.Empty;
                case ATPColor.LightGrey:
                    return Color.LightGray;
                case ATPColor.Grey:
                    return Color.Gray;
                case ATPColor.Yellow:
                    return Color.Yellow;
                case ATPColor.Orange:
                    return Color.Orange;
                case ATPColor.Red:
                    return Color.Red;
                case ATPColor.White:
                    return Color.White;
                default:
                    LogMgr.Error(string.Format("Can not translate speed color(={0}) to color", atpColor));
                    return Color.Red;
            }
        }
    }
}