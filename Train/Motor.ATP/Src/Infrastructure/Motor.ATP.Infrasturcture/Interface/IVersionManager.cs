using System;
using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    public interface IVersionManager : INotifyPropertyChanged
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        Version MainVersion { get; }

        /// <summary>
        /// 主屏版本
        /// </summary>
        Version DMIMainVersion { get; }

        /// <summary>
        /// 辅屏版本
        /// </summary>
        Version DMIAssistVersion { get; }

        string CFSK { get; }
    }
}