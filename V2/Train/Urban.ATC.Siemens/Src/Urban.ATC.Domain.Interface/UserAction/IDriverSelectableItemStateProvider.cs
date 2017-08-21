using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.UserAction
{
    public interface IDriverSelectableItemStateProvider : INotifyPropertyChanged
    {
        bool Enabled { get; }

        bool Visible { get; }

        /// <summary>
        /// 外边是否可见
        /// </summary>
        bool OutlineVisible { get; }

        /// <summary>
        /// 改变后的内容
        /// </summary>
        string ChangedContent { get; }

        /// <summary>
        /// 司机选择类型
        /// </summary>
        DriverSelectedType SelectedType { get; }

        void Initalize(IDriverSelectableItem item);

        /// <summary>
        /// 手动更新状态
        /// </summary>
        void UpdateState();
    }
}