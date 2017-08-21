namespace Urban.ATC.Siemens.WPF.Interface
{
    public interface INavagator
    {
        /// <summary>
        /// 导航到
        /// </summary>
        /// <param name="fullName">控件全名称</param>
        void NavagateTo(string fullName);
    }
}