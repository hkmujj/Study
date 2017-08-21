using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Interface
{
    public interface IPopupViewServiceProvider 
    {
        IPopupViewService PopupViewService { get; }
    }
}