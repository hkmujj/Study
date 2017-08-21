using System;
using System.Diagnostics;

namespace Subway.ShiJiaZhuangLine1.Interface.Attibutes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TitleNameAttribute : Attribute
    {
        [DebuggerStepThrough]
        public TitleNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { private set; get; }
      
    }
}