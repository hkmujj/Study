using System.Windows.Forms;
using MMI.Facility.Interface.Service;

namespace Motor.ATP.Domain.Interface.Service
{
    public interface IPopupViewService : IService
    {
        /// <summary>
        /// 激活一个弹出框, 如果参数为null 则取消弹出框
        /// </summary>
        /// <param name="popupView"></param>
        void Active(Control popupView);


        /// <summary>
        /// 激活一个弹出框, 如果参数为null 则取消弹出框
        /// </summary>
        /// <param name="popupView"></param>
        void ActiveView(PopViewParam popupView);
    }
}