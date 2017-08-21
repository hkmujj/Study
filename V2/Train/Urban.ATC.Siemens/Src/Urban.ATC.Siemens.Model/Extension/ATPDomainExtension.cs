namespace Motor.ATP.Domain.Model.Extension
{
    public static class ATPDomainExtension
    {
        /// <summary>
        /// 设置整个屏的可见状态
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetAllVisible(this ATPDomain atp, bool visible)
        {
            atp.Visible = visible;
            atp.SetRegionAVisible(visible);
            atp.SetRegionBVisible(visible);
            atp.SetRegionCVisible(visible);
            atp.SetRegionDVisible(visible);
            atp.SetRegionEVisible(visible);
            atp.SetRegionFVisible(visible);
        }

        /// <summary>
        /// 设置 A 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionAVisible(this ATPDomain atp, bool visible)
        {
            atp.WarningIntervention.BrakeWarningVisible = visible;
            atp.WarningIntervention.TargetDistanceVisible = visible;
            atp.WarningIntervention.Visible = visible;
        }

        /// <summary>
        /// 设置 B 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionBVisible(this ATPDomain atp, bool visible)
        {
            atp.TrainInfo.Speed.Visible = visible;
        }

        /// <summary>
        /// 设置 C 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionCVisible(this ATPDomain atp, bool visible)
        {
            atp.CTCS.Visible = visible;
            atp.TrainInfo.Brake.Visible = visible;
        }

        /// <summary>
        /// 设置 D 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionDVisible(this ATPDomain atp, bool visible)
        {
            atp.SpeedMonitoringSection.Visible = visible;
            atp.CabSignal.Visible = visible;
        }

        /// <summary>
        /// 设置 E 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionEVisible(this ATPDomain atp, bool visible)
        {
            atp.Message.Visible = visible;
            atp.TrainInfo.KilometerPost.Visible = visible;
            atp.TrainControlTypeVisible = visible;
            atp.TrainInfo.Driver.TrainIdVisible = visible;
            atp.TrainInfo.ConnectState.Visible = visible;
        }

        /// <summary>
        /// 设置 F 区可见
        /// </summary>
        /// <param name="atp"></param>
        /// <param name="visible"></param>
        public static void SetRegionFVisible(this ATPDomain atp, bool visible)
        {
            atp.RegionFStateProvier.Visible = visible;
        }
    }
}