using System.Drawing;
using ATP200C.PopupViews;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Attribute;

namespace ATP200C.MainView
{
    /// <summary>
    /// 弹出窗口的画布
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class PopupViewCanve : ATPBaseClass, IPopupViewCanve
    {
        private PopupViewItem m_PopupViewItem;

        public static IPopupViewCanve Instance { private set; get; }

        private PopupViewItem m_DefaultpPopupViewItem;

        public PopupViewItem PopupViewItem
        {
            set
            {
                m_PopupViewItem = value ?? m_DefaultpPopupViewItem;
            }
            get { return m_PopupViewItem; }
        }

        public override bool Initalize()
        {
            m_DefaultpPopupViewItem = new DesktopViewItem(this);

            Instance = this;

            PopupViewItem = null;

            return true;
        }

        public override void paint(Graphics g)
        {
            if (ATPButtonScreen.BtnState != null && ATPButtonScreen.BtnState.BtnId >= BtnTypeName.Num1 && ATPButtonScreen.BtnState.BtnId <= BtnTypeName.Switch)
            {
                ActionData((UserActionData) ATPButtonScreen.BtnState.BtnId);
            }

            if (PopupViewItem != null)
            {
                PopupViewItem.OnPaint(g);
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
         
            if (nParaA == 2 && PopupViewItem != null)
            {
                PopupViewItem.Reset();
            }
        }

        public void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd)
        {
            if (PopupViewItem != null)
            {
                PopupViewItem.ActionCommand(baseClass, cmd);
            }
        }

        public void ActionData(UserActionData data)
        {
            if (PopupViewItem != null)
            {
                PopupViewItem.ActionData(data);
            }
        }
    }
}