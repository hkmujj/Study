using ATP200C.MainView;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Data;

namespace ATP200C.PopupViews
{
    public class InputTrainLineViewItem : InputViewItemBase
    {
        public InputTrainLineViewItem()
        {
            m_ConsText.Text = "车次号";
            m_InputContent.Text = GlobalParam.Instance.TrainInfo.TrainId == null ? "".PadRight(8, '-') : GlobalParam.Instance.TrainInfo.TrainId.PadRight(8, '-');
            m_InputContentArray = m_InputContent.Text.ToCharArray();
            CurrentInputIndex = 0;
        }

        public override void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd)
        {
            if (cmd == UserActionCommand.Ok)
            {
                var rlt = (new TrainInfoToFloatExpression()).Interprete(new TrainInfo() { TrainId = m_InputContent.Text.Replace('-', ' ').Trim() });

                baseClass.append_postCmd(CmdType.SetFloatValue, ATPMain.Instance.UIObj.OutFloatList[0], 0, rlt[0]);
                baseClass.append_postCmd(CmdType.SetFloatValue, ATPMain.Instance.UIObj.OutFloatList[1], 0, rlt[1]);
                if (rlt[0] != 0)
                {
                    GlobalParam.Instance.TrainInfo.TrainId = m_InputContent.Text.Replace('-', ' ').Trim();
                }
                PopupViewCanve.Instance.PopupViewItem.Reset();
                return;
            }

            base.ActionCommand(baseClass, cmd);
        }
    }
}