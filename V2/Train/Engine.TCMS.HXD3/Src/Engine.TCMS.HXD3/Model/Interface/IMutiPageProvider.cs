using System.ComponentModel;

namespace Engine.TCMS.HXD3.Model.Interface
{
    public interface IMutiPageProvider : INotifyPropertyChanged
    {
        /// <summary>
        /// 向后
        /// </summary>
        bool CanNavigateBack{ get; }

        /// <summary>
        /// 向前
        /// </summary>
        bool CanNavigateForward { get; }

        void NavigateBack();

        void NavigateForward();

        void NavagateDefault();
    }
}