using System.ComponentModel;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.BtnStragy
{
    public interface IBtnStateProvider : INotifyPropertyChanged
    {
        ///// <summary>
        ///// ÏìÓ¦°´¼üµ¯Æð
        ///// </summary>
        BtnItem Parent { set; get; }

        bool Visible { get; }

        bool IsSelected { set; get; }

        NavigationButtonState ButtonState { get; }

        void UpdateState();
    }
}