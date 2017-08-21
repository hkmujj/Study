namespace Motor.HMI.CRH5A.底层共用
{
    public class BtnInfo
    {
        public BtnInfo(int btnId)
        {
            BtnId = btnId;
        }

        /// <summary>
        /// 按钮编号
        /// </summary>
        public int BtnId { get; private set; }


        /// <summary>
        /// 当前编号的按钮是否按下
        /// </summary>
        public bool IsPress { get; private set; }

        /// <summary>
        /// 当前按钮已经按下
        /// </summary>
        public void Press()
        {
            IsPress = true;
        }
    }
}