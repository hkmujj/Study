namespace Urban.Phillippine.View.EventArgs
{
    public class NavigatorEventArgs
    {
        /// <summary>
        ///     需要导航到的区域
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        ///     需要导航到的控件全名称
        /// </summary>
        public string Name { get; set; }
    }
}