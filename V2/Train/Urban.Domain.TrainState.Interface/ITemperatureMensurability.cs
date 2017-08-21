namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 温度可测量的
    /// </summary>
    public interface ITemperatureMensurability
    {
        double Temperature { set; get; } 
    }
}