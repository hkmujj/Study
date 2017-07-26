using System;
using System.Diagnostics;

namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerStepThrough]
    public class DriverSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <param name="selectedType"></param>
        public DriverSelectedEventArgs(IDriverSelectableItem selectedItem, DriverSelectedType selectedType)
        {
            SelectedType = selectedType;
            SelectedItem = selectedItem;
        }

        /// <summary>
        /// 选择元素
        /// </summary>
        public IDriverSelectableItem SelectedItem { private set; get; }

        /// <summary>
        /// 选择方式
        /// </summary>
        public DriverSelectedType SelectedType { private set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DriverSelectedType
    {
        /// <summary>
        /// 按下
        /// </summary>
        Press,

        /// <summary>
        /// 释放 
        /// </summary>
        Relase,
    }
}