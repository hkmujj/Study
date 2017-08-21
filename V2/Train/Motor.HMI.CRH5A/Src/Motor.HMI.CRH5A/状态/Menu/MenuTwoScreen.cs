using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class MenuTwoScreen : MenuScreenBase
    {
        public MenuTwoScreen()
        {
            TextFileIndex = "MenuTwoScreen";
            NameArr = new[]
                       {
                           "400V交流电", "电池关断开关", "高压接触器\nSAZ3",
                           "辅助接触器", "轴温检测系统", "蛇行检测系统",
                           "客室空调", "司机室空调", "应急通风",
                           "列车端部沙箱", "中间轴沙箱"
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