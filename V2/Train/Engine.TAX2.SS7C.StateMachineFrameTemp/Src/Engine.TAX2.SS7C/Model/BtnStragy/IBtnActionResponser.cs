namespace Engine.TAX2.SS7C.Model.BtnStragy
{
    public interface IBtnActionResponser
    {
        ///// <summary>
        ///// 响应按键按下
        ///// </summary>
        //void ResponseMouseDown();

        ///// <summary>
        ///// 响应按键弹起
        ///// </summary>
        //void ResponseMouseUp();
        BtnItem Parent { set; get; }

        void UpdateState();

        /// <summary>
        /// 响应按键
        /// </summary>
        void ResponseClick();
    }
}