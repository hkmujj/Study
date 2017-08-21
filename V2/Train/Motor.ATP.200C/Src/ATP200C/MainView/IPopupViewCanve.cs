using ATP200C.PopupViews;
using ATP200C.PublicComponents;

namespace ATP200C.MainView
{
    public interface IPopupViewCanve : IUserInputReciver
    {
        PopupViewItem PopupViewItem { set; get; }

    }
}