namespace CommonUtil.Controls.Button
{
    /// <summary>
    /// 按键管理. 用于产生 Click事件
    /// </summary>
    public class BtnManager
    {
        private static BtnManager m_Instance;

        /// <summary>
        /// 
        /// </summary>
        public static BtnManager Instance
        {
            get { return m_Instance ?? (m_Instance = new BtnManager()); }
        }

        private BtnManager()
        {
            
        }

        /// <summary>
        /// 上次鼠标点下的按键
        /// </summary>
        public GDIButton LastMouseDownBtn { set; get; }

    }
}
