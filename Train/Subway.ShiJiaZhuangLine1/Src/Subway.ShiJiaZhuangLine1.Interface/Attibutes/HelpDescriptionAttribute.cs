using System;
using System.ComponentModel;
using System.Runtime;

namespace Subway.ShiJiaZhuangLine1.Interface.Attibutes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class HelpDescriptionAttribute : DescriptionAttribute
    {

        // 摘要: 
        //     不带参数初始化 System.ComponentModel.DescriptionAttribute 类的新实例。
        public HelpDescriptionAttribute() { }
        //
        // 摘要: 
        //     初始化 System.ComponentModel.DescriptionAttribute 类的新实例并带有说明。
        //
        // 参数: 
        //   description:
        //     说明文本。
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public HelpDescriptionAttribute(string description)
            : base(description)
        {

        }
    }
}