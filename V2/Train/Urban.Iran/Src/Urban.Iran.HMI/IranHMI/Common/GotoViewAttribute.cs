using System;

namespace Urban.Iran.HMI.Common
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class GotoViewAttribute : Attribute
    {
        public GotoViewAttribute(IranViewIndex viewIndex)
        {
            ViewIndex = viewIndex;
        }

        public IranViewIndex ViewIndex { private set; get; }
    }
}