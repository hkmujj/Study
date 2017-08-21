using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MenuTwoScreen : MenuScreenBase
    {
        public MenuTwoScreen()
        {
            TextFileIndex = "MenuTwoScreen";
            NameArr = new[]
                       {
                           "中压供电", "电池状态", "KSAZ3",
                           "轴温系统", "客室空调", "司机室空调",
                           "应急通风", "列车砂箱", "常用制动隔离",
                           "EB电磁阀", "UB电磁阀"
                       };
        }

        //2
        public override string GetInfo()
        {
            return "菜单2视图";
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
                case 15: //1
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16: //2
                    append_postCmd(CmdType.ChangePage, 2, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 17: //3
                    append_postCmd(CmdType.ChangePage, 3, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18: //其他
                    append_postCmd(CmdType.ChangePage, 4, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}