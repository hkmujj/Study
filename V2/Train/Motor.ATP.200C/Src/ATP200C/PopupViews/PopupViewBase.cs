using ATP200C.Common;
using ATP200C.MainView;
using ATP200C.PublicComponents;
using CommonUtil.Controls;

namespace ATP200C.PopupViews
{
    public abstract class PopupViewItem : CommonInnerControlBase, IUserInputReciver
    {
        protected PopupViewItem()
        {
            m_OutLineRectangle = ATP200CCommon.RectD[0];
        }

        public virtual void Reset()
        {
            
        }

        public virtual void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd)
        {
            
        }

        public virtual void ActionData(UserActionData data)
        {
            
        }
    }
}