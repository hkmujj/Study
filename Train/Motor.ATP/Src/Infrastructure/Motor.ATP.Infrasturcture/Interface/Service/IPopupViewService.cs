using MMI.Facility.Interface.Service;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPopupViewService : IService
    {
        /// <summary>
        /// 激活一个弹出框, 如果参数为null 则取消弹出框
        /// </summary>
        /// <param name="popupView"></param>
        void Active(System.Windows.Forms.Control popupView);


        /// <summary>
        /// 激活一个弹出框, 如果参数为null 则取消弹出框
        /// </summary>
        /// <param name="popupView"></param>
        void ActiveView(PopViewParam popupView);
    }
}