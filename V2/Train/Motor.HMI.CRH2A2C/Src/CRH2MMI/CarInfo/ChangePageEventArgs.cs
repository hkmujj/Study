using System;

namespace CRH2MMI
{
    internal class ChangeCarInfoPageEventArgs : EventArgs
    {
        public ChangeCarInfoPageEventArgs(ChangePageType type)
        {
            Type = type;
        }

        public ChangePageType Type { private set; get; }
    }

    internal enum ChangePageType
    {
        /// <summary>
        /// 到第一页
        /// </summary>
        ToPage1,

        /// <summary>
        /// 到第二页
        /// </summary>
        ToPage2,
    }
}