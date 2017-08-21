using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Event;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class InputOkActionResponser : InputControlWordActionResponser
    {
        protected override InputControlWordEvent.OkOrCancel Word
        {
            get { return InputControlWordEvent.OkOrCancel.Ok; }
        }
    }

    [Export]
    public class InputCancelPasswordActionResponser : InputControlWordActionResponser
    {
        protected override InputControlWordEvent.OkOrCancel Word
        {
            get { return InputControlWordEvent.OkOrCancel.Cancel; }
        }

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            base.ResponseClick();

            NavigateBack();
        }
    }

    [Export]
    public class InputCancelActionResponser : InputControlWordActionResponser
    {
        protected override InputControlWordEvent.OkOrCancel Word
        {
            get { return InputControlWordEvent.OkOrCancel.Cancel; }
        }
    }

    public abstract class InputControlWordActionResponser : BtnActionResponserBase
    {
        protected abstract InputControlWordEvent.OkOrCancel Word { get; }

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            EventAggregator.GetEvent<InputControlWordEvent>().Publish(new InputControlWordEvent.Args(Word));
        }
    }
}