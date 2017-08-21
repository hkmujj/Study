using System;
using System.Linq;
using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Data;

namespace ATP200C.PopupViews
{
    public class DesktopViewItem : PopupViewItem
    {
        private readonly ATPBaseClass m_TargetObject;

        public DesktopViewItem(ATPBaseClass obj)
        {
            m_TargetObject = obj;
        }

        public override void ActionData(UserActionData data)
        {
            FunBtnMenu btnMenu;
            switch (data)
            {
                case UserActionData.Num1:
                    btnMenu = FunctionButtonView.Instance.AllFunBtnState[2];
                    ResponseButton(btnMenu, "调车");

                    break;
                case UserActionData.Num2:
                    btnMenu = FunctionButtonView.Instance.AllFunBtnState[2];
                    ResponseButton(btnMenu, "目视");

                    break;
                case UserActionData.Num3:
                    btnMenu = FunctionButtonView.Instance.AllFunBtnState[0];
                    ResponseButton(btnMenu, "启动");

                    break;
                case UserActionData.Num4:
                    btnMenu = FunctionButtonView.Instance.AllFunBtnState[0];
                    ResponseButton(btnMenu, "缓解");
                    break;
                case UserActionData.Num5:
                    break;
                case UserActionData.Num6:
                    break;
                case UserActionData.Num7:
                    break;
                case UserActionData.Num8:
                    break;
                case UserActionData.Num9:
                    break;
                case UserActionData.Num0:
                    break;
                case UserActionData.Switch:
                    btnMenu = FunctionButtonView.Instance.AllFunBtnState[0];
                    ResponseButton(btnMenu, "警惕");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("data");
            }
        }

        private void ResponseButton(FunBtnMenu btnMenu, string btnContent)
        {
            var btn = btnMenu.BtnInfoArr.First(f => f.FunButton.BtnContent == btnContent);
            btn.RefreshState();
            if (!btn.Locked)
            {
                btn.SendTheValue(m_TargetObject);
            }
        }
    }
}