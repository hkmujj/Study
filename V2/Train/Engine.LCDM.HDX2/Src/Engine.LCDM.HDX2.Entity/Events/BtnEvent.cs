using CommonUtil.Controls;
using Engine.LCDM.HDX2.Entity.Model.Domain;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;

namespace Engine.LCDM.HDX2.Entity.Events
{
    public class BtnEvent : CompositePresentationEvent<BtnEventArg>
    {
         
    }

    public class BtnEventArg
    {
        public BtnEventArg(MouseState mouseState, HXD2HardwareBtn hardwareBtn)
        {
            MouseState = mouseState;
            HardwareBtn = hardwareBtn;
        }

        public MouseState MouseState { private set; get; }

        public HXD2HardwareBtn HardwareBtn { private set; get; }
    }
}