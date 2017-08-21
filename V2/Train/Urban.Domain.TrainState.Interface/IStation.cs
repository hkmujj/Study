namespace Urban.Domain.TrainState.Interface
{
    public interface IStation
    {
        /// <summary>
        /// 序号
        /// </summary>
        int Number { get; }

        /// <summary>
        /// 站名
        /// </summary>
        string Name { get; }
    }
}