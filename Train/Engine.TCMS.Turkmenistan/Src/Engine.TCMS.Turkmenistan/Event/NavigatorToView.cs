using Microsoft.Practices.Prism.Events;

namespace Engine.TCMS.Turkmenistan.Event
{
    public class NavigatorToView : CompositePresentationEvent<NavigatorToView.Args>
    {
        public class Args
        {
            public Args(string viewName)
            {
                ViewName = viewName;
            }
            /// <summary>
            /// 视图名称
            /// </summary>
            public string ViewName { get; private set; }
        }
    }
}
