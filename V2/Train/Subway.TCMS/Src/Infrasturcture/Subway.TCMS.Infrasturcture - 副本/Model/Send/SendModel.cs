namespace Subway.TCMS.Infrasturcture.Model.Send
{
    /// <summary>
    /// 封装发送的数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SendModel<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public SendModel(T data)
        {
            Data = data;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; private set; }
    }
}