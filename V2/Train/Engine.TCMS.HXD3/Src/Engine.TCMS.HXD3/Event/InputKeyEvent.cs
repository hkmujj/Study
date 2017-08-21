using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.HXD3.Event
{
    public class InputKeyEvent : CompositePresentationEvent<IntputKeyEventArgs>
    {

    }

    public class IntputKeyEventArgs
    {
        public IntputKeyEventArgs(InputKeyBoardState state)
        {
            State = state;
        }
        /// <summary>
        /// 按下的按键
        /// </summary>
        public InputKeyBoardState State { get; private set; }
    }
}