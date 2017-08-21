using System.Globalization;
using ATP200C.MainView;
using ATP200C.PublicComponents;
using MMI.Facility.Interface.Data;

namespace ATP200C.PopupViews
{
    public class InputDriverIDViewItem : InputViewItemBase
    {

        public InputDriverIDViewItem()
        {
            m_ConsText.Text = "司机号";
            m_InputContent.Text=GlobalParam.Instance.TrainInfo.DriverId == null ? "".PadRight(7, '-') : GlobalParam.Instance.TrainInfo.DriverId.PadRight(10, '-');
            m_InputContentArray = m_InputContent.Text.ToCharArray();
        }

        public override void ActionCommand(ATPBaseClass baseClass, UserActionCommand cmd)
        {
            if (cmd == UserActionCommand.Ok)
            {
                var rlt = (new TrainInfoToFloatExpression()).Interprete(new TrainInfo() { DriverId = m_InputContent.Text.Replace('-', ' ').Trim() });

                baseClass.append_postCmd(CmdType.SetFloatValue, ATPMain.Instance.UIObj.OutFloatList[2], 0, (int)rlt[2]);
                GlobalParam.Instance.TrainInfo.DriverId = rlt[2].ToString(CultureInfo.InvariantCulture);
                if (GlobalParam.Instance.TrainLineViewItem == null)
                {
                    GlobalParam.Instance.TrainLineViewItem = new InputTrainLineViewItem();
                }
                PopupViewCanve.Instance.PopupViewItem = GlobalParam.Instance.TrainLineViewItem;
                return;
            }
            base.ActionCommand(baseClass, cmd);
        }
    }
}