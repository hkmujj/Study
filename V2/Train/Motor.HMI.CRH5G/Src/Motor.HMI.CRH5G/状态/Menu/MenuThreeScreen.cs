using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuThreeScreen : MenuScreenBase
    {
        public MenuThreeScreen()
        {
            TextFileIndex = "MenuThreeScreen";
            NameArr = new[]{ "备用制动", "直通制动", "警惕电磁阀",
                                      "EB环路", "UB环路", "乘客制动阀",
                                      "制动控制单元", "制动状态", "抱轴",
                                      "防滑保护", "停放制动" };
        }

        //2
        public override string GetInfo()
        {
            return "菜单3视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "1", "2", "3", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {
            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 3, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://其他
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}