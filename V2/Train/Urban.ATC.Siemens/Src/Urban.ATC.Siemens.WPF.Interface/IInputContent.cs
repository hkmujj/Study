namespace Urban.ATC.Siemens.WPF.Interface
{
    public interface IInputContent
    {
        /// <summary>
        /// 提交输入数据
        /// </summary>
        /// <param name="sPara">提交的数据</param>
        void Sunbmit(string sPara);

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="sPara"></param>
        void Input(string sPara);
    }
}