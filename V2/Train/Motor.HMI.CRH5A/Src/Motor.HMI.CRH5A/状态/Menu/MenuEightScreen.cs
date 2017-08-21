using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuEightScreen : MenuScreenBase
    {
        public MenuEightScreen()
        {
            TextFileIndex = "MenuEightScreen";
            NameArr = new[]{ "电气柜烟火\n传感器1", "电气柜烟火\n传感器2", "客室烟火传感器1",
                                      "客室烟火传感器2", "卫生间烟火\n传感器1", "卫生间烟火\n传感器2",
                                      "旅客信息系统", "阅读灯", "全灯",
                                      "半灯", "灯关闭" };
        }

        //2
        public override string GetInfo()
        {
            return "菜单8视图";
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