using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH5A.Staus.Menu
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MenuElevenScreen : MenuScreenBase
    {
        public MenuElevenScreen()
        {
            TextFileIndex = "MenuElevenScreen";

            NameArr = new[]{ "1位转向架蛇行系统", "1位轴箱轴温检测系统", "5位轴箱轴温检测系统",
                                        "2位轴箱轴温检测系统", "6位轴箱轴温检测系统",
                                        "2位转向架蛇行系统", "3位轴箱轴温检测系统", "7位轴箱轴温检测系统",
                                        "4位轴箱轴温检测系统", "8位轴箱轴温检测系统" };
        }

        //2
        public override string GetInfo()
        {
            return "菜单11视图";
        }

        protected override void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor)
        {
            visitor.ButtonContentCollection = new List<string>() { "10", "11", "", "其它", "", "", "", "", "", Button10 };
            visitor.SelectableIndexCollection = new ReadOnlyCollection<int>(Enumerable.Range(0, 10).ToList());
        }

        protected override void ResponseUser()
        {

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress) return;
            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 15://1
                    append_postCmd(CmdType.ChangePage, 10, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 16://2
                    append_postCmd(CmdType.ChangePage, 11, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;
                case 18:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    ButtonsScreen.BtnState.Press();
                    break;

            }
        }
    }
}

