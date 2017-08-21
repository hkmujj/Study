using System;

namespace CommonUtil.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        /// <param name="objs"></param>
        public static void OnAction(Delegate act, params object[] objs)
        {
            if (act != null)
            {
                act.DynamicInvoke(objs);
            }
        }
    }
}