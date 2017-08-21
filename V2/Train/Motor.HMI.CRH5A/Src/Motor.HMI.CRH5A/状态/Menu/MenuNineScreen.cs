using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuNineScreen : MenuScreenBase
    {
        public MenuNineScreen()
        {
            TextFileIndex = "MenuNineScreen";
            NameArr = new[]{ "左侧灯", "左近光灯", "左前照灯",
                                      "左红灯", "中间前照灯", "右侧灯",
                                      "右近光灯", "右红灯", "右前照灯",
                                      "右厕所", "左侧所/餐车\n给水控制单元" };
        }
        //2
        public override string GetInfo()
        {
            return "菜单9视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "7", "8", "9", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 7, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 8, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17://3
                    append_postCmd(CmdType.ChangePage, 9, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18://其他
                    append_postCmd(CmdType.ChangePage, 10, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}