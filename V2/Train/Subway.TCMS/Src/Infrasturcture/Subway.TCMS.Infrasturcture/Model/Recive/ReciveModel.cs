namespace Subway.TCMS.Infrasturcture.Model.Recive
{
    /// <summary>
    /// 接口数据模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReciveModel<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        public ReciveModel(T data)
        {
            Data = data;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; private set; }
    }
}