using System;
using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class PopViewParam
    {
        public const string TitleKey = "Motor.ATP.Infrasturcture.Interface.Service.Title";

        public const string PopViewTitleKey = "Motor.ATP.Infrasturcture.Interface.Service.PopViewTitleKey";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModelType"></param>
        /// <param name="title"></param>
        /// <param name="popViewName"></param>
        /// <param name="attachedTitle"></param>
        [DebuggerStepThrough]
        public PopViewParam(Type viewModelType, string title = null, string popViewName = null, string attachedTitle =null)
        {
            ViewModelType = viewModelType;
            PopViewTitle = attachedTitle;
            Title = title;
            PopViewName = popViewName;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { private set; get; }


        /// <summary>
        /// 标题
        /// </summary>
        public string PopViewTitle { get; private set; }

        /// <summary>
        /// 弹出框名
        /// </summary>
        public string PopViewName { private set; get; }

        /// <summary>
        /// view model 类型
        /// </summary>
        public Type ViewModelType { private set; get; }
    }
}