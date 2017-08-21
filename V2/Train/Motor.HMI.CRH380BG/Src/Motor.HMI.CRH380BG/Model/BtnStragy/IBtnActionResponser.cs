using MMI.Facility.WPFInfrastructure.Interactivity;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Model.BtnStragy
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
        void ResponseClick(StateViewModel stateViewModel);


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