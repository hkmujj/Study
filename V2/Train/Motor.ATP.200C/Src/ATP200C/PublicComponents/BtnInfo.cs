namespace ATP200C.PublicComponents
{
    public class BtnInfo
    {
        public BtnInfo(BtnTypeName btnId)
        {
            BtnId = btnId;
        }

        /// <summary>
        /// 按钮编号
        /// </summary>
        public BtnTypeName BtnId { get; private set; }


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