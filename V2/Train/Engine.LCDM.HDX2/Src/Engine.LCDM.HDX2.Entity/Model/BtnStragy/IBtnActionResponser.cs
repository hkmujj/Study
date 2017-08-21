namespace Engine.LCDM.HDX2.Entity.Model.BtnStragy
{
    public interface IBtnActionResponser
    {
        /// <summary>
        /// 响应按键按下
        /// </summary>
        void ResponseMouseDown();

        /// <summary>
        /// 响应按键弹起
        /// </summary>
        void ResponseMouseUp();
    }
}