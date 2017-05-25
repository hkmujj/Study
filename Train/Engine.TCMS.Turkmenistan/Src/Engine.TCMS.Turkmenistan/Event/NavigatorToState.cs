using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.Turkmenistan.Event
{
    public class NavigatorToState : CompositePresentationEvent<NavigatorToState.Args>
    {

        public class Args
        {
            public Args(string @params)
            {
                Params = @params;
            }

            public string Params { get; private set; }
        }
    }
}
