namespace Motor.ATP._300T.共用
{
    public class BtnInfo
    {
        public BtnInfo(int btnId)
        {
            BtnType = (ButtonType)btnId;
            IsPressResponsed = false;
            IsUpResponsed = false;
        }

        public ButtonType BtnType { private set; get; }

        /// <summary>
        /// 按钮编号
        /// </summary>
        public int BtnId { get { return (int)BtnType; } }

        /// <summary>
        /// 当前编号的按钮是否按下
        /// </summary>
        public bool IsPressResponsed { get; private set; }

        /// <summary>
        /// 按键弹起被响应
        /// </summary>
        public bool IsUpResponsed { private set; get; }

        /// <summary>
        /// 当前按钮已经按下
        /// </summary>
        public void ResponsePress()
        {
            IsPressResponsed = true;
        }

        public void ResponseUp()
        {
            IsUpResponsed = true;
        }
    }
}