namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 车站
    /// </summary>
    public interface IStation : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// 当前车站
        /// </summary>
        string CurrentStation { set; get; }

        /// <summary>
        /// 当前开门侧
        /// </summary>
        OpenDoorLocation CurrentOpenDoorLocation { set; get; }
    }
}