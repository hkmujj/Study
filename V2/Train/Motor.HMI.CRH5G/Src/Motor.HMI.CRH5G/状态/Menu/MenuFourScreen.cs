using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Staus.Menu;

namespace Motor.HMI.CRH5G.Staus
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuFourScreen : MenuScreenBase
    {
        public MenuFourScreen()
        {
            TextFileIndex = "MenuFourScreen";

            NameArr = new[]{ "旅客报警1", "旅客报警2", "监控室报警",
                                      "1轴制动状态", "1轴防滑", "2轴制动状态",
                                      "2轴防滑", "3轴制动状态", "3轴防滑",
                                      "4轴制动状态", "4轴防滑" };
        }
        //2
        public override string GetInfo()
        {
            return "菜单4视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "4", "5", "6", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 5, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 6, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://其他
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}