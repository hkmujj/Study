namespace Urban.NanJing.NingTian.DDU.通讯状态
{
    /// <summary>
    /// 功能描述：通讯信息模块info
    /// 创建人：唐林
    /// 创建时间：2014-07-24
    /// </summary>
    public class MessageModeInfo
    {
        /// <summary>
        /// 读取或设置编号属性
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 读取或设置名称属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public MessageModeInfo(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}