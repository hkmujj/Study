using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Model.BtnStragy
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

        /// <summary>
        /// 响应按下
        /// </summary>
        /// <param name="parameter"></param>
        void ResponseMouseDown(CommandParameter parameter);

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        void ResponseMouseUp(CommandParameter parameter);
    }
}