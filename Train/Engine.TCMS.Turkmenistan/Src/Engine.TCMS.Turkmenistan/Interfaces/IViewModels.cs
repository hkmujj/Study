using System.ComponentModel;

namespace Engine.TCMS.Turkmenistan.Interfaces
{
    public interface IViewModels : INotifyPropertyChanged
    {
        /// <summary>
        /// 课程结束清除的数据
        /// </summary>
        void Clear();
        /// <summary>
        /// 课程开始初始化的数据
        /// </summary>
        void Initlization();
    }
}