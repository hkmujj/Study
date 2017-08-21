using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Event;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class Input0ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '0'; }
        }
    }

    [Export]
    public class Input1ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '1'; }
        }
    }

    [Export]
    public class Input2ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '2'; }
        }
    }

    [Export]
    public class Input3ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '3'; }
        }
    }

    [Export]
    public class Input4ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '4'; }
        }
    }

    [Export]
    public class Input5ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '5'; }
        }
    }

    [Export]
    public class Input6ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '6'; }
        }
    }

    [Export]
    public class Input7ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '7'; }
        }
    }

    [Export]
    public class Input8ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '8'; }
        }
    }

    [Export]
    public class Input9ActionResponser : InputCharActionResponser
    {
        protected override char Input
        {
            get { return '9'; }
        }
    }



    public abstract class InputCharActionResponser : BtnActionResponserBase
    {
        protected abstract char Input { get; }

        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            EventAggregator.GetEvent<InputCharEvent>().Publish(new InputCharEvent.Args(Input));
        }
    }
}