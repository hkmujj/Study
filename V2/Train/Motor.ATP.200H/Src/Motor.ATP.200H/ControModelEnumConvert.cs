using System;

namespace Motor.ATP._200H
{
    public static class ControModelEnumConvert
    {
        public static ControModelEnum ConvertFrom(float fromSignal)
        {
            return ConvertFrom(Convert.ToInt32(fromSignal));
        }

        private static ControModelEnum ConvertFrom(int model)
        {
            ControModelEnum currentModel;
            switch (model)
            {
                case 1:
                    currentModel = ControModelEnum.完全;
                    break;
                case 2:
                    currentModel = ControModelEnum.部分;
                    break;
                case 3:
                    currentModel = ControModelEnum.目视;
                    break;
                case 4:
                    currentModel = ControModelEnum.引导;
                    break;
                case 5:
                    currentModel = ControModelEnum.调车;
                    break;
                case 6:
                    currentModel = ControModelEnum.LKJ;
                    break;
                case 7:
                    currentModel = ControModelEnum.待机;
                    break;
                case 8:
                    currentModel = ControModelEnum.隔离;
                    break;
                case 10:
                    currentModel = ControModelEnum.冒进;
                    break;
                case 11:
                    currentModel = ControModelEnum.冒进后;
                    break;

                default:
                    currentModel = ControModelEnum.Null;
                    break;
            }
            return currentModel;
        }
    }
}