using System;
using System.Diagnostics;

namespace Motor.ATP.Domain.Interface.Service
{
    public class PopViewParam
    {
        [DebuggerStepThrough]
        public PopViewParam(Type viewModelType, string title = null, string popViewName = null)
        {
            ViewModelType = viewModelType;
            Title = title;
            PopViewName = popViewName;
        }

        public string Title { private set; get; }

        public string PopViewName { private set; get; }

        public Type ViewModelType { private set; get; }
    }
}