using System;
using ATP200C.Domain;
using ATP200C.PublicComponents;

namespace ATP200C.MainView
{
    /// <summary>
    /// 列车信息
    /// </summary>
    public class TrainSpeedInfo
    {
        /// <summary>
        /// 列车速度
        /// </summary>
        public float Vtrain { get; set; }

        /// <summary>
        /// 允许速度
        /// </summary>
        public float Vperm { get; set; }

        /// <summary>
        /// 目标速度
        /// </summary>
        public float Vtarget { get; set; }

        /// <summary>
        /// 干预速度
        /// </summary>
        public float Vint { get; set; }

        /// <summary>
        /// 开口速度
        /// </summary>
        public float Vrelease { get; set; }

        /// <summary>
        /// 紧急制动速度
        /// </summary>
        public float VEmergent { set; get; }

        /// <summary>
        /// 速度指针的颜色
        /// </summary>
        public TrainInfoColor PointerColor
        {
            get { return GetPointerColor(); }
        }

        /// <summary>
        /// 允许速度环颜色
        /// </summary>
        public TrainInfoColor VpermCircleColor
        {
            get { return GetVpermCircleColor(); }
        }

        public TrainInfoColor VEmergentColor
        {
            get { return TrainInfoColor.Red; }
        }

        private TrainInfoColor GetVpermCircleColor()
        {
            switch (TrainControlMode.CurrentTrainControl)
            {

                case ControlModeName.CSM:
                    return TrainInfoColor.White;
                case ControlModeName.TSM:
                case ControlModeName.RSM:
                case ControlModeName.Other:
                    return TrainInfoColor.Yellow;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 目标速度环颜色
        /// </summary>
        public TrainInfoColor VtargetCircleColor
        {
            get { return GetVtargetCircleColor(); }
        }

        private TrainInfoColor GetVtargetCircleColor()
        {
            switch (TrainControlMode.CurrentTrainControl)
            {
                case ControlModeName.CSM:
                    return TrainInfoColor.White;
                case ControlModeName.TSM:
                case ControlModeName.RSM:
                case ControlModeName.Other:
                    return TrainInfoColor.Gray;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 干预速度环颜色
        /// </summary>
        public TrainInfoColor VintCircleColor
        {
            get { return GetVintCircleColor(); }
        }

        private TrainInfoColor GetVintCircleColor()
        {
            return TrainInfoColor.Orange;
        }

        /// <summary>
        /// 获取速度指针的颜色
        /// </summary>
        /// <returns></returns>
        private TrainInfoColor GetPointerColor()
        {
            if (ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS0 || ATPMain.Instance.CTCSLevel == CTCSLevel.CTCS1)
            {
                return TrainInfoColor.Gray;
            }

            switch (ATPMain.Instance.ControlModel)
            {
                case ControModelEnum.Null:
                    return TrainInfoColor.Gray;
                case ControModelEnum.完全:
                    return GetColor();
                case ControModelEnum.目视:
                    return GetColor();
                case ControModelEnum.部分:
                    return GetColor();
                case ControModelEnum.引导:
                    return GetColor();
                case ControModelEnum.调车:
                    return GetColor();
                case ControModelEnum.LKJ:
                    return GetColor();
                case ControModelEnum.待机:
                    return GetColor();
                case ControModelEnum.隔离:
                    if (Vtrain > 0)
                    {
                        return TrainInfoColor.Red;
                    }
                    return TrainInfoColor.Gray;
                case ControModelEnum.冒进:
                    return GetColor();
                case ControModelEnum.冒后:
                    return GetColor();
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private TrainInfoColor GetColor()
        {
            if (Vtrain > VEmergent)
            {
                return VEmergentColor;
            }
            if (Vtrain > Vint)
            {
                return ATPMain.Instance.BrakeLevel.HasEmergenceBrake() ? VEmergentColor : VintCircleColor;
            }
            if (Vtrain > Vperm)
            {
                return ATPMain.Instance.BrakeLevel.HasMaxServiceBrake() ? VEmergentColor : VintCircleColor;
            }
            if (Vtrain > Vtarget)
            {
                return VpermCircleColor;
            }
            return VtargetCircleColor;
        }
    }
}