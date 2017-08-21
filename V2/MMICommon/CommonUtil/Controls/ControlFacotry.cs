using System;

namespace CommonUtil.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public static class ControlFacotry
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initAction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateControl<T>(Action<T> initAction = null) where T : CommonInnerControlBase, new()
        {
            var control = new T();

            if (initAction!= null)
            {
                initAction(control);
            }
            return control;
        }
    }
}